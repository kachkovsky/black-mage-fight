using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Hero : MonoBehaviour
{
    public static Hero instance;

    void Awake() {
        instance = this;
    }

    void OnEnable() {
        instance = this;
    }

    public Cell Position() {
        return transform.parent.GetComponent<Cell>();
    }

    public void MoveTo(Cell cell) {
        if (cell == null) {
            return;
        }
        cell.MoveHeroHere();
    }
}
