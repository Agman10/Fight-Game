using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfCharacterIsPlaying : MonoBehaviour
{
    public int characterId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        /*if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player1.characterId == this.characterId || GameManager.Instance.player2.characterId == this.characterId)
                this.gameObject.SetActive(false);
        }*/

        if(CharacterManager.Instance != null)
        {
            if (CharacterManager.Instance.player1Id == this.characterId || CharacterManager.Instance.player2Id == this.characterId)
                this.gameObject.SetActive(false);
        }
    }
}
