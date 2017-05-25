using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatuesCounter : MonoBehaviour
{
    public static StatuesCounter instance;
    public Counter counter;
    public int max;

    public void Awake() {
        instance = this;
        counter = GetComponent<Counter>();
        max = GetComponent<CounterGreaterOrEqual>().value;
    }
}
