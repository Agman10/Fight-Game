using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThemePlayer : MonoBehaviour
{
    public AudioSource[] themes;

    public int musicId;

    private void OnEnable()
    {
        //this.PlayRandomCharacterTheme();

        //this.StartCoroutine(this.PlayerRandomCoroutine());
    }

    [ContextMenu("PlayRandomCharacterTheme")]
    public void PlayRandomCharacterTheme()
    {
        foreach (AudioSource theme in this.themes)
        {
            if (theme != null)
                theme.Stop();
        }
            

        if (GameManager.Instance != null)
        {
            int number = Random.Range(0, 2);
            //Debug.Log(number);

            if (number == 0 && GameManager.Instance.player1 != null)
            {
                if (GameManager.Instance.player1.characterId <= this.themes.Length - 1 && this.themes[GameManager.Instance.player1.characterId] != null)
                {
                    this.themes[GameManager.Instance.player1.characterId].Play();
                    this.musicId = GameManager.Instance.player1.characterId;
                }
            }
            else if (GameManager.Instance.player2 != null)
            {
                if (GameManager.Instance.player2.characterId <= this.themes.Length - 1 && this.themes[GameManager.Instance.player2.characterId] != null)
                {
                    this.themes[GameManager.Instance.player2.characterId].Play();
                    this.musicId = GameManager.Instance.player2.characterId;
                }
            }
        }
    }

    [ContextMenu("PlayP1CharacterTheme")]
    public void PlayP1CharacterTheme()
    {
        foreach (AudioSource theme in this.themes)
        {
            if (theme != null)
                theme.Stop();
        }

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player1 != null)
            {
                if (GameManager.Instance.player1.characterId <= this.themes.Length - 1 && this.themes[GameManager.Instance.player1.characterId] != null)
                {
                    this.themes[GameManager.Instance.player1.characterId].Play();
                    this.musicId = GameManager.Instance.player1.characterId;
                }
            }
        }
    }


    [ContextMenu("PlayP2CharacterTheme")]
    public void PlayP2CharacterTheme()
    {
        foreach (AudioSource theme in this.themes)
        {
            if (theme != null)
                theme.Stop();
        }

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player2 != null)
            {
                if (GameManager.Instance.player2.characterId <= this.themes.Length - 1 && this.themes[GameManager.Instance.player2.characterId] != null)
                {
                    this.themes[GameManager.Instance.player2.characterId].Play();
                    this.musicId = GameManager.Instance.player2.characterId;
                }
            }
        }
    }

    private IEnumerator PlayerRandomCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        this.PlayRandomCharacterTheme();
    }
}
