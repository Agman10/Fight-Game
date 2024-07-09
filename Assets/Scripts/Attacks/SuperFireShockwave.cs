using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperFireShockwave : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool cantBeCanceled;

    public GameObject startParticle;

    public TestHitbox hitbox1;
    public TestHitbox hitbox2;

    public VisualEffect fire;

    public ParticleSystem shockwaveVfx;

    public AudioSource fireSfx;
    public AudioSource shockwaveSfx;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && !this.cantBeCanceled)
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
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                

                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);

                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TestShockwaveSuper());
                }
            }
        }
    }

    private IEnumerator TestShockwaveSuper()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ShockwavePunchStart();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * -0.6f), this.user.transform.position.y + 3.3f, -0.3f), Quaternion.Euler(0, 0, 0));
        }

        if (this.user.soundEffects != null)
        {
            this.user.soundEffects.PlaySuperSfx();
        }

        yield return new WaitForSeconds(0.3f);
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        if (this.shockwaveVfx != null)
            this.shockwaveVfx.Play();

        if (this.shockwaveSfx != null)
        {
            this.shockwaveSfx.time = 0.08f;
            this.shockwaveSfx.Play();
        }

        if (this.fireSfx != null)
            this.fireSfx.Play();

        this.PlayFire(true);

        if (this.animations != null)
            this.animations.ShockwavePunch();

        this.cantBeCanceled = true;
        this.user.knockbackInvounrability = true;

        this.user.rb.isKinematic = true;

        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        yield return new WaitForSeconds(0.6f);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        this.PlayFire(false);

        if (this.fireSfx != null)
            this.fireSfx.Stop();


        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        this.user.rb.isKinematic = false;

        this.cantBeCanceled = false;
        this.user.knockbackInvounrability = false;

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;
        this.user.knockbackInvounrability = false;

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.fireSfx != null)
            this.fireSfx.Stop();

        /*if (this.shockwaveVfx != null)
            this.shockwaveVfx.Stop();*/

        this.PlayFire(false);
        this.cantBeCanceled = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }
}