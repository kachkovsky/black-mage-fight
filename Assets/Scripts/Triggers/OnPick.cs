using UnityEngine;
using System.Collections;

public class OnPick : Trigger
{
    public Figure figure;

	public void Awake() {
		if (figure == null) {
			figure = GetComponent<Figure>();
		}
	}

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
