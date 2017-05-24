using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ProfileSelectionPanel : MonoBehaviour
{   
    public List<UnityEngine.UI.Button> profileButtons;

    public void Show() {
        for (int i = 0; i < GameManager.instance.gameState.profiles.Count; i++) {
            var profile = GameManager.instance.gameState.profiles[i];
            profileButtons[i].GetComponentInChildren<Text>().text = profile.Description();
        }
        gameObject.SetActive(true);
    }


    public void SelectProfile(int id) {
        GameManager.instance.gameState.currentProfileIndex = id;
        GameManager.instance.Save();
        GameManager.instance.UpdateState();
    }
}
