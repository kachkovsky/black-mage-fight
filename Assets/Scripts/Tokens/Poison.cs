using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Poison : Token
{
    public static Poison instance;
    public static Poison secondInstance;

    public int damage = 1;
    public int timeout = 10;
    public int spent = 0;

    public bool dropOnDamage = true;

    public bool second = false;

    public Color cellColor;

    void Awake() {
        if (second) {
            secondInstance = this;
        } else {
            instance = this;
        }
    }

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        ++spent;
        if (spent >= timeout) {
            hero.Hit(damage);
            if (dropOnDamage) {
                spent = 0;
            }
        }
        //FindObjectsOfType<Cell>().ForEach(cell => {
        //    var extraDistance = Mathf.Abs(cell.x-hero.Position.x)+Mathf.Abs(cell.y - hero.Position.y) - (timeout-spent) - 1;
        //    if (extraDistance > 0) {
        //        cell.RestoreColor();
        //    } else {
        //        cell.ChangeColor(cellColor);
        //    }
        //});
    }

    public void Suppress() {
        spent = -1;
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved;
    }
}
