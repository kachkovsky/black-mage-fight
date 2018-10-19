using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shifter : MonoBehaviour
{
	public List<Mark> blocks;

	public bool Aligned(Cell a, Cell b, Cell c) {
		return a.x == b.x && b.x == c.x || a.y == b.y && b.y == c.y;
	}

	public void OnHeroMove(Unit hero, Cell from, Cell to, IntVector2 direction) {
		List<Figure> toShift = new List<Figure>();
		Board.instance.cellsList.Where(cell => Aligned(cell, from, to)).ForEach(cell => {
			cell.figures.ForEach(f => {
				if (f != hero && !f.Marked(blocks)) {
					toShift.Add(f);
					//Debug.LogFormat("Shifting {0} to {1}", f, direction);
				}
			});
		});
		toShift.ForEach(f => f.MoveTo(direction));
	}
}
