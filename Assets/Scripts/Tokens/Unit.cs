using UnityEngine;
using System.Collections;

public class Unit : Figure
{
    public AudioSource hitSound;
    public AudioSource injuredSound;
    public AudioSource healSound;

    public int injuredDamage = 2;

    public int health;
    public int maxHealth;

    public bool Dead {
        get {
            return health <= 0;
        }
    }

    public void Reborn() {
        health = maxHealth;
    }

    public virtual void Hit(int damage, bool silent = false) {
        health -= damage;

        if (!silent) {
            this.TryPlay(damage >= injuredDamage ? injuredSound : hitSound);
        }
    }

    public void Heal(int heal) {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        this.TryPlay(healSound);
    }
}
