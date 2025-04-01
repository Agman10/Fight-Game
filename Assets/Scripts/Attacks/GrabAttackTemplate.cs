using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAttackTemplate : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public GameObject startParticle;

    public TestHitbox hitbox;
    public TestPlayer grabbedPlayer;

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
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TryGrabCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TryGrabCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }*/

        yield return new WaitForSeconds(0.3f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;


            yield return null;
        }

        if (this.grabbedPlayer == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            //yield return new WaitForSeconds(0.1f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {


        yield return new WaitForSeconds(1);

        this.StopGrab(player);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Grab(TestPlayer player)
    {
        if(player != null && !player.dead)
        {

        }

        if (!player.countering)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.grabbedPlayer = player;
            player.OnHit?.Invoke();
            player.lookAtPlayer();
            player.preventDeath = true;
            player.attackStuns.Add(this.gameObject);

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

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

            if (!player.dead)
            {
                player.rb.isKinematic = false;

                player.animations.SetDefaultPose();
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
