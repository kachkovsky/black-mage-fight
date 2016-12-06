using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Periodic : Trigger
{
    public int period = 5;
    public int phase = 0;
    public List<int> runPhases = new List<int>(new int[] {0});

    public void Tick() {
        if (runPhases.Contains(phase % period)) {
            Run();
        } 
        phase += 1;
    }
}
