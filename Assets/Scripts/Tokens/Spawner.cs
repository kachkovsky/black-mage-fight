using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Spawner : Token
{
    public GameObject sample;

    public void Start() {
        sample.SetActive(false);
    }

    public void Spawn() {
        var spawn = Instantiate(sample);
        spawn.SetActive(true);
        spawn.AddComponent<SpawnedBy>().spawner = this;
        spawn.GetComponentsInChildren<OnSpawn>().ForEach(onSpawn => onSpawn.Run());
    }

    public List<GameObject> Spawns() {
        return FindObjectsOfType<SpawnedBy>().Where(sb => sb.spawner == this).Select(sb => sb.gameObject).ToList();
    }
}
