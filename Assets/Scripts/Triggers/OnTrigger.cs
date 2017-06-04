using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnTrigger : Trigger
{
    public Trigger trigger;

    void Start() {
        trigger.effect.AddListener(Run);
    }
}
