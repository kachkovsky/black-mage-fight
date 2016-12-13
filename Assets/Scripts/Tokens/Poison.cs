using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Poison : Token
{
    public static Poison instance;

    public Slider slider;

    public int damage = 1;
    public int timeout = 10;
    public int spent = 0;

    public Color cellColor;

    void Awake() {
        instance = this;
    }

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
        slider.maxValue = timeout;
        slider.value = spent;
        slider.onValueChanged.Invoke(0);
    }

    private void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        ++spent;
        slider.maxValue = timeout;
        slider.value = spent;
        if (spent > timeout) {
            hero.Hit(damage);
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
        spent = 0;
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved;
    }
}
