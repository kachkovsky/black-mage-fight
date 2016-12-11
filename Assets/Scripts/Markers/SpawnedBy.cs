using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnedBy : MonoBehaviour {
    public Spawner spawner;

    void OnDestroy() {
        spawner.spawnedObjects.Remove(gameObject);
    }
}
