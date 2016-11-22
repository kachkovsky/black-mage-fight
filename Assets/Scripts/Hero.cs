using UnityEngine;
using System.Collections;

public class Hero : Unit
{
    public static Hero instance;

    void Awake() {
        instance = this;
    }
}
