using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PushSwap : MonoBehaviour
{
	public Figure figure;

	public List<Mark> ignoreWithMark;

	public bool Occupied(Cell cell) {
		return cell.figures.Any(f => !ignoreWithMark.Any(mark => f.Marked(mark)));
	}

	public void Push(IntVector2 direction) {
		var cell = figure.Position.ToDirection(direction);
		if (cell != null && !Occupied(cell)) {
			cell.MoveHere(figure);
		} else {
			cell = figure.Position.ToDirection(direction, -1);
			if (cell != null) {
				cell.MoveHere(figure);
			}
		}
	}
}
