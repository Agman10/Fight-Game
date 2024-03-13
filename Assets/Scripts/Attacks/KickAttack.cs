using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
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
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
            }
                
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.KickCoroutine());*/

            if (Mathf.Abs(this.user.transform.position.y) < 0.2f)
            {
                //Debug.Log(Mathf.Abs(this.user.transform.position.y));
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KickCoroutine());
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.AirKickCoroutine());
            }


            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.4f, true);
            else
                this.user.AddStun(0.4f, false);

            this.StartCoroutine(this.KickCoroutine2());*/


            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.4f, true);
            else
                this.user.AddStun(0.4f, false);

            this.StartCoroutine(this.KickHitboxCoroutine());

            this.animationCoroutine = this.KickAnimationCoroutine();

            if (this.animationCoroutine != null)
            {
                this.StartCoroutine(this.animationCoroutine);
            }*/

        }
    }

    IEnumerator KickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.Kick(0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.Kick(1);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.Kick(2);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.Kick(3);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator AirKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);


        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SexKickStart();

        yield return new WaitForSeconds(0.05f);



        if (this.animations != null)
            this.animations.SexKick();

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
            this.animations.SexKickStart();

        yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.StartCoroutine(this.HitCoroutine());

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/




        /*if (this.animationCoroutine != null)
        {
            this.StopCoroutine(this.animationCoroutine);
            this.animationCoroutine = null;
        }*/


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

    IEnumerator KickCoroutine2()
    {
        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;*/

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartKickPose0();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartKickPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetKickPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.2f);



        /*this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }

    /*IEnumerator KickCoroutineOld()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartKickPose0();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetStartKickPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetKickPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }*/



    /*IEnumerator KickHitboxCoroutine()
    {
        if (this.animations != null)
            this.animations.SetStartKickPose0();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetStartKickPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetKickPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }


    IEnumerator KickAnimationCoroutine()
    {
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartKickPose0();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetStartKickPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetKickPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.onGoing = false;
    }*/
}