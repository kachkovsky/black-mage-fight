using UnityEngine;
using System.Collections;

public class OnHeroMove : Trigger
{
    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero) {
        Run();
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved;
    }
}
