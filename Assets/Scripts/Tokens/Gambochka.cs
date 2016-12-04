﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Gambochka : PlayerUnit
{
    public static Gambochka instance;

    protected override void Awake() {
        base.Awake();
        instance = this;
    }

    void Start() {
        GameManager.instance.onHeroMove += OnHeroMove;
        GameObject.Find("Gambochka Status").GetComponentInChildren<HealthSlider>().unit = this;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        GameManager.instance.onHeroMove -= OnHeroMove;
    }

    void OnHeroMove(Unit hero) {
        if (hero != this) {
            Controls.instance.activeUnit = this;
        } else {
            Controls.instance.activeUnit = Hero.instance;
        }
    }

    public override void Collide(Figure f) {
        base.Collide(f);
        if (f is BlackMage) {
            Hit(5);
            Blink();
        }
    }
}
