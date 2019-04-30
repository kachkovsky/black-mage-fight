using System;
using UnityEngine;
using System.Collections.Generic;

public class CheckDifficulty : Condition
{
    public int difficulty = 0;
    public List<int> diffuculties;
	public bool atLeast;
	public bool atMost;

    public override bool Satisfied() {
		if (GameManager.instance.gameState.CurrentRun == null) {
			return false;
		}
        if (diffuculties.Count > 0) {
            return diffuculties.Contains(GameManager.instance.gameState.CurrentRun.difficulty);
        }
		if (atLeast) {
			return GameManager.instance.gameState.CurrentRun.difficulty >= difficulty;
		}
		if (atMost) {
			return GameManager.instance.gameState.CurrentRun.difficulty <= difficulty;
		}
        return GameManager.instance.gameState.CurrentRun.difficulty == difficulty;
    }
}

