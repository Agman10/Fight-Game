using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseLogic : MonoBehaviour
{
    public Button resumeButton;
    public Button nothingButton;
    public int sceneIndex = 1;

    public bool lifeNumbersEnabled = false;
    public GameObject p1LifeNumbers;
    public GameObject p2LifeNumbers;
    public GameObject[] healthAndSuperNumbers;

    private void OnEnable()
    {
        /*if (this.resumeButton != null)
            this.resumeButton.Select();*/

        this.StartCoroutine(this.SelectResumeCoroutine());

        //Debug.Log("test");
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.nothingButton != null)
            this.nothingButton.Select();
    }

    public void Resume()
    {
        if (GameManager.Instance != null)
        {
            //Debug.Log("test");
            GameManager.Instance.UnPauseGame();
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.sceneIndex);
    }

    private IEnumerator SelectResumeCoroutine()
    {
        //yield return new WaitForSecondsRealtime(0.2f);
        yield return new WaitForSecondsRealtime(0.05f);
        if (this.resumeButton != null)
            this.resumeButton.Select();
    }

    public void NextStage()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.tempSkyboxAndStageLogic != null)
            {
                GameManager.Instance.tempSkyboxAndStageLogic.LoadNextStage(false);
            }
        }
    }

    public void PreviousStage()
    {
        if (GameManager.Instance.tempSkyboxAndStageLogic != null)
        {
            GameManager.Instance.tempSkyboxAndStageLogic.LoadNextStage(true);
        }
    }

    public void GiveFullSuper()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player1 != null)
            {
                GameManager.Instance.player1.GiveSuperCharge(GameManager.Instance.player1.maxSuperCharge);
            }

            if (GameManager.Instance.player2 != null)
            {
                GameManager.Instance.player2.GiveSuperCharge(GameManager.Instance.player2.maxSuperCharge);
            }
        }
    }

    public void ShowHealthNumbers()
    {
        this.lifeNumbersEnabled = !this.lifeNumbersEnabled;
        /*if (this.p1LifeNumbers != null)
            this.p1LifeNumbers.SetActive(this.lifeNumbersEnabled);

        if (this.p2LifeNumbers != null)
            this.p2LifeNumbers.SetActive(this.lifeNumbersEnabled);*/

        foreach (GameObject healthAndSuperNumber in this.healthAndSuperNumbers)
        {
            if (healthAndSuperNumber != null)
                healthAndSuperNumber.SetActive(this.lifeNumbersEnabled);
        }
    }

    public void NextSkybox()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.tempSkyboxAndStageLogic != null)
            {
                GameManager.Instance.tempSkyboxAndStageLogic.LoadNextSkybox();
            }
        }
    }
    public void PreviousSkybox()
    {

    }
}
