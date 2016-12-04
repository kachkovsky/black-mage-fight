using UnityEngine;
using System.Collections;
using System.Linq;

public class Heart : Figure
{
    public int heal = 3;

    public override void Collide(Figure f) {
        var hero = f as PlayerUnit;
        if (hero != null) {
            hero.Heal(heal);
            Locator.Locate(this);
        }
    }
}
