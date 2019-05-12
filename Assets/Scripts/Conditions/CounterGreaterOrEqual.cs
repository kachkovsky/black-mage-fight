using System;
using UnityEngine;

public class CounterGreaterOrEqual : Condition
{
    public Counter counter;
    public int value;
	public IntValueProvider valueProvider;

	public int Value {
		get {
			return valueProvider ? valueProvider.Value : value;
		}
	}

    public override bool Satisfied() {
        return counter.value >= Value;
    }
}

