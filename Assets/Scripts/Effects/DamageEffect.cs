using UnityEngine;
using System.Collections;

public class DamageEffect : Effect
{
    public Unit target;

    public int damage = 1;

    public override void Run() {
        target.Hit(damage);
    }
}
