using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenDrawSkins : MonoBehaviour
{
    public bool isPlayer1 = true;
    public CharacterSkinTest skin;
    private void OnEnable()
    {
        if (CharacterManager.Instance != null && this.skin != null)
        {
            if (this.isPlayer1 && CharacterManager.Instance.player1Skin != null)
            {
                this.skin.SetSkin(CharacterManager.Instance.player1Skin);
            }
            else if (!this.isPlayer1 && CharacterManager.Instance.player2Skin != null)
            {
                this.skin.SetSkin(CharacterManager.Instance.player2Skin);

            }
        }

    }
}
