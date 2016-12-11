using UnityEngine;
using System.Collections;

public class Heal : UnitEffect
{
    public int amount = 1;

    public override void Run(Unit hero) {
        hero.Heal(amount);
    }
}
