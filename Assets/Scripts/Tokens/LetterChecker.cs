using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class LetterChecker : MonoBehaviour
{
	public List<Mark> letterMarks;
	public UnityEvent onSuccess;

	public bool Check() {
		for (int i = 0; i < letters.Count - 1; i++) {
			if (letters[i].Position.y + 1 != letters[i + 1].Position.y) {
				return false;
			}
			if (letters[i].Position.x != letters[i + 1].Position.x) {
				return false;
			}
		}
		return true;
	}

	public void Start() {
	}

	public void Run() {
		if (Check()) {
			onSuccess.Invoke();
		}
	}
}
