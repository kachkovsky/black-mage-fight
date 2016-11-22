﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Hero : Unit
{
    public static Hero instance;

    void Awake() {
        instance = this;
    }

    void OnEnable() {
        instance = this;
    }
}
