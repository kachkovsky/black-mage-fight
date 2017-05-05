using UnityEngine;
using System.Collections;
using System.Linq;

public class OnSpawnedLess : Trigger
{
    public Spawner spawner;
    public int requiredAmount;

    public void Check() {
        if (spawner.spawnedObjects.Where(gameObject => gameObject.activeInHierarchy).Count() < requiredAmount) {
            Run();
        }
    }
}
