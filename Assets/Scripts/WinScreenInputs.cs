using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinScreenInputs : MonoBehaviour
{
    public int gameSceneId;


    private void OnEnable()
    {
        if(UserInputManager.Instance != null)
        {
            if(UserInputManager.Instance.player1Input != null)
            {
                PlayerInput playerInput = UserInputManager.Instance.player1Input.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.StartInput += this.ReloadScene;
                    playerInput.KickInput += this.ReloadScene;
                    playerInput.Special2Input += this.QuitToTitle;
                }
            }

            if (UserInputManager.Instance.player2Input != null)
            {
                PlayerInput playerInput = UserInputManager.Instance.player2Input.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.StartInput += this.ReloadScene;
                    playerInput.KickInput += this.ReloadScene;
                    playerInput.Special2Input += this.QuitToTitle;
                }
            }
        }
    }

    private void OnDisable()
    {
        if (UserInputManager.Instance != null)
        {
            if (UserInputManager.Instance.player1Input != null)
            {
                PlayerInput playerInput = UserInputManager.Instance.player1Input.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.StartInput -= this.ReloadScene;
                    playerInput.KickInput -= this.ReloadScene;
                    playerInput.Special2Input -= this.QuitToTitle;
                }
            }

            if (UserInputManager.Instance.player2Input != null)
            {
                PlayerInput playerInput = UserInputManager.Instance.player2Input.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.StartInput -= this.ReloadScene;
                    playerInput.KickInput -= this.ReloadScene;
                    playerInput.Special2Input -= this.QuitToTitle;
                }
            }
        }
    }


    public void ReloadScene(bool reload)
    {
        if (reload)
            SceneManager.LoadScene(this.gameSceneId);
    }
    public void QuitToTitle(bool quit)
    {
        if (quit)
            SceneManager.LoadScene("Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
