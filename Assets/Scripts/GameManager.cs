using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    const string GAMESTATE_FILE = "gamestate.dat";
    const int MAX_HISTORY = 1000;

    public static GameManager instance;

    public static GameState gameState;

    public GameObject heartPrefab;
    public GameObject portalPrefab;
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
        Level1();
    }

    public void Level1() {
        NewGame(40, 100, 0, 3, 3);
    }

    public void Level2() {
        NewGame(40, 100, 1, 3, 3);
    }

    public void Level3() {
        NewGame(50, 100, 3, 3, 3);
    }

    public void NewGame(int blackMageHealth, int heroHealth, int teleports, int heartCount, int heartHeal) {
        teleports = Mathf.Clamp(teleports, 0, 3);
        heartCount = Mathf.Clamp(heartCount, 0, 3);
        BlackMage.instance.hitDamage = 1;
        BlackMage.instance.maxHealth = blackMageHealth;
        Hero.instance.maxHealth = heroHealth;
        FindObjectsOfType<Portal>().ForEach(x => Destroy(x.gameObject));
        FindObjectsOfType<Heart>().ForEach(x => Destroy(x.gameObject));
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
        Restart();
    }

    public void Restart() {
        FindObjectsOfType<Unit>().ForEach(u => u.Reborn());
        FindObjectsOfType<Figure>().ForEach(f => f.Blink());
    }

    void OnDestroy() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
        Debug.LogFormat("Destroy");
    }
}
