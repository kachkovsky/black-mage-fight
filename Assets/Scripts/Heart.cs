using UnityEngine;
using System.Collections;

public class Heart : Figure
{
    public int heal = 3;

    public void Pick(Hero hero) {
        hero.Heal(heal);
        Blink();
    }
}
