using UnityEngine;
using System.Collections;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;
using System.Collections.Generic;

public class SwapperHelper : Token
{
	public List<LineRenderer> lines;

	void See(Cell cell, IntVector2 direction, LineRenderer line) {
		var start = cell;
		cell = cell.ToDirection(direction);
		for (int i = 0; i < 100 && cell != null && !cell.figures.Any(f => f is Swapper && f.gameObject.activeSelf); i++) {
            cell = cell.ToDirection(direction);
        }
		if (cell != null) {
			line.enabled = true;
			line.SetPosition(
				0, 
				((start.transform.position + start.ToDirection(direction).transform.position)/2)
					.Change(z: -1)
			);
			line.SetPosition(
				1, 
				((cell.transform.position+cell.ToDirection(-direction).transform.position)/2)
					.Change(z: -1));
		} else {
			line.enabled = false;
		}
    }

    public void Run() {
        See(Hero.instance.Position, new IntVector2(1, 0), lines[0]);
        See(Hero.instance.Position, new IntVector2(-1, 0), lines[1]);
        See(Hero.instance.Position, new IntVector2(0, 1), lines[2]);
        See(Hero.instance.Position, new IntVector2(0, -1), lines[3]);
    }
}
