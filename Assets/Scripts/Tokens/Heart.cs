using UnityEngine;
using System.Collections;
using System.Linq;

public class Heart : Figure
{
    public int heal = 3;

    public override void Pick(Hero hero) {
        hero.Heal(heal);
        Locator.Locate(this);
    }
}
