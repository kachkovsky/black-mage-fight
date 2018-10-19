using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shifter : MonoBehaviour
{
	public List<Mark> blocks;
	bool shifting = false;

	public void Start() {
		EventManager.instance.beforeFigureMove += BeforeFigureMove;
	}

	public bool Aligned(Cell a, Cell b, Cell c) {
		return a.x == b.x && b.x == c.x || a.y == b.y && b.y == c.y;
	}

	public void BeforeFigureMove(Figure figure, IntVector2 direction, EventController e) {
		if (shifting) {
			return;
		}
		if (figure != Hero.instance) {
			return;
		}
		shifting = true;
		e.cancelEvent = true;
		Shift(figure, direction);
		shifting = false;
	}

	void Shift(Figure figure, IntVector2 direction) {
		List<Figure> toShift = new List<Figure>();
		var from = figure.Position;
		var to = figure.Position.ToDirection(direction);
		Board.instance.cellsList.Where(cell => Aligned(cell, from, to)).ForEach(cell => {
			cell.figures.ForEach(f => {
				if (!f.Marked(blocks)) {
					toShift.Add(f);
					//Debug.LogFormat("Shifting {0} to {1}", f, direction);
				}
			});
		});
		if (toShift.Any(f => f.Position.ToDirection(direction).figures.Any(b => b.Marked(blocks)))) {
			return;
		}
		toShift.ForEach(f => f.MoveTo(direction).Done());
	}

	public void OnDestroy() {
		if (EventManager.instance != null) {
			EventManager.instance.beforeFigureMove -= BeforeFigureMove;
		}
	}
}
