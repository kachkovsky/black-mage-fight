using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using RSG;

public class LetterChecker : MonoBehaviour
{
	public List<Mark> letterMarks;
	public UnityEvent onSuccess;

	Promise check;

	void CheckPosition(int x, int y) {
		List<Letter> result = new List<Letter>();
		for (int i = 0; i < letterMarks.Count; i++) {
			if (!Board.instance.Inside(x, y + i)) {
				return;
			}
			var letter = Board.instance.cells[x, y + i].figures.FirstOrDefault(f => f.Marked(letterMarks[i]));
			if (letter == null) {
				return;
			}
			result.Add(letter.GetComponent<Letter>());
		}
		onSuccess.Invoke();
		result.ForEach(l => l.Success());
	}

	public void Check() {
		for (int i = 0; i < Board.instance.cells.GetLength(0); i++) {
			for (int j = 0; j < Board.instance.cells.GetLength(1); j++) {
				CheckPosition(i, j);
			}
		}
	}

	public IPromise Change() {
		if (check == null) {
			check = new Promise();
		}
		return check;
	}

	public void Update() {
		if (check != null) {
			var checking = check;
			check = null;
			Check();
			checking.Resolve();
		}
	}
}
