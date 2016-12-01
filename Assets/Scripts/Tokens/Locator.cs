using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Locator : Figure
{
    public override void Pick(Hero hero) {
        Blink();
    }

    void LocateHere(Figure figure) {
        position.MoveHere(figure);
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
