using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GameState
{
    public int[,] state;

    public List<int[,]> history = new List<int[,]>();
}
