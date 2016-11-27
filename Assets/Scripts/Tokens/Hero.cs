using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Hero : Unit
{
    public static Hero instance;

    public AudioSource moveSound;

    void Awake() {
        instance = this;
    }

    public override void MoveTo(Cell cell) {
        var oldPosition = position;
        base.MoveTo(cell);
        if (cell == null) {
            return;
        }
        moveSound.Play();
        CheckCollisions(oldPosition);
        GameManager.instance.HeroMoved(this);
    }

    public void CheckCollisions(Cell oldPosition) {
        foreach (Figure f in FindObjectsOfType<Figure>()) {
            if (f.position == position) {
                f.Pick(this);
            }
        } 
        foreach (Edge e in FindObjectsOfType<Edge>()) {
            if (e.position.a == oldPosition && e.position.b == position) {
                e.Pick(this);
            }
            if (e.position.a == position && e.position.b == oldPosition) {
                e.ReversePick(this);
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
