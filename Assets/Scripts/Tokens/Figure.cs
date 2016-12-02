using UnityEngine;
using System.Collections;

public class Figure : Token
{
    [SerializeField]
    private Cell position;

    public Cell Position {
        get { return position; }
    }

    public void SetPosition(Cell cell) {
        if (position != null) {
            position.figures.Remove(this);
        }
        position = cell;
        if (position != null) {
            position.figures.Add(this);
        }
    }

    public virtual void MoveTo(Cell cell) {
        if (cell == null) {
            return;
        }
        cell.MoveHere(this);
    }

    [ContextMenu("Place")]
    public void Place() {
        position.MoveHere(this);
    }

    public void Blink() {
        Board.instance.RandomEmptyCell().MoveHere(this);
    }

    public virtual void Collide(Figure other) {
    }

    public virtual bool Occupies() {
        return true;
    }

    void OnDestroy() {
        Debug.LogFormat("OnDestroy {0}", transform.Path());
        if (position != null) {
            Debug.LogFormat("position.figures.Remove {0}", transform.Path());
            position.figures.Remove(this);
        }
    }
}
