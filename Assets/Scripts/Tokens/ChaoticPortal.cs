using UnityEngine;
using System.Collections;
using System.Linq;

public class ChaoticPortal : Figure
{
    public override void Collide(Figure f) {
        var hero = f as Hero;
        if (hero != null) {
            hero.Blink();
            Blink();
        }
    }
}
