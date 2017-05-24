using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{   
    public void SwitchProfile() {
        GameManager.instance.gameState.currentProfileIndex = -1;
        GameManager.instance.Save();
        GameManager.instance.UpdateState();
    }

    public void InterruptRun() {
        UI.instance.Confirm("Прохождение потеряется полностью. Продолжить?").Then(() => {
            GameManager.instance.gameState.CurrentProfile.currentRuns.Clear();
            GameManager.instance.Save();
            GameManager.instance.UpdateState();
        });
    }

    public void Exit() {
        UI.instance.Confirm("Прощаемся?").Then(() => {
            Debug.LogFormat("Quit");
            Application.Quit();    
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        });
    }
}
