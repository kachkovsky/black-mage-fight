using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bomb : Figure
{
    public Text text;

    public int timer = 10;

    public int damage = 1;

    void Start() {
        UpdateText();
    }

    void UpdateText() {
        text.text = timer.ToString();
    }

    public void Tick() {
        if (gameObject.activeSelf == false) {
            return;
        }
        timer -= 1;
        UpdateText();
        if (timer == 0) {
            Hero.instance.Hit(damage);
            Destroy(gameObject);
        }
    }

    public override void Collide(Figure f) {
        if (f is Hero) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
