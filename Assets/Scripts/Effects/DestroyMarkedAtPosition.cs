using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyMarkedAtPosition : Effect
{
	public Mark mark;
	public List<Mark> marks;
	public Figure target;

	public void Awake() {
		if (target == null) {
			target = GetComponent<Figure>();
		}
	}

	public override void Run() {
		target.Position.figures.ForEach(f => {
			if (f.Marked(mark) || f.Marked(marks)) {
				f.gameObject.SetActive(false);
				Destroy(f.gameObject);
			}
		});
	}
}
