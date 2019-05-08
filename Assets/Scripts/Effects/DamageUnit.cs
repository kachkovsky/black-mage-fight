using UnityEngine;
using System.Collections;

public class DamageUnit : UnitEffect
{
    public int damage = 1;

	public IntValueProvider damageProvider;

	public int Damage {
		get {
			return damageProvider != null ? damageProvider.Value : damage;
		}
	}

    public override void Run(Unit hero) {
        hero.Hit(Damage);
    }
}
