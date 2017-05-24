using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Level : MonoBehaviour {
    [ContextMenu("Run")] 
    void Run() {
        GameManager.instance.RunLevel(gameObject);
    }
}
