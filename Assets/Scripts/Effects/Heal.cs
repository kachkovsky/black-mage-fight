using UnityEngine;
using System.Collections;

public class Heal : Effect
{
    public Unit target;
	public int amount = 1;
	public IntValueProvider amountProvider;

	public int Amount {
		get {
			return amountProvider ? amountProvider.Value : amount;
		}
	}

    public override void Run() {
        target.Heal(Amount);
    }
}
