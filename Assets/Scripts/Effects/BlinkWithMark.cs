using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BlinkWithMark : Effect
{
	public List<Mark> marks;

	public bool cashList = true;

	IEnumerable<Figure> cashedList = null;

	public override void Run() {
		if (!cashList || cashedList == null) {
			cashedList = FindObjectsOfType<Figure>().Where(f => f.Marked(marks));
		}
		cashedList.ForEach(f => f.Relocate());
	}
}
