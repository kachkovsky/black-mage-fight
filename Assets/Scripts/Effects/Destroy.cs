using UnityEngine;
using System.Collections;

public class Destroy : Effect
{
    public GameObject target;

	public void Awake() {
		if (target == null) {
			target = gameObject;
		}
	}

    public override void Run() {
        target.SetActive(false);
        Destroy(target);
    }
}
