using UnityEngine;
using System.Collections;

public class Blink : Effect
{
    public Figure target;

    public override void Run() {
        target.Blink();
    }
}
