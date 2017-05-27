using System;
using UnityEngine;

public class CheckDifficulty : Condition
{
    public int difficulty = 0;

    public override bool Satisfied() {
        return GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.difficulty == difficulty;
    }
}

