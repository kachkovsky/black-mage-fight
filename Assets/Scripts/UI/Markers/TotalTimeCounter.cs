using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalTimeCounter : MonoBehaviour
{
    public static TotalTimeCounter instance;
    public Counter counter;

    public void Awake() {
        instance = this;
        counter = GetComponent<Counter>();
    }
}
