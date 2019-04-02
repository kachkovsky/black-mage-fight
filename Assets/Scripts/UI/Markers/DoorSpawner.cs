using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorSpawner : MonoBehaviour
{
    public static DoorSpawner instance;
	public PeriodicCounter periodicCounter;

    public void Awake() {
        instance = this;
        periodicCounter = GetComponentInChildren<PeriodicCounter>();
    }
}
