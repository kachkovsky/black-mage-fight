using UnityEngine;
using System.Collections;

public class BlackMage : Unit
{
    public static BlackMage instance;

    void Awake() {
        instance = this;
    }
}
