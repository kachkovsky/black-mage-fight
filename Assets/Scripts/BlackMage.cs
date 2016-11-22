using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BlackMage : Unit
{
    public static BlackMage instance;

    void Awake() {
        instance = this;
    }

    void OnEnable() {
        instance = this;
    }
}
