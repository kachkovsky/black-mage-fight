using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MultipleTimes : Effect
{
	public int times = 1;
	public UnityEvent effect;

	public override void Run() {
		for (int i = 0; i < times; i++) {
			effect.Invoke();
		}
	}
}
