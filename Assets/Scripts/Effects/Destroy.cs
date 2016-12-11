using UnityEngine;
using System.Collections;

public class Destroy : Effect
{
    public GameObject target;

    public override void Run() {
        target.SetActive(false);
        Destroy(target);
    }
}
