using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombSetter : MonoBehaviour
{
    public static BombSetter instance;

    public void Awake() {
        instance = this;
    }
}
