using UnityEngine;
using System.Collections;

public class BlackMage : Unit
{
    public static BlackMage instance;

    void Awake() {
        instance = this;
    }

    public void Hit() {
    }

    public void Blink() {
        Board.instance.cells.Rand().MoveHere(this);
        Hero.instance.CheckAttack();
    }
}
