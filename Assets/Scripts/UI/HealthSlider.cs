using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthSlider : MonoBehaviour
{
    public Unit unit;
    public Slider slider;

    void Update() {
        if (unit != null) {
            slider.value = unit.health;
            slider.maxValue = unit.maxHealth;
        } else {
            slider.value = 0;
        }
    }
}
