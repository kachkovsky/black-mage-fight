using UnityEngine;
using System.Collections;
using System.Linq;

public class OnSpawnedEnough : Trigger
{
    public Spawner spawner;
    public int requiredAmount;

    public void Check() {
        if (spawner.Spawns().Count >= requiredAmount) {
            Run();
        }
    }
}
