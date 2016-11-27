using UnityEngine;
using System.Collections;

public class Unit : Figure
{
    public int health;
    public int maxHealth;

    public bool Dead() {
        return health <= 0;
    }

    public void Reborn() {
        health = maxHealth;
    }

    public void Hit(int damage) {
        health -= damage;
    }

    public void Heal(int heal) {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
