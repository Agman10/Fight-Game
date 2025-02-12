using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePunishmentHandler : MonoBehaviour
{
    public TestPlayer belongsTo;

    public PunishmentKnife[] innerKnifes;
    public PunishmentKnife[] outerKnifes;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.belongsTo != null)
            this.SetOwner(this.belongsTo);

        this.StartCoroutine(this.KnifesCoroutine());
    }

    private IEnumerator KnifesCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        this.StartInnerKnifes();
        yield return new WaitForSeconds(0.1f);
        this.StartOuterKnifes();
    }

    public void StartInnerKnifes()
    {
        foreach(PunishmentKnife knife in this.innerKnifes)
        {
            if(knife != null && knife.enabled)
            {
                knife.Move();
            }
        }
    }

    public void StartOuterKnifes()
    {
        foreach (PunishmentKnife knife in this.outerKnifes)
        {
            if (knife != null && knife.enabled)
            {
                knife.Move();
            }
        }
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;


            foreach (PunishmentKnife knife in this.innerKnifes)
            {
                if (knife != null)
                {
                    knife.SetOwner(player);
                }
            }

            foreach (PunishmentKnife knife in this.outerKnifes)
            {
                if (knife != null)
                {
                    knife.SetOwner(player);
                }
            }
        }


        
    }
}
