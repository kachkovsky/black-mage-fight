using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Hero : PlayerUnit
{
    public static Hero instance;

    public override void Awake() {
        base.Awake();
        instance = this;
        Debug.LogFormat("Hero instance: {0}", transform.Path());
    }

    public override void Hit(int damage, bool silent = false) {
        base.Hit(damage);
    }
}
