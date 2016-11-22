using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static UI instance;
    public Text criteria;

    void Awake() {
        instance = this;
    }
}
