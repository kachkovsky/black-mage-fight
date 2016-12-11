using UnityEngine;
using System.Collections;

public class OnPick : Trigger
{
    public Figure figure;

    void Start() {
        figure.onCollide.AddListener(OnCollide);
    }

    void OnCollide(Figure f) {
        if (f is Hero) {
            Run();
        }
    }

    void OnDestroy() {
        figure.onCollide.RemoveListener(OnCollide);
    }
}
