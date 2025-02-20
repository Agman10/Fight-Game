using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPortraitEnabler : MonoBehaviour
{
    public int id;
    public GameObject portrait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.EnablePortrait();
    }

    public void EnablePortrait()
    {
        if (this.portrait != null && CharacterSelectLogic.Instance != null && CharacterSelectLogic.Instance.p1Cursor != null && CharacterSelectLogic.Instance.p2Cursor != null)
        {
            if(CharacterSelectLogic.Instance.p1Cursor.currentCharacterId == this.id || CharacterSelectLogic.Instance.p2Cursor.currentCharacterId == this.id)
            {
                this.portrait.SetActive(true);
            }
            else
            {
                this.portrait.SetActive(false);
            }
        }
    }
}
