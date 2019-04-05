using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent effect;

    public virtual void Run() {
		if (!gameObject.activeInHierarchy) {
			return;
		}
        effect.Invoke();
    }
}
