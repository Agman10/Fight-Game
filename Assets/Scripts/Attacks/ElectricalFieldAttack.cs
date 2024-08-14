using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalFieldAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox;
    public TestHitbox hitbox2;
    public ParticleSystem electricity;
    public GameObject glowingEyes;
    public int animationId;

    public AudioSource electricitySfx;

    public GameObject magneticHitboxes;

    [Space]
    public float startDelay = 0.2f;
    public float endLag = 0.4f;

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

            
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            if(this.animationId == 1)
                this.animations.ElectricAttackPose2();
            else
                this.animations.ElectricAttackPose();
        }
            

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(true);

        yield return new WaitForSeconds(this.startDelay);

        if (this.electricitySfx != null)
            this.electricitySfx.Play();

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.electricity != null)
            this.electricity.Play();

        if (this.magneticHitboxes != null)
            this.magneticHitboxes.gameObject.SetActive(true);

        //yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);

        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.5f);

        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.5f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.5f);


        /*yield return new WaitForSeconds(0.25f);

        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);
        if (this.user.input != null && this.user.input.special2)
            yield return new WaitForSeconds(0.25f);*/


        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.electricity != null)
            this.electricity.Stop();

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        if (this.magneticHitboxes != null)
            this.magneticHitboxes.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(this.endLag);

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.electricity != null)
            this.electricity.Stop();

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        if (this.magneticHitboxes != null)
            this.magneticHitboxes.gameObject.SetActive(false);

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
