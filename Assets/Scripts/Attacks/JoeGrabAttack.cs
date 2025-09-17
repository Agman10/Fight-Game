using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeGrabAttack : Attack
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

        //MAKE IT SO HE CAN GRAB THE BALL IN THE FUTURE

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
        /*if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }*/

        if (this.onGoing && this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y, 0);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TryGrabCoroutineGrounded());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TryGrabCoroutineGrounded()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
            this.user.rb.AddForce(0, 300, 0);*/

        /*if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }*/

        if (this.animations != null)
            this.animations.JoeGrabStart(0);
        //yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(0.15f);
        this.animations.JoeGrabStart(1);
        yield return new WaitForSeconds(0.1f);


        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.JoeGrabStart(2);
        yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);


        if (this.grabbedPlayer == null)
        {
            /*if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);*/

            //yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
            this.animations.JoeGrabStart(3);
            yield return new WaitForSeconds(0.3f);
            this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.2f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutineGrounded(TestPlayer player)
    {
        Vector3 pos = this.user.transform.position;
        int charId = player.characterId;

        if (charId == 3 || charId == 4 || charId == 7)
            this.SetEnemyPos(player, 1.25f, 0.5f);
        else
            this.SetEnemyPos(player, 1.25f, 0f);

        yield return new WaitForSeconds(0.35f);

        this.animations.JoeGrabbing(0);
        player.animations.body.localEulerAngles = new Vector3(0f, 0f, -15f);

        if (charId == 3 || charId == 4 || charId == 7)
            this.SetEnemyYPos(player, 1.1f);
        else
            this.SetEnemyYPos(player, 0.6f);

        

        yield return new WaitForSeconds(0.1f);
        this.animations.JoeGrabbing(1);
        player.animations.JoeGrabbed();
        player.animations.body.localPosition = new Vector3(1.25f, player.animations.body.transform.localPosition.y, player.animations.body.transform.localPosition.z);

        if (charId == 3 || charId == 4 || charId == 7)
            player.animations.body.localPosition = new Vector3(1.45f, player.animations.body.transform.localPosition.y, player.animations.body.transform.localPosition.z);
        else
            player.animations.body.localPosition = new Vector3(1.25f, player.animations.body.transform.localPosition.y, player.animations.body.transform.localPosition.z);

        /*if (charId == 3 || charId == 4 || charId == 7)
            this.SetEnemyPos(player, 0f, 1.5f);
        else
            this.SetEnemyPos(player, -0.2f, 1.95f);*/

        if (charId == 3 || charId == 4 || charId == 7)
            this.SetEnemyYPos(player, 1.95f);
        else
            this.SetEnemyYPos(player, 1.5f);

        float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float startpositionY = this.transform.position.y;

        float targetPositionY = startpositionY + 3.5f;
        if (targetPositionY >= 10.5f)
            targetPositionY = 10.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startpositionY, targetPositionY, currentTime / duration), 0);

            /*if (charId == 3 || charId == 4 || charId == 7)
                this.SetEnemyPos(player, 0f, 1.5f);
            else
                this.SetEnemyPos(player, -0.2f, 1.95f);*/

            if (charId == 3 || charId == 4 || charId == 7)
                this.SetEnemyYPos(player, 1.95f);
            else
                this.SetEnemyYPos(player, 1.5f);

            yield return null;
        }
        yield return new WaitForSeconds(0.05f);

        //yield return new WaitForSeconds(0.1f);
        this.animations.JoeGrabbing(2);

        if (charId == 3 || charId == 4 || charId == 7)
            this.SetEnemyPos(player, 1.25f, 0.5f);
        else
            this.SetEnemyPos(player, 1.25f, 0f);

        player.animations.KnifePunishmentFalling(0);
        this.StopGrab(player);

        

        yield return new WaitForSeconds(0.1f);


        this.user.rb.isKinematic = false;
        yield return new WaitForSeconds(0.025f);




        float animDuration = 0.025f;

        this.animations.body.localEulerAngles = new Vector3(0f, 0f, -65f);
        float waitTime = 0.05f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.animations.body.localEulerAngles = new Vector3(0f, 0f, -90f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.animations.body.localEulerAngles = new Vector3(0f, 0f, -90f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.animations.RollAnimation();
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, -135f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.animations.body.localEulerAngles = new Vector3(0f, 0f, -180f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        //this.animations.body.localEulerAngles = new Vector3(0f, 0f, -225f);
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 135f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        //this.animations.body.localEulerAngles = new Vector3(0f, 0f, -270f);
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 90f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 35f);
        waitTime = animDuration;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }


        if (this.animations != null)
            this.animations.DemonCradle(5);

        waitTime = 1f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if(Mathf.Abs(this.user.rb.velocity.y) == 0f)
            this.animations.RoadRollerEndLand();
        else
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.15f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
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

                player.animations.SetDefaultPose();
                player.animations.KnifePunishmentHit();

                this.animations.SetDefaultPose();
                this.animations.Grabbing2();

                /*Vector3 pos = this.user.transform.position;
                int charId = player.characterId;*/
                /*float xForward = 1.25f;
                //float xForward = 1.58f;
                float yUp = 0f;

                if (charId == 3 || charId == 4 || charId == 7)
                {
                    xForward = 1.25f;
                    yUp = 0.5f;
                }

                //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), 0f, 0f);
                player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), pos.y + yUp, 0f);*/

                /*if (charId == 3 || charId == 4 || charId == 7)
                    this.SetEnemyPos(player, 1.25f, 0.5f);
                else
                    this.SetEnemyPos(player, 1.25f, 0f);*/

                this.StartCoroutine(this.GrabbingCoroutineGrounded(player));
            }
            else
            {
                player.OnHitFromPlayer?.Invoke(this.user);
            }
        }

        
    }

    public void SetEnemyPos(TestPlayer player, float xForward = 1.25f, float yUp = 0f)
    {
        Vector3 pos = this.user.transform.position;
        int charId = player.characterId;
        /*float xForward = 1.25f;
        //float xForward = 1.58f;
        float yUp = 0f;

        if (charId == 3 || charId == 4 || charId == 7)
        {
            xForward = 1.25f;
            yUp = 0.5f;
        }*/

        //player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), 0f, 0f);

        player.transform.position = new Vector3(pos.x + (this.user.transform.forward.z * xForward), pos.y + yUp, 0f);
        //player.transform.position = new Vector3(player.transform.position.x, pos.y + yUp, 0f);
    }

    public void SetEnemyYPos(TestPlayer player, float yUp = 0f)
    {
        Vector3 pos = this.user.transform.position;
        int charId = player.characterId;
        player.transform.position = new Vector3(player.transform.position.x, pos.y + yUp, 0f);
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

                //player.animations.SetDefaultPose();

                //player.TakeDamage(this.user.transform.position, 20f, 1f, this.user.transform.forward.z * 700f, -1000f, true, true, false, false, true, false, true, true, 0f, 0.5f, this.user);
                player.TakeDamage(new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 2f), this.user.transform.position.y + 4.25f, 0f), 18f, 0.75f, this.user.transform.forward.z * 700f, -1000f, true, true, false, false, true, false, true, true, 0f, 0.75f, this.user);
                if (player.soundEffects != null)
                    player.soundEffects.hitSound.PlaySound();
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
