using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Geary geary;
    public GameObject eventSystem;

    [Space]
    public Image fader;
    public AudioSource music;

    public Text languageButtonText;
    public int currentLanguageId = 0;

    [Space]
    public Geary evilGeary;
    public Material evilSkybox;

    //public AudioSource defaultMusic;
    public AudioSource evilMusic;

    public Slider slider;
    public float sliderValue;

    private void Awake()
    {
        int number = Random.Range(0, 101);
        if(number <= 50)
        {
            this.geary.gameObject.SetActive(false);
            this.geary = this.evilGeary;
            this.geary.gameObject.SetActive(true);

            this.music = this.evilMusic;
            this.music.Play();

            RenderSettings.skybox = this.evilSkybox;
            
        }
        else
        {
            this.music.Play();
        }
    }

    public void SelectLanguage()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.SelectLanguage();
    }

    public void SetLanguage()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.SetLanguage();

        if(this.languageButtonText != null && this.geary != null && !this.geary.disabled)
        {
            if (this.currentLanguageId <= 0)
            {
                this.currentLanguageId = 1;
                this.languageButtonText.text = "Also English";
            }
            else if (this.currentLanguageId >= 1)
            {
                this.currentLanguageId = 0;
                this.languageButtonText.text = "English";
            }
        }
    }

    public void OnSelectTurnOffGeary()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectTurnOffGeary();
    }
    public void OnTurnOffGeary()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnTurnOffGeary();
    }

    public void SelectBack()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectBack();
    }

    public void Back()
    {
        if (this.geary != null)
            this.geary.OnBack();

        //SceneManager.LoadScene("Menu");
    }

    public void OnSelectVolume()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectVolume();

        if (this.slider != null)
            this.sliderValue = this.slider.value;
    }
    public void OnVolumeChange()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnVolumeChange();

        if (!this.geary.disabled)
        {
            if (this.slider != null)
                this.sliderValue = this.slider.value;
        }
        else
        {
            Debug.Log(this.slider.value);
            if (this.slider != null)
                this.slider.value = this.sliderValue;
        }
        
    }


    public void EnableEventSystem(bool enable = true)
    {
        if (this.eventSystem != null)
            this.eventSystem.SetActive(enable);
    }

    public void FadeToBlackAndGoToMenu()
    {
        this.StartCoroutine(this.FadeToBlackAndGoToMenuCoroutine());
    }

    private IEnumerator FadeToBlackAndGoToMenuCoroutine()
    {
        /*float currentTime = 0;
        float duration = 1f;
        float targetVolume = 0.05f;
        //float targetRotation = 0f;
        float startVolume = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.music != null)
                this.music.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

            yield return null;
        }


        currentTime = 0;
        duration = 0.2f;*/

        if (this.fader != null)
            this.fader.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 1f;
        float targetVolume = 0.05f;
        float startVolume = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.fader != null)
                this.fader.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, currentTime / duration));

            if (this.music != null)
                this.music.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

            yield return null;
        }

        this.GoBackToMenu();
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
