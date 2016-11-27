using UnityEngine;
using System.Collections;

public class Arrow : Edge
{
    public int damage;

    public override void Pick(Hero hero) {
        Destroy(gameObject);
    }

    public override void ReversePick(Hero hero) {
        hero.Hit(damage);
        Destroy(gameObject);
    }
}
