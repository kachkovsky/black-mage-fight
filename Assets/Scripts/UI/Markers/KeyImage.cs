using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class KeyImage : MonoBehaviour
{
	public GameObject keyToken;

	public Counter counter;

	public Image keyImage;
	public Image inactivekeyImage;

	public void Update() {
		if (counter) {
			keyImage.enabled = counter.value == 1;
			inactivekeyImage.enabled = counter.value == 0;
		} else {
			keyImage.enabled = false;
			inactivekeyImage.enabled = false;
		}
	}
}
