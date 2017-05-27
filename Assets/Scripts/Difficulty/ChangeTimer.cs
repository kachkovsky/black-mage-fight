using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChangeTimer : MonoBehaviour {
    public Bomb bomb;
    public int deltaTimer;

    public void Start() {
        bomb.timer += deltaTimer;
    }
}
