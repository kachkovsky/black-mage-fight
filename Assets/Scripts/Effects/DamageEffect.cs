using UnityEngine;
using System.Collections;

public class DamageEffect : Effect
{
    public Unit target;

    public int damage = 1;

	public IntValueProvider damageProvider;

	public int Damage {
		get {
			return damageProvider != null ? damageProvider.Value : damage;
		}
	}

    public override void Run() {
        if (target == null) {
            target = Hero.instance;
        }
		target.Hit(Damage);
    }
}
