using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonExistantAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public AudioSource errorSfx;
    private bool isOnCooldown;

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
            if (!this.isOnCooldown)
            {
                if (this.errorSfx != null)
                    this.errorSfx.Play();

                this.StartCoroutine(this.TemplateCoroutine2());
            }
            

            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }*/

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine2()
    {
        this.isOnCooldown = true;

        /*this.user.AddStun(0.2f, true);
        if (this.animations != null)
            this.animations.Shrug(2);*/

        yield return new WaitForSeconds(0.2f);

        this.isOnCooldown = false;
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        this.isOnCooldown = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}