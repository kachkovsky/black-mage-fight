using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeriodicCounter : MonoBehaviour
{
    public Slider slider;
    public Periodic periodic;

    void Start() {
        slider.maxValue = 0;
        periodic.tick.AddListener(UpdateSlider);
        UpdateSlider();
        slider.onValueChanged.Invoke(slider.value);
    }

    void UpdateSlider() {
        var phase = periodic.phase % periodic.period;
        var value = periodic.runPhases.ExtMin(x => x > phase ? x - phase : x + periodic.period - phase);
        slider.maxValue = Mathf.Max(value, slider.maxValue);
        slider.value = slider.maxValue-value;
    }
}
