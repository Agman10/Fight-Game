using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCopier : MonoBehaviour
{
    public CharacterSkinTest skinToCopy;
    public CharacterSkinTest skin;
    private void OnEnable()
    {
        this.skin = this.GetComponent<CharacterSkinTest>();

        if(this.skin != null && this.skinToCopy != null)
        {
            //this.skin.skin = this.skinToCopy.skin;

            this.skin.SetSkin(this.skinToCopy.skin);
        }
    }
}
