using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnLetterChecker : MonoBehaviour
{
	public LetterChecker letterChecker;

	public UnityEvent onSuccess;

	void Start() {
		letterChecker.onSuccess.AddListener(Success);
	}

	private void Success() {
		onSuccess.Invoke();
	}

	void OnDestroy() {
		letterChecker.onSuccess.RemoveListener(Success);
	}
}
