using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControls : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup sounds;
    public AudioMixerGroup music;
    public Toggle soundsToggle;
    public Toggle musicToggle;
    public Slider soundsSlider;
    public Slider musicSlider;

    public AudioSource sampleSound;

    float soundsVolume;
    float musicVolume;

    bool inited;

    public void Init() {
        Debug.LogFormat("Init");
        mixer.GetFloat("SoundsVolume", out soundsVolume);
        mixer.GetFloat("MusicVolume", out musicVolume);
        soundsSlider.value = PlayerPrefs.GetFloat("SoundsVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("SCMusicVolume", 1f);

        soundsToggle.isOn = PlayerPrefs.GetInt("Sounds", 1) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("SCMusic", 1) == 1;

        inited = true;
        TimeManager.Wait(0).Then(() => {
            Refresh();
        });
    }

    public void Refresh() {
        if (!inited) {
            return;
        }
        mixer.SetFloat("SoundsVolume", soundsToggle.isOn ? soundsVolume + Mathf.Log(soundsSlider.value) * 10 : -80);
        mixer.SetFloat("MusicVolume", musicToggle.isOn ? musicVolume + Mathf.Log(musicSlider.value) * 10 : -80);
        PlayerPrefs.SetFloat("SoundsVolume", soundsSlider.value);
        PlayerPrefs.SetFloat("SCMusicVolume", musicSlider.value);
        PlayerPrefs.SetInt("Sounds", soundsToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SCMusic", musicToggle.isOn ? 1 : 0);
        sampleSound.Play();
    }
}
