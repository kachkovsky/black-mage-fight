using System;
using UnityEngine;

public class ExistByCondition : MonoBehaviour
{
    public Condition condition;

    public void Awake() {
        if (!condition.Satisfied()) {
            Destroy(gameObject);
        }
    }
}

