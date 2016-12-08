using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

public class Figure : Token
{
    [SerializeField]
    private Cell position;

    public Cell Position {
        get { return position; }
    }

    public virtual void BeforeLeaveCell() {
    }

    public virtual void AfterEnterCell() {
    }

    public void SetPosition(Cell cell) {
        if (position != null) {
            BeforeLeaveCell();
            position.figures.Remove(this);
        }
        position = cell;
        if (position != null) {
            position.figures.Add(this);
            AfterEnterCell();
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
        EditorUtility.SetDirty(this);
    }

    public void Blink() {
        Debug.LogFormat("Blink {0}", transform.Path());
        Board.instance.RandomEmptyCell().MoveHere(this);
    }

    public void Relocate() {
        var locators = FindObjectsOfType<Locator>().ToList();
        if (locators.Count > 0) {
            locators.Rnd().LocateHere(this);
            return;
        }
        var trajectoryPosition = Position.figures.FirstOrDefault(f => f is TrajectoryPosition) as TrajectoryPosition;
        if (trajectoryPosition != null) {
            trajectoryPosition.trajectory.positions.cyclicNext(trajectoryPosition).position.MoveHere(this);
            return;
        }
        Blink();
    }

    public virtual void Collide(Figure other) {
    }

    public virtual bool Occupies() {
        return true;
    }

    protected virtual void OnDestroy() {
        //Debug.LogFormat("OnDestroy {0}", transform.Path());
        if (position != null) {
            //Debug.LogFormat("Remove {0} from {1}", transform.Path(), position.transform.Path());
            position.figures.Remove(this);
        }
    }
}
