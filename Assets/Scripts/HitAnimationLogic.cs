using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimationLogic : MonoBehaviour
{
    public TestPlayer user;
    public TempPlayerAnimations animations;
    public bool onGoing;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += this.OnHit;
            this.user.OnDeath += this.OnDeath;
            this.user.OnReset += this.OnReset;
            //this.user.OnAttack += this.Cancel;
        }
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= this.OnHit;
            this.user.OnDeath -= this.OnDeath;
            this.user.OnReset -= this.OnReset;
            //this.user.OnAttack -= this.Cancel;
        }
    }

    public void OnHit()
    {
        if (/*!this.user.dead &&*/ this.onGoing /*&& !this.canNotBeStopped*/)
        {
            if (!this.user.dead)
                this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }
    public void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }
    public void OnReset()
    {
        this.Stop();
    }

    /*public void OnAttack()
    {
        if (this.onGoing)
            this.Stop();
    }*/

    public void TriggerHitAnimation()
    {
        if (this.user != null && this.animations != null && !this.user.knockbackInvounrability && !this.user.countering && !this.user.dead/*&& this.user.attackStuns.Count <= 0*/)
            this.StartCoroutine(this.HitAnimationCoroutine());


        /*if (this.user != null && this.animations != null && !this.user.countering)
        {
            if (this.user.knockbackInvounrability)
                this.StartCoroutine(this.HitAnimationEyesOnlyCoroutine());
            else
                this.StartCoroutine(this.HitAnimationCoroutine());
        }*/
            
        //Debug.Log("hitAnim");
    }

    private IEnumerator HitAnimationCoroutine()
    {
        //this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.animations.KnifePunishmentHit();
        //this.animations.DressHitPose();

        while (this.user.stuns.Count > 0)
        {
            yield return null;
        }

        if (!this.user.dead && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        //this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitAnimationEyesOnlyCoroutine()
    {
        this.onGoing = true;

        this.animations.SetEyes(2);

        while (this.user.stuns.Count > 0)
        {
            yield return null;
        }

        if (!this.user.dead)
            this.animations.SetEyes(0);

        this.onGoing = false;
    }

    public void Stop()
    {
        this.StopAllCoroutines();
        /*this.canNotBeStopped = false;

        this.canCancel = false;*/

        this.onGoing = false;
        //this.user.attackStuns.Remove(this.gameObject);
    }


    public void Cancel()
    {
        if (!this.user.dead && this.onGoing /*&& this.canCancel*/ && this.user.stuns.Count <= 0)
        {
            //this.user.AddStun(0.1f, true);
            if (this.animations != null)
                this.animations.SetDefaultPose();
            //Debug.Log("Cancel");
            this.Stop();

        }
    }
}
