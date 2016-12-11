using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnHeroMove : MonoBehaviour
{
    public MoveEvent run;

    void Start() {
        GameManager.instance.onHeroMove += HeroMoved;
    }

    private void HeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        run.Invoke(hero, from, to, direction);
    }

    void OnDestroy() {
        GameManager.instance.onHeroMove -= HeroMoved;
    }
}
