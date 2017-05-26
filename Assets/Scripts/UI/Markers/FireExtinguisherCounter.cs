using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireExtinguisherCounter : MonoBehaviour
{
    public static FireExtinguisherCounter instance;

    public Counter counter;

    public void Awake() {
        instance = this;
        counter = GetComponent<Counter>();
    }
}
