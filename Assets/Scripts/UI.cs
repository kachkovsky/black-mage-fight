using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static UI instance;
    public GameObject newGame;
    public GameObject customLevel;

    void Awake() {
        instance = this;
    }

    void Start() {
        CloseMenu();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (newGame.activeSelf) {
                CloseMenu();
            } else {
                NewGame();
            }
        }
    }

    public void CloseMenu() {
        newGame.SetActive(false);
        customLevel.SetActive(false);
    }

    public void CustomLevel() {
        newGame.SetActive(false);
        customLevel.SetActive(true);
    }

    public void NewGame() {
        newGame.SetActive(true);
        customLevel.SetActive(false);
    }
}
