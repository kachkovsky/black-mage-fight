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

    void Update() {
        slider.value = spawner.spawnedObjects.Count(go => go.activeInHierarchy);
        slider.maxValue = maxValue;
        if (tilt != null) {
            tilt.Switch((int)(spawner.spawnedObjects.Count(go => go.activeInHierarchy) + 1 - slider.maxValue));
        }
    }
}
