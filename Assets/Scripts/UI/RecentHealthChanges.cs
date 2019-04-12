using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecentHealthChanges : MonoBehaviour
{
	public UnitValueProvider unitProvider;

	Unit unit {
		get {
			return unitProvider.Value;
		}
	}

	public GameObject recentDamage;
	public GameObject recentHeal;

	public Text recentDamageText;
	public Text recentHealText;

	public void Update() {
		if (unit == null) {
			return;
		}
		recentDamage.SetActive(unit.recentDamage > 0);
		recentHeal.SetActive(unit.recentDamage < 0);
		recentDamageText.text = unit.recentDamage.ToString();
		recentHealText.text = (-unit.recentDamage).ToString();
	}
}
