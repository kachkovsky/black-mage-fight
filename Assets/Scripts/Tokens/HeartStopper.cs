using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartStopper : Token
{
    public int damage = 1;

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        hero.Hit(damage);
    }

    void OnDestroy() {
		if (GameManager.instance) {
			GameManager.instance.onHeroMove -= HeroMoved;
		}
    }
}
