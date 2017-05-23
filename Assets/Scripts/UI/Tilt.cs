using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Tilt : MonoBehaviour
{
    public AudioSource audioSource;
    public RawImage image;

    public void Switch(int level = 1) {
        if (GameManager.instance.GameOver()) {
            level = 0;
        }
        image.enabled = (level > 0);
        image.color = image.color.Change(a: level * 0.2f);
        audioSource.mute = level == 0;
        audioSource.volume = 0.15f * level;
    }

    public void Update() {
        if (Hero.instance == null) {
            return;
        }
        if (Hero.instance.health > Hero.instance.maxHealth * 20 / 100) {
            Switch(0);
        } else if (Hero.instance.health > Hero.instance.maxHealth * 10 / 100) {
            Switch(1);
        } else if (Hero.instance.health > Hero.instance.maxHealth * 5 / 100) {
            Switch(2);
        } else if (Hero.instance.health > 1) {
            Switch(3);
        } else {       
            Switch(4);
        }
    }
}
