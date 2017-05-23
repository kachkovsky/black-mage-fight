using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GameRun
{
    public bool continuousRun = false;
    public bool panicMode = false;
    public bool randomized = false;
    public bool buildMode = false;

    public int difficulty = -1;
    public int triesLeft = 5;
    public int levelsCompleted = 0;
}
