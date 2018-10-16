using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CameraFollow : MonoBehaviour {
    public void Update() {
        Camera.main.transform.position = this.transform.position.Change(z: Camera.main.transform.position.z);
    }
}
