using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenLogic : MonoBehaviour
{
    public int winnerCharacterId;
    public int loserCharacterId;

    public CharacterVictoryScreen[] winnerCharacterQuotes;
    public GameObject[] loserGhosts;

    public GameObject error;

    public DrawScreenLogic drawScreen;

    public VictoryScreenSound victoryScreenSound;

    private void OnEnable()
    {
        if(this.drawScreen != null && CharacterManager.Instance != null && CharacterManager.Instance.draw)
        {
            this.drawScreen.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            if (this.victoryScreenSound != null)
                this.victoryScreenSound.PlayVictoryBgm();

            if (CharacterManager.Instance != null)
            {
                this.winnerCharacterId = CharacterManager.Instance.winnerCharacterId;
                this.loserCharacterId = CharacterManager.Instance.loserCharacterId;
            }

            if (this.winnerCharacterId <= this.winnerCharacterQuotes.Length - 1 && this.winnerCharacterQuotes.Length >= 1 && this.winnerCharacterId >= 0 && this.loserCharacterId >= 0)
            {
                if (this.winnerCharacterQuotes[this.winnerCharacterId])
                {
                    this.winnerCharacterQuotes[this.winnerCharacterId].opponentId = this.loserCharacterId;
                    this.winnerCharacterQuotes[this.winnerCharacterId].gameObject.SetActive(true);
                }
            }
            else
            {
                if (this.error != null)
                    this.error.SetActive(true);
            }

            if (this.loserCharacterId <= this.loserGhosts.Length - 1 && this.loserGhosts.Length >= 1 && this.loserCharacterId >= 0)
            {
                if (this.loserGhosts[this.loserCharacterId] != null)
                {
                    this.loserGhosts[this.loserCharacterId].SetActive(true);
                }
            }
        }

        /*if(CharacterManager.Instance != null)
        {
            this.winnerCharacterId = CharacterManager.Instance.winnerCharacterId;
            this.loserCharacterId = CharacterManager.Instance.loserCharacterId;
        }

        if (this.winnerCharacterId <= this.winnerCharacterQuotes.Length - 1 && this.winnerCharacterQuotes.Length >= 1 && this.winnerCharacterId >= 0 && this.loserCharacterId >= 0)
        {
            if (this.winnerCharacterQuotes[this.winnerCharacterId])
            {
                this.winnerCharacterQuotes[this.winnerCharacterId].opponentId = this.loserCharacterId;
                this.winnerCharacterQuotes[this.winnerCharacterId].gameObject.SetActive(true);
            }
        }
        else
        {
            if (this.error != null)
                this.error.SetActive(true);
        }

        if(this.loserCharacterId <= this.loserGhosts.Length - 1 && this.loserGhosts.Length >= 1 && this.loserCharacterId >= 0)
        {
            if (this.loserGhosts[this.loserCharacterId] != null)
            {
                this.loserGhosts[this.loserCharacterId].SetActive(true);
            }
        }*/
    }

    public void SkipTextScrolling()
    {
        if (this.winnerCharacterId <= this.winnerCharacterQuotes.Length - 1 && this.winnerCharacterQuotes.Length >= 1 && this.winnerCharacterId >= 0 && this.loserCharacterId >= 0)
        {
            if (this.winnerCharacterQuotes[this.winnerCharacterId])
            {
                this.winnerCharacterQuotes[this.winnerCharacterId].SkipTextScrolling();
            }
        }
    }
}
