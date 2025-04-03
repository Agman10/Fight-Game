using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlameGrabMasterAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public TestHitbox hitbox;
    public TestPlayer grabbedPlayer;

    public VisualEffect fire;
    public GameObject explosionEffect;
    public GameObject trail;

    public AudioSource explosionImpactSfx;
    public AudioSource flameSfx;

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
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryGrabCoroutine());
                }
                ;
            }

            
        }
    }

    private IEnumerator TryGrabCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.FlameGrabStartPose();*/

        if (this.animations != null)
            this.animations.MasterFlameGrabStartPose();

        if(GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            float distance = this.user.transform.position.x - GameManager.Instance.gameCamera.transform.position.x;

            this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.user.transform.forward.z * 90f + (distance * 5), this.animations.body.localEulerAngles.z);
        }


        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            float xOffset = 0f;
            if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            {
                float distance = this.user.transform.position.x - GameManager.Instance.gameCamera.transform.position.x;

                xOffset = this.user.transform.forward.z * (-distance * 0.1f);
                xOffset = (-distance * 0.1f);
            }


            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + xOffset, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);

        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        /*if (this.animations != null)
            this.animations.FlameGrabDash();*/

        if (this.animations != null)
            this.animations.MasterFlameGrabDash();

        float currentTime = 0;
        float duration = 1.4f;
        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;
            if (this.user.rb != null)
                this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 25f, 0f, 0f);

            yield return null;
        }


        if (this.trail != null)
            this.trail.SetActive(false);

        if (this.grabbedPlayer == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            if (this.user != null && this.user.rb != null)
                this.user.rb.velocity = Vector3.zero;

            //yield return new WaitForSeconds(0.1f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        float forwardZ = this.user.transform.forward.z;
        float pos = this.user.transform.position.x;
        int charId = player.characterId;

        float xForward = 1.6f;

        float xMax = 14f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
            xMax = 11f;

        //float maxX = 10.5f;

        if (charId == 3 || charId == 4 || charId == 7)
        {
            //maxX = 10.5f;
            xForward = 1.75f;
        }

        //float maxX = 14f - xForward;
        float maxX = xMax - xForward;

        if (pos > maxX && forwardZ == 1 || pos < -maxX && forwardZ == -1)
        {
            /*float currentTime2 = 0;
            float duration2 = 0.15f;
            while (currentTime2 < duration2)
            {
                this.user.transform.position = new Vector3(Mathf.Lerp(this.user.transform.position.x, maxX * forwardZ, currentTime2 / duration2), this.user.transform.position.y, 0f);
                player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), player.transform.position.y, 0f);

                currentTime2 += Time.deltaTime;
                yield return null;
            }*/

            this.user.transform.position = new Vector3(maxX * forwardZ, this.user.transform.position.y, 0f);
            player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), player.transform.position.y, 0f);
        }
        /*else
        {
            yield return new WaitForSeconds(0.15f);
        }*/
        //yield return new WaitForSeconds(0.35f);





        yield return new WaitForSeconds(0.5f);
        this.PlayFire(true);
        if (this.flameSfx != null)
            this.flameSfx.Play();

        float time = 0.1f;
        float damage = 5f;

        int amount = 7;
        while (amount > 0)
        {
            yield return new WaitForSeconds(time);
            player.TakeDamage(this.user.transform.position, damage);
            if (player.soundEffects != null)
                player.soundEffects.PlayHitSound();

            amount -= 1;
            yield return null;
        }

        this.animations.MasterFlameGrab(1);
        amount = 2;
        while (amount > 0)
        {
            yield return new WaitForSeconds(time);
            player.TakeDamage(this.user.transform.position, damage);
            if (player.soundEffects != null)
                player.soundEffects.PlayHitSound();

            amount -= 1;
            yield return null;
        }

        //yield return new WaitForSeconds(1);
        this.PlayFire(false);
        yield return new WaitForSeconds(time);
        player.TakeDamage(this.user.transform.position, damage);
        if (player.soundEffects != null)
            player.soundEffects.PlayHitSound();


        this.animations.MasterFlameGrab(2);

        yield return new WaitForSeconds(0.02f);

        this.animations.MasterFlameGrab(3);

        if (this.explosionEffect != null)
        {
            GameObject explosionPrefab = this.explosionEffect;
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.284f), 2.9f, 0), Quaternion.Euler(0, 0, 0));
        }

        if (this.explosionImpactSfx != null)
        {
            //this.explosionImpactSfx.PlaySound();
            this.explosionImpactSfx.time = 0.08f;
            this.explosionImpactSfx.Play();
        }

        player.TakeDamage(this.user.transform.position, 20);
        if (player.soundEffects != null)
            player.soundEffects.PlayHitSound();

        float currentTime = 0;
        float duration = 0.5f;
        float startPosX = player.animations.body.localPosition.x;
        float newY = 0f;
        float testTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.05f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }
        //yield return new WaitForSeconds(0.5f);


        this.StopGrab(player);
        //player.animations.KnifePunishmentFalling(0);

        //player.TakeDamage(this.user.transform.position, 10f, 0.5f, this.user.transform.forward.z * 1000f, 1000f, true, true, false, false, true, false, true);
        //player.TakeDamage(this.user.transform.position, 0f, 0.5f, this.user.transform.forward.z * 1000f, 1000f, true, true, false, false, true, false, true);

        /*if (player.soundEffects != null)
            player.soundEffects.PlayHitSound();*/

        yield return new WaitForSeconds(0.5f);

        this.animations.MasterFlameGrab(4);

        yield return new WaitForSeconds(0.02f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

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

            this.animations.MasterFlameGrab(0);
            player.animations.SetDefaultPose();
            player.animations.MasterFlameGrabbed();

            Vector3 pos = this.user.transform.position;
            int charId = player.characterId;

            float xForward = 1.6f;
            //float xForward = 1.58f;
            float yUp = 0.88f;

            if(charId == 3 || charId == 4 || charId == 7)
            {
                //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * 1.75f), pos.y + 1.4f, 0f);

                xForward = 1.75f;
                yUp = 1.4f;
            }
            else if (charId == 9)
            {
                //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * 1.6f), pos.y + 0.74f, 0f);

                //xForward = 1.6f;
                yUp = 0.74f;
            }
            /*else
            {
                //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * 1.6f), pos.y + 0.88f, 0f);

                xForward = 1.6f;
                yUp = 0.88f;
            }*/


            if (this.fire != null)
                this.fire.transform.localPosition = new Vector3(xForward + 0.05f, this.fire.transform.localPosition.y, this.fire.transform.localPosition.z);

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

            if (!player.dead)
            {
                player.rb.isKinematic = false;
                player.TakeDamage(this.user.transform.position, 0f, 1f, this.user.transform.forward.z * 1000f, 1000f, true, true, false, false, true, false, true);

                if (!player.dead)
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

            if (this.grabbedPlayer.health > 0f)
                this.grabbedPlayer.animations.SetDefaultPose();

            if (this.onGoing && this.grabbedPlayer.health <= 0f)
            {
                this.grabbedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false, false, false, true, false, true);
            }

            this.grabbedPlayer = null;
        }

        if (this.trail != null)
            this.trail.SetActive(false);

        this.PlayFire(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayFire(bool playing)
    {
        if (playing)
        {
            if (this.fire != null)
                this.fire.Play();
        }
        else
        {
            if (this.fire != null)
                this.fire.Stop();
        }
    }
}
