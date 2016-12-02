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

    void LocateHere(Figure figure) {
        Position.MoveHere(figure);
        Blink();
    }

    public static void Locate(Figure figure) {
        var locators = FindObjectsOfType<Locator>().ToList();
        if (locators.Count == 0) {
            figure.Blink();
        } else {
            locators.Rnd().LocateHere(figure);
        }
    }
}
