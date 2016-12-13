using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpawnCounter : MonoBehaviour
{
    public Slider slider;
    public Spawner spawner;
    public int maxValue;

    void Update() {
        slider.value = spawner.spawnedObjects.Count;
        slider.maxValue = maxValue;
    }
}
