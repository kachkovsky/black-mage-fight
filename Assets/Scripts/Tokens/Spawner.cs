using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Spawner : Token
{
    public GameObject sample;

    public List<GameObject> spawnedObjects;

    public void Start() {
        sample.SetActive(false);
    }

    public void Spawn() {
        var spawn = Instantiate(sample);
        spawn.SetActive(true);
        spawnedObjects.Add(spawn);
        spawn.AddComponent<SpawnedBy>().spawner = this;
        spawn.GetComponentsInChildren<OnSpawn>().ForEach(onSpawn => onSpawn.Run());
    }
}
