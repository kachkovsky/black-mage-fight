using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using RSG;

public class LetterPainter : MonoBehaviour
{
	public List<Color> colors;

	public SpriteRenderer toPaint;

	public void Start() {
		var letter = GetComponent<Letter>();
		var index = letter.checker.letterMarks.IndexOf(letter.marker.mark);
		toPaint.color = colors[index];
	}
}
