using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJoeButtSlam : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public TestHitbox hitbox;
    public TestPlayer grabbedPlayer;

    public ParticleSystem hitEffect;

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
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
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

        //this.animations.SuperExplosiveGrabStartPose();
        this.animations.FlameGrabStartPose();
        this.animations.InversePose();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.4f);

        //MAYBE REPLACE IT WITH A TACKLE ANIMATION
        //this.animations.DemonRageKickLaunch(1);
        this.animations.JoeTackle();

        this.moving = true;

        //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * (30f + extraRage), 0f, 0f);

        this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 15f, 0f, 0f);

        /*if (this.trail != null)
            this.trail.SetActive(true);*/

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.3f;

        float startVelocity = 15f;

        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;
            float velocity = Mathf.Lerp(startVelocity, 2f, currentTime / duration);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * velocity, 0f, 0f);

            yield return null;
        }

        //this.moving = false;
        /*if (this.trail != null)
            this.trail.SetActive(false);*/

        if (this.grabbedPlayer == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            //this.user.rb.velocity = new Vector3(0f, 0f, 0f);

            //yield return new WaitForSeconds(0.1f);

            

            //this.animations.DemonRageKickLaunch(2);
            this.animations.JoeTackleMiss();

            yield return new WaitForSeconds(0.1f);

            //this.animations.DemonRageKickLaunch(3);

            this.user.rb.velocity = new Vector3(0f, 0f, 0f);
            this.moving = false;

            yield return new WaitForSeconds(0.2f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
        else
        {
            this.moving = false;
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.RollAnimation();

        float currentTime = 0;
        float duration = 0.2f;
        float startX = this.user.transform.position.x;
        float startY = this.user.transform.position.y;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, -2000f * Time.deltaTime));


            this.user.transform.position = new Vector3(
                Mathf.Lerp(startX, player.transform.position.x, currentTime / duration),
                Mathf.Lerp(startY, 3f, currentTime / duration),
                0f);
            yield return null;
        }

        this.animations.JoeButtSlam(0);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 3.95f, 0f);
        currentTime = 0;
        duration = 0.15f;
        startY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startY, 0f, currentTime / duration), 0f);
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startY, 0.57f, currentTime / duration), 0f);
            yield return null;
        }

        this.animations.JoeButtSlam(1);
        player.animations.JoeButtSlamLaying(1);

        player.TakeDamage(this.user.transform.position, 23f, 0f, 0f, 0f, false, true, false, true);
        player.soundEffects.PlayHitSound();


        /*this.user.transform.position = new Vector3(this.user.transform.position.x, -0.05f, 0f);
        player.transform.position = new Vector3(player.transform.position.x, -0.05f, 0f);*/

        

        if (player.health <= 0f)
        {
            this.user.rb.isKinematic = false;
            player.preventDeath = false;
            player.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, false);

            yield return new WaitForSeconds(0.025f);

            this.StopGrab(player);

            yield return new WaitForSeconds(1f);

            this.user.rb.AddForce(this.user.transform.forward.z * -50f, 0f, 0f);

            this.animations.SetDefaultPose();

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
        else
        {
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, this.animations.body.localPosition.y - 0.05f, this.animations.body.localPosition.z);
            player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, player.animations.body.localPosition.y - 0.05f, player.animations.body.localPosition.z);

            yield return new WaitForSeconds(0.05f);

            this.animations.JoeButtSlam(1);
            player.animations.JoeButtSlamLaying(1);

            /*this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
            player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);*/


            yield return new WaitForSeconds(0.3f);
            player.animations.JoeButtSlamLaying(2);

            yield return new WaitForSeconds(1);

            /*currentTime = 0;
            duration = 0.2f;
            startX = this.user.transform.position.x;
            startY = this.user.transform.position.y;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (this.animations != null)
                    this.animations.body.transform.Rotate(new Vector3(0f, 0f, -2000f * Time.deltaTime));


                this.user.transform.position = new Vector3(
                    Mathf.Lerp(startX, player.transform.position.x, currentTime / duration),
                    Mathf.Lerp(startY, 3f, currentTime / duration),
                    0f);
                yield return null;
            }*/

            /*this.StopGrab(player);

            this.user.rb.isKinematic = false;

            if (this.animations != null)
                this.animations.SetDefaultPose();

            this.StopGrab(player);

            yield return new WaitForSeconds(0.4f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);*/



            //this.StopGrab(player);

            this.user.rb.isKinematic = false;

            this.user.rb.AddForce(this.user.transform.forward.z * -550f, 800f, 0f);

            this.animations.RollAnimation();
            currentTime = 0;
            duration = 5.2f;
            //float duration = 0.05f;
            while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
            {
                currentTime += Time.deltaTime;
                if (this.animations != null)
                    this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));

                yield return null;
            }

            this.animations.SetDefaultPose();
            player.animations.KnifePunishmentFalling(1);

            yield return new WaitForSeconds(0.1f);

            this.StopGrab(player);
            //player.AddStun(0.1f, true);

            

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }


        
    }

    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead)
        {
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

                player.TakeDamage(this.user.transform.position, 5f, 0f, 0f, 0f, false, true, false, true);
                player.soundEffects.PlayHitSound();

                player.animations.JoeButtSlamLaying(0);
                player.animations.SetEyes(2);

                if (this.hitEffect != null)
                    this.hitEffect.Play();

                /*Vector3 pos = this.user.transform.position;
                //float xForward = 1.25f;
                float xForward = 1.75f;
                player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), 0f, 0f);*/


                float xForward = 1.75f;

                float forwardZ = this.user.transform.forward.z;
                float posX = this.user.transform.position.x;

                float xMax = 14f;
                if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
                    xMax = 11f;

                float maxX = xMax - xForward;

                if (posX > maxX && forwardZ == 1 || posX < -maxX && forwardZ == -1)
                {
                    this.user.transform.position = new Vector3(maxX * forwardZ, 0f, 0f);
                    player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), 0f, 0f);
                }
                else
                {
                    player.transform.position = new Vector3(posX + (this.user.transform.forward.z * xForward), 0f, 0f);
                }
                

                this.StartCoroutine(this.GrabbingCoroutine(player));
            }
            else
            {
                player.OnHitFromPlayer?.Invoke(this.user);
            }
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
            //this.user.rb.isKinematic = false;

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
