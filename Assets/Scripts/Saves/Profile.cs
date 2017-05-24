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

    public string Description() {
        if (name == "") {
            return String.Format("<b><size=24>Пусто</size></b>");
        }
        return String.Format("<b><size=24>{0}</size></b>{1}", name, currentRuns.Count == 0 ? "" : "\n"+currentRuns[0].Description());
    }

}
