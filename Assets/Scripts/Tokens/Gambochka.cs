using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Gambochka : Unit
{
    public static Gambochka instance;

    public AudioSource moveSound;

    void Awake() {
        instance = this;
    }

    void Start() {
        GameManager.instance.onHeroMove += OnHeroMove;
    }

    void OnHeroMove(Unit hero) {
        if (hero != this) {
            Controls.instance.activeUnit = this;
        } else {
            Controls.instance.activeUnit = Hero.instance;
        }
    }

    public override void MoveTo(Cell cell) {
        var oldPosition = Position;
        base.MoveTo(cell);
        if (cell == null) {
            return;
        }
        moveSound.Play();
        CheckCollisions(oldPosition);
        GameManager.instance.HeroMoved(this);
    }

    public void CheckCollisions(Cell oldPosition) {
        foreach (Edge e in FindObjectsOfType<Edge>()) {
            if (e.position.a == oldPosition && e.position.b == Position) {
                e.Pick(this);
            }
            if (e.position.a == Position && e.position.b == oldPosition) {
                e.ReversePick(this);
            }
        }
    }
}
