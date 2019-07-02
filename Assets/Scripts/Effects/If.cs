using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public class If : Effect
{
    public Condition condition;
	public Effect then;
	public Effect else_;

	public UnityEvent thenEvent;
	public UnityEvent elseEvent;

    public override void Run() {
		if (condition == null || condition.Satisfied()) {
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
