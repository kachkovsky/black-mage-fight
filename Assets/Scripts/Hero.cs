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
        CheckCollisions();
        Hit(1);
    }

    public void CheckCollisions() {
        foreach (Figure f in FindObjectsOfType<Figure>()) {
            if (f.position == position) {
                f.Pick(this);
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
