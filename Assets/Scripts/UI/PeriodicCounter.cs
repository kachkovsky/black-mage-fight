using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeriodicCounter : MonoBehaviour
{
    public Periodic periodic;

	public bool Multiple() {
		return periodic.multipleRunPhases.Count > 1;
	}

    public int Value() {
        var phase = periodic.phase % periodic.Period;
		var value = Mathf.Min(
			periodic.runPhases.ExtMin(x => x > phase ? x - phase : x + periodic.Period - phase),
			periodic.multipleRunPhases.ExtMin(x => x.phase > phase ? x.phase - phase : x.phase + periodic.Period - phase)
		);
        return value;
    }

    public int MaxValue() {
        return periodic.Period;
    }

}
