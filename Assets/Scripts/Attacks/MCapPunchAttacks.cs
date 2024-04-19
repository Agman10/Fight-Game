using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCapPunchAttacks : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public bool onGoingNeutral;
    public TestHitbox hitbox;
    public TestHitbox airHitbox;


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (Mathf.Abs(this.user.transform.position.y) < 0.2f) //not in air
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.AddStun(0.1f, true);
                else
                    this.user.AddStun(0.1f, false);

                this.StartCoroutine(this.PunchCoroutine());
            }
            else //in air
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.AirPunchCoroutine());
            }

        }
    }

    IEnumerator PunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartPunchPose0();
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.SetStartPunchPose();

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator AirPunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);*/

        if (this.animations != null)
            this.animations.AirPunch(0);

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.AirPunch(1);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.2f;
        //float duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.StartCoroutine(this.HitCoroutine());

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/


        /*if (this.animationCoroutine != null)
        {
            this.StopCoroutine(this.animationCoroutine);
            this.animationCoroutine = null;
        }*/

        //this.onGoingNeutral = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);
    }
}