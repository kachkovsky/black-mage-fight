using UnityEngine;
using System.Collections;

public class TimeTrigger : Trigger
{
    public float runEvery;
    float lastRun;

	public bool pausable = false;

    void Update() {
        if (GameManager.instance.LevelIsRunning()) {
            if (lastRun + runEvery < TimeManager.Time()) {
                Run();
            }
        }
    }

    public override void Run() {
        base.Run();
        lastRun = TimeManager.Time();
    }
}
