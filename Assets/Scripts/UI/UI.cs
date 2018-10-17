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
    public DifficultySelectionPanel difficultySelector;
    public ProfileSelectionPanel profileSelector;
    public Warning warning;
    public Warning soloWarning;
    public GameObject profileName;
    public AudioSource battleMusic;

	[Space]
    public GameObject heroHealth;
    public GameObject blackMageHealth;
    public GameObject statuesCounter;
    public GameObject poisonCounter;
    public GameObject secondPoisonCounter;
    public GameObject bombCreationCounter;
    public GameObject evilEyesCreationCounter;
    public GameObject statuesCreationCounter;
    public GameObject fireExtinguisherCounter;
    public GameObject ankhCounter;
    public GameObject timeCounter;
	public GameObject keysUI;
	public List<KeyImage> keyImages;

	[Space]
    public GameObject floatMessage;
    public Text floatMessageText;

    public GameObject loseMessage;
    public GameObject winMessage;


    public SoundControls volumes;

    public List<Blur> blur;

    void Awake() {
        instance = this;
        volumes.Init();
    }

    void Start() {
        CloseAll();

		keysUI.SetActive(false);
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
        difficultySelector.Show();
    }

    public void Escape() {
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

    public void CloseAll() {
        floatMessage.SetActive(false);
        menu.gameObject.SetActive(false);
        customLevel.SetActive(false);
        difficultySelector.gameObject.SetActive(false);
        profileSelector.gameObject.SetActive(false);

        profileName.SetActive(false);
        warning.Hide();
        soloWarning.Hide();
        volumes.gameObject.SetActive(false);
    }

    public void Volumes() {
        CloseAll();
        volumes.gameObject.SetActive(true);
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
    }

    public void Update() {
        statuesCounter.SetActive(StatuesCounter.instance);
        poisonCounter.SetActive(Poison.instance);
        secondPoisonCounter.SetActive(Poison.secondInstance);
        bombCreationCounter.SetActive(BombSetter.instance);
        statuesCreationCounter.SetActive(StatueSetter.instance);
        fireExtinguisherCounter.SetActive(FireExtinguisherCounter.instance);
        evilEyesCreationCounter.SetActive(EvilEyesSetter.instance);
        timeCounter.SetActive(TimeCounter.instance);
        ankhCounter.SetActive(GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.continuousRun);


		keysUI.SetActive(KeyCounter.instance);

        //battleMusic.mute = menu.gameObject.activeSelf || Intermission.active || customLevel.activeSelf || GameManager.instance.GameOver() || GameManager.instance.gameState.CurrentRun == null;

        if (Hero.instance != null) {
            heroHealth.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", Hero.instance.health, Hero.instance.maxHealth);
        }
        if (BlackMage.instance != null) {
            blackMageHealth.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", BlackMage.instance.health, BlackMage.instance.maxHealth);
        }
        if (BombSetter.instance) {
            bombCreationCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", BombSetter.instance.GetComponent<PeriodicCounter>().Value(), BombSetter.instance.GetComponent<PeriodicCounter>().MaxValue());
        }        
        if (Poison.instance) {
            poisonCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", Poison.instance.timeout-Poison.instance.spent, Poison.instance.timeout);
        }     
        if (Poison.secondInstance) {
            secondPoisonCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", Poison.secondInstance.timeout-Poison.secondInstance.spent, Poison.secondInstance.timeout);
        }
        if (StatueSetter.instance) {
            var text = statuesCreationCounter.GetComponentInChildren<Text>();
            var counter = StatueSetter.instance.GetComponent<PeriodicCounter>();
            text.text = string.Format("<b>{0}/{1}</b>", counter.Value(), counter.MaxValue());
        }    
        if (EvilEyesSetter.instance) {
            var text = evilEyesCreationCounter.GetComponentInChildren<Text>();
            var counter = EvilEyesSetter.instance.periodicCounter;
            text.text = string.Format("<b>{0}/{1}</b>", counter.Value(), counter.MaxValue());
        }   
        if (StatuesCounter.instance) {
            statuesCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", StatuesCounter.instance.counter.value, StatuesCounter.instance.max);
        }      
        if (FireExtinguisherCounter.instance) {
            fireExtinguisherCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", FireExtinguisherCounter.instance.counter.value, FireExtinguisherCounter.instance.counter.maxValue);
        }      
        if (GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.continuousRun) {
            ankhCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}</b>", GameManager.instance.gameState.CurrentRun.triesLeft);
        }     
        if (TimeCounter.instance) {
            timeCounter.GetComponentInChildren<Text>().text = string.Format("<b>{0}/{1}</b>", TimeCounter.instance.counter.maxValue-TimeCounter.instance.counter.value, TimeCounter.instance.counter.maxValue);
        }  
    }

    public IPromise Confirm(string text) {
        return warning.Show(text);
    }

    public IPromise SoloConfirm(string text) {
        return soloWarning.Show(text);
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
