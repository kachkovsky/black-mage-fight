using UnityEngine;
using System.Collections;

public class SetBomb : Effect
{
    public GameObject bombPrefab;

    public override void Run() {
        var bomb = Instantiate(bombPrefab).GetComponent<Bomb>();
        bomb.Blink();
    }
}
