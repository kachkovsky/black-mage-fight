using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class KeyCounter : MonoBehaviour
{
	public static KeyCounter instance;

	public Counter counter;

	public GameObject keyToken;

	public void Awake() {
		instance = this;
		UI.instance.keyImages.First(ki => ki.keyToken == keyToken).counter = counter;
	}
}
