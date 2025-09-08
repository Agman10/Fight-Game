using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeCounterAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool countering;
    public TestHitbox hitbox;
    public TestHitbox hitboxNoDamage;

    public AudioSource counterSfx;
    public AudioSource punchSfx;

    public ParticleSystem counterEffect;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && !this.countering && !this.user.countering)
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

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
        {
            this.user.OnHitFromPlayer += this.HitFromPlayer;
        }



    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
        {
            this.user.OnHitFromPlayer -= this.HitFromPlayer;
        }



    }

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.1f);*/

        float distance = 0f;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            distance = this.user.transform.position.x - GameManager.Instance.gameCamera.transform.position.x;

            //this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.user.transform.forward.z * 90f + (distance * 5), this.animations.body.localEulerAngles.z);
        }

        this.animations.JoeCounter(0, distance);

        yield return new WaitForSeconds(0.15f);

        this.animations.JoeCounter(1, distance);

        yield return new WaitForSeconds(0.1f);

        this.animations.JoeCounter(2, distance);

        yield return new WaitForSeconds(0.1f);


        this.animations.JoeCounter(3, distance);

        this.SetCountering(true);

        yield return new WaitForSeconds(0.35f);

        this.SetCountering(false);

        this.animations.JoeCounter(2, distance);

        yield return new WaitForSeconds(0.05f);

        this.animations.JoeCounter(1, distance);

        yield return new WaitForSeconds(0.05f);

        this.animations.JoeCounter(0, distance);

        yield return new WaitForSeconds(0.05f);



        /*this.SetCountering(true);
        this.user.animations.MCapRagingBeastBlock();

        yield return new WaitForSeconds(2.6f);*/

        //this.SetCountering(false);
        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator CounterCoroutine(TestPlayer player)
    {
        if (this.counterSfx != null)
            this.counterSfx.Play();

        if (this.punchSfx != null)
            this.punchSfx.Play();

        if (this.counterEffect != null)
            this.counterEffect.Play();

        if (this.hitboxNoDamage != null)
            this.hitboxNoDamage.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.HoodGuyGrabStart(0);

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.HoodGuyGrabMid(0);

        yield return new WaitForSeconds(0.025f);

        this.animations.HoodGuyGrab(0);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.hitboxNoDamage != null)
            this.hitboxNoDamage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        this.SetCountering(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void HitFromPlayer(TestPlayer player)
    {
        if (player != null && player != this.user)
        {
            if (this.countering /*&& this.grabbedPlayer == null*/)
            {
                this.StopAllCoroutines();
                this.user.lookAtPlayer();
                //this.SetCountering(false);
                this.countering = false;
                Debug.Log("trestt");
                this.StartCoroutine(this.CounterCoroutine(player));
            }

        }
    }


    public override void Stop()
    {
        base.Stop();

        this.SetCountering(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitboxNoDamage != null)
            this.hitboxNoDamage.gameObject.SetActive(false);

        /*this.countering = false;
        this.user.knockbackInvounrability = false;
        //this.user.countering = false;
        //this.user.rb.isKinematic = false;
        this.StartCoroutine(this.HitCoroutine());*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void SetCountering(bool counter)
    {
        if (counter)
        {
            this.countering = true;
            if (this.user != null)
            {
                this.user.knockbackInvounrability = true;
                this.user.damageMitigation = 1f;
                this.user.countering = true;
                //this.user.rb.isKinematic = true;
            }
        }
        else
        {
            this.countering = false;
            if (this.user != null)
            {
                this.user.knockbackInvounrability = false;
                this.user.damageMitigation = 0f;
                this.user.countering = false;
                //this.user.rb.isKinematic = false;
            }
        }
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        this.user.damageMitigation = 0f;
        this.user.countering = false;
    }
}
