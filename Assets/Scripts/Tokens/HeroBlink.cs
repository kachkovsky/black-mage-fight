using UnityEngine;
using System.Collections;
using System.Linq;

public class HeroBlink : Figure
{
    public IntVector2 lastDirection;

    public void OnHeroMoved(Unit hero, Cell from, Cell to, IntVector2 direction) {
        if (direction == -lastDirection) {
            Position.MoveHere(hero);
            Blink();
        }
    }
}
