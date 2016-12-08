using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TrajectoryPosition : Figure
{
    public Trajectory trajectory;

    public override bool Occupies() {
        return false;
    }

    public override void BeforeLeaveCell() {
        Position.RestoreColor();
    }

    public override void AfterEnterCell() {
        Position.ChangeColor(trajectory.color);
    }
}

