using UnityEngine;
using System.Collections;

public class Blink : Effect
{
    public Figure target;
    public Locator locator;

    public override void Run() {
        if (locator != null) {
            locator.LocateHere(target);
        } else {
            target.Blink();
        }
    }
}
