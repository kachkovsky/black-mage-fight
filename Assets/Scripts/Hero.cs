using UnityEngine;
using System.Collections;
using System.Linq;

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
        CheckHeart();
        Hit(1);
    }

    public void CheckHeart() {
        foreach (Heart h in FindObjectsOfType<Heart>()) {
            if (h.position == position) {
                h.Pick(this);
            }
        }
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
