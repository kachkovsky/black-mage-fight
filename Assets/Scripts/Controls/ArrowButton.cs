using UnityEngine;
using System.Collections;

public class ArrowButton : Button {
    public IntVector2 direction;

    public override void Press() {
        if (Hero.instance.Dead() || BlackMage.instance.Dead()) {
            return;
        }
        if (Controls.instance.activeUnit == null) {
            return;
        }
        Controls.instance.activeUnit.MoveTo(direction);
    }
}
