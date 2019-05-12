using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MultipleTimes : Effect
{
	public int times = 1;

	public IntValueProvider timesProvider;

	public int Times {
		get {
			return timesProvider ? timesProvider.Value : times;
		}
	}

	public UnityEvent effect;

	public override void Run() {
		for (int i = 0; i < Times; i++) {
			effect.Invoke();
		}
	}
}
