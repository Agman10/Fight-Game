using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ZombieBurn : MonoBehaviour
{
    public GameObject zombie;
    public GameObject burntZombie;
    public GameObject ash;
    public VisualEffect fire;
    public VisualEffect smoke;

    public int burntStage;
    public bool burning;
    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.ResetZombie();
    }

    [ContextMenu("Burn")]
    public void Burn()
    {
        if (this.gameObject.active)
        {
            this.StopAllCoroutines();
            this.ResetZombie();
            this.StartCoroutine(this.BurnCoroutine());
        }


        /*if (this.gameObject.active)
        {
            if (this.burntStage <= 1)
                this.StartCoroutine(this.BurnCoroutine2());
        }*/
    }

    private IEnumerator BurnCoroutine()
    {
        if (this.fire != null)
            this.fire.gameObject.SetActive(true);

        this.burning = true;

        yield return new WaitForSeconds(5f);

        if (this.zombie != null)
            this.zombie.SetActive(false);

        if (this.burntZombie != null)
            this.burntZombie.SetActive(true);

        this.burntStage = 1;

        yield return new WaitForSeconds(5f);

        /*if (this.fire != null)
            this.fire.gameObject.SetActive(false);*/

        if (this.fire != null)
            this.fire.Stop();

        if (this.burntZombie != null)
            this.burntZombie.SetActive(false);

        if (this.ash != null)
            this.ash.SetActive(true);

        this.burntStage = 2;

        this.burning = false;

        yield return new WaitForSeconds(5f);

        if (this.smoke != null)
            this.smoke.Stop();
    }

    [ContextMenu("BurnContinue")]
    public void BurnContinue()
    {
        if (this.gameObject.active)
        {
            if (this.burntStage <= 1 && !this.burning)
            {
                this.burning = true;
                this.StartCoroutine(this.BurnCoroutineContinue());
            }
        }
    }

    private IEnumerator BurnCoroutineContinue()
    {
        if (this.fire != null)
            this.fire.gameObject.SetActive(true);

        if (this.burntStage <= 0)
        {
            
            if (this.fire != null)
                this.fire.Play();

            yield return new WaitForSeconds(5f);

            if (this.zombie != null)
                this.zombie.SetActive(false);

            if (this.burntZombie != null)
                this.burntZombie.SetActive(true);

            this.burntStage = 1;
        }

        if (this.burntStage == 1)
        {
            if (this.fire != null)
                this.fire.Play();

            yield return new WaitForSeconds(5f);

            if (this.fire != null)
                this.fire.Stop();

            if (this.burntZombie != null)
                this.burntZombie.SetActive(false);

            if (this.ash != null)
                this.ash.SetActive(true);

            this.burntStage = 2;
            this.burning = false;
        }

        yield return new WaitForSeconds(5f);

        if (this.smoke != null)
            this.smoke.Stop();
    }

    [ContextMenu("StopBurning")]
    public void StopBurning()
    {
        if (this.burntStage <= 1)
        {
            this.StopAllCoroutines();

            if (this.fire != null)
                this.fire.Stop();

            this.burning = false;
        }
    }

    [ContextMenu("Reset")]
    public void ResetZombie()
    {
        this.StopAllCoroutines();

        if (this.zombie != null)
            this.zombie.SetActive(true);

        if (this.burntZombie != null)
            this.burntZombie.SetActive(false);

        if (this.ash != null)
            this.ash.SetActive(false);

        if (this.fire != null)
            this.fire.gameObject.SetActive(false);

        this.burntStage = 0;
        this.burning = false;
    }
}
