using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class Periodic : Trigger
{
    public int period = 5;
    public int phase = 0;
    public List<int> runPhases = new List<int>(new int[] {0});

    public UnityEvent tick;

    public void Tick() {
        phase += 1;
        if (runPhases.Contains(phase % period)) {
            Run();
        } 
        tick.Invoke();
    }
}
