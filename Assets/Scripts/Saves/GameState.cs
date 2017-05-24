using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GameState
{
    public List<Profile> profiles = new List<Profile>();
    public int currentProfileIndex = 0;

    public Profile CurrentProfile {
        get {
            if (currentProfileIndex < 0 || currentProfileIndex >= profiles.Count) {
                return null;
            }
            return profiles[currentProfileIndex];
        }
    }

    public GameRun CurrentRun {
        get {
            if (CurrentProfile == null) {
                return null;
            }
            return CurrentProfile.currentRuns.FirstOrDefault();
        }
    }

    public GameState() {
        for (int i = 0; i < 3; i++) {
            profiles.Add(new Profile());
        }
    }
}
