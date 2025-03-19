using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUpManaAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public ParticleSystem chargeUpParticle;
    public GameObject book;

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
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
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

        if (this.animations != null)
            this.animations.NinjaTeleport();

        yield return new WaitForSeconds(0.1f);

        if (this.book != null)
            this.book.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        if (this.chargeUpParticle != null)
            this.chargeUpParticle.Play();

        while (this.user.input != null && this.user.input.special)
        {
            yield return new WaitForSeconds(0.1f);
            this.user.GiveSuperCharge(0.4f);
            yield return null;
        }
        if (this.chargeUpParticle != null)
            this.chargeUpParticle.Stop();

        yield return new WaitForSeconds(0.1f);

        if (this.book != null)
            this.book.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.chargeUpParticle != null)
            this.chargeUpParticle.Stop();

        if (this.book != null)
            this.book.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
