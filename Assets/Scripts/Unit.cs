using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Cell position;

    public void MoveTo(Cell cell) {
        if (cell == null) {
            return;
        }
        cell.MoveHere(this);
    }
}
