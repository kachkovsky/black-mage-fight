using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class OnControlsReady : Trigger
{
	public UnityEvent onControlsReady;

	public void Start() {
		Controls.instance.ready.AddListener(ControlsReady);
		Controls.instance.Ready();
	}

	public void OnDestroy() {
		if (Controls.instance) {
			Controls.instance.ready.RemoveListener(ControlsReady);
		}
	}

	public void ControlsReady() {
		onControlsReady.Invoke();
	}
}

