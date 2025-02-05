using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodGuyGrabAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;

    public ParticleSystem particle;

    public TestPlayer grabbedPlayer;
    public bool grabbing;

    public ParticleSystem bloodEffect;
    public ParticleSystem oilEffect;


    public override void OnEnable()
    {
        base.OnEnable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += Grab;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= Grab;
    }

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
                this.StartCoroutine(this.TryGrabCoroutine());
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

        //this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.HoodGuyGrabStart();

        yield return new WaitForSeconds(0.2f);
        if (this.animations != null)
            this.animations.HoodGuyGrabMid();

        yield return new WaitForSeconds(0.05f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.HoodGuyGrab();


        if (!this.grabbing)
        {
            //yield return new WaitForSeconds(0.5f);

            float currentTime = 0;
            float duration = 0.5f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                yield return null;
            }

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            //this.user.rb.isKinematic = false;

            this.user.attackStuns.Remove(this.gameObject);
            this.onGoing = false;
        }



        //End

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }

    IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        if (this.particle != null)
            this.particle.Play();

        yield return new WaitForSeconds(0.1f);

        int amount = 30;
        //int amount = 180; kill the opponent
        while (amount > 0)
        {
            /*if (this.animations != null)
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);*/

            
            yield return new WaitForSeconds(0.075f);
            if (!player.dead)
                player.animations.HoodGuyGrabbed();

            player.TakeDamage(this.user.transform.position, 0.5f, 0f, 0f, 0f, false, true, false, true);

            this.user.GiveSuperCharge(0.25f);
            player.GiveSuperCharge(0.125f);

            amount -= 1;

            if (amount <= 1 && this.particle != null)
            {
                this.particle.Stop();
                //Debug.Log("test");
            }
                

            yield return null;
        }

        if (this.particle != null)
            this.particle.Stop();

        if (this.animations != null)
            this.animations.HoodGuyGrabMid();

        player.animations.SetDefaultPose();
        player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);

        yield return new WaitForSeconds(0.05f);


        /*if (this.animations != null)
            this.animations.HoodGuyGrabMid();

        yield return new WaitForSeconds(0.05f);*/


        /*player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);
        yield return new WaitForSeconds(0.01f);*/
        this.StopGrab(player);

        if (this.particle != null)
            this.particle.Stop();

        /*if (this.animations != null)
            this.animations.HoodGuyGrabMid();*/

        if (this.animations != null)
            this.animations.HoodGuyThrow();

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead && !player.countering)
        {
            this.grabbing = true;
            this.grabbedPlayer = player;

            player.OnHit?.Invoke();

            player.LookAtTarget();

            this.grabbedPlayer.preventDeath = true;

            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.33f), this.user.gameObject.transform.position.y + 1.74f, 0f);


            player.TakeDamage(this.user.transform.position, 5f, 0f, 0f, 0f, false, true, false, true);

            this.user.GiveSuperCharge(3f);
            player.GiveSuperCharge(1.5f);

            this.PlayBloodEffect(player.characterId);

            player.animations.HoodGuyGrabbed();

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

            player.attackStuns.Add(this.gameObject);

            this.StartCoroutine(this.GrabbingCoroutine(player));
        }
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            this.grabbing = false;

            player.rb.isKinematic = false;

            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);


            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            if (!player.dead)
            {
                player.rb.isKinematic = false;
                player.TakeDamage(new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1.5f), player.transform.position.y - 0.5f, 0f), 5f, 1f, this.user.transform.forward.z * 700f, 900f);

                this.user.GiveSuperCharge(3f);
                player.GiveSuperCharge(1.5f);

                if (player.soundEffects != null)
                    player.soundEffects.PlayHitSound();
            }

            player.animations.SetDefaultPose();

            

            this.user.rb.isKinematic = false;

            

            this.grabbedPlayer = null;
        }
    }

    public override void Stop()
    {
        base.Stop();

        if (this.particle != null)
            this.particle.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.grabbing = false;

        this.user.rb.isKinematic = false;

        this.user.knockbackInvounrability = false;

        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.rb.isKinematic = false;
            this.grabbedPlayer.knockbackInvounrability = false;

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            this.grabbedPlayer.animations.SetDefaultPose();

            this.grabbedPlayer = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayBloodEffect(int charId = 0)
    {
        if(charId == 2)
        {
            if (this.oilEffect != null)
                this.oilEffect.Play();
        }
        else
        {
            if (this.bloodEffect != null)
                this.bloodEffect.Play();
        }
    }
}
