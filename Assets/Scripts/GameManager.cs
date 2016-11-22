using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    const string GAMESTATE_FILE = "gamestate.dat";
    const int MAX_HISTORY = 1000;

    public static GameManager instance;

    public static GameState gameState;

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
        NewGame();
    }

    public void NewGame() {
        Hero.instance.health = 100;
        BlackMage.instance.health = 100;
        Hero.instance.MoveTo(Board.instance.cells.Rand());
        BlackMage.instance.MoveTo(Board.instance.cells.Rand());
        Hero.instance.CheckAttack();
    }

    void OnDestroy() {
        FileManager.SaveToFile(gameState, GAMESTATE_FILE);
        Debug.LogFormat("Destroy");
    }
}
