using UnityEngine;
using System.Collections;

public class BlackMage : Unit
{
    public static BlackMage instance;
    public AudioSource hitSound;

    void Awake() {
        instance = this;
    }

    public void Hit() {
        health -= 1;
        hitSound.time = 0.25f;
        hitSound.Play();
    }
}
