using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadSceneAsync("CharacterSelect");
        //this.LoadCharacterSelect();
        //this.StartCoroutine(this.LoadSceneCoroutine());
    }

    /*private void Awake()
    {
        //SceneManager.LoadSceneAsync("CharacterSelect");
        this.LoadCharacterSelect();
    }*/
    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync("CharacterSelect");
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadSceneAsync("CharacterSelect");
    }
}
