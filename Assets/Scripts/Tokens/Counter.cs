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

    public void Start() {
        slider.maxValue = maxValue;
        slider.value = value;
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
}
