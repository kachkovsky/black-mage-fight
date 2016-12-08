using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Skull : Figure
{
    public int damage;
    public TextMesh text;

    void Start() {
        UpdateText();
    }

    public void Increment() {
        damage += 1; 
        UpdateText();
    }

    void UpdateText() {
        text.text = damage.ToString();
    }

    public override bool Occupies() {
        return false;
    }

    public override void Collide(Figure f) {
        var hero = f as Hero;
        if (hero != null) {
            hero.Hit(damage);
        }
        if (f != this) {
            Destroy(gameObject);
        }
    }
}
