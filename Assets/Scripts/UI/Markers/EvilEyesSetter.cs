using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EvilEyesSetter : MonoBehaviour
{
    public static EvilEyesSetter instance;

    public PeriodicCounter periodicCounter;

    public void Awake() {
        instance = this;
        periodicCounter = GetComponent<PeriodicCounter>();
    }
}
