using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Blink : Effect
{
    public Figure target;
    public Locator locator;
    public List<Spawner> ignoreSpawnedBy;

    bool Occupies(Figure f) {
        var result = !ignoreSpawnedBy.Any(spawner => spawner.spawnedObjects.Contains(f.gameObject));
        //Debug.LogFormat("{0} occupies versus {1}", f, this);
        return result;
    }

    public override void Run() {
        if (locator != null) {
            locator.LocateHere(target);
        } else {
            target.Blink(Occupies);
        }
    }
}
