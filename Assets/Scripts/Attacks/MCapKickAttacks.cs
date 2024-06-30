using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCapKickAttacks : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox;
    public TestHitbox airHitbox;

    public TestHitbox forwardHitbox;

    public AudioSource kickSwooshSfx;
    public AudioSource airKickSwooshSfx;
    public AudioSource forwardKickSwooshSfx;


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
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

            if (Mathf.Abs(this.user.transform.position.y) < 0.2f)
            {
                //Debug.Log(Mathf.Abs(this.user.transform.position.y));
                /*this.user.AddStun(0.1f, true);
                this.StartCoroutine(this.KickCoroutine());*/


                if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.ForwardKickCoroutine());
                }
                else
                {
                    this.user.AddStun(0.1f, true);
                    this.StartCoroutine(this.KickCoroutine());
                }
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.AirKickCoroutine());
            }

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

        if (this.kickSwooshSfx != null)
        {
            this.kickSwooshSfx.time = 0.01f;
            this.kickSwooshSfx.Play();
        }

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

        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator AirKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;


        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);


        yield return new WaitForSeconds(0.05f);*/
        if (this.animations != null)
            this.animations.SexKickStart();

        if (this.airKickSwooshSfx != null)
        {
            this.airKickSwooshSfx.time = 0.02f;
            this.airKickSwooshSfx.Play();
        }

        yield return new WaitForSeconds(0.05f);



        if (this.animations != null)
            this.animations.SexKick();

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.15f;
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

        //yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ForwardKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float animSpeed = 0.05f;

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 0);

        if (this.forwardKickSwooshSfx != null)
        {
            this.forwardKickSwooshSfx.time = 0.02f;
            this.forwardKickSwooshSfx.Play();
        }

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 150f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 0);

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(animSpeed + 0.035f);


        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);

        //yield return new WaitForSeconds(animSpeed);




        yield return new WaitForSeconds(animSpeed);

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
            this.hitbox.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/




        /*if (this.animationCoroutine != null)
        {
            this.StopCoroutine(this.animationCoroutine);
            this.animationCoroutine = null;
        }*/

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);

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