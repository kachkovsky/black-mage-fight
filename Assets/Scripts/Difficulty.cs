﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Difficulty : MonoBehaviour {
    public string difficultyName;
    public string description;

    public int Value() {
        return GameLevels.instance.difficulties.IndexOf(this);
    }
}
