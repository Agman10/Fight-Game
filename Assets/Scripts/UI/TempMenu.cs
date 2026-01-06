using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class TempMenu : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject vsButtons;
    public GameObject eventSystem;
    public Button startButton;
    public int currentMenuIndex;
    private bool starting;
    public UnityEngine.InputSystem.PlayerInput playerInput;

    [Space]
    public bool backing;
    public Action<bool> BackInput;
    [HideInInspector] public bool pastBackInput;
    // Start is called before the first frame update
    void Start()
    {
        if (this.playerInput != null && this.playerInput.devices.Count >= 1)
        {
            //Debug.Log("Removed " + this.playerInput.devices.Count + " device(s).");
            this.playerInput.user.UnpairDevicesAndRemoveUser();
            
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.BackInput += this.OnBack;
    }

    private void OnDisable()
    {
        this.BackInput -= this.OnBack;
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("TestStage");

        //SceneManager.LoadScene(8);

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterAndStageSelect");

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

        SceneManager.LoadSceneAsync("CharacterAndStageSelect");

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

        SceneManager.LoadSceneAsync("CharacterAndStageSelect");

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

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("zTestSceneHowToPlay");

        this.DisableEventSystem();
    }

    public void SelectMainMode()
    {
        if (this.mainButtons != null)
            this.mainButtons.SetActive(false);

        if (this.vsButtons != null)
            this.vsButtons.SetActive(true);

        this.currentMenuIndex = 1;
    }

    //RENAME THIS TO SOMETHING LIKE BackToStartMenu OR SOMETHING ELSE
    public void Back()
    {
        if (this.mainButtons != null)
            this.mainButtons.SetActive(true);

        if (this.vsButtons != null)
            this.vsButtons.SetActive(false);

        this.currentMenuIndex = 0;
        //this.backing = false;
    }

    public void OnBackInput(InputAction.CallbackContext ctx)
    {

        bool boolean = ctx.ReadValueAsButton();
        this.backing = boolean;
        if (this.pastBackInput != boolean)
        {
            this.BackInput?.Invoke(boolean);
            this.pastBackInput = boolean;
            this.backing = false;
        }
    }

    public void OnBack(bool boolean)
    {
        if (boolean && !this.starting)
        {
            if (this.currentMenuIndex == 1)
            {
                this.Back();
                //this.backing = false;
                if (this.startButton != null)
                    this.startButton.Select();
            }
                
        }
    }

    public void DisableEventSystem()
    {
        if (this.eventSystem != null)
            this.eventSystem.SetActive(false);

        this.starting = true;
    }
}
