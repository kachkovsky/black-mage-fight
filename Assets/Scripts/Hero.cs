using UnityEngine;
using System.Collections;

public class Hero : Unit
{
    public static Hero instance;

    public AudioSource moveSound;

    void Awake() {
        instance = this;
    }

    public override void MoveTo(Cell cell) {
        base.MoveTo(cell);
        moveSound.Play();
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
