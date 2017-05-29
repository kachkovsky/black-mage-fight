using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using RSG;

public class Trail : MonoBehaviour
{
    public new SpriteRenderer renderer;

    public void ChangeAlpha(float newAlpha) {
        var color = renderer.color;
        color.a = newAlpha;
        renderer.color = color;
    }
}
