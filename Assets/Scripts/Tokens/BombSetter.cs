using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombSetter : Token
{
    public GameObject bombPrefab;

    public Periodic periodic;

    public int timer = 13;

    public void SetBomb() {
        var bomb = Instantiate(bombPrefab).GetComponent<Bomb>();
        bomb.timer = timer;
        bomb.Blink();
    }
}
