using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using RSG;

public class GameManager : MonoBehaviour {
    const string GAMESTATE_FILE = "gamestate.dat";
    const int MAX_HISTORY = 1000;

    public static GameManager instance;

    public GameState gameState;

    GameObject lastLevel;
    GameObject currentLevel;

    public AudioSource loseSound;
    public AudioSource winSound;

    public Intermission ending;
    public Intermission badEnding;

    public event Action<Unit, Cell, Cell, IntVector2> onHeroMove = (h, a, b, d) => { };

    public List<Material> portalMaterials;

    public int wins;
    public int losses;

    public void Win() {
        Destroy(BlackMage.instance.gameObject);
        ++wins;
        this.TryPlay(winSound);
        UI.instance.Win();
        gameState.CurrentRun.levelsCompleted++;
        if (gameState.CurrentRun.continuousRun) {
            gameState.CurrentRun.triesLeft++;
        } 
        Save();
    }

    public void Lose() {
        Destroy(Hero.instance.gameObject);
        ++losses;
        this.TryPlay(loseSound);
        UI.instance.Lose();
        Save();
    }

    public bool GameOver() {
        return !LevelIsRunning();
    }

    [ContextMenu("Drop Save")]
    public void DropSaveFile() {
        gameState = new GameState();
        Save();
        UpdateState();
    }

    void Awake() {
        instance = this;
        ending.Hide();
    }

    public void Load() {
        gameState = FileManager.LoadFromFile<GameState>(GAMESTATE_FILE);
        if (gameState != null) {

        } else {
            gameState = new GameState();
        }
    }

    public void ConfirmWin() {
        UpdateState();
    }

    public void ConfirmLose() {
        if (gameState.CurrentRun.continuousRun) {
            if (gameState.CurrentRun.triesLeft > 0) {
                Restart();
            } else {
                FailGame();
            }
        } else {
            Restart();
        }
    }

    public void UpdateState() {
        if (GameManager.instance.gameState.CurrentProfile == null) {
            UI.instance.ChooseProfile();
            return;
        }
        if (gameState.CurrentProfile.name == "") {
            UI.instance.AskName();
            return;
        }
        if (gameState.CurrentProfile.currentRuns.Count == 0) {
            UI.instance.AskDifficulty();
            return;
        }
        ContinueGame(gameState.CurrentProfile.currentRuns.First());
    }

    void ContinueGame(GameRun run) {
        if (run.levelsCompleted == GameLevels.instance.commonLevels.Count) {
            FinishGame(run);
            return;
        }
        RunLevel(GameLevels.instance.commonLevels[run.levelsCompleted].gameObject);
    }

    void FinishGame(GameRun run) {
        ending.Show().Then(() => {
            gameState.CurrentProfile.completedRuns.Add(run);
            gameState.CurrentProfile.currentRuns.Remove(run);
            UpdateState();
        });
    }

    void FailGame() {
        badEnding.Show().Then(() => {
            gameState.CurrentProfile.currentRuns.Remove(gameState.CurrentRun);
            UpdateState();
        });
    }

    void Start() {
        Load();
        UpdateState();
    }
    
    public void Clear() {
        FindObjectsOfType<Token>().ForEach(x => {
            x.gameObject.SetActive(false);
            Destroy(x.gameObject);
        });
        if (currentLevel != null) {
            currentLevel.SetActive(false);
            Destroy(currentLevel);
        }
    }

    public void ResetPositions() {
        FindObjectsOfType<Figure>().ForEach(f => f.SetPosition(null));
        FindObjectsOfType<Figure>().ForEach(f => f.Blink());
    }

    public void RunLevel(GameObject level, bool restarted = false) {
        if (gameState.CurrentRun.continuousRun) {
            if (gameState.CurrentRun.triesLeft <= 0) {
                FailGame();
                return;
            }
            gameState.CurrentRun.triesLeft--;
            if (gameState.CurrentRun.levelsCompleted == 0) {
                gameState.CurrentRun.triesLeft = 4;
            }
        }
        lastLevel = level;
        Clear();
        currentLevel = Instantiate(level);
        currentLevel.SetActive(true);
        Controls.instance.activeUnit = Hero.instance;
        UI.instance.CloseAll();
        TimeManager.Wait(0).Then(() => {
            UI.instance.UpdateHUD();
        });

        Board.instance.Restore();

        FindObjectsOfType<Figure>().ForEach(f => {
            if (f.Position == null) {
                f.Blink();
            }
        });

        FindObjectsOfType<OnLevelStart>().ForEach(t => t.Run());
        BlackMage.instance.maxHealth += 5 * (gameState.CurrentRun.difficulty-4);
        BlackMage.instance.health += 5 * (gameState.CurrentRun.difficulty-4);

        var intro = currentLevel.GetComponentsInChildren<Intermission>().FirstOrDefault(i => !i.ending);
        if (intro != null)
        {
            if (restarted) {
                intro.Hide();
            } else {
                intro.Show();
            }
        } 
    }

    public void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        onHeroMove(hero, from, to, direction);
    }

    public void Restart() {
        RunLevel(lastLevel, restarted: true);
    }

    public void Save() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
    }

    void OnDestroy() {
        Save();
    }

    bool LevelIsRunning() {
        return Hero.instance != null && BlackMage.instance != null;
    }

    public bool Won() {
        return GameOver() && Hero.instance != null;
    }

    public bool Lost() {
        return GameOver() && BlackMage.instance != null;
    }

    void Update() {
        if (LevelIsRunning()) {
            if (BlackMage.instance.Dead) {
                Win();
            } else if (Hero.instance.Dead) {
                Debug.LogFormat("Destroying hero: {0}", Hero.instance.transform.Path());
                Lose();
            }
        }
    }
}
