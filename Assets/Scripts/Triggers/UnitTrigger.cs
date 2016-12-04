using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class UnitTrigger : MonoBehaviour
{
    public UnitEffect effect;

    protected virtual void Run(Unit unit) {
        effect.Run(unit);
    }
}
