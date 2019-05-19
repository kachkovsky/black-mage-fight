using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarrelSetter : MonoBehaviour
{
	public static BarrelSetter instance;

	public void Awake() {
		instance = this;
	}
}
