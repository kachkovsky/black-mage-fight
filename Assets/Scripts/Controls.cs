using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    public static Controls instance;
    const int N = 9;

    public Cell hovered;
    public Cell selected;

    public Unit activeUnit;

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

    void Moves() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }

        if (activeUnit == null) {
            return;
        }

        var heroPosition = activeUnit.Position;
        if (Input.GetButtonDown("Up")) {
            activeUnit.MoveTo(heroPosition.Up());
        }
        if (Input.GetButtonDown("Down")) {
            activeUnit.MoveTo(heroPosition.Down());
        }
        if (Input.GetButtonDown("Left")) {
            activeUnit.MoveTo(heroPosition.Left());
        }
        if (Input.GetButtonDown("Right")) {
            activeUnit.MoveTo(heroPosition.Right());
        }
    }

    void Update() {
        Moves();
        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.instance.Restart();
        }
    }
}
