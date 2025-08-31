using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeExplosiveSuperGrab : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public TestHitbox hitbox;
    public TestPlayer grabbedPlayer;
    public GameObject trail;

    public ParticleSystem explosion;
    public ParticleSystem dotsParticle;
    public AudioSource explosionSfx;

    //public ParticleSystem ex

    private bool moving = false;

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

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.Grab;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.Grab;
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    private void Update()
    {
        if (this.onGoing && !this.moving)
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
                if (this.user.superCharge >= this.user.maxSuperCharge)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryGrabCoroutine());
                }
            }

            
        }
    }

    private IEnumerator TryGrabCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.animations.SuperExplosiveGrabStartPose();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.3f);

        this.animations.SuperExplosiveGrab();

        this.moving = true;

        //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * (30f + extraRage), 0f, 0f);

        this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 15f, 0f, 0f);

        if (this.trail != null)
            this.trail.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        //yield return new WaitForSeconds(0.35f);

        float currentTime = 0;
        float duration = 0.5f;

        float startVelocity = 15f;

        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;
            float velocity = Mathf.Lerp(startVelocity, 2f, currentTime / duration);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * velocity, 0f, 0f);

            yield return null;
        }

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;


            yield return null;
        }*/

        this.moving = false;
        if (this.trail != null)
            this.trail.SetActive(false);

        if (this.grabbedPlayer == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.user.rb.velocity = new Vector3(0f, 0f, 0f);

            //yield return new WaitForSeconds(0.1f);

            this.animations.SuperExplosiveGrabMiss();

            yield return new WaitForSeconds(0.2f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        //this.animations.SetGrabbingPose();
        //player.animations.KnifePunishmentHit();

        yield return new WaitForSeconds(0.3f);

        if (this.dotsParticle != null)
            this.dotsParticle.Play();

        yield return new WaitForSeconds(1);

        if (this.dotsParticle != null)
            this.dotsParticle.Stop();

        this.StopGrab(player);

        //yield return new WaitForSeconds(0.4f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead)
        {

        }

        if (!player.countering)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            if (this.trail != null)
                this.trail.SetActive(false);

            this.grabbedPlayer = player;
            player.OnHit?.Invoke();
            player.lookAtPlayer();
            player.preventDeath = true;
            player.attackStuns.Add(this.gameObject);

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

            player.animations.SetDefaultPose();
            player.animations.KnifePunishmentHit();

            this.animations.SetDefaultPose();
            this.animations.SetGrabbingPose();

            Vector3 pos = this.user.transform.position;
            int charId = player.characterId;
            float xForward = 1.25f;
            //float xForward = 1.58f;
            float yUp = 0f;

            if (charId == 3 || charId == 4 || charId == 7)
            {
                xForward = 1.25f;
                yUp = 0.5f;
            }

            //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), 0f, 0f);
            player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), pos.y + yUp, 0f);

            this.StartCoroutine(this.GrabbingCoroutine(player));
        }
        else
        {
            player.OnHitFromPlayer?.Invoke(this.user);
        }
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            player.preventDeath = false;

            /*if (this.dotsParticle != null)
                this.dotsParticle.Stop();*/

            if (!player.dead)
            {
                player.rb.isKinematic = false;

                //player.animations.SetDefaultPose();
                player.TakeDamage(this.user.transform.position, 60f, 1f, this.user.transform.forward.z * 700f, 1000f, true, true, false, false, true, false, true, true, 0f, 0.5f, this.user);
                this.user.TakeDamage(player.transform.position, 30f, 1f, this.user.transform.forward.z * -600f, 700f, true, true, false, false, true, false, true, true, 0f, 0.5f, this.user);

                if (this.explosion != null)
                    this.explosion.Play();

                if (this.explosionSfx != null)
                {
                    this.explosionSfx.time = 0.08f;
                    this.explosionSfx.Play();
                }
                    

            }
            this.user.rb.isKinematic = false;

            this.grabbedPlayer = null;
        }
    }





    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.knockbackInvounrability = false;
        this.user.rb.isKinematic = false;

        this.moving = false;

        if (this.trail != null)
            this.trail.SetActive(false);

        if (this.dotsParticle != null)
            this.dotsParticle.Stop();

        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.rb.isKinematic = false;
            this.grabbedPlayer.knockbackInvounrability = false;

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            this.grabbedPlayer.animations.SetDefaultPose();

            /*if (this.grabbedPlayer.health > 0f)
                this.grabbedPlayer.animations.SetDefaultPose();

            if (this.onGoing && this.grabbedPlayer.health <= 0f)
            {
                this.grabbedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false, false, false, true, false, true);
            }*/

            this.grabbedPlayer = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
