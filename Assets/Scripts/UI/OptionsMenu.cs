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

    public Slider maxWinsSlider;
    private int maxWins = 2;
    public Text maxWinsText;

    public Slider timerSlider;
    private int timerId = 10;
    //public int timerId;
    private int gameTimer = 99;
    private bool timerInfinite = true;
    public Text timerText;

    //maybe rename "MaxFightBallPoints" to just "MaxGoals"?
    public Slider maxFightBallPointsSlider; 
    private int maxFightBallPoints = 6;
    public Text maxFightBallPointsText;

    private void Awake()
    {
        int number = Random.Range(0, 101);
        if(number <= 1)
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

        

        this.LoadPlayerPrefs();
    }

    public void LoadPlayerPrefs()
    {
        this.maxWins = PlayerPrefs.GetInt("MaxWins", 2);
        if (this.maxWinsSlider != null)
            this.maxWinsSlider.value = this.maxWins;

        if (this.maxWinsText != null)
            this.maxWinsText.text = this.maxWins.ToString();

        
        this.timerId = PlayerPrefs.GetInt("GameTimerValueId", 10);
        this.gameTimer = PlayerPrefs.GetInt("GameTimer", 99);
        this.timerInfinite = this.intToBool(PlayerPrefs.GetInt("GameTimerIsInfinite", 1));

        if (this.timerSlider != null)
            this.timerSlider.value = this.timerId;

        if (this.timerText != null)
        {
            if (!this.timerInfinite)
                this.timerText.text = this.gameTimer.ToString();
            else
                this.timerText.text = "Infinite";
        }


        this.maxFightBallPoints = PlayerPrefs.GetInt("MaxFightBallPoints", 6);
        if (this.maxFightBallPointsSlider != null)
            this.maxFightBallPointsSlider.value = this.maxFightBallPoints;

        if (this.maxFightBallPointsText != null)
            this.maxFightBallPointsText.text = this.maxFightBallPoints.ToString();
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
            this.geary.OnSelectExit();
    }

    public void Back()
    {
        if (this.geary != null)
            this.geary.OnExit();

        PlayerPrefs.Save();

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


    public void OnSelectMaxWins()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectMaxWins();
    }

    public void OnMaxWinsChange()
    {
        if(this.maxWinsSlider != null)
        {
            if (!this.geary.disabled)
            {
                //float maxWinsSliderValue = maxWinsSlider.value;
                int maxWinsSliderValue = (int)maxWinsSlider.value;
                if (maxWinsSliderValue >= 5)
                {
                    this.maxWinsSlider.value = 1;
                    maxWinsSliderValue = 1;
                }
                else if (maxWinsSliderValue <= 0)
                {
                    this.maxWinsSlider.value = 4;
                    maxWinsSliderValue = 4;
                }


                //this.maxWins = (int)maxWinsSliderValue;
                this.maxWins = maxWinsSliderValue;

                if (this.maxWinsText != null)
                    this.maxWinsText.text = this.maxWins.ToString();

                PlayerPrefs.SetInt("MaxWins", this.maxWins);
                //PlayerPrefs.Save();
            }
            else
            {
                this.maxWinsSlider.value = this.maxWins;
            }

        }
    }

    public void OnSelectTimer()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectTimer();
    }

    public void OnTimerChange()
    {
        if (this.timerSlider != null)
        {
            if (!this.geary.disabled)
            {
                //float maxWinsSliderValue = maxWinsSlider.value;
                int timerSliderIdValue = (int)timerSlider.value;
                /*if (timerSliderIdValue <= -1)
                {
                    this.timerSlider.value = 3;
                    timerSliderIdValue = 3;
                }
                else if (timerSliderIdValue == 0)
                {
                    this.gameTimer = 30;
                }
                else if (timerSliderIdValue == 1)
                {
                    this.gameTimer = 60;
                }
                else if (timerSliderIdValue == 2)
                {
                    this.gameTimer = 99;
                }
                else if (timerSliderIdValue == 3)
                {
                    this.gameTimer = 99;
                }
                else if (timerSliderIdValue >= 4)
                {
                    this.timerSlider.value = 0;
                    timerSliderIdValue = 0;
                }*/


                if (timerSliderIdValue <= -1)
                {
                    this.timerSlider.value = 10;
                    timerSliderIdValue = 10;
                }
                else if (timerSliderIdValue >= 11)
                {
                    this.timerSlider.value = 0;
                    timerSliderIdValue = 0;
                }
                else
                {
                    switch (timerSliderIdValue)
                    {

                        case 0:
                            this.gameTimer = 10;
                            break;
                        case 1:
                            this.gameTimer = 20;
                            break;
                        case 2:
                            this.gameTimer = 30;
                            break;
                        case 3:
                            this.gameTimer = 40;
                            break;
                        case 4:
                            this.gameTimer = 50;
                            break;
                        case 5:
                            this.gameTimer = 60;
                            break;
                        case 6:
                            this.gameTimer = 70;
                            break;
                        case 7:
                            this.gameTimer = 80;
                            break;
                        case 8:
                            this.gameTimer = 90;
                            break;
                        default:
                            this.gameTimer = 99;
                            break;
                    }
                }




                //this.maxWins = (int)maxWinsSliderValue;
                this.timerId = timerSliderIdValue;
                this.timerInfinite = timerSliderIdValue == 10;

                if (this.timerText != null)
                {
                    if (!this.timerInfinite)
                        this.timerText.text = this.gameTimer.ToString();
                    else
                        this.timerText.text = "Infinite";
                }


                PlayerPrefs.SetInt("GameTimer", this.gameTimer);
                PlayerPrefs.SetInt("GameTimerValueId", this.timerId);
                PlayerPrefs.SetInt("GameTimerIsInfinite", this.boolToInt(this.timerInfinite));
                //PlayerPrefs.Save();
            }
            else
            {
                this.timerSlider.value = this.timerId;
            }

        }
    }

    public void OnSelectMaxFightBallPoints()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectMaxFightBallPoints();
    }

    public void OnMaxFightBallPointsChange()
    {
        if (this.maxFightBallPointsSlider != null)
        {
            if (!this.geary.disabled)
            {
                //float maxWinsSliderValue = maxWinsSlider.value;
                int maxWinsSliderValue = (int)maxFightBallPointsSlider.value;
                if (maxWinsSliderValue >= 10)
                {
                    this.maxFightBallPointsSlider.value = 1;
                    maxWinsSliderValue = 1;
                }
                else if (maxWinsSliderValue <= 0)
                {
                    this.maxFightBallPointsSlider.value = 9;
                    maxWinsSliderValue = 9;
                }


                //this.maxWins = (int)maxWinsSliderValue;
                this.maxFightBallPoints = maxWinsSliderValue;

                if (this.maxFightBallPointsText != null)
                    this.maxFightBallPointsText.text = this.maxFightBallPoints.ToString();

                PlayerPrefs.SetInt("MaxFightBallPoints", this.maxFightBallPoints);
                //PlayerPrefs.Save();
            }
            else
            {
                this.maxFightBallPointsSlider.value = this.maxFightBallPoints;
            }

        }
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    public void OnSelectAudio()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectAudio();
    }

    public void OnSelectVideo()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectVideo();
    }

    public void OnSelectMisc()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectMisc();
    }


    public void OnSelectBack()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectBack();
    }


    public void OnSelectUnavailable()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectUnavalaible();
    }
    public void OnClickUnavailable()
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnClickUnavailable();
    }

    /*public void OnSelectCustom(int dialogueId, string normalDialogue, int normalExpression, string evilDialogue, int evilExpression)
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnSelectCustom();
    }
    public void OnClickCustom(int dialogueId, string normalDialogue, int normalExpression, string evilDialogue, int evilExpression)
    {
        if (this.geary != null && !this.geary.disabled)
            this.geary.OnClickCustom();
    }*/







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
