using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderWithValue : MonoBehaviour
{
    public Slider slider;
    public Text valueText;
    public string formatString;

    void OnEnable() {
        slider.onValueChanged.AddListener(ChangeValue);
        ChangeValue(slider.value);
    }

    void OnDisable() {
        slider.onValueChanged.RemoveListener(ChangeValue);
    }

    void ChangeValue(float value) {
        valueText.text = string.Format(formatString, slider.value, slider.maxValue - slider.value, slider.maxValue);
    }
}
