using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Periodic : Trigger
{
    public int period = 5;
    public int phase = 0;

    public void Tick() {
        if (phase % period == 0) {
            Run();
        } 
        phase += 1;
    }
}
