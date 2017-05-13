using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GameState
{
    public List<Profile> profiles = new List<Profile>();
    public int currentProfileIndex = 0;

    public GameState() {
        for (int i = 0; i < 3; i++) {
            profiles.Add(new Profile());
        }
    }
}
