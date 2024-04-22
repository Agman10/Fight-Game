using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlamingGrab : Attack
{
    //public TestPlayer user;
    public TestHitbox hitbox;
    public TempPlayerAnimations animations;

    public TestPlayer victim;

    public bool moving;
    public bool onGoing;
    public VisualEffect fire;
    public GameObject explosionEffect;
    public GameObject trail;
    public GameObject startParticle;
    public VisualEffect dashFire1;
    public VisualEffect dashFire2;

    public float dashSpeed = 16f;
    private float dashDuration = 1.4f;

    public bool hover;

    public float damagePerTick = 5;

    public override void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
            this.user.OnReset += OnReset;
        }

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.DoFlameGrab;

    }
    public override void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
            this.user.OnReset -= OnReset;
        }

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.DoFlameGrab;

    }
    public override void OnHit()
    {
        if (this.moving && !this.onGoing)
        {
            //Debug.Log("test");
            this.Stop();
            if (this.animations != null && !this.user.dead)
                this.animations.SetDefaultPose();
        }

    }
    public override void OnDeath()
    {
        this.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.moving && this.user != null && this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.dashSpeed, 0f, 0f);
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        if (this.user != null && this.user.stuns.Count <= 0 && this.user.attackStuns.Count <= 0)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryFlameGrabCoroutine());
                }
            }
        }
    }


    IEnumerator TryFlameGrabCoroutine()
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
        if (this.hover)
        {
            if (this.animations != null)
                this.animations.FlameGrabDash2();

            this.PlayFire(true, 1);
        }
        else
        {
            if (this.animations != null)
                this.animations.FlameGrabDash();
        }

        this.moving = true;
        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(this.dashDuration);
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

            this.PlayFire(false);
        }
    }
    public void DoFlameGrab(TestPlayer player)
    {
        this.PlayFire(false);

        player.attackStuns.Add(this.gameObject);
        player.OnHit.Invoke();
        //player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);
        this.user.transform.position = new Vector3(player.transform.position.x - (this.user.transform.forward.z * 1.1f), 0f, 0f);
        player.preventDeath = true;
        this.user.preventDeath = true;
        this.user.knockbackInvounrability = true;
        player.knockbackInvounrability = true;
        if (this.trail != null)
            this.trail.SetActive(false);

        this.victim = player;
        this.moving = false;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetGrabbingPose();

        this.user.rb.constraints = RigidbodyConstraints.FreezeAll;
        player.rb.constraints = RigidbodyConstraints.FreezeAll;

        if (player.animations != null)
            player.animations.body.localPosition = new Vector3(0f, 1.95f, 0f);

        this.StartCoroutine(this.FlameGrabCoroutine(player));
        //this.StartCoroutine(this.PunchBarageCoroutine(player));
    }

    IEnumerator FlameGrabCoroutine(TestPlayer player)
    {
        this.onGoing = true;

        float currentTime = 0;
        float duration = 0.025f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        this.PlayFire(true);

        float time = 0.1f;
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        this.animations.SetGrabbingHeadbutPose1();
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(time);
        this.PlayFire(false);
        player.TakeDamage(this.user.transform.position, this.damagePerTick);
        yield return new WaitForSeconds(0.1f);
        this.animations.SetGrabbingHeadbutPose2();

        if (this.explosionEffect != null)
        {
            GameObject explosionPrefab = this.explosionEffect;
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 2f, 0), Quaternion.Euler(0, 0, 0));
        }

        this.user.preventDeath = false;
        player.preventDeath = false;
        player.knockbackInvounrability = false;
        player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        player.TakeDamage(this.user.transform.position, this.damagePerTick, 0.5f, this.user.transform.forward.z * 1000f, 1000f);
        player.attackStuns.Remove(this.gameObject);


        //player.TakeDamage(this.user.transform.position, 0f);

        //this.user.knockbackInvounrability = false;

        yield return new WaitForSeconds(0.1f);
        this.user.animations.FlameGrabHitPose();
        yield return new WaitForSeconds(0.75f);
        this.StopFlameGrab(player);
    }

    public void StopFlameGrab(TestPlayer player)
    {
        //this.PlayFire(true);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.knockbackInvounrability = false;

        if (!player.dead)
        {
            player.animations.SetDefaultPose();
            //player.ragdoll.transform.localPosition = new Vector3(0f, 1.95f, 0f);
        }


        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        this.user.attackStuns.Remove(this.gameObject);
        //player.attackStuns.Remove(this.gameObject);
        this.onGoing = false;
        this.victim = null;
        //this.user.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true);
    }

    public void PlayFire(bool playing, int id = 0)
    {
        /*if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }*/
        if (playing)
        {
            if (id == 0)
            {
                if (this.fire != null)
                    this.fire.Play();
            }
            else
            {
                if (this.dashFire1 != null)
                    this.dashFire1.Play();

                if (this.dashFire2 != null)
                    this.dashFire2.Play();
            }
        }
        else
        {
            if (this.fire != null)
                this.fire.Stop();

            if (this.dashFire1 != null)
                this.dashFire1.Stop();

            if (this.dashFire2 != null)
                this.dashFire2.Stop();
        }


        
        
    }


    public override void Stop()
    {
        this.StopAllCoroutines();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.trail != null)
            this.trail.SetActive(false);

        this.moving = false;
        this.user.preventDeath = false;
        this.user.knockbackInvounrability = false;
        this.PlayFire(false);

        if (this.victim != null)
        {
            this.victim.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            this.victim.attackStuns.Remove(this.gameObject);
            this.victim.preventDeath = false;
            this.victim.knockbackInvounrability = false;
            if (this.onGoing && this.victim.health <= 0f)
            {
                this.victim.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false);
            }
            this.victim = null;
        }
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    IEnumerator PunchBarageCoroutine(TestPlayer player)
    {
        this.onGoing = true;
        yield return new WaitForSeconds(0.5f);
        this.PlayFire(true);
        float time = 0.05f;

        this.animations.SetStartPunchPose();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage1();
        yield return new WaitForSeconds(time);
        this.animations.PunchBarage2();
        yield return new WaitForSeconds(time);
        this.animations.SetPunchLeftPose();
        yield return new WaitForSeconds(time);
        this.animations.SetDefaultPose();
        this.PlayFire(false);

        if (this.explosionEffect != null)
        {
            GameObject explosionPrefab = this.explosionEffect;
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 2f, 0), Quaternion.Euler(0, 0, 0));
        }

        this.user.preventDeath = false;
        player.preventDeath = false;
        player.knockbackInvounrability = false;
        player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        player.TakeDamage(this.user.transform.position, 5f, 0.5f, this.user.transform.forward.z * 1000f, 1000f);
        player.attackStuns.Remove(this.gameObject);


        //player.TakeDamage(this.user.transform.position, 0f);

        //this.user.knockbackInvounrability = false;

        this.user.animations.FlameGrabHitPose();
        yield return new WaitForSeconds(0.75f);
        this.StopFlameGrab(player);
    }
}
