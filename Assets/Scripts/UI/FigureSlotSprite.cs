using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class FigureSlotSprite : MonoBehaviour
{
	public GameObject empty;
	public GameObject busy;
	FigureSlot slot;

	public void Awake() {
		slot = GetComponent<FigureSlot>();
	}

	public void Start() {
		slot.onBusyChanged.AddListener(UpdateStatus);
	}

	public void UpdateStatus() {
		busy.SetActive(slot.busy);
		empty.SetActive(!slot.busy);
	}

	public void OnDestroy() {
		if (slot) {
			slot.onBusyChanged.RemoveListener(UpdateStatus);
		}
	}
}

