using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreAttackTypeOnDeath : MonoBehaviour
{
    public TestPlayer user;
    public TempAttacks attacks;
    public Attack forwardSpecial;
    private void OnEnable()
    {
        if (this.user != null)
            this.user.OnDeath += this.RestoreAttack;
    }
    private void OnDisable()
    {
        if (this.user != null)
            this.user.OnDeath -= this.RestoreAttack;
    }

    public void RestoreAttack()
    {
        if(this.attacks.forwardSpecial != this.forwardSpecial)
        {
            this.attacks.forwardSpecial = this.forwardSpecial;
            //Debug.Log("restored attack");
        }
    }
}
