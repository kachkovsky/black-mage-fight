using System;
using UnityEngine;
using System.Collections.Generic;

public class Marked : FigureCondition
{
	public List<Mark> marks;

	public override bool Satisfied(Figure obj) {
		return obj.Marked(marks);
	}
}

