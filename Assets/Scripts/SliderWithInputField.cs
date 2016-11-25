using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderWithInputField : MonoBehaviour
{
    public Slider slider;
    public InputField inputField;

    void OnEnable() {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        OnSliderValueChanged(slider.value);
    }

    void OnDisable() {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
    }

    void OnSliderValueChanged(float value) {
        inputField.text = slider.value.ToString();
    }

    void OnInputFieldValueChanged(string value) {
        try {
            slider.value = int.Parse(inputField.text);
            OnSliderValueChanged(slider.value);
        } catch (System.FormatException) {
        }
    }

    void OnInputFieldEndEdit(string value) {
        try {
            slider.value = int.Parse(inputField.text);
        } catch (System.FormatException) {
            slider.value = slider.minValue;
        }
        OnSliderValueChanged(slider.value);
    }
}
