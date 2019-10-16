using UnityEngine;
using System.Collections;
using System;

public class ArrowButton : Button {

    private const int DELAY_BETWEEN_PRESS_ACTIONS = 220;
    private static bool arrowPressed;

    public IntVector2 direction;
    private static int oldTick;
    private bool pressed;

    private void ResetOldTick() {
        oldTick = Int32.MaxValue;
    }

    private bool IsNewTick(int tick) {
        if (tick < oldTick) {
            if (tick > (Int32.MinValue + DELAY_BETWEEN_PRESS_ACTIONS)) {
                oldTick = tick;
                return true;
            }
            return false;
        }
        if (tick - oldTick > DELAY_BETWEEN_PRESS_ACTIONS) {
            oldTick = tick;
            return true;
        }
        return false;
    }

    private void Awake() {
        arrowPressed = false;
        pressed = false;
        ResetOldTick();
    }

    void Update() {
        if (Input.GetButtonUp(button)) {
            ResetOldTick();
            if (pressed) {
                arrowPressed = false;
            }
            pressed = false;
        } else if (!arrowPressed || (arrowPressed && pressed)) {
            if (Input.GetButton(button) || Input.GetButtonDown(button)) {
                //Debug.Log("frame_count " + Time.frameCount);
                if (pressed) {
                    CheckForPress();
                } else {
                    pressed = true;
                    arrowPressed = true;
                    oldTick = Environment.TickCount;
                    Press();
                }
            }
        }
    }

    private void CheckForPress() {
        if (IsNewTick(Environment.TickCount)) {
            Press();
        }
    }

    public override void Press() {
        Controls.instance.Move(direction);
        Cursor.visible = false;
    }
}
