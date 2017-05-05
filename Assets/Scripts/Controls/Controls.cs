using UnityEngine;
using System.Collections;

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

    public void Move(IntVector2 direction) {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }
        if (activeUnit == null) {
            return;
        }
        activeUnit.MoveTo(direction);
    }

    public void Restart() {
        GameManager.instance.Restart();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space)) {
            Restart();
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Cursor.visible = true;
        }
    }
}
