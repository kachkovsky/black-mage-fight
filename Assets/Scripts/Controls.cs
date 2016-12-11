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

    public void Up() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }

        if (activeUnit == null) {
            return;
        }
        activeUnit.MoveTo(activeUnit.Position.Up());
    }

    public void Down() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }

        if (activeUnit == null) {
            return;
        }
        activeUnit.MoveTo(activeUnit.Position.Down());
    }

    public void Left() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }

        if (activeUnit == null) {
            return;
        }
        activeUnit.MoveTo(activeUnit.Position.Left());
    }

    public void Right() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }

        if (activeUnit == null) {
            return;
        }
        activeUnit.MoveTo(activeUnit.Position.Right());
    }

    public void Restart() {
        GameManager.instance.Restart();
    }

    void Update() {

        if (Input.GetButtonDown("Up")) {
            Up();
        }
        if (Input.GetButtonDown("Down")) {
            Down();
        }
        if (Input.GetButtonDown("Left")) {
            Left();
        }
        if (Input.GetButtonDown("Right")) {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }
}
