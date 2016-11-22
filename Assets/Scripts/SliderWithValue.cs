using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderWithValue : MonoBehaviour
{
    public Slider slider;
    public Text valueText;
    public string formatString;

    void Update() {
        valueText.text = string.Format(formatString, slider.value);
    }
}
