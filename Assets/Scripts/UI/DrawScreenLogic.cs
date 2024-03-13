using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawScreenLogic : MonoBehaviour
{
    public int p1CharacterId;
    public int p2CharacterId;

    public GameObject[] p1Ghosts;
    public GameObject[] p2Ghosts;

    public GameObject textPanel;

    public Text winnerHeader;

    public VictoryScreenSound victoryScreenSound;

    private void OnEnable()
    {
        if (this.victoryScreenSound != null)
            this.victoryScreenSound.PlayDrawBgm();

        if (this.textPanel != null)
            this.textPanel.SetActive(false);

        if (this.winnerHeader != null)
            this.winnerHeader.text = "DRAW";

        if (CharacterManager.Instance != null)
        {
            this.p1CharacterId = CharacterManager.Instance.player1Id;
            this.p2CharacterId = CharacterManager.Instance.player2Id;
        }

        if (this.p1CharacterId <= this.p1Ghosts.Length - 1 && this.p1Ghosts.Length >= 1 && this.p1CharacterId >= 0)
        {
            if (this.p1Ghosts[this.p1CharacterId] != null)
            {
                this.p1Ghosts[this.p1CharacterId].SetActive(true);
            }
        }

        if (this.p2CharacterId <= this.p2Ghosts.Length - 1 && this.p2Ghosts.Length >= 1 && this.p2CharacterId >= 0)
        {
            if (this.p2Ghosts[this.p2CharacterId] != null)
            {
                this.p2Ghosts[this.p2CharacterId].SetActive(true);
            }
        }

    }
}
