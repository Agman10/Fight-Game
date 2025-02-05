using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject vsButtons;
    public GameObject eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("TestStage");

        //SceneManager.LoadScene(8);

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterSelect");

        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.gameModeId = 0;
            GameModeManager.Instance.vsAi = false;
        }

        //SceneManager.LoadSceneAsync("Loading");
        //SceneManager.LoadScene("Loading");

        this.DisableEventSystem();
    }

    public void StartFightBall()
    {
        //SceneManager.LoadScene("TestFootBall");
        //SceneManager.LoadScene(8);

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterSelect");

        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.gameModeId = 1;
            GameModeManager.Instance.vsAi = false;
        }
        //SceneManager.LoadScene("Loading");

        this.DisableEventSystem();
    }

    public void StartVsAi(int vsId)
    {
        //SceneManager.LoadScene("TestStage");

        //SceneManager.LoadScene(8);

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterSelect");

        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.gameModeId = 0;
            GameModeManager.Instance.vsAi = true;
            GameModeManager.Instance.vsAiId = vsId;
        }
        //SceneManager.LoadSceneAsync("Loading");
        //SceneManager.LoadScene("Loading");

        this.DisableEventSystem();
    }


    public void GoToOptions()
    {
        SceneManager.LoadScene("Settings");

        this.DisableEventSystem();
    }

    public void QuitGame()
    {
        Application.Quit();
        //this.DisableEventSystem();
    }

    public void SelectMainMode()
    {
        if (this.mainButtons != null)
            this.mainButtons.SetActive(false);

        if (this.vsButtons != null)
            this.vsButtons.SetActive(true);
    }

    public void Back()
    {
        if (this.mainButtons != null)
            this.mainButtons.SetActive(true);

        if (this.vsButtons != null)
            this.vsButtons.SetActive(false);
    }

    public void DisableEventSystem()
    {
        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);
    }
}
