using UnityEngine;
using System.Collections;

public abstract class Button : MonoBehaviour {
    public string button;

    public abstract void Press();

    void Update() {
        if (Input.GetButtonDown(button)) {
            Press();
        }
    }
}
