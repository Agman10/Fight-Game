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
}
