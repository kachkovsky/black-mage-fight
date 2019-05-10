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
		var keyImage = UI.instance.keyImages.FirstOrDefault(ki => ki.keyToken == keyToken);
		if (keyImage) {
			keyImage.counter = counter;
		}
	}
}
