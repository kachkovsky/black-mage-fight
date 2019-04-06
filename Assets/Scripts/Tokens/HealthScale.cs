using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class HealthScale : MonoBehaviour
{
	public List<int> health = new List<int>() {40, 40, 40, 40, 40, 40};

	public void Awake() {
		var unit = GetComponent<Unit>();
		unit.health = unit.maxHealth = health[GameManager.instance.gameState.CurrentRun.difficulty];
	}
}
