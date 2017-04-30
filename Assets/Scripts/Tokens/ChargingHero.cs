using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ChargingHero : Hero
{
    public override bool MoveTo(IntVector2 direction) {
        var cell = Position;
        var next = cell;
        for (int i = 0; i < 100; i++) {
            next = cell.ToDirection(direction);
            if (next != null && next.figures.Count == 0) {
                cell = next;
            }
        }
        if (next != null) {
            ChargeHit(next.figures[0]);
        } else {
            if (cell != Position) {
                FindObjectOfType<Spawner>().Spawn();
                Position.MoveHere(FindObjectOfType<Spawner>().spawnedObjects.Last().GetComponent<Crate>());
            }
        }
        if (cell != Position || next != null) {
            var oldPosition = Position;
            cell.MoveHere(this);
            moveSound.Play();
            GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
            return true;
        } else {
            return false;
        }
    }

    void ChargeHit(Figure f) {
        if (f is BlackMage) {
            var bm = f as BlackMage;
            bm.Hit();
            bm.Relocate();
        }
        if (f is Crate) {
            f.gameObject.SetActive(false);
            Destroy(f.gameObject);
        }
    }
}
