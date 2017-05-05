using UnityEngine;
using System.Collections;

public class DestroySpawnedByAtPosition : Effect
{
    public Spawner spawner;
    public Figure target;

    public override void Run() {
        spawner.spawnedObjects.ForEach(go => {
            if (go.GetComponent<Figure>().Position == target.Position) {
                go.SetActive(false);
                Destroy(go);
            }
        });
    }
}
