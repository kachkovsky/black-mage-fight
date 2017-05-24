using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Intro : MonoBehaviour
{
    public static bool active = false;

    public List<GameObject> frames;

    public void Show() {
        gameObject.SetActive(true);
        active = true;
        frames.ForEach(f => f.gameObject.SetActive(false));
        frames.First().gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
        active = false;
    }

    void NextFrame() {  
        var currentFrame = frames.FirstOrDefault(f => f.activeSelf);
        if (currentFrame == null) {
            return;
        }
        if (currentFrame == frames.Last()) {
            Hide();
        }
        currentFrame.SetActive(false);
        currentFrame = frames.cyclicNext(currentFrame);
        currentFrame.SetActive(true);
    }

    public void Update() {
        if (Input.anyKeyDown) {
            NextFrame();
        }
    }
}
