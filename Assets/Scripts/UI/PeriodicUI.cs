using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PeriodicUI : MonoBehaviour
{
	public int n = 17;

	public List<PeriodicUIAxe> axes;

	public Color basic;
	public Color active;

	public Transform arrow;

	public void Awake() {
		for (int i = 0; i < n - 1; i++) {
			var newAxe = Instantiate(axes[0], transform);
			axes.Add(newAxe);
		}
	}

	public void UpdatePeriodic(PeriodicCounter p) {
		for (int i = 0; i < axes.Count; i++) {
			if (i < p.periodic.period) {
				axes[i].gameObject.SetActive(true);
				axes[i].transform.rotation = Quaternion.Euler(Vector3.forward * (90 - 360 * (i - 0.5f) / p.periodic.period));
				axes[i].image.color = basic;
			} else {
				axes[i].gameObject.SetActive(false);
			}
		}
		for (int i = 0; i < p.periodic.runPhases.Count; i++) {
			axes[p.periodic.runPhases[i]].image.color = active;
		}
		arrow.rotation = Quaternion.Euler(Vector3.forward * (90 - 360 * p.periodic.phase / p.periodic.period));
	}

}
