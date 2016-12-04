using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Hero : PlayerUnit
{
    public static Hero instance;

    protected override void Awake() {
        base.Awake();
        instance = this;
    }
}
