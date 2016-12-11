using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnHeroMove : MonoBehaviour
{
    public MoveEvent run;
    public HeroEvent heroRun;

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        run.Invoke(hero, from, to, direction);
        heroRun.Invoke(hero);
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved;
    }
}
