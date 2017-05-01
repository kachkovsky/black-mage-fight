using UnityEngine;
using System.Collections;

public class Heal : Effect
{
    public Unit target;
    public int amount = 1;

    public override void Run() {
        target.Heal(amount);
    }
}
