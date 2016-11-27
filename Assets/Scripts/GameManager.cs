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

    public event Action<Hero> onHeroMove = (h) => { };

    public List<Material> portalMaterials;

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
        Level5();
    }

    public void CreateHeartStopper() {
        Instantiate(heartStopperPrefab);
    }

    public void CreateBombSetter() {
        var bombSetter = Instantiate(bombSetterPrefab).GetComponent<BombSetter>();
        bombSetter.periodic.period = 3;
        bombSetter.timer = 13;
    }

    public void CreateArrowSetter() {
        var arrowSetter = Instantiate(arrowSetterPrefab).GetComponent<ArrowSetter>();
        arrowSetter.periodic.period = 2;
    }

    public void Level1() {
        NewGame(40, 100, 0, 3, 3, CreateHeartStopper);
    }

    public void Level2() {
        NewGame(40, 100, 1, 3, 3, CreateHeartStopper);
    }

    public void Level3() {
        NewGame(50, 100, 3, 3, 3, CreateHeartStopper);
    }

    public void Level4() {
        NewGame(50, 10, 0, 1, 1, CreateBombSetter);
    }

    public void Level5() {
        NewGame(50, 100, 0, 1, 1, CreateArrowSetter);
    }

    public void NewGame(int blackMageHealth, int heroHealth, int teleports, int heartCount, int heartHeal, Action create) {
        teleports = Mathf.Clamp(teleports, 0, 3);
        heartCount = Mathf.Clamp(heartCount, 0, 3);
        FindObjectsOfType<Token>().ForEach(x => {
            if (x != Hero.instance && x != BlackMage.instance) {
                Destroy(x.gameObject);
            }
        });
        BlackMage.instance.hitDamage = 1;
        BlackMage.instance.maxHealth = blackMageHealth;
        Hero.instance.maxHealth = heroHealth;

        for (int i = 0; i < heartCount; i++) {
            var heartObject = Instantiate(heartPrefab);
            var heart = heartObject.GetComponent<Heart>();
            heart.heal = heartHeal;
        }
        for (int i = 0; i < teleports; i++) {
            for (int j = 0; j < 2; j++) {
                var portalObject = Instantiate(portalPrefab);
                var portal = portalObject.GetComponent<Portal>();
                portal.id = i;
                var spriteRenderer = portal.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.sharedMaterial = portalMaterials[i];
            }
        }
        create();

        Restart();
    }

    public void HeroMoved(Hero hero) {
        onHeroMove(hero);
    }

    public void Restart() {
        FindObjectsOfType<Unit>().ForEach(u => u.Reborn());
        FindObjectsOfType<Figure>().ForEach(f => f.Blink()); 
        FindObjectsOfType<Bomb>().ForEach(x => {
            Destroy(x.gameObject);
        });
        FindObjectsOfType<Arrow>().ForEach(x => {
            Destroy(x.gameObject);
        });
        FindObjectsOfType<BombSetter>().ForEach(x => {
            x.periodic.phase = 0;
        });
    }

    void OnDestroy() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
        Debug.LogFormat("Destroy");
    }
}
