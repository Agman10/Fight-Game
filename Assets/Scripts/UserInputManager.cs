using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UserInputManager : MonoBehaviour
{
    public static UserInputManager Instance;

    public PlayerInputManager playerInputManager;

    public UnityEngine.InputSystem.PlayerInput player1Input;
    public UnityEngine.InputSystem.PlayerInput player2Input;

    public Action<int> PlayerJoined;

    public string testname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnEnable()
    {
        this.playerInputManager.onPlayerJoined += Test;
    }
    private void OnDisable()
    {
        this.playerInputManager.onPlayerJoined -= Test;
    }*/
    /*private void OnEnable()
    {
        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= this.OnSceneLoaded;
    }*/
    public void Test(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        //Debug.Log(playerInput.playerIndex);

        this.PlayerJoined?.Invoke(playerInput.playerIndex);

        if (playerInput.playerIndex == 0)
        {
            this.player1Input = playerInput;
            playerInput.gameObject.transform.parent = this.transform;

            if(CharacterSelectLogic.Instance != null)
            {
                CharacterSelectLogic.Instance.p1Cursor.input = playerInput.GetComponent<CharacterSelectInput>();
                CharacterSelectLogic.Instance.p1Cursor.gameObject.SetActive(true);
            }

        }
            
        if (playerInput.playerIndex == 1)
        {
            this.player2Input = playerInput;
            playerInput.gameObject.transform.parent = this.transform;

            if (CharacterSelectLogic.Instance != null)
            {
                CharacterSelectLogic.Instance.p2Cursor.input = playerInput.GetComponent<CharacterSelectInput>();
                CharacterSelectLogic.Instance.p2Cursor.gameObject.SetActive(true);
            }
        }
            
        //Debug.Log("test");
    }
    public void Testtt()
    {

    }
    /*public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //MAKE IT SO THIS GETS DESTROYED WHEN ENTERING CHARACTER SELECT IF IT ALREDY EXIST SO WE GET A BRAND NEW

        if (scene.name == "CharacterSelect")
        {
            Debug.Log("die");
        }

        //Debug.Log("test");
        *//*if(UserInputManager.Instance != this.gameObject)
        {
            Debug.Log("test");
            //Destroy(this.gameObject);
        }*/

        /*if(FindObjectOfType<UserInputManager>() != this.gameObject)
        {
            Debug.Log("test");
        }*/

        /*if (Instance != null && Instance != this)
        {

            Destroy(this.gameObject);
        }
        else Instance = this;*//*

    }*/
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Instance = this;
        if (Instance != null)
        {

            Destroy(this.gameObject);
        }
        else Instance = this;

        /*Debug.Log("destroying " + Instance.testname);

            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }*/
    }


}
