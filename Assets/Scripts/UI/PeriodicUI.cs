using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PeriodicUI : MonoBehaviour
{
	public int n = 17;

	public List<Transform> axes;

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
				axes[i].rotation = Quaternion.Euler(Vector3.forward * (90 + 360 * i / p.periodic.period));
			} else {
				axes[i].gameObject.SetActive(false);
			}
		}
	}

}
