using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    public UnityEvent effect;

    public virtual void Run() {
        effect.Invoke();
    }
}
