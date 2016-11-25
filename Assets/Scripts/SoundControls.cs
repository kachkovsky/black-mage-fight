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

    public void MuteSounds(bool mute) {
        mixer.SetFloat("SoundsVolume", soundsToggle.isOn ? 0 : -80);
    }
    public void MuteMusic(bool mute) {
        mixer.SetFloat("MusicVolume", musicToggle.isOn ? 0 : -80);
    }

    void Update() {

    }
}
