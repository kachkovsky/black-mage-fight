using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class Letter : MonoBehaviour
{
	public Figure figure;
	public Marker marker;

	public LetterChecker checker;

	public UnityEvent afterSuccess;

	void Awake() {
		figure = GetComponent<Figure>();
		figure.afterMove.AddListener(AfterMove);
		marker = GetComponent<Marker>();
	}

	void AfterMove() {
		Controls.instance.Lock(this);
		checker.Change().Then(() => Controls.instance.Unlock(this)).Done();
	}

	public void Success() {
		TimeManager.Wait(0).Then(() => afterSuccess.Invoke()).Done();
	}
}
