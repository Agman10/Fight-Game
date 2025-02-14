using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperKnifePunishmentAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public KnifePunishmentHandler punishmentKnives;

    public TestPlayer grabbedPlayer;
    public TestHitbox hitbox;
    public GameObject ghost;

    public GameObject hitParticle;

    public GameObject knivesRight;
    public GameObject knivesLeft;
    public GameObject book;

    public AudioSource takeOutKnivesSfx;
    public AudioSource knifeAppearSfx;

    public AudioSource ghostSwooshSfx;

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

        this.animations.KnifePunishmentStart();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.3f);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);*/

        if (this.ghost != null)
            this.ghost.SetActive(true);

        if(this.ghostSwooshSfx != null)
        {
            this.ghostSwooshSfx.time = 0.06f;
            this.ghostSwooshSfx.Play();
        }

        yield return new WaitForSeconds(0.2f);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        if (this.ghost != null)
            this.ghost.SetActive(false);

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
        if (player != null && !player.dead && !player.countering && !player.knockbackInvounrability)
        {
            //this.grabbing = true;
            this.grabbedPlayer = player;

            player.OnHit?.Invoke();

            player.LookAtTarget();

            this.grabbedPlayer.preventDeath = true;

            player.lookAtPlayer();

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.33f), this.user.gameObject.transform.position.y + 1.74f, 0f);

            if (this.hitParticle != null)
            {
                GameObject hitParticlePrefab = this.hitParticle;
                hitParticlePrefab = Instantiate(hitParticlePrefab, new Vector3(player.transform.position.x, 1.75f, -0.5f), Quaternion.Euler(0, 0, 0));
            }

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
        else if (player != null && !player.dead && !player.countering && player.knockbackInvounrability)
        {
            //player.TakeDamage(this.user.transform.position, 10f, 0f, 0f, 0f, false, true, false, true);
            player.TakeDamage(this.user.transform.position, 10f, 0f, 1000f, 1000f, true, true, false, false, false, false, true);
            if (this.hitParticle != null)
            {
                GameObject hitParticlePrefab = this.hitParticle;
                hitParticlePrefab = Instantiate(hitParticlePrefab, new Vector3(player.transform.position.x, 1.75f, -0.5f), Quaternion.Euler(0, 0, 0));
            }
        }
    }


    private IEnumerator KnifePunishmentCoroutine(TestPlayer player)
    {
        float newY = 0f;

        float currentTime = 0;
        float testTime = 0f;
        float duration = 0.05f;
        float targetPosition = 0f;
        float start = player.transform.position.y;

        float startPosX = player.animations.body.localPosition.x;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            testTime += Time.deltaTime;

            newY = Mathf.Sin(testTime * 100f);
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        //this.animations.rightArmJoint.localEulerAngles = new Vector3(this.animations.rightArmJoint.localEulerAngles.x, this.animations.rightArmJoint.localEulerAngles.y, 39f);

        currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        this.animations.KnifePunishmentWryPoseAnim(0);
        if (this.takeOutKnivesSfx != null)
            this.takeOutKnivesSfx.Play();
        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        this.animations.KnifePunishmentWryPoseAnim(1);

        
        //this.animations.SetEyes(0);

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        if (this.knivesRight != null) this.knivesRight.SetActive(true);
        if (this.knivesLeft != null) this.knivesLeft.SetActive(true);

        currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        //player.ragdoll.transform.localScale = new Vector3(0.1f, 0.1f, player.transform.forward.z * 0.1f);

        

        this.user.ragdoll.transform.localScale = new Vector3(0f, 0f, 0f);

        if (this.knivesRight != null) this.knivesRight.SetActive(false);
        if (this.knivesLeft != null) this.knivesLeft.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.PauseMusic();

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);

        if(GameManager.Instance != null)
        {
            while (GameManager.Instance.gameIsPaused)
                yield return null;
        }

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
            GameManager.Instance.UnPauseMusic();


        /*currentTime = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }*/

        this.TeleportAwayIfTooClose(player);

        this.user.ragdoll.transform.localScale = new Vector3(1f, 1f, this.user.transform.forward.z * 1f);

        this.animations.Book();
        if (this.book != null) this.book.SetActive(true);

        if (this.punishmentKnives != null)
        {
            KnifePunishmentHandler knifePunishmentPrefab = this.punishmentKnives;
            knifePunishmentPrefab = Instantiate(knifePunishmentPrefab, new Vector3(player.transform.position.x, 1.95f, 0f), Quaternion.Euler(0, 0, 0));
            knifePunishmentPrefab.SetOwner(this.user);
        }

        if (this.knifeAppearSfx != null)
            this.knifeAppearSfx.Play();

        //yield return new WaitForSeconds(1.1f);
        currentTime = 0;
        duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }

        if (player.health <= 0)
        {
            //this.StopGrab(player);
            this.RemovePlayer(player);
            player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * 5), player.transform.position.y + -2.5f, 0f), 0, 0f, 0f, 0f, true, false, false, false, true, false, true);

            /*this.animations.BookStart();

            yield return new WaitForSeconds(0.1f);*/

            if (this.book != null) this.book.SetActive(false);
            this.animations.SetDefaultPose();
        }
        else
        {
            //player.animations.HoodGuyGrabbed();
            player.animations.KnifePunishmentFalling(0);



            player.rb.isKinematic = false;
            player.knockbackInvounrability = false;

            player.AddKnockback(player.transform.forward.z * -300f, 600f);
            yield return new WaitForSeconds(0.1f);
            currentTime = 0;
            duration = 1f;
            while (currentTime < duration && Mathf.Abs(player.rb.velocity.y) > 0f && !player.dead)
            {
                currentTime += Time.deltaTime;
                if (Mathf.Abs(player.rb.velocity.y) <= 0f)
                    player.rb.velocity = new Vector3(0f, player.rb.velocity.y, 0f);
                yield return null;
            }

            //player.animations.LayingDownPose();
            //player.animations.LayDown();
            if (!player.dead)
            {
                player.animations.KnifePunishmentFalling(1);
                player.rb.velocity = new Vector3(0f, player.rb.velocity.y, 0f);
            }

            

            yield return new WaitForSeconds(0.15f);

            if (this.book != null) this.book.SetActive(false);
            this.animations.SetDefaultPose();
            yield return new WaitForSeconds(0.15f);

            if (!player.dead)
                player.animations.SetDefaultPose();
            yield return new WaitForSeconds(0.1f);

            //yield return new WaitForSeconds(0.3f);

            //yield return new WaitForSeconds(1.3f);

            /*currentTime = 0;
            duration = 1f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                testTime += Time.deltaTime;
                newY = Mathf.Sin(testTime * 100f);
                player.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
                yield return null;
            }*/

            /*this.animations.BookStart();
            if (this.book != null) this.book.SetActive(false);
            yield return new WaitForSeconds(0.1f);*/

            /*if (this.book != null) this.book.SetActive(false);
            this.animations.SetDefaultPose();*/

            this.StopGrab(player);
        }

        
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        if (this.ghost != null)
            this.ghost.SetActive(false);

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

        if (this.knivesRight != null) this.knivesRight.SetActive(false);
        if (this.knivesLeft != null) this.knivesLeft.SetActive(false);
        if (this.book != null) this.book.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            //this.grabbing = false;

            //player.rb.isKinematic = false;

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);


            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            if (!player.dead)
            {
                player.rb.isKinematic = false;

                player.animations.SetDefaultPose();
                //player.TakeDamage(new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1.5f), player.transform.position.y - 0.5f, 0f), 5f, 1f, this.user.transform.forward.z * 700f, 900f);

                /*this.user.GiveSuperCharge(3f);
                player.GiveSuperCharge(1.5f);

                if (player.soundEffects != null)
                    player.soundEffects.PlayHitSound();*/
            }

            //player.animations.SetDefaultPose();


            this.user.rb.isKinematic = false;

            this.grabbedPlayer = null;
        }
    }

    public void RemovePlayer(TestPlayer player)
    {
        if (player != null)
        {

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

            //player.animations.SetDefaultPose();


            this.user.rb.isKinematic = false;

            this.grabbedPlayer = null;
        }
    }

    public void TeleportAwayIfTooClose(TestPlayer player)
    {
        if(this.user != null && player != null)
        {
            //Debug.Log(Mathf.Abs(this.user.transform.position.x - player.transform.position.x));
            if(Mathf.Abs(this.user.transform.position.x - player.transform.position.x) < 4.5f)
            {
                float newXPos = player.transform.position.x + (4.5f * player.transform.forward.z);

                float maxXPos = 14f;

                if(GameManager.Instance != null && GameManager.Instance.gameMode == 1)
                    maxXPos = 11f;

                if (Mathf.Abs(newXPos) > maxXPos)
                {
                    newXPos = player.transform.position.x + (4.5f * -player.transform.forward.z);
                    //this.user.lookAtPlayer();
                }

                this.user.transform.position = new Vector3(newXPos, this.user.transform.position.y, 0f);

                this.user.lookAtPlayer();
            }
        }
    }
}
