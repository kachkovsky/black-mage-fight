using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {
    const string GAMESTATE_FILE = "gamestate.dat";
    const int MAX_HISTORY = 1000;

    public static GameManager instance;

    public static GameState gameState;

    public GameObject heartPrefab;
    public GameObject portalPrefab;
    public GameObject bombPrefab;
    public GameObject heartStopperPrefab;
    public GameObject bombSetterPrefab;
    public GameObject arrowSetterPrefab;
    public GameObject locatorPrefab;
    public GameObject skullSetterPrefab;
    public GameObject candlePrefab;
    public GameObject gambochkaPrefab;
    public GameObject chaoticPortal;
    public Damage damage;

    GameObject lastLevel;
    GameObject currentLevel;

    public event Action<Unit> onHeroMove = (h) => { };

    public List<Material> portalMaterials;

    public int wins;
    public int losses;

    public void Win() {
        ++wins;
    }

    public void Lose() {
        ++losses;
    }

    void Awake() {
        instance = this;
        gameState = FileManager.LoadFromFile<GameState>(GAMESTATE_FILE);
        if (gameState != null) {
            if (gameState.history == null) {
                gameState.history = new List<int[,]>();
            }
        } else {
            gameState = new GameState();
        }
    }

    void Start() {
        NewGame(Levels.instance.level1);
    }

    public void CreateHeartStopper() {
        Instantiate(heartStopperPrefab);
    }

    public void CreateBombSetter() {
        var bombSetter = Instantiate(bombSetterPrefab).GetComponent<BombSetter>();
        bombSetter.periodic.period = 7;
        bombSetter.periodic.runPhases.Add(3);
        bombSetter.timer = 13;
    }

    public void CreateArrowSetter() {
        var arrowSetter = Instantiate(arrowSetterPrefab).GetComponent<ArrowSetter>();
        arrowSetter.periodic.period = 3;
        arrowSetter = Instantiate(arrowSetterPrefab).GetComponent<ArrowSetter>();
        arrowSetter.periodic.period = 3;
        arrowSetter.periodic.phase = 1;
    }

    public void CreateSkullSetter() {
        var skullSetter = Instantiate(skullSetterPrefab).GetComponent<SkullSetter>();
        skullSetter.periodic.period = 1;
    }

    //public void Level1() {
    //    NewGame(new BasicGameStartConfig(40, 100, 0, 3, 3, CreateHeartStopper));
    //}

    //public void Level2() {
    //    NewGame(new BasicGameStartConfig(40, 100, 1, 3, 3, CreateHeartStopper));
    //}

    //public void Level3() {
    //    NewGame(new BasicGameStartConfig(50, 100, 3, 3, 3, CreateHeartStopper));
    //}

    //public void Level4() {
    //    NewGame(new BasicGameStartConfig(50, 10, 0, 1, 1, CreateBombSetter));
    //}

    //public void Level5() {
    //    NewGame(new BasicGameStartConfig(50, 25, 0, 1, 1, CreateArrowSetter));
    //}

    //public void Level6() {
    //    NewGame(new BasicGameStartConfig(50, 20, 0, 0, 1, CreateSkullSetter));
    //}

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

    public void NewBasicGame(BasicGameStartConfig config) {    
        //UnityEngine.Random.seed = 42;
        config.teleports = Mathf.Clamp(config.teleports, 0, 3);
        config.heartCount = Mathf.Clamp(config.heartCount, 0, 7);
        Clear();
        BlackMage.instance.hitDamage = 1;
        BlackMage.instance.maxHealth = config.blackMageHealth;
        Hero.instance.maxHealth = config.heroHealth;

        for (int i = 0; i < config.heartCount; i++) {
            var heartObject = Instantiate(heartPrefab);
            var heart = heartObject.GetComponent<Heart>();
            heart.heal = config.heartHeal;
        }
        for (int i = 0; i < config.teleports; i++) {
            for (int j = 0; j < 2; j++) {
                var portalObject = Instantiate(portalPrefab);
                var portal = portalObject.GetComponent<Portal>();
                portal.id = i;
                var spriteRenderer = portal.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.sharedMaterial = portalMaterials[i];
            }
        }
        config.extraCreations();
        //Instantiate(locatorPrefab);
        for (int i = 0; i < 20; i++) {
            //Instantiate(candlePrefab);
        }
        //Instantiate(gambochkaPrefab);
        for (int i = 0; i < 5; i++) {
            //Instantiate(chaoticPortal);
        }

        FindObjectsOfType<Unit>().ForEach(u => u.Reborn());

        //Hero.instance.Hit(50);
        //BlackMage.instance.ResetDamageTokens();

        FindObjectsOfType<Figure>().ForEach(f => f.SetPosition(null));
        FindObjectsOfType<Figure>().ForEach(f => f.Blink());
        Controls.instance.activeUnit = Hero.instance;
    }

    public void ResetPositions() {
        FindObjectsOfType<Figure>().ForEach(f => f.SetPosition(null));
        FindObjectsOfType<Figure>().ForEach(f => f.Blink());
    }

    public void NewGame(GameObject level) {
        lastLevel = level;
        Clear();
        currentLevel = Instantiate(level);
        currentLevel.SetActive(true);
        Controls.instance.activeUnit = Hero.instance;
        UI.instance.UpdateHUD();

        FindObjectsOfType<Figure>().ForEach(f => {
            if (f.Position == null) {
                f.Blink();
            }
        });

        Board.instance.Restore();

        FindObjectsOfType<OnLevelStart>().ForEach(t => t.Run());
    }

    public void HeroMoved(Unit hero) {
        onHeroMove(hero);
    }

    public void Restart() {
        NewGame(lastLevel);
    }

    void OnDestroy() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
    }
}
