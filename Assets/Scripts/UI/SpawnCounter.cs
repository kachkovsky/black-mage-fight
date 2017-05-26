using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class SpawnCounter : MonoBehaviour
{
    public Tilt tilt;
    public Spawner spawner;
    public int maxValue;
    Color defaultColor = Color.white;
    public Color tiltColor = Color.red;

    void Update() {
        int value = spawner.spawnedObjects.Count(go => go.activeInHierarchy);
    }
}
