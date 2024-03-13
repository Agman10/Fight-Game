using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UltraSpinGrab : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool moving;
    public TestHitbox hitbox;

    public TestPlayer victim;

    public GameObject startParticle;
    public GameObject trail;
    public GameObject impactEffect;
    public VisualEffect legFire1;
    public VisualEffect legFire2;

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.DoUltraSpinGrab;
    }
    public override void OnDisable()
    {
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.DoUltraSpinGrab;
    }
    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.moving && !this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
    public override void OnReset()
    {
        base.OnReset();
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TryUltraSpinGrabCoroutine());
        }
    }

    IEnumerator TryUltraSpinGrabCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.FlameGrabStartPose();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.FlameGrabDash2();
        this.moving = true;
        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 13f, 0f, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(1f);

        if (this.victim == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);
            this.moving = false;

            if (this.user != null && this.user.rb != null)
                this.user.rb.velocity = Vector3.zero;

            if (this.animations != null)
                this.animations.SetDefaultPose();

            if (this.trail != null)
                this.trail.SetActive(false);

            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    public void DoUltraSpinGrab(TestPlayer player)
    {
        if (player != null && !player.dead)
        {
            player.attackStuns.Add(this.gameObject);
            this.moving = false;

            this.victim = player;

            this.user.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            player.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            this.user.AddStun(1.2f, true);
            player.AddStun(1.5f, true);
            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            //this.grabbedPlayerYPos = player.ragdoll.transform.localPosition.y;

            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), player.gameObject.transform.position.y, player.gameObject.transform.position.z);
            player.ragdoll.gameObject.transform.parent = this.user.ragdoll.transform;

            //this.grabbing = true;

            if (this.animations != null)
                this.animations.SetGrabbingPose();

            /*this.user.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));
            player.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));*/
            //this.StartCoroutine(this.GrabbingCoroutine(player, 1));

            /*this.user.rb.AddForce(new Vector3(0f, 500, 0f));
            player.rb.AddForce(new Vector3(0f, 500, 0f));*/
            this.StartCoroutine(this.UltraGrabbingCoroutine(player));
        }
    }
    private IEnumerator UltraGrabbingCoroutine(TestPlayer player)
    {
        //this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        yield return new WaitForSeconds(1f);
        this.user.rb.AddForce(new Vector3(0f, 500, 0f));
        player.rb.AddForce(new Vector3(0f, 500, 0f));
        yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    /*IEnumerator GrabbingCoroutine(TestPlayer player, float time)
    {
        yield return new WaitForSeconds(0.5f);
        this.MidGrab(player);
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(this.PlayerOnGround);
        yield return new WaitForSeconds(0.05f);
        this.user.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));
        player.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));
        player.TakeDamage(this.user.transform.position, this.damage, 0.1f);
        this.user.GiveSuperCharge(10f);
        player.GiveSuperCharge(5f);

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 0.1f, 0), Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(0.5f);
        this.MidGrab(player);
        yield return new WaitUntil(this.PlayerOnGround);

        this.StopGrab(player);
        this.animations.SetSpinGrabEndPose();

        yield return new WaitForSeconds(0.4f);
        this.user.attackStuns.Remove(this.gameObject);
        this.animations.SetDefaultPose();
        *//*yield return new WaitForSeconds(time);
        this.StopGrab(player);*//*
        //this.ThrowFireBall();
    }*/

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
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.trail != null)
            this.trail.SetActive(false);

        this.moving = false;
        this.user.preventDeath = false;
        this.user.knockbackInvounrability = false;

        if (this.victim != null)
        {
            this.victim.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            this.victim.attackStuns.Remove(this.gameObject);
            this.victim.preventDeath = false;
            this.victim.knockbackInvounrability = false;
            this.victim.ragdoll.gameObject.transform.parent = this.victim.transform;
            if (this.onGoing && this.victim.health <= 0f)
            {
                this.victim.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false);
            }
            else
            {
                this.victim.animations.SetDefaultPose();
                //this.victim.ragdoll.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            }
            this.victim = null;
        }
    }
}
