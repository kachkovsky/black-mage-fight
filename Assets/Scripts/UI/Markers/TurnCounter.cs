using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour
{
    public static TurnCounter instance;
    public Counter counter;

    public void Awake() {
        instance = this;
        counter = GetComponent<Counter>();
    }
}
