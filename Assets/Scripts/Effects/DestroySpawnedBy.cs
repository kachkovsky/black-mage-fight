using UnityEngine;
using System.Collections;

public class DestroySpawnedBy : Effect
{
    public Spawner spawner;

    public override void Run() {
        spawner.Spawns().ForEach(go => {
            go.SetActive(false);
            Destroy(go);
        });
    }
}
