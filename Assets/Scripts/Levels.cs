using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Levels : MonoBehaviour {
    public static Levels instance;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;

    public GameObject startLevel;

    void Awake() {
        instance = this;
    }

    void Start() {
        transform.Children().ForEach(c => c.gameObject.SetActive(false));
    }
}
