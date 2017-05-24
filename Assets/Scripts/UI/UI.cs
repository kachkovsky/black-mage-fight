using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using RSG;

public class UI : MonoBehaviour {
    public static UI instance;
    public MenuPanel menu;
    public GameObject customLevel;
    public GameObject intro;
    public DifficultySelectionPanel difficultySelector;
    public ProfileSelectionPanel profileSelector;
    public Warning warning;
    public GameObject profileName;
    public AudioSource battleMusic;

    public HealthSlider heroHealthSlider;
    public HealthSlider blackMageHealthSlider;
    public HealthSlider gambochkaHealthSlider;

    public GameObject floatMessage;
    public Text floatMessageText;

    public GameObject loseMessage;
    public GameObject winMessage;


    public GameObject volumes;

    public List<Blur> blur;

    void Awake() {
        instance = this;
    }

    void Start() {
        CloseAll();
#if UNITY_EDITOR
        //intro.SetActive(true);
#else
        intro.SetActive(true);
#endif
    }

    public void ChooseProfile() {
        CloseAll();
        profileSelector.Show();
    }

    public void AskName() {
        CloseAll();
        profileName.SetActive(true);
    }

    public void AskDifficulty() {
        CloseAll();
        difficultySelector.gameObject.SetActive(true);
    }

    public void Escape() {
        if (intro.activeSelf) {
            intro.SetActive(false);
            return;
        }
        if (menu.gameObject.activeSelf) {
            CloseAll();
            return;
        } 
        Menu();
    }

    public void Menu() {
        CloseAll();
        menu.Show();
    }
    
    void Update() {
        battleMusic.mute = menu.gameObject.activeSelf || intro.activeSelf || customLevel.activeSelf || GameManager.instance.GameOver();
    }

    public void CloseAll() {
        floatMessage.SetActive(false);
        menu.gameObject.SetActive(false);
        customLevel.SetActive(false);
        difficultySelector.gameObject.SetActive(false);
        profileSelector.gameObject.SetActive(false);

        profileName.SetActive(false);
        warning.Hide();
        volumes.SetActive(false);
    }

    public void Volumes() {
        CloseAll();
        volumes.SetActive(true);
    }

    public void Win() {
        blur.ForEach(b => b.enabled = true);
        winMessage.SetActive(true);
    }

    public void Lose() {
        blur.ForEach(b => b.enabled = true);
        loseMessage.SetActive(true);
    }

    public void UpdateHUD() {
        blur.ForEach(b => b.enabled = false);
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
        heroHealthSlider.unit = Hero.instance;
        blackMageHealthSlider.unit = BlackMage.instance;
        gambochkaHealthSlider.unit = Gambochka.instance;
    }

    public IPromise Confirm(string text) {
        return warning.Show(text);
    }

    public void ShowMessage(string message) {
        StopAllCoroutines();
        StartCoroutine(ShowMessageForTime(message));
    }

    IEnumerator ShowMessageForTime(string message) {
        floatMessage.SetActive(true);
        floatMessageText.text = message;
        yield return new WaitForSeconds(1);
        floatMessage.SetActive(false);
    }
}
