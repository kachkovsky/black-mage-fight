using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class Unit : Figure
{
    public AudioSource hitSound;
    public AudioSource injuredSound;
    public AudioSource healSound;

    public int injuredDamage = 2;

    public int health;
    public int maxHealth;

	public List<Buff> buffs;

	public UnityEvent onHitInvulnerable;

	public bool Invulnerable {
		get {
			return buffs.Any(b => b is Invulnerability);
		}
	}

    public bool Dead {
        get {
            return health <= 0;
        }
    }

    public void Reborn() {
        health = maxHealth;
    }

    public virtual void Hit(int damage, bool silent = false) {
		if (Invulnerable) {
			onHitInvulnerable.Invoke();
			return;
		}

        health -= damage;
        if (health < 0) {
            health = 0;
        }

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
