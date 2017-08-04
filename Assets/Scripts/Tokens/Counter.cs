using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Counter : Token
{
    public int value = 0;
    public int maxValue = 0;

    public UnityEvent onMax;
    public UnityEvent onIncrement;
    public UnityEvent onZero;
    public UnityEvent onDecrement;

    public void Increment() {
        if (value == maxValue) {
            return;
        }
        value += 1;
        onIncrement.Invoke();
        if (value == maxValue) {
            onMax.Invoke();
        }
    }

    public void Decrement() {
        if (value == 0) {
            return;
        }
        value -= 1;
        onDecrement.Invoke();
        if (value == 0) {
            onZero.Invoke();
        }
    }

    public void Drop() {
        value = 0;
    }

    public void ShowCounter() {
        UI.instance.ShowMessage(string.Format("{0}/{1}", value, maxValue));
    }

    public void UpdateText(Text text) {
        var format = "{0}";
        text.text = string.Format(format, value);
    }
}
