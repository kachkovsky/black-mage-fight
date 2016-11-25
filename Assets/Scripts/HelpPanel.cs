using UnityEngine;
using System.Collections;

public class HelpPanel : MonoBehaviour
{
    void Update() {
        if (Input.GetButtonDown("Up") || Input.GetButtonDown("Down") || Input.GetButtonDown("Left") ||Input.GetButtonDown("Right")) {
            gameObject.SetActive(false);
        }
    }
}
