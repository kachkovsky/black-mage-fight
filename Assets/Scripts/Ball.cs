using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
    public List<Color> colors;

    public MeshRenderer meshRenderer;

    public void Init() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Awake() {
        Init();
    }
}
