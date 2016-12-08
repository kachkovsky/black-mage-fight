using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BasicGameStartConfig : GameStartConfig {
    public int blackMageHealth;
    public int heroHealth;
    public int teleports;
    public int heartCount; 
    public int heartHeal; 
    public Action extraCreations;

    public BasicGameStartConfig(int blackMageHealth, int heroHealth, int teleports, int heartCount, int heartHeal, Action extraCreations) {
        this.blackMageHealth = blackMageHealth;
        this.heroHealth = heroHealth;
        this.teleports = teleports;
        this.heartCount = heartCount;
        this.heartHeal = heartHeal;
        this.extraCreations = extraCreations;
    }
}
