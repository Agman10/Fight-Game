using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGamePlayerIcons : MonoBehaviour
{
    public bool isPlayer1 = true;
    public GameObject[] icons;
    private void OnEnable()
    {
        if (CharacterManager.Instance != null && this.icons.Length > 0)
        {
            if (this.isPlayer1)
            {
                if (CharacterManager.Instance.player1Id <= this.icons.Length - 1)
                {
                    if (this.icons[CharacterManager.Instance.player1Id] != null)
                        this.icons[CharacterManager.Instance.player1Id].SetActive(true);
                }
            }
            else
            {
                if (CharacterManager.Instance.player2Id <= this.icons.Length - 1)
                {
                    if (this.icons[CharacterManager.Instance.player2Id] != null)
                        this.icons[CharacterManager.Instance.player2Id].SetActive(true);
                }

            }
        }
    }
}
