using UnityEngine;
using System.Collections;

public class ArrowButton : Button {
    public IntVector2 direction;

    public override void Press() {
        Controls.instance.Move(direction);
        Cursor.visible = false;
    }
}
