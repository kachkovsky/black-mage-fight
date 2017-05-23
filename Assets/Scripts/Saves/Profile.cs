using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Profile
{
    public string name = "";

    public List<GameRun> completedRuns = new List<GameRun>();
    public List<GameRun> currentRuns = new List<GameRun>();
}
