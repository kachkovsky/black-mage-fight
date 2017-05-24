using UnityEngine;
using System.Collections;
using RSG;
using System;

public class Controls : MonoBehaviour {
    public static Controls instance;
    const int N = 9;

    public Cell hovered;
    public Cell selected;

    public Unit activeUnit;

    public Button up;
    public Button down;
    public Button left;
    public Button right;

    void Awake() {
        instance = this;
    }

    private void RefreshHovered() {
        hovered = null;
        var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(new Ray(cursor, Vector3.forward), out hit)) {
            var cell = hit.collider.gameObject.GetComponent<Cell>();
            if (cell != null) {
                hovered = cell;
            }
        }
    }

    IPromise commands = Promise.Resolved();

    void Command(Func<IPromise> command) {
        commands = commands.Then(command);
        commands.Done();
    }

    public void Move(IntVector2 direction) {
        if (Hero.instance.Dead || BlackMage.instance.Dead) {
            return;
        }
        if (Intermission.active) {
            return;
        }
        if (activeUnit == null) {
            return;
        }
        Command(() => activeUnit.MoveTo(direction).Untyped());
    }

    public void Restart() {
        GameManager.instance.Restart();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift)) {
            GameManager.instance.DropSaveFile();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.instance.UpdateState();
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.F5)) {
            Cheats.on ^= true;
        }
        if (Cheats.on) {
            if (Input.GetKeyDown(KeyCode.W)) {
                GameManager.instance.Win();
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                GameManager.instance.Lose();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UI.instance.Escape();
        }
    }
}
