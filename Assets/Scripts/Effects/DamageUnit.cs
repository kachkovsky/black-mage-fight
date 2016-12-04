using UnityEngine;
using System.Collections;

public class DamageUnit : UnitEffect
{
    public int damage = 1;

    public override void Run(Unit hero) {
        hero.Hit(damage);
    }
}
