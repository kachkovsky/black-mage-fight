using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Locator : Figure
{
    public override void Collide(Figure f) {
        if (f is Hero) {
            Blink();
        }
    }

    public void LocateHere(Figure figure) {
        if (!gameObject.activeInHierarchy) {
            figure.Blink();
            return;
        }
        Position.MoveHere(figure);
        Blink();
    }
}
