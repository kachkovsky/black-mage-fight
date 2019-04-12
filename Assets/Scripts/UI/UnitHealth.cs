using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
	public UnitValueProvider unitProvider;

	Unit unit {
		get {
			return unitProvider.Value;
		}
	}

	public Text healthText;

	public void Update() {
		if (unit == null) {
			return;
		}
		healthText.text = string.Format("<b>{0}/{1}</b>", unit.health, unit.maxHealth);
	}
}
