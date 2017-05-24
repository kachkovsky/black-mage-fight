using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class DifficultyButton : MonoBehaviour
{   
    public Difficulty difficulty;

    public Color basic;
    public Color locked;

    public Text completedText;

    void Start() {
        GetComponentInChildren<Text>().text = String.Format("<size=26>{0}</size>\n{1}", difficulty.difficultyName, difficulty.description);
    }
}
