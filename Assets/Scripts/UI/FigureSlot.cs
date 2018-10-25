using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class FigureSlot : MonoBehaviour
{
	public Figure figure;
	public FigureCondition condition;
	public bool busy;

	public UnityEvent onBusyChanged;

	public void Awake() {
		figure = GetComponent<Figure>();
	}

	public void UpdateStatus() {
		busy = figure.Position.figures.Any(f => condition.Satisfied(f));
		onBusyChanged.Invoke();
	}
}

