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

    public override void Pick(Hero hero) {
        other.position.MoveHere(hero);
        Blink();
        other.Blink();
    }
}
