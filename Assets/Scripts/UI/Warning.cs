using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using RSG;
using System;

public class Warning : MonoBehaviour
{   
    public Text text;

    Promise p;

    public IPromise Show(string text) {
        this.text.text = text;
        gameObject.SetActive(true);
        p = new Promise();
        return p;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OK() {
        Hide();
        p.Resolve();
        p = null;
    }

    public void Cancel() {
        Hide();
        p.Reject(new Exception("User rejected action"));
        p = null;
    }
}   

