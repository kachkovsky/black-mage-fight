using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimedBomb : MonoBehaviour
{
    public UnityEvent blow;

    public float blowChance = 0.01f;

    public void Tick() {
        if (gameObject.activeSelf == false) {
            return;
        }

        if (Random.Range(0f, 1f) < blowChance) {
            blow.Invoke();
        }
    }
}
