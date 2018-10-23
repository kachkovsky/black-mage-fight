using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class LookAt : MonoBehaviour
{
	public Transform target;

	public bool eachFrame;

	public void Start() {
		UpdateTransform();
	}

	public void Update() {
		if (eachFrame) {
			UpdateTransform();
		}
	}

	[ContextMenu("Update Transform")]
	public void UpdateTransform() {
		transform.LookAt(target, worldUp: Vector3.back);
	}
}
