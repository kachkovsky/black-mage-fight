using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class FigureFollow : MonoBehaviour
{
	public Figure figure;
	public Figure target;
	public int dx;
	public int dy;

	public void Awake() {
		figure = GetComponent<Figure>();
	}

	public void Start() {
		target.afterMove.AddListener(AfterTargetMove);
		UpdatePosition();
	}

	[ContextMenu("Update Position")]
	public void UpdatePosition() {
		target.Position.ToDirection(new IntVector2(dx, dy)).MoveHere(figure);
	}

	void AfterTargetMove() {
		UpdatePosition();
	}

	public void OnDestroy() {
		target.afterMove.RemoveListener(AfterTargetMove);
	}
}
