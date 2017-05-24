using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ProfileSelectionPanel : MonoBehaviour
{   
    public List<UnityEngine.UI.Button> profileButtons;
    public List<UnityEngine.UI.Button> diffucultyButtons;

    public void Show() {
        for (int i = 0; i < GameManager.instance.gameState.profiles.Count; i++) {
            var profile = GameManager.instance.gameState.profiles[i];
            profileButtons[i].GetComponentInChildren<Text>().text = ProfileDescription(profile);
        }
        gameObject.SetActive(true);
    }

    string ProfileDescription(Profile profile) {
        if (profile.name == "") {
            return String.Format("<b><size=24>Пусто</size></b>");
        }
        return String.Format("<b><size=24>{0}</size></b>{1}", profile.name, profile.currentRuns.Count == 0 ? "" : "\n"+RunDescription(profile.currentRuns[0]));
    }

    string RunDescription(GameRun run) {
        string result = string.Format("{0}\nПройдено: {1}", diffucultyButtons[run.difficulty].GetComponentInChildren<Text>().text, run.levelsCompleted);
        if (run.continuousRun) {
            result += "\n" + run.triesLeft;
        }
        return result;
    }

    public void SelectProfile(int id) {
        GameManager.instance.gameState.currentProfileIndex = id;
        GameManager.instance.Save();
        GameManager.instance.UpdateState();
    }
}
