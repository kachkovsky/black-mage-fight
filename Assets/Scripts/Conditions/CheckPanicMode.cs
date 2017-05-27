using System;
using UnityEngine;

public class CheckPanicMode : Condition
{
    public bool on = true;

    public override bool Satisfied() {
        return GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.panicMode == on;
    }
}

