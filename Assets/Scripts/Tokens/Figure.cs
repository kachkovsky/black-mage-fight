using UnityEngine;
using System.Collections;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;
using System;
using RSG;

public class Figure : Token
{
    [SerializeField]
    private Cell position;

    public FigureEvent onCollide;

    public AudioSource swapSound;

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

    bool Swap(IntVector2 direction) {
        var cell = Position.ToDirection(direction);
        while (cell != null && !cell.figures.Any(f => f is Swapper)) {
            cell = cell.ToDirection(direction);
        }
        if (cell != null) {
            var s = cell.figures.First(f => f is Swapper);
            var x = Position;
            cell.MoveHere(this);
            x.MoveHere(s);
            if (swapSound != null) {
                swapSound.Play();
            }
            return true;
        }
        return false;
    }

    public virtual void OnSee(Figure other) {
    }

    public void See(IntVector2 direction) {
        var cell = Position.ToDirection(direction);
        while (cell != null && !cell.figures.Any(f => f is EvilEye)) {
            cell = cell.ToDirection(direction);
        }
        if (cell != null) {
            //cell.figures.First().GetComponent<OnSee>().Run();
            cell.figures.First().OnSee(this);
        }
    }

    public virtual IPromise<bool> MoveTo(IntVector2 direction) {
        See(direction);
        if (Swap(direction)) {
            return Promise<bool>.Resolved(true);
        }
        var cell = Position.ToDirection(direction);
        while (cell != null && cell.figures.Any(f => f is Ice)) {
            cell = cell.ToDirection(direction);
        }
        if (cell == null) {
            return Promise.v(false);
        }
        cell.MoveHere(this);
        return Promise.v(true);
    }

    [ContextMenu("Place")]
    public void Place() {
        position.MoveHere(this);
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void Blink(Func<Figure, bool> occupies = null) {
        //Debug.LogFormat("Blink {0}", transform.Path());
        Board.instance.RandomEmptyCell(occupies).MoveHere(this);
    }

    public void Relocate() {
        var blink = GetComponent<Blink>();
        if (blink != null) {
            blink.Run();
            return;
        }
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
        onCollide.Invoke(other);
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
