using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class TokenCounter : MonoBehaviour
{
	public Mark mark;
	public static Map<Mark, int> cnt = new Map<Mark, int>();
	public static Map<Mark, List<GameObject>> list = new Map<Mark, List<GameObject>>(() => new List<GameObject>());

	void Start() {
		cnt[mark]++;
		list[mark].Add(gameObject);
		Debug.LogFormat("Added {0}", mark);
	}

	void OnDestroy() {
		if (GameManager.instance != null) {
			cnt[mark]--;
			list[mark].Remove(gameObject);
			Debug.LogFormat("Removed {0}", mark);
		}
	}
}
