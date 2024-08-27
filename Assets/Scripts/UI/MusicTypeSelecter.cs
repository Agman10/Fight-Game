using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicTypeSelecter : MonoBehaviour
{
    public int currentId;
    public TextMeshProUGUI currentMusicText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("NextMusicType")]
    public void NextMusicType()
    {
        if (this.currentId == 3)
            this.SetMusicType(0);
        else
            this.SetMusicType(this.currentId + 1);
    }

    [ContextMenu("PreviousMusicType")]
    public void PreviousMusicType()
    {
        if (this.currentId == 0)
            this.SetMusicType(3);
        else
            this.SetMusicType(this.currentId - 1);
    }

    public void SetMusicType(int id)
    {
        this.currentId = id;

        if (this.currentMusicText != null)
        {
            switch (id)
            {
                case 0:
                    this.currentMusicText.text = "Stage";
                    break;
                case 1:
                    this.currentMusicText.text = "Character (Random)";
                    break;
                case 2:
                    this.currentMusicText.text = "Character (P1)";
                    break;
                case 3:
                    this.currentMusicText.text = "Character (P2)";
                    break;
                default:
                    this.currentMusicText.text = "";
                    break;
            }
        }
    }
}
