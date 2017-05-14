using System;
using UnityEngine;

public class CounterGreaterOrEqual : Condition
{
    public Counter counter;
    public int value;

    public override bool Satisfied() {
        return counter.value >= value;
    }
}

