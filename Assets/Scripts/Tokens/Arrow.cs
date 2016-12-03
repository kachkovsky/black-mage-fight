using UnityEngine;
using System.Collections;

public class Arrow : Edge
{
    public int damage;

    public override void Pick(Unit hero) {
        Destroy(gameObject);
    }

    public override void ReversePick(Unit hero) {
        hero.Hit(damage);
        Destroy(gameObject);
    }
}
