using UnityEngine;
using System.Collections;

public class If : Effect
{
    public Condition condition;
	public Effect then;
	public Effect else_;

    public override void Run() {
		if (condition.Satisfied()) {
			if (then != null) {
				then.Run();
			}
		} else {
			if (else_ != null) {
				else_.Run();
			}
		}
    }
}
