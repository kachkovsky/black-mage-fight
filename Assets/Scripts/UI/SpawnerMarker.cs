using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnerMarker<T> : OptionalSingletone<T> where T : MonoBehaviour 
{
	public PeriodicCounter periodicCounter;

	public override void Awake() {
		base.Awake();
		periodicCounter = GetComponentInChildren<PeriodicCounter>();
	}
}
