using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePunishmentHandler : MonoBehaviour
{
    public TestPlayer belongsTo;

    public PunishmentKnife[] innerKnifes;
    public PunishmentKnife[] outerKnifes;

    public AudioSource knifeSwooshSfx;
    public AudioSource knifeHitSfx;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.belongsTo != null)
            this.SetOwner(this.belongsTo);

        this.StartCoroutine(this.KnifesCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator KnifesCoroutine()
    {
        yield return new WaitForSeconds(0.4f);

        if (this.knifeSwooshSfx != null)
            this.knifeSwooshSfx.Play();
        this.StartInnerKnifes();
        yield return new WaitForSeconds(0.05f);
        this.StartOuterKnifes();

        yield return new WaitForSeconds(0.1f);
        if (this.knifeHitSfx != null)
            this.knifeHitSfx.Play();

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
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
