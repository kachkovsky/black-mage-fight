using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Tilt : MonoBehaviour
{
    public AudioSource audioSource;
    public RawImage image;

    public void Switch(bool on = true) {
        image.enabled = on;
        audioSource.mute = !on;
    }
}
