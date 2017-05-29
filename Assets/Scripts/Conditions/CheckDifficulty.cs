using System;
using UnityEngine;
using System.Collections.Generic;

public class CheckDifficulty : Condition
{
    public int difficulty = 0;
    public List<int> diffuculties;

    public override bool Satisfied() {
        if (diffuculties.Count > 0) {
            return GameManager.instance.gameState.CurrentRun != null && diffuculties.Contains(GameManager.instance.gameState.CurrentRun.difficulty);
        }
        return GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.difficulty == difficulty;
    }
}

