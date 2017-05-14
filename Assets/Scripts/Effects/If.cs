using UnityEngine;
using System.Collections;

public class If : Effect
{
    public Condition condition;
    public Effect then;

    public override void Run() {
        if (condition.Satisfied()) {
            then.Run();
        }
    }
}
