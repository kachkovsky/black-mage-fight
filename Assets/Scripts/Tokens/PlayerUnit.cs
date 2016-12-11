using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit
{
    public AudioSource moveSound;

    SpriteRenderer spriteRenderer;

    protected virtual void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override bool MoveTo(IntVector2 direction) {
        var oldPosition = Position;
        if (!base.MoveTo(direction)) {
            return false;
        }
        moveSound.Play();
        CheckCollisions(oldPosition);
        GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
        return true;
    }

    public void CheckCollisions(Cell oldPosition) {
        foreach (Edge e in FindObjectsOfType<Edge>()) {
            if (e.position.a == oldPosition && e.position.b == Position) {
                e.Pick(this);
            }
            if (e.position.a == Position && e.position.b == oldPosition) {
                e.ReversePick(this);
            }
        }
    }

    public bool IsActiveUnit() {
        return Controls.instance.activeUnit == this;
    }

    void Update() {
        spriteRenderer.material.ChangeAlpha(IsActiveUnit() ? 1 : 0.3f);
    }
}
