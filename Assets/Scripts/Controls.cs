using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    public static Controls instance;
    const int N = 9;

    public Cell hovered;
    public Cell selected;

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

        var heroPosition = Hero.instance.position;
        if (Input.GetButtonDown("Up")) {
            Hero.instance.MoveTo(heroPosition.Up());
        }
        if (Input.GetButtonDown("Down")) {
            Hero.instance.MoveTo(heroPosition.Down());
        }
        if (Input.GetButtonDown("Left")) {
            Hero.instance.MoveTo(heroPosition.Left());
        }
        if (Input.GetButtonDown("Right")) {
            Hero.instance.MoveTo(heroPosition.Right());
        }
    }

    void Update() {
        Moves();
        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.instance.NewGame();
        }
    }
}
