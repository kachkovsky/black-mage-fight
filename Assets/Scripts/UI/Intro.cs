using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Intro : MonoBehaviour
{
    public List<GameObject> frames;

    public void Update() {
        if (Input.anyKeyDown) {
            var currentFrame = frames.FirstOrDefault(f => f.activeSelf);
            if (currentFrame == null) {
                return;
            }
            if (currentFrame == frames.Last()) {
                gameObject.SetActive(false);
            }
            currentFrame.SetActive(false);
            currentFrame = frames.cyclicNext(currentFrame);
            currentFrame.SetActive(true);
        }
    }
}
