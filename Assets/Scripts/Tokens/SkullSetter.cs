using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class SkullSetter : Token
{
    public GameObject skullPrefab;

    public Periodic periodic;

    public void SetSkull() {
        var target = Board.instance.RandomEmptyCell();
        var skull = FindObjectsOfType<Skull>().FirstOrDefault(s => s.Position == target);
        if (skull != null) {
            skull.Increment();
        } else {
            skull = Instantiate(skullPrefab).GetComponent<Skull>();
            target.MoveHere(skull);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            SetSkull();
        }
    }
}
