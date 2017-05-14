using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Statue : MonoBehaviour
{
    public Condition activated;
    new SpriteRenderer renderer;

    bool Activated() {
        return activated.Satisfied();
    }

    float ActivatedScale() {
        return 1.5f + 0.3f * Mathf.Sin(TimeManager.Time() * 15);
    }

    float ActivatedRotation() {
        return 10 + 5 * Mathf.Sin(TimeManager.Time()*11);
    }

    Color ActivatedColor() {
        return Color.Lerp(Color.red, Color.black, 0.5f + 0.5f * Mathf.Sin(TimeManager.Time()*7));
    }

    float DefaultScale() {
        return 1;
    }

    float DefaultRotation() {
        return 0;
    }

    Color DefaultColor() {
        return Color.black;
    }

    float Scale() {
        return Activated() ? ActivatedScale() : DefaultScale();
    }

    float Rotation() {
        return Activated() ? ActivatedRotation() : DefaultRotation();
    }

    Color CurrentColor() {
        return Activated() ? ActivatedColor() : DefaultColor();
    }

    public void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
        transform.localScale = Vector3.one * 0.2f;
    }

    public void Update() {
        if (GameManager.instance.GameOver()) {
            return;
        }
        float part = (1-Mathf.Pow(0.5f, Time.deltaTime / 0.05f));
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * Scale(), part);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.forward * Rotation(), part);
        renderer.color = Color.Lerp(renderer.color, CurrentColor(), part);
    }
}
