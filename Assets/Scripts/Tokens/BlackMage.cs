using UnityEngine;
using System.Collections;
using System.Linq;

public class BlackMage : Unit
{
    public static BlackMage instance;
    public AudioSource hitSound;

    public int hitDamage = 1;

    void Awake() {
        instance = this;
    }

    public void Hit() {
        base.Hit(hitDamage);
        hitSound.time = 0.25f;
        hitSound.Play();
    }

    public override void Collide(Figure f) {
        if (f is Hero) {
            Hit();
            Locator.Locate(this);
        }
    }
}
