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
        for (int i = 0; i < 30; i++) {
            arrowSetter.SetArrow();
        }
    }

    public void Level1() {
        NewGame(new GameStartConfig(40, 100, 0, 3, 3, CreateHeartStopper));
    }

    public void Level2() {
        NewGame(new GameStartConfig(40, 100, 1, 3, 3, CreateHeartStopper));
    }

    public void Level3() {
        NewGame(new GameStartConfig(50, 100, 3, 3, 3, CreateHeartStopper));
    }

    public void Level4() {
        NewGame(new GameStartConfig(50, 10, 0, 1, 1, CreateBombSetter));
    }

    public void Level5() {
        NewGame(new GameStartConfig(50, 100, 0, 1, 1, CreateArrowSetter));
    }

    GameStartConfig lastConfig;

    public void NewGame(GameStartConfig config) {
        this.lastConfig = config;
        config.teleports = Mathf.Clamp(config.teleports, 0, 3);
        config.heartCount = Mathf.Clamp(config.heartCount, 0, 3);
        FindObjectsOfType<Token>().ForEach(x => {
            if (x != Hero.instance && x != BlackMage.instance) {
                Destroy(x.gameObject);
            }
        });
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
        FindObjectsOfType<Unit>().ForEach(u => u.Reborn());
        FindObjectsOfType<Figure>().ForEach(f => f.Blink()); 
    }

    public void HeroMoved(Hero hero) {
        onHeroMove(hero);
    }

    public void Restart() {
        NewGame(lastConfig);
    }

    void OnDestroy() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
        Debug.LogFormat("Destroy");
    }
}
