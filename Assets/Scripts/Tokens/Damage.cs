using UnityEngine;
using System.Collections;

public class Damage : Figure
{
    public static int damageTotal = 0;

    public int damage = 1;

    public override void Collide(Figure f) {
        var hero = f as PlayerUnit;
        if (hero != null) {
            damageTotal += damage;
            Destroy(gameObject);
        }
    }
}
