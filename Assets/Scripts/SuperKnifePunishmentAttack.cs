using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperKnifePunishmentAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public TestPlayer grabbedPlayer;
    public TestHitbox hitbox;

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
            this.hitbox.OnPlayerCollision += Grab;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= Grab;
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
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TemplateCoroutine());
                }
            }

            
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.2f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        /*while (this.grabbedPlayer == null)
        {
            yield return null;
        }*/

        if (this.grabbedPlayer == null)
        {
            if (this.animations != null)
                this.animations.SetDefaultPose();

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }

        
    }


    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead && !player.countering)
        {
            //this.grabbing = true;
            this.grabbedPlayer = player;

            player.OnHit?.Invoke();

            player.LookAtTarget();

            this.grabbedPlayer.preventDeath = true;

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.33f), this.user.gameObject.transform.position.y + 1.74f, 0f);


            player.TakeDamage(this.user.transform.position, 5.75f, 0f, 0f, 0f, false, true, false, true);

            /*this.user.GiveSuperCharge(3f);
            player.GiveSuperCharge(1.5f);*/

            //this.PlayBloodEffect(player.characterId);

            //player.animations.HoodGuyGrabbed();
            player.animations.KnifePunishmentHit();

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

            player.attackStuns.Add(this.gameObject);

            this.StartCoroutine(this.KnifePunishmentCoroutine(player));
        }
    }


    private IEnumerator KnifePunishmentCoroutine(TestPlayer player)
    {
        float newY = 0f;

        float currentTime = 0;
        float testTime = 0f;
        float duration = 0.025f;
        float targetPosition = 0f;
        float start = player.transform.position.y;

        float startPosX = this.animations.body.localPosition.x;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            testTime += Time.deltaTime;

            newY = Mathf.Sin(testTime * 100f);
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        currentTime = 0;
        duration = 2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            testTime += Time.deltaTime;

            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        this.StopGrab(player);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
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

            this.grabbedPlayer = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            //this.grabbing = false;

            player.rb.isKinematic = false;

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);


            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            if (!player.dead)
            {
                player.rb.isKinematic = false;
                //player.TakeDamage(new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1.5f), player.transform.position.y - 0.5f, 0f), 5f, 1f, this.user.transform.forward.z * 700f, 900f);

                /*this.user.GiveSuperCharge(3f);
                player.GiveSuperCharge(1.5f);

                if (player.soundEffects != null)
                    player.soundEffects.PlayHitSound();*/
            }

            player.animations.SetDefaultPose();


            this.user.rb.isKinematic = false;

            this.grabbedPlayer = null;
        }
    }
}
