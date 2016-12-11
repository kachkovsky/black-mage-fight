using UnityEngine;
using System.Collections;
using System.Linq;

public class HeroBlink : Figure
{
    public IntVector2 backDirection;

    public void OnHeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (direction == backDirection) {
            Position.MoveHere(hero);
        }
        backDirection = -direction;
        transform.eulerAngles = Vector3.forward * backDirection.xy().Direction();
        Blink();
    }

    public override void Collide(Figure other) {
        if (other is Hero) {
            Blink();
        }
    }
}
