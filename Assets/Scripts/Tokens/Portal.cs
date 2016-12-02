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
            other.Position.MoveHere(hero);
            Blink();
            other.Blink();
        }
    }
}
