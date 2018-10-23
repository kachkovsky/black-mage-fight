using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FigureFilter : MonoBehaviour
{
	public Condition condition;

	public FigureEvent thenEvent;
	public FigureEvent elseEvent;

	public override void Run(Figure figure) {
		if (condition.Satisfied()) {
			if (then != null) {
				then.Run();
			}
			thenEvent.Invoke();
		} else {
			if (else_ != null) {
				else_.Run();
			}
			elseEvent.Invoke();
		}
	}
}
