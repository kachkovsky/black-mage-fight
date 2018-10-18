using UnityEngine;
using System.Collections;

public class PushSwap : MonoBehaviour
{
	public Figure figure;

	public void Push(IntVector2 direction) {
		var cell = figure.Position.ToDirection(direction);
		if (cell != null && cell.figures.Count == 0) {
			cell.MoveHere(figure);
		} else {
			cell = figure.Position.ToDirection(direction, -1);
			if (cell != null) {
				cell.MoveHere(figure);
			}
		}
	}
}
