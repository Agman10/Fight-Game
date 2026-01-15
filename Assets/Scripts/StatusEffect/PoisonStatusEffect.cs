using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{


    private void Update()
    {
        /*if (this.onGoing)
        {
            if (this.effectTime > 0f)
            {
                this.effectTime -= Time.deltaTime;
            }
            else
            {
                this.effectTime = 0f;
                //this.RemoveStatusEffect();
            }

        }*/
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        //this.StopAllCoroutines();
    }

    public override void GiveEffect(float time = 10)
    {
        base.GiveEffect(time);
        this.StartCoroutine(this.StatusEffectTickCoroutine());
        //this.StartCoroutine(this.StatusEffectTimerCoroutine());
        //this.StartCoroutine(this.StatusEffectTickCoroutine2());

    }

    private IEnumerator StatusEffectTickCoroutine()
    {
        float tickTime = 0.05f;
        float damage = 0.25f;

        /*if (this.user != null && !this.user.dead)
            this.user.TakeDamage(this.user.transform.position, damage, 0, 0, 0, false, true, false, false, false);*/

        float time = tickTime;
        while (time > 0f && this.onGoing && this.user != null && !this.user.dead)
        {
            time -= Time.deltaTime;
            //this.effectTime -= Time.deltaTime;
            yield return null;
        }
        this.effectTime = this.effectTime - tickTime;
        this.effectTime = Mathf.Round(this.effectTime * 1000) * 0.001f;

        if (this.user != null && !this.user.dead)
            this.user.TakeDamage(this.user.transform.position, damage, 0, 0, 0, false, true, false, false, false);

        if (this.onGoing && this.user != null && !this.user.dead)
            this.StartCoroutine(this.StatusEffectTickCoroutine());

        /*while (this.effectTime > 0)
        {
            yield return new WaitForSeconds(3f);
            if (this.user != null)
                this.user.TakeDamage(this.user.transform.position, 5, 0, 0, 0, false, true, false, false, false);
            yield return null;
        }

        this.RemoveStatusEffect();*/

        /*if (this.user != null)
            this.user.TakeDamage(this.user.transform.position, 1, 0, 0, 0, false, true, false, false, false);

        while (this.onGoing)
        {
            yield return new WaitForSeconds(0.25f);
            if (this.user != null)
                this.user.TakeDamage(this.user.transform.position, 1, 0, 0, 0, false, true, false, false, false);

            Debug.Log("tick");
            yield return null;
        }*/


    }

    /*private IEnumerator StatusEffectTimerCoroutine()
    {
        while (this.effectTime > 0f && this.onGoing)
        {
            this.effectTime -= Time.deltaTime;
            yield return null;
        }
        //this.effectTime = 0f;
        //Debug.Log("end");

        this.RemoveStatusEffect();
    }*/

    /*private IEnumerator StatusEffectTickCoroutine2()
    {
        if (this.user != null)
            this.user.TakeDamage(this.user.transform.position, 0.25f, 0, 0, 0, false, true, false, false, false);
        int amount = 39;
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (this.user != null)
                this.user.TakeDamage(this.user.transform.position, 0.25f, 0, 0, 0, false, true, false, false, false);
            amount -= 1;
            yield return null;
        }

        this.RemoveStatusEffect();
    }*/
}
