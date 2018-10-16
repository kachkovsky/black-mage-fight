using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using RSG;

public class HeroWithShotgun : Hero
{
    public AudioSource shotgunSound;
    public AudioSource explosionSound;

    public GameObject bulletSample;
    public GameObject explosionSample;
    public GameObject lineSample;

    IPromise PlayBulletFlyAnimation(Cell from, Cell to) {
        Vector3 move = (to.transform.position - from.transform.position);

        var bullet = Instantiate(bulletSample);
        bullet.SetActive(true);
        bullet.transform.position = from.transform.position;
        bullet.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg);

        var bulletLine = Instantiate(lineSample);
        bulletLine.SetActive(true);
        bulletLine.GetComponentInChildren<LineRenderer>().SetPosition(0, from.transform.position + Vector3.back);
        bulletLine.GetComponentInChildren<LineRenderer>().SetPosition(1, to.transform.position + Vector3.back);
        TimeManager.Wait(0.1f).Then(() => {
            Destroy(bulletLine);
        }).Done();

        return AnimationsManager.Move(bullet.transform, from.transform.position, to.transform.position, 100).Then(() => {
            Destroy(bullet);
        });
    }

    IPromise PlayExplosionAnimation(Cell from) {
        var explosion = Instantiate(explosionSample);
        explosion.SetActive(true);
        explosion.transform.position = from.transform.position;

        return TimeManager.Wait(0.1f).Then(() => {
            Destroy(explosion);
        });
    }

    public override IPromise<bool> MoveTo(IntVector2 direction) {
        var cell = Position;
        var next = cell;
        for (int i = 0; i < 100; i++) {
            next = cell.ToDirection(direction);
            if (next != null && next.Figures.Count() == 0) {
                cell = next;
            }
        }
        return Promise.Do(() => {
            if (next != null) {
                shotgunSound.Play();
                return PlayBulletFlyAnimation(Position, next).Then(() => {
                    ShotgunHit(next.figures[0]);
                });
            } else {
                return Promise.Resolved();
            }
        }).Then(() => {
            cell = Position.ToDirection(direction);
            if (cell != null) { 
                if (cell.figures.FirstOrDefault() is Crate) {
                    Position.MoveHere(cell.figures.First());
                }      
                var oldPosition = Position;
                cell.MoveHere(this);
                moveSound.Play();
                GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
            }
        }).Return(true);
    }

    void Explosion(Cell cell) {
        if (cell == null) {
            return;
        }
        if (cell.Figures.Count > 0) {
            ShotgunHit(cell.Figures.First());
        }
    }

    void ShotgunHit(Figure f) {
        if (f is BlackMage) {
            var bm = f as BlackMage;
            bm.Hit();
            bm.Relocate();
        }
        if (f is Hero) {
            (f as Hero).Hit(10);
        }
        if (f is Barrel) {
            var cell = f.Position;
            f.gameObject.SetActive(false);
            Destroy(f.gameObject);
            PlayExplosionAnimation(cell);
            this.TryPlay(explosionSound);
            Explosion(cell.ToDirection(new IntVector2(1, 0)));
            Explosion(cell.ToDirection(new IntVector2(-1, 0)));
            Explosion(cell.ToDirection(new IntVector2(0, 1)));
            Explosion(cell.ToDirection(new IntVector2(0, -1)));
            Explosion(cell.ToDirection(new IntVector2(1, 1)));
            Explosion(cell.ToDirection(new IntVector2(-1, 1)));
            Explosion(cell.ToDirection(new IntVector2(1, -1)));
            Explosion(cell.ToDirection(new IntVector2(-1, -1)));
        }
        if (f is Crate) {
            f.gameObject.SetActive(false);
            Destroy(f.gameObject);
        }
		if (f.gameObject.activeSelf) {
			var onHit = f.GetComponent<OnShotgunHit>();
			if (onHit != null) {
				onHit.Run();
			}
		}
    }
}
