using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodGuySpecializedSupers : MonoBehaviour
{
    public TestPlayer user;
    public TempAttacks attackScript;

    public Attack[] supers;

    private void OnEnable()
    {
        //this.ChangeSuper();

        this.StartCoroutine(this.TemplateCoroutine());
    }

    private IEnumerator TemplateCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        this.ChangeSuper();
    }

    public void ChangeSuper()
    {
        if (this.user != null && this.user.tempOpponent != null && this.attackScript != null)
        {
            if (this.supers.Length -1 >= this.user.tempOpponent.characterId)
            {
                /*Debug.Log(this.supers.Length - 1);
                Debug.Log(this.user.tempOpponent.characterId);*/

                if (this.supers[this.user.tempOpponent.characterId] != null)
                {
                    this.attackScript.neutralSuper = this.supers[this.user.tempOpponent.characterId];
                }
            }
        }
    }
}
