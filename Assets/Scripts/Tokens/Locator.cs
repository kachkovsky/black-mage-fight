using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Locator : Figure
{
    public override void Collide(Figure f) {
        //if (f is Hero) {
        //    Blink();
        //}
    }

    public void LocateHere(Figure figure) {
        Position.MoveHere(figure);
        Blink();
    }
}
