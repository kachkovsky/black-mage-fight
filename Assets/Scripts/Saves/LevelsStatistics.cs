using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class LevelsStatistics
{
    public Map<string, LevelStatistics> levels = new Map<string, LevelStatistics>();
}
