using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class KeyImage : MonoBehaviour
{
	public GameObject keyToken;

	public Counter counter;

	public GameObject keyImage;
	public GameObject inactivekeyImage;

	public void Update() {
		if (counter) {
			keyImage.SetActive(counter.value == 1);
			inactivekeyImage.SetActive(counter.value == 0);
		} else {
			keyImage.SetActive(false);
			inactivekeyImage.SetActive(false);
		}
	}
}
