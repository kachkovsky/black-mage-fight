using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FigureFilter : MonoBehaviour
{
	public FigureCondition condition;

	public FigureEvent thenEvent;
	public FigureEvent elseEvent;

	public void Run(Figure f) {
		if (condition.Satisfied(f)) {
			thenEvent.Invoke(f);
		} else {
			elseEvent.Invoke(f);
		}
	}
}
