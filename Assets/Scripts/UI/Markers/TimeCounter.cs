using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public Counter counter;

    public void Awake() {
        instance = this;
        counter = GetComponent<Counter>();
    }
}
