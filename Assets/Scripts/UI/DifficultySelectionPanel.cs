using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class DifficultySelectionPanel : MonoBehaviour
{   
    public static DifficultySelectionPanel instance;

    public Toggle continuousRun;
    public Toggle panicMode;
    public Toggle randomized;
    public Toggle buildMode;
    public List<UnityEngine.UI.Button> difficultyButtons;

    public void Awake() {
        instance = this;
    }

    public void Go(int difficulty) {
        var run = new GameRun();
        run.difficulty = difficulty;
        run.continuousRun = continuousRun.isOn;
        run.panicMode = panicMode.isOn;
        run.randomized = randomized.isOn;
        run.buildMode = buildMode.isOn;
        GameManager.instance.gameState.CurrentProfile.currentRuns.Add(run);
        GameManager.instance.Save();
        GameManager.instance.UpdateState();
    }

}
