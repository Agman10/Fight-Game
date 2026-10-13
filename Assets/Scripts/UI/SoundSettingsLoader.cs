using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettingsLoader : MonoBehaviour
{
    public AudioMixer audioMixer;
    /*public AudioMixerGroup masterMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;*/
    void Start()
    {
        this.LoadSoundSettings();
    }

    private void OnEnable()
    {
        //this.LoadSoundSettings();
    }

    private void Awake()
    {

    }

    public void LoadSoundSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 100);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 100);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100);

        this.audioMixer.SetFloat("Master", Mathf.Log10(masterVolume * 0.01f) * 20);
        this.audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume * 0.01f) * 20);
        this.audioMixer.SetFloat("Music", Mathf.Log10(musicVolume * 0.01f) * 20);
    }
}
