using UnityEngine;
using System.Collections;

public class EscapeHint : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }
}
