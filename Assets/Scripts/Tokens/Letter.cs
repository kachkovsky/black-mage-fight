using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class Letter : MonoBehaviour
{
	public List<Mark> letterMarks;

	public Figure figure;
	public Marker marker;

	void Awake() {
		figure = GetComponent<Figure>();
		figure.afterMove.AddListener(AfterMove);
		marker = GetComponent<Marker>();
	}

	bool Check() {
		int x0 = figure.Position.x;
		int y0 = figure.Position.y - letterMarks.IndexOf(marker.mark);
		for (int i = 0; i < letterMarks.Count; i++) {
			if (!Board.instance.cells[x0, y0 + i].figures.Any(f => f.Marked(letterMarks[i]))) {
				return false;
			}
		}
		return true;
	}

	void AfterMove() {

	}
}
