using UnityEngine;
using System.Collections;

public class DestroyMarkedAtPosition : Effect
{
	public Mark mark;
	public Figure target;

	public override void Run() {
		target.Position.figures.ForEach(f => {
			if (f.Marked(mark)) {
				f.gameObject.SetActive(false);
				Destroy(f.gameObject);
			}
		});
	}
}
