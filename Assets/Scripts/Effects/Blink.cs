using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Blink : Effect
{
    public Figure target;
    public Locator locator;
	public List<Spawner> ignoreSpawnedBy;
	public List<Mark> ignoreWithMark;

    bool Occupies(Figure f) {
		var result = !ignoreSpawnedBy.Any(spawner => spawner.spawnedObjects.Contains(f.gameObject)) &&
		                   !f.GetComponents<Marker>().Any(marker => ignoreWithMark.Contains(marker.mark));
        //Debug.LogFormat("{0} occupies versus {1}", f, this);
        return result;
    }

    public override void Run() {
		Debug.LogFormat("Blink.Run");
        if (locator != null) {
            locator.LocateHere(target);
        } else {
            target.Blink(Occupies);
        }
    }
}
