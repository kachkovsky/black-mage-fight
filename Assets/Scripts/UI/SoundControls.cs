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

    float soundsVolume;
    float musicVolume;

    void Awake() {
        mixer.GetFloat("SoundsVolume", out soundsVolume);
        mixer.GetFloat("MusicVolume", out musicVolume);
    }

    public void MuteSounds(bool mute) {
        mixer.SetFloat("SoundsVolume", soundsToggle.isOn ? soundsVolume : -80);
    }
    public void MuteMusic(bool mute) {
        mixer.SetFloat("MusicVolume", musicToggle.isOn ? musicVolume : -80);
    }

    void Start() {
#if UNITY_EDITOR
        //musicToggle.isOn = false;
        MuteMusic(true);
#endif
    }
}
