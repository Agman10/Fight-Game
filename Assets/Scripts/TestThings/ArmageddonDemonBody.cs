using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmageddonDemonBody : MonoBehaviour
{
    public Transform mainBody;
    public Transform armageddonBody;

    public CharacterSkinTest mainBodySkin;
    public CharacterSkinTest armageddonBodySkin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (this.armageddonBody != null)
        {
            this.armageddonBody.localPosition = this.mainBody.localPosition;
            this.armageddonBody.localEulerAngles = this.mainBody.localEulerAngles;
            this.armageddonBody.localScale = this.mainBody.localScale;
        }

        if (this.mainBodySkin != null && this.armageddonBody != null && this.mainBodySkin.skin != null)
            this.armageddonBodySkin.SetSkin(this.mainBodySkin.skin);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(this.armageddonBody != null)
        {
            this.armageddonBody.localPosition = this.mainBody.localPosition;
            this.armageddonBody.localEulerAngles = this.mainBody.localEulerAngles;
            this.armageddonBody.localScale = this.mainBody.localScale;
        }*/
        
    }
}
