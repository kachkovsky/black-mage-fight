using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MonsterMove : MonoBehaviour
{
	public List<Mark> blocks;

	public Figure figure;

	public List<IntVector2> directions = new List<IntVector2>() {
		new IntVector2(1, 0),
		new IntVector2(-1, 0),
		new IntVector2(0, 1),
		new IntVector2(0, -1),
	};

	public void Awake() {
		figure = GetComponent<Figure>();
	}

	public void Move() {
		if (Random.Range(0f, 1f) > 0.25f) {
			return;
		}
		for (int i = 0; i < 10; i++) {
			var direction = directions.Rnd();
			var cell = figure.Position.ToDirection(direction);
			if (cell == null || cell.figures.Any(f => f.Marked(blocks))) {
				continue;
			}
			figure.MoveTo(direction).Done();
			break;
		}
	}
}
