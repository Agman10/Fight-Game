using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenSkins : MonoBehaviour
{
    public int characterId;
    public bool changeSkinOnLose = false;
    public CharacterSkinTest skin;
    private void OnEnable()
    {
        if(CharacterManager.Instance != null && this.skin != null)
        {
            //Debug.Log("test1");
            if (!this.changeSkinOnLose)
            {
                //Debug.Log("test2W");
                if (this.characterId == CharacterManager.Instance.winnerCharacterId)
                {
                    //Debug.Log("test3W");
                    if (CharacterManager.Instance.winnerWasPlayer1 && CharacterManager.Instance.player1Skin != null)
                    {
                        this.skin.SetSkin(CharacterManager.Instance.player1Skin);
                        //Debug.Log("test4W");
                    }
                    else if (!CharacterManager.Instance.winnerWasPlayer1 && CharacterManager.Instance.player2Skin != null)
                    {
                        this.skin.SetSkin(CharacterManager.Instance.player2Skin);
                        //Debug.Log("test4WP2");
                    }
                }
            }
            else
            {
                //Debug.Log("test2L");
                if (this.characterId == CharacterManager.Instance.loserCharacterId)
                {
                    //Debug.Log("test3L");
                    if (!CharacterManager.Instance.winnerWasPlayer1 && CharacterManager.Instance.player1Skin != null)
                    {
                        this.skin.SetSkin(CharacterManager.Instance.player1Skin);
                        //Debug.Log("test4L");
                    }
                    else if (CharacterManager.Instance.winnerWasPlayer1 && CharacterManager.Instance.player2Skin != null)
                    {
                        this.skin.SetSkin(CharacterManager.Instance.player2Skin);
                        //Debug.Log("test4LP2");
                    }
                }
            }
        }
        
    }
}
