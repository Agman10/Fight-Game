using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public TestPlayer user;
    public bool onGoing;
    public float effectTime;

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {
        this.StopAllCoroutines();
        this.onGoing = false;
        this.effectTime = 0f;

    }

    public virtual void GiveStatusEffect(float time = 10f)
    {
        if (!this.user.dead)
        {
            /*this.effectTime = time;
            this.onGoing = true;*/

            if (!this.onGoing)
                this.GiveEffect(time);
            else if (time > this.effectTime)
                this.effectTime = time;
        }
        
    }

    public virtual void GiveEffect(float time = 10f)
    {
        this.effectTime = time;
        this.onGoing = true;
        this.StartCoroutine(this.StatusEffectTimerCoroutine());
    }

    public virtual void RemoveStatusEffect()
    {
        this.StopAllCoroutines();
        this.onGoing = false;
        this.effectTime = 0f;

    }

    private IEnumerator StatusEffectTimerCoroutine()
    {
        while (this.effectTime > 0f && this.onGoing)
        {
            //this.effectTime -= Time.deltaTime;
            yield return null;
        }

        this.RemoveStatusEffect();
    }
}
