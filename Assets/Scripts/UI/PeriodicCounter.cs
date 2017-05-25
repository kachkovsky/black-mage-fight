using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeriodicCounter : MonoBehaviour
{
    public Periodic periodic;

    public int Value() {
        var phase = periodic.phase % periodic.period;
        var value = periodic.runPhases.ExtMin(x => x > phase ? x - phase : x + periodic.period - phase);
        return value;
    }

    public int MaxValue() {
        return periodic.period;
    }

}
