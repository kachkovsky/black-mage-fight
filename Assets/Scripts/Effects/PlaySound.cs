using UnityEngine;
using System.Collections;

public class PlaySound : Effect
{
    public AudioSource target;
    public float last = float.PositiveInfinity;

    public override void Run() {
        target.time = target.clip.length - last;
        target.Play();
    }
}
