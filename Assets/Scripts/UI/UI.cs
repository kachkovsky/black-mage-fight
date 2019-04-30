using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using RSG;

public class UI : Singletone<UI> {
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
    public GameObject heartstopperPeriod;
    public GameObject statuesCounter;
    public GameObject poisonCounter;
    public GameObject secondPoisonCounter;
    public GameObject bombCreationCounter;
	public GameObject bombTimerIcons;
	public GameObject bombOneTimerIcon;	
	public GameObject bombRandomTimerIcon;
	public GameObject fireCreationCounter;
	public GameObject doorCreationCounter;
    public GameObject monsterCreationCounter;
    public GameObject evilEyesCreationCounter;
    public GameObject evilEyesDamage;
    public GameObject statuesCreationCounter;
	public GameObject statuesDamage;
    public GameObject fireExtinguisherCounter;
    public GameObject ankhCounter;
	public GameObject timeCounter;
    public GameObject turnCounter;
    public GameObject totalTimeCounter;
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

	public bool RandomBombs() {
		var bomb = BombSetter.instance.GetComponent<Spawner>().sample.GetComponent<Bomb>();
		if (bomb && bomb.steps.Count > 1) {
			return true;
		}
		return false;
	}

    public void Update() {
        statuesCounter.SetActive(StatuesCounter.instance);
        statuesCreationCounter.SetActive(StatueSetter.instance);
		statuesDamage.SetActive(StatueSetter.instance);

        poisonCounter.SetActive(Poison.instance);
		heartstopperPeriod.SetActive(HeartStopperPeriodic.instance);
        secondPoisonCounter.SetActive(Poison.secondInstance);

        bombCreationCounter.SetActive(BombSetter.instance);
		bombTimerIcons.SetActive(BombSetter.instance);
		if (BombSetter.instance) {
			bombRandomTimerIcon.SetActive(RandomBombs());
			bombOneTimerIcon.SetActive(!RandomBombs());
		}

		doorCreationCounter.SetActive(DoorSpawner.instance);
		fireCreationCounter.SetActive(FireSpawner.instance);
        fireExtinguisherCounter.SetActive(FireExtinguisherCounter.instance);

        evilEyesCreationCounter.SetActive(EvilEyesSetter.instance);
		evilEyesDamage.SetActive(EvilEyesSetter.instance);

		monsterCreationCounter.SetActive(MonsterSetter.instance);
        timeCounter.SetActive(TimeCounter.instance);
        ankhCounter.SetActive(GameManager.instance.gameState.CurrentRun != null && GameManager.instance.gameState.CurrentRun.continuousRun);


		keysUI.SetActive(KeyCounter.instance);

        battleMusic.mute = menu.gameObject.activeSelf || Intermission.active || customLevel.activeSelf || GameManager.instance.GameOver() || GameManager.instance.gameState.CurrentRun == null;

		if (DoorSpawner.instance) {
			if (DoorSpawner.instance.periodicCounter.Multiple()) {
				doorCreationCounter.GetComponentInChildren<Text>().text = string.Format(
					"<b>{0}</b>",
					DoorSpawner.instance.periodicCounter.Value()
				);
			} else {
				doorCreationCounter.GetComponentInChildren<Text>().text = string.Format(
					"<b>{0}/{1}</b>",
					DoorSpawner.instance.periodicCounter.Value(),
					DoorSpawner.instance.periodicCounter.MaxValue()
				);
			}
		}
		if (FireSpawner.instance) {
			fireCreationCounter.GetComponentInChildren<Text>().text = string.Format(
				"<b>{0}/{1}</b>",
				FireSpawner.instance.periodicCounter.Value(),
				FireSpawner.instance.periodicCounter.MaxValue()
			);
		}
		if (MonsterSetter.instance) {
			monsterCreationCounter.GetComponentInChildren<Text>().text = string.Format(
				"<b>{0}/{1}</b>",
				MonsterSetter.instance.periodicCounter.Value(),
				MonsterSetter.instance.periodicCounter.MaxValue()
			);
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
		if (HeartStopperPeriodic.instance) {
			var text = heartstopperPeriod.GetComponentInChildren<Text>();
			var counter = HeartStopperPeriodic.instance.periodicCounter;
			text.text = string.Format("<b>{0}/{1}</b>", counter.Value(), counter.MaxValue());
        }
        if (StatueSetter.instance) {
            var text = statuesCreationCounter.GetComponentInChildren<Text>();
            var counter = StatueSetter.instance.GetComponent<PeriodicCounter>();
            text.text = string.Format("<b>{0}/{1}</b>", counter.Value(), counter.MaxValue());
			var damageText = statuesDamage.GetComponentInChildren<Text>();
			damageText.text = string.Format("<b>{0}</b>", StatueSetter.instance.GetComponent<DamageEffect>().Damage);
        }
		if (EvilEyesSetter.instance) {
			var text = evilEyesCreationCounter.GetComponentInChildren<Text>();
			var counter = EvilEyesSetter.instance.periodicCounter;
			text.text = string.Format("<b>{0}/{1}</b>", counter.Value(), counter.MaxValue());
			var damageText = evilEyesDamage.GetComponentInChildren<Text>();
			damageText.text = string.Format("<b>{0}</b>", EvilEyesSetter.instance.GetComponent<Spawner>().sample.GetComponent<EvilEye>().Damage);
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
		if (TurnCounter.instance) {
			turnCounter.GetComponentInChildren<Text>().text = "<b>{0}</b>".i(TurnCounter.instance.counter.value);
		}
		if (TotalTimeCounter.instance) {
			totalTimeCounter.GetComponentInChildren<Text>().text = "<b>{0}</b>".i(TotalTimeCounter.instance.counter.value);
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
