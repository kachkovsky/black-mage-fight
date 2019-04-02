using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Profile
{
    public string name = "";
	public bool skipIntros = false;

    public List<GameRun> completedRuns = new List<GameRun>();
    public List<GameRun> currentRuns = new List<GameRun>();

    public string Description() {
        if (name == "") {
            return String.Format("<b><size=24>Пусто</size></b>");
        }
        return String.Format("<b><size=24>{0}</size></b>{1}", name, currentRuns.Count == 0 ? "" : "\n"+currentRuns[0].Description());
    }

    public bool Unlocked(Difficulty d) {
        var diff = d.Value();
        if (diff <= 2) {
            return true;
        }
        return completedRuns.Any(cr => cr.difficulty >= diff - 1);
    }

    public bool Visible(Difficulty d) {
        var diff = d.Value();
        if (diff <= 3) {
            return true;
        }
        var delta = 2;
        if (diff == 5) {
            delta = 1;
        }
        return completedRuns.Any(cr => cr.difficulty >= diff - delta);
    }
}
