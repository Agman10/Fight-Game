using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIconManager : MonoBehaviour
{
    public CharacterIconSkins[] characterIcons;
    public bool isPlayer1 = true;

    public GameObject placeholderIcon;

    private void OnEnable()
    {
        //this.SetIcon();
    }

    /*public void InstantiateIcon(int characterId, SO_Skin skinData)
    {
        if (this.characterIcons.Length >= characterId + 1 && skinData != null)
        {

        }
    }*/

    public void SetIcon()
    {
        if (CharacterManager.Instance != null)
        {
            if (this.isPlayer1 && CharacterManager.Instance.player1Skin != null)
            {
                if (this.characterIcons.Length >= CharacterManager.Instance.player1Id + 1)
                {
                    this.InstantiateIconPrefab(CharacterManager.Instance.player1Id, CharacterManager.Instance.player1Skin);
                }
                else
                {
                    this.InstantiatePlaceholder();
                }
            }
            else if (!this.isPlayer1 && CharacterManager.Instance.player2Skin != null)
            {
                if (this.characterIcons.Length >= CharacterManager.Instance.player2Id + 1)
                {
                    this.InstantiateIconPrefab(CharacterManager.Instance.player2Id, CharacterManager.Instance.player2Skin);
                }
                else
                {
                    this.InstantiatePlaceholder();
                }

            }
        }
    }

    public void InstantiateIconPrefab(int characterId, SO_Skin skinData)
    {
        if(this.characterIcons[characterId] != null)
        {
            CharacterIconSkins iconPrefab = this.characterIcons[characterId];

            iconPrefab = Instantiate(iconPrefab, this.transform);
            iconPrefab.SetSkin(skinData);
        }
        else
        {
            this.InstantiatePlaceholder();
        }
        
    }

    public void InstantiatePlaceholder()
    {
        if(this.placeholderIcon != null)
        {
            GameObject placeholderPrefab = this.placeholderIcon;

            placeholderPrefab = Instantiate(placeholderPrefab, this.transform);
        }
    }
}
