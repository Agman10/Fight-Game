using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSmoke : MonoBehaviour
{
    public TestPlayer belongsTo;


    //THIS WILL GIVE STATUS EFFECT
    //THE STATUS EFFECT LAST LONGER THE FURTHER IN THE HITBOX YOU ARE
    //WILL ALSO HAVE INSTANT HEALING, DAMAGE, SUPER METER
    //the further in the hitbox (more like hitsphere) the more healing/damage it does if its an instant effect

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            /*if (this.hitbox != null)
                this.hitbox.belongsTo = player;*/
        }
    }
}
