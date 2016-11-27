using UnityEngine;
using System.Collections;

public class DamageHero : Effect
{
    public int damage = 1;

    public override void Run() {
        Debug.LogFormat("Damage hero");
        Hero.instance.Hit(damage);
    }
}
