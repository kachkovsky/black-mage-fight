using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Counter : Token
{
    public Slider slider;

    public int value = 0;
    public int maxValue = 0;

    public UnityEvent onMax;
    public UnityEvent onIncrement;

    public void Start() {
        UpdateSlider();
        slider.onValueChanged.Invoke(0);
    }

    public void UpdateSlider() {
        slider.maxValue = maxValue;
        slider.value = value;
    }

    public void Increment() {
        if (value == maxValue) {
            return;
        }
        value += 1;
        onIncrement.Invoke();
        if (value == maxValue) {
            onMax.Invoke();
        }
        UpdateSlider();
    }

    public void Decrement() {
        if (value == 0) {
            return;
        }
        value -= 1;
        UpdateSlider();
    }

    public void Drop() {
        value = 0;
        UpdateSlider();
    }

    public void ShowCounter() {
        UI.instance.ShowMessage(string.Format("{0}/{1}", value, maxValue));
    }
}
