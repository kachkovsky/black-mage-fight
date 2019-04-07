using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChangePeriod : MonoBehaviour {
    public Periodic periodic;
    public int deltaPeriod;

    public void Start() {
        periodic.Period += deltaPeriod;
    }
}
