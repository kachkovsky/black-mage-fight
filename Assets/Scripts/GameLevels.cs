using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameLevels : MonoBehaviour {
    public Difficulty difficulty1;
    public Difficulty difficulty2;
    public Difficulty difficulty3;
    public Difficulty difficulty4;
    public Difficulty difficulty5;
    public Difficulty difficulty6;

    public static GameLevels instance;

    void Awake() {
        instance = this;
    }
}
