using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bomb : Figure
{
    public Text text;

    public int timer = 10;

    public int damage = 1;

	public IntValueProvider damageProvider;

	public int Damage {
		get {
			return damageProvider ? damageProvider.Value : damage;
		}
	}

	public IntValueProvider timerProvider;

	public int Timer {
		get {
			return timer;
		}
		set {
			timer = value;
		}
	}

	public bool randomized = false;

	public List<int> steps = new List<int>() { 1 };

    void Start() {
		if (timerProvider) {
			timer = timerProvider.Value;
		}
        UpdateText();
    }

    void UpdateText() {
        text.text = Timer.ToString();
    }

    public void Tick() {
        if (gameObject.activeSelf == false) {
            return;
        }
		Timer -= steps.Rnd();
        UpdateText();
        if (Timer <= 0) {
            Hero.instance.Hit(Damage);
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
