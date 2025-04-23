using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownLogic : MonoBehaviour
{
    public TestPlayer user;
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool canCancel;
    private bool canNotBeStopped;

    /*private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }*/

    private void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += this.OnHit;
            this.user.OnDeath += this.OnDeath;
            this.user.OnReset += this.OnReset;
            this.user.OnAttack += this.Cancel;
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
            this.user.OnAttack -= this.Cancel;
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


    //[ContextMenu("Initiate")]
    public void KnockDown(float xForce = 300f, float yForce = 600f, float stunTime = 2f, float impactStunDuration = 0f, float sitDuration = 0.5f)
    {
        /*float xForce = 300f;
        float yForce = 600f;
        float stunTime = 2f;
        float impactStunDuration = 0f;*/

        if (this.user != null && !this.user.dead)
        {
            if (this.onGoing)
                this.Stop();

            if (/*!this.user.rb.isKinematic &&*/ !this.user.knockbackInvounrability && !this.user.countering)
            {
                this.user.rb.isKinematic = false;
                this.user.AddStun(0.05f, true);
                this.StartCoroutine(this.KnockDownCoroutine(xForce, yForce, stunTime, impactStunDuration, sitDuration));

                //DO THIS LATER
                //this.StartCoroutine(this.CanBeStoppedCoroutine());
            }

            //Debug.Log("test");
        }
        else if (this.user != null && this.animations != null)
        {
            //this.animations.SetEyes(2);
        }
        
    }

    private IEnumerator KnockDownCoroutine(float xForce = 300f, float yForce = 600f, float stunTime = 2f, float impactStunDuration = 0f, float sitDuration = 0.5f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.canCancel = false;

        this.user.LookAtDirection(xForce);

        this.animations.SetDefaultPose();

        float stunnedTime = 0;
        //float impactStunDuration = 0f;
        if (impactStunDuration > 0f)
        {
            this.user.rb.isKinematic = true;
            this.animations.KnifePunishmentHit();
            float currentImpactStunTime = 0;

            float newY = 0f;
            float testTime = 0f;
            float startPosX = this.animations.body.localPosition.x;
            while (currentImpactStunTime < impactStunDuration)
            {
                currentImpactStunTime += Time.deltaTime;

                testTime += Time.deltaTime;

                //stunnedTime += Time.deltaTime;

                newY = Mathf.Sin(testTime * 100f);
                this.animations.body.localPosition = new Vector3(startPosX + (newY * 0.02f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                yield return null;
            }
            this.user.rb.isKinematic = false;
        }

        if (stunnedTime >= stunTime)
            this.canCancel = true;


        this.animations.KnifePunishmentFalling(0);

        //float stunTime = 2f;

        //this.user.AddKnockback(this.user.transform.forward.z * -900f, 600f);
        //this.user.AddKnockback(this.user.transform.forward.z * -300f, 1200f);
        //this.user.AddKnockback(this.user.transform.forward.z * -300f, 600f);

        //this.user.AddKnockback(this.user.transform.forward.z * - xForce, yForce);
        this.user.AddKnockback(xForce, yForce);
        //yield return new WaitForSeconds(0.1f);
        
        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            stunnedTime += Time.deltaTime;
            if (stunnedTime >= stunTime)
                this.canCancel = true;
            yield return null;
        }

        /*float currentTime = 0;
        float duration = 2f;
        while (currentTime < duration && Mathf.Abs(this.user.rb.velocity.y) > 0f && !this.user.dead)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.canCancel = true;*/

        //stunnedTime = 0;
        currentTime = 0;
        duration = 3f;
        while (currentTime < duration && Mathf.Abs(this.user.rb.velocity.y) > 0f && !this.user.dead)
        {
            currentTime += Time.deltaTime;
            stunnedTime += Time.deltaTime;
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if(stunnedTime >= stunTime)
                this.canCancel = true;
            yield return null;
        }

        //player.animations.LayingDownPose();
        //player.animations.LayDown();
        if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
        {
            this.animations.KnifePunishmentFalling(1);
            //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            //yield return new WaitForSeconds(1f);
            currentTime = 0;
            duration = 0.5f;
            while (currentTime < sitDuration && !this.user.dead)
            {
                currentTime += Time.deltaTime;
                stunnedTime += Time.deltaTime;
                /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y, 0f);

                if (stunnedTime >= stunTime)
                    this.canCancel = true;
                yield return null;
            }
        }

        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.canCancel = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Stop()
    {
        this.StopAllCoroutines();
        this.canNotBeStopped = false;

        this.canCancel = false;

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public void Cancel()
    {
        if (!this.user.dead && this.onGoing && this.canCancel && this.user.stuns.Count <= 0)
        {
            //this.user.AddStun(0.1f, true);
            if (this.animations != null)
                this.animations.SetDefaultPose();
            //Debug.Log("Cancel");
            this.Stop();

        }
    }

    private IEnumerator CanBeStoppedCoroutine()
    {
        this.canNotBeStopped = true;
        yield return new WaitForSeconds(0.001f);
        this.canNotBeStopped = false;
    }
}
