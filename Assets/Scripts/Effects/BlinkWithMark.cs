using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BlinkWithMark : Effect
{
	public List<Mark> marks;

	public override void Run() {
		FindObjectsOfType<Figure>().Where(f => f.Marked(marks)).ForEach(f => f.Relocate());
	}
}
