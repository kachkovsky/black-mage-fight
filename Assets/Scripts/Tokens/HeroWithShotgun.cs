﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class HeroWithShotgun : Hero
{
    public AudioSource shotgunSound;

    public override bool MoveTo(IntVector2 direction) {
        var cell = Position;
        var next = cell;
        for (int i = 0; i < 100; i++) {
            next = cell.ToDirection(direction);
            if (next != null && next.figures.Count == 0) {
                cell = next;
            }
        }
        if (next != null && next.figures[0] is BlackMage) {
            ShotgunHit(next.figures[0]);
            shotgunSound.Play();
        }
        cell = Position.ToDirection(direction);
        if (cell != null) { 
            if (cell.figures.FirstOrDefault() is Crate) {
                Position.MoveHere(cell.figures.First());
            }      
            var oldPosition = Position;
            cell.MoveHere(this);
            moveSound.Play();
            GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
            return true;
        } else {
            return false;
        }
    }

    void ShotgunHit(Figure f) {
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
