using UnityEngine;
using System.Collections;

public class DestroySkullsAtPosition : Effect
{
	public Figure target;

	public override void Run() {
		target.Position.figures.ForEach(f => {
			if (f is Skull) {
				Destroy(f.gameObject);
			}
		});
	}
}
