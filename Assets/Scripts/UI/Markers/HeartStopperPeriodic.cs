using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartStopperPeriodic : MonoBehaviour
{
    public static HeartStopperPeriodic instance;
	public PeriodicCounter periodicCounter;

    public void Awake() {
        instance = this;
        periodicCounter = GetComponent<PeriodicCounter>();
    }
}
