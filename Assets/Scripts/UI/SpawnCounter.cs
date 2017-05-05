using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class SpawnCounter : MonoBehaviour
{
    public Tilt tilt;
    public Slider slider;
    public Spawner spawner;
    public int maxValue;
    Color defaultColor = Color.white;
    public Color tiltColor = Color.red;

    void Update() {
        int value = spawner.spawnedObjects.Count(go => go.activeInHierarchy);
        slider.value = value;
        slider.maxValue = maxValue;
        slider.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", value, maxValue);
        slider.GetComponentInChildren<Text>().color = value >= maxValue ? tiltColor : defaultColor;

        int tiltLevel = Mathf.Clamp(value + 1 - (int)slider.maxValue, 0, 1);
        if (tilt != null) {
            tilt.Switch(tiltLevel);
        }
    }
}
