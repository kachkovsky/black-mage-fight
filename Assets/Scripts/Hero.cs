using UnityEngine;
using System.Collections;

public class Hero : Unit
{
    public static Hero instance;

    void Awake() {
        instance = this;
    }

    public override void MoveTo(Cell cell) {
        base.MoveTo(cell);
        CheckAttack();
    }

    public void CheckAttack() {
        if (BlackMage.instance.position == position) {
            Attack();
        }
    }

    private void Attack() {
        BlackMage.instance.Hit();
        BlackMage.instance.Blink();
    }
}
