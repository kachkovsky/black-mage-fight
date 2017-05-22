using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Levels : MonoBehaviour {
    public static Levels instance;

    public GameObject startLevel;

    void Awake() {
        instance = this;
        transform.Children().ForEach(c => c.gameObject.SetActive(false));
    }
}
