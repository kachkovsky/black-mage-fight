using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MonsterMove : MonoBehaviour
{
	public List<Mark> blocks;

	public Figure figure;

	public IntVector2 savedDirection;

	public Edge arrow;

	public List<IntVector2> directions = new List<IntVector2>() {
		new IntVector2(1, 0),
		new IntVector2(-1, 0),
		new IntVector2(0, 1),
		new IntVector2(0, -1),
	};

	public void Awake() {
		figure = GetComponent<Figure>();
	}

	public void Start() {
		arrow.gameObject.SetActive(false);
	}

	public void PlanNextMove() {
		savedDirection = new IntVector2(0, 0);
		if (Random.Range(0f, 1f) > 0.25f) {
			return;
		}
		for (int i = 0; i < 10; i++) {
			var direction = directions.Rnd();
			var cell = figure.Position.ToDirection(direction);
			if (cell == null || cell.figures.Any(f => f.Marked(blocks))) {
				continue;
			}
			savedDirection = direction;
			break;
		}
	}

	public void UpdateArrow() {
		if (savedDirection.x == 0 && savedDirection.y == 0) {
			arrow.gameObject.SetActive(false);
		} else {
			arrow.gameObject.SetActive(true);
			arrow.position.a = figure.Position;
			arrow.position.b = figure.Position.ToDirection(savedDirection);
			arrow.Place();
		}
	}

	public void UsePlannedMove() {
		if (savedDirection.x == 0 && savedDirection.y == 0) {
			arrow.gameObject.SetActive(false);
		}
		var cell = figure.Position.ToDirection(savedDirection);
		if (cell == null || cell.figures.Any(f => f.Marked(blocks))) {
			return;
		}
		figure.MoveTo(savedDirection).Done();
	}

	public void Move() {
		UsePlannedMove();
		PlanNextMove();
		UpdateArrow();
	}
}
