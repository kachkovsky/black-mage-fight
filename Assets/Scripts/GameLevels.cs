using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameLevels : MonoBehaviour {
    public List<Difficulty> difficulties;

    public static GameLevels instance;

    void Awake() {
        instance = this;
    }
}
