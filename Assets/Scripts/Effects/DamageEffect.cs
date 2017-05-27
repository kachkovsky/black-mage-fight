using UnityEngine;
using System.Collections;

public class DamageEffect : Effect
{
    public Unit target;

    public int damage = 1;

    public override void Run() {
        if (target == null) {
            target = Hero.instance;
        }
        target.Hit(damage);
    }
}
