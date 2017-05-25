using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatueSetter : MonoBehaviour
{
    public static StatueSetter instance;

    public void Awake() {
        instance = this;
    }
}
