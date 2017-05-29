using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class EvilEye : Figure
{
    public int damage;

    public override void OnSee(Figure other) {
        if (other.Position.Distance(this.Position) != 1) {
            var hero = other as Hero;
            hero.Hit(damage);
        }
    }
}
