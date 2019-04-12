using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

// the earliest priority – it need to prevent all other Awakes from illegal active levels
public class ClearWorld : MonoBehaviour
{
	public void Awake() {
		FindObjectsOfType<Level>().ForEach(l => l.gameObject.SetActive(false));
	}
}
