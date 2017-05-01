using UnityEngine;
using System.Collections;

public class PlaySound : Effect
{
    public AudioSource target;

    public override void Run() {
        target.Play();
    }
}
