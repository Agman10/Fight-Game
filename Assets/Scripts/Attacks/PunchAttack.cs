using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public bool onGoingNeutral;
    public TestHitbox hitbox;
    public TestHitbox airHitbox;

    //private IEnumerator hitboxCoroutine;
    //private IEnumerator animationCoroutine;

    /*public override void OnEnable()
    {
        base.OnEnable();

        this.animationCoroutine = this.PunchAnimationCoroutine();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }*/


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }

        /*if (!this.user.dead && this.onGoingNeutral)
        {
            this.NeutralStop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }*/
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();

        /*if (this.onGoing || this.onGoingNeutral)
            this.Stop();*/

        /*this.StopAllCoroutines();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        /*if (this.onGoing)
        {
            this.Stop();
            this.StopAllCoroutines();
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);
        }*/



    }

    /*public override void OnReset()
    {
        base.OnReset();


        this.StopAllCoroutines();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }*/

    /*private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.1f, true);
            else
                this.user.AddStun(0.1f, false);

            this.StartCoroutine(this.PunchCoroutine());*/


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


            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.PunchCoroutine2());*/


            /*if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
            {

            }
            else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
            {

            }
            else //Neutral
            {

            }*/

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.PunchHitboxCoroutine());

            this.animationCoroutine = this.PunchAnimationCoroutine();

            if (this.animationCoroutine != null)
            {
                this.StartCoroutine(this.animationCoroutine);
            }*/

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

        yield return new WaitForSeconds(0.1f);

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

        yield return new WaitForSeconds(0.1f);
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

        yield return new WaitForSeconds(0.1f);

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


    IEnumerator PunchCoroutine2()
    {
        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartPunchPose0();
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartPunchPose();

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetPunchPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }






    /*public void NeutralStop()
    {
        *//*base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*//*


        if (this.animationCoroutine != null)
        {
            this.StopCoroutine(this.animationCoroutine);
            this.animationCoroutine = null;
        }

        this.onGoingNeutral = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    IEnumerator PunchHitboxCoroutine()
    {
        *//*if (this.animations != null)
            this.animations.SetStartPunchPose0();*//*
        yield return new WaitForSeconds(0.01f);

        *//*if (this.animations != null)
            this.animations.SetStartPunchPose();*//*

        yield return new WaitForSeconds(0.04f);

        *//*if (this.animations != null)
            this.animations.SetPunchPose();*//*

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        *//*if (this.animations != null)
            this.animations.SetDefaultPose();*//*

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }


    IEnumerator PunchAnimationCoroutine()
    {
        //this.onGoing = true;
        this.onGoingNeutral = true;

        if (this.animations != null)
            this.animations.SetStartPunchPose0();

        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.SetStartPunchPose();

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        *//*if (this.punchHitbox != null)
            this.punchHitbox.gameObject.SetActive(true);*//*

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        *//*if (this.punchHitbox != null)
            this.punchHitbox.gameObject.SetActive(false);*//*

        //this.onGoing = false;
        this.onGoingNeutral = false;
    }*/
}
