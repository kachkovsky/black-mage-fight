using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using RSG;

public class ChargingHero : Hero
{
    public Trail trailSample;

    IPromise PlayMoveAnimation(Cell from, Cell to) {
        Vector3 move = (to.transform.position - from.transform.position);
        int steps = 2 * from.Distance(to);
        Vector3 step = move / steps;

        for (int i = 1; i < steps; i++) {
            var j = i;
            TimeManager.Wait(0.05f * i / steps).Then(() => {
                var trail = Instantiate(trailSample);
                trail.gameObject.SetActive(true);
                trail.transform.position = from.transform.position + j * step + Vector3.back;
                trail.ChangeAlpha(1f * j / steps); 
                Destroy(trail.gameObject, 0.1f * j / steps);
            });
        }

        return Promise.Resolved();
    }

    public override IPromise<bool> MoveTo(IntVector2 direction) {
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
            PlayMoveAnimation(oldPosition, Position);
            GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
            return Promise<bool>.Resolved(true);
        } else {
            return Promise<bool>.Resolved(false);
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
        if (f is Heart) {
            (f as Heart).Collide(Hero.instance);
        }
    }
}
