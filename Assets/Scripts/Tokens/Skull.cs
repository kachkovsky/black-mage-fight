using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Skull : Figure
{
    public int damage;
	public int step;
    public TextMesh text;

    void Start() {
        UpdateText();
		UpdateScale();
    }

    public void Increment() {
        damage += step; 
		step += 1;
        UpdateText();
		UpdateScale();
    }

    void UpdateText() {
        text.text = damage.ToString();
    }

	void UpdateScale() {
		var scale = 0.3f + 0.1f * Mathf.Clamp(step, 0, 8);
		transform.localScale = new Vector3(scale, scale, 1);
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
