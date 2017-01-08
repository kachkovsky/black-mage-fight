using UnityEngine;
using System.Collections;
using System.Linq;

public class Portal : Figure
{
    public int id;
    Portal other;

    void Start() {
        other = FindObjectsOfType<Portal>().Where(p => p.id == this.id && p != this).First();
    }

    public override void Collide(Figure f) {
        var hero = f as Hero;
        if (hero != null) {
            var newHeroPosition = other.Position;
            Blink();
            other.Blink();
            newHeroPosition.MoveHere(hero);
        }
    }
}
