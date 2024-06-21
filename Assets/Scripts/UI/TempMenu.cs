using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
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

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterSelect");

        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.gameModeId = 0;
        }
        //SceneManager.LoadSceneAsync("Loading");
        //SceneManager.LoadScene("Loading");
    }

    public void StartFightBall()
    {
        //SceneManager.LoadScene("TestFootBall");

        if (UserInputManager.Instance != null)
            Destroy(UserInputManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("CharacterSelect");

        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.gameModeId = 1;
        }
        //SceneManager.LoadScene("Loading");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
