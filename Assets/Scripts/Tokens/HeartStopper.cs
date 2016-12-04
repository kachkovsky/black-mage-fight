using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartStopper : Token
{
    int damage = 1;

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero) {
        hero.Hit(damage);
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved; 
    }
}
