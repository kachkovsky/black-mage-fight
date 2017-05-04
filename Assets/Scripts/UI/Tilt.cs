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
        image.enabled = (level > 0);
        image.color = image.color.Change(a: level * 0.2f);
        audioSource.mute = level == 0;
        audioSource.volume = 0.15f * level;
    }
}
