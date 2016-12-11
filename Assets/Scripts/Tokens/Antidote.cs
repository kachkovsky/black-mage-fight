using UnityEngine;
using System.Collections;
using System.Linq;

public class Antidote : Figure
{
    public override void Collide(Figure f) {
        if (f is Hero) {
            Poison.instance.Suppress();
            Relocate();
        }
    }
}
