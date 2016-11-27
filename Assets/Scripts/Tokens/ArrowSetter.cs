using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowSetter : Token
{
    public GameObject arrowPrefab;

    public Periodic periodic;

    public void SetArrow() {
        var arrow = Instantiate(arrowPrefab).GetComponent<Arrow>();
        arrow.Blink();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            SetArrow();
        }
    }
}
