using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageKickSuper : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool moving;
    public GameObject startParticle;
    public GameObject rageSymbol;

    public TestHitbox hitbox;
    public TestHitbox kickHitbox;
    public TestPlayer grabbedPlayer;
    public GameObject hitParticle;

    private float extraDamage = 0f;

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

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        /*int amount = 20;
        int maxAmount = 50;
        float animSpeed = 0.001f;
        int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            if (this.animations != null)
                this.animations.DemonRageKickStart(animId);

            //yield return new WaitForSeconds(0.03f);
            yield return new WaitForSeconds(animSpeed);

            if (animId == 0)
                animId = 1;
            else
                animId = 0;

            amount -= 1;
            maxAmount -= 1;

            if (amount <= 0 && maxAmount > 0 && this.user.input.super)
                amount = 1;

            if (this.rageSymbol != null && maxAmount <= 6)
                this.rageSymbol.SetActive(true);

            yield return null;
        }*/


        /*int amount = 10;
        int maxAmount = 40;
        float animSpeed = 0.01f;
        //int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //Debug.Log("start");
            if (this.animations != null)
                this.animations.DemonRageKickStart(0);

            //yield return new WaitForSeconds(0.03f);
            yield return new WaitForSeconds(animSpeed);

            if (this.animations != null)
                this.animations.DemonRageKickStart(1);

            yield return new WaitForSeconds(animSpeed);
            //Debug.Log("end");

            amount -= 1;
            maxAmount -= 1;

            if (amount <= 0 && maxAmount > 0 && this.user.input.super)
                amount = 1;

            if (this.rageSymbol != null && maxAmount <= 6)
                this.rageSymbol.SetActive(true);

            yield return null;
        }*/

        int amount = 3;
        float animSpeed = 0.001f;
        int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            if (this.animations != null)
                this.animations.DemonRageKickStartStart(animId);

            //yield return new WaitForSeconds(0.03f);
            yield return new WaitForSeconds(animSpeed);

            if (animId == 0)
                animId = 1;
            else
                animId = 0;

            amount -= 1;

            yield return null;
        }

        amount = 7;
        animSpeed = 0.01f;
        //int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //Debug.Log("start");
            if (this.animations != null)
                this.animations.DemonRageKickStart(0);

            //yield return new WaitForSeconds(0.03f);
            yield return new WaitForSeconds(animSpeed);

            if (this.animations != null)
                this.animations.DemonRageKickStart(1);

            yield return new WaitForSeconds(animSpeed);
            //Debug.Log("end");

            amount -= 1;

            yield return null;
        }

        amount = 20;
        animSpeed = 0.01f;
        float extraRage = 0f;
        //int animId = 0;
        //bool idForward = true;
        while (amount > 0 && this.user.input.super)
        {
            //Debug.Log("start");
            if (this.animations != null)
                this.animations.DemonRageKickStart(0);

            //yield return new WaitForSeconds(0.03f);
            yield return new WaitForSeconds(animSpeed);

            if (this.animations != null)
                this.animations.DemonRageKickStart(1);

            yield return new WaitForSeconds(animSpeed);
            //Debug.Log("end");

            amount -= 1;
            extraRage++;

            if (this.rageSymbol != null && amount <= 5)
                this.rageSymbol.SetActive(true);

            /*if (amount <= 0 && this.user.input.super)
                amount = 1;*/

            yield return null;
        }

        Debug.Log(extraRage);
        //float extraDistance = 0.25f * extraRage;
        float extraDistance = 1.5f * extraRage;
        
        this.extraDamage = 0.25f * extraRage;



        if (this.rageSymbol != null)
            this.rageSymbol.SetActive(false);

        if (this.animations != null)
            this.animations.DemonRageKickLaunch(0);

        this.moving = true;

        //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * (30f + extraRage), 0f, 0f);
        this.user.rb.velocity = new Vector3(this.user.transform.forward.z * (25f + extraDistance), 0f, 0f);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.DemonRageKickLaunch(1);

        yield return new WaitForSeconds(0.25f);


        //yield return new WaitForSeconds(0.3f);



        /*float currentTime = 0;
        float duration = 0.25f;
        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * (30f + extraRage), 0f, 0f);

            yield return null;
        }*/

        if (this.grabbedPlayer == null)
        {
            this.extraDamage = 0f;

            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 5f, 0f, 0f);

            if (this.animations != null)
                this.animations.DemonRageKickLaunch(2);
            yield return new WaitForSeconds(0.15f);

            if (this.animations != null)
                this.animations.DemonRageKickLaunch(3);
            yield return new WaitForSeconds(0.01f);

            if (this.animations != null)
                this.animations.DemonRageKickLaunch(4);

            yield return new WaitForSeconds(0.1f);

            this.moving = false;

            this.user.rb.velocity = new Vector3(0f, 0f, 0f);


            //kick starts here

            if (this.animations != null)
                this.animations.DemonRageKickMissKick(0);

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.DemonRageKickMissKick(1);

            yield return new WaitForSeconds(0.01f);

            if (this.animations != null)
                this.animations.DemonRageKickMissKick(2);


            yield return new WaitForSeconds(0.01f);

            if (this.animations != null)
                this.animations.DemonRageKickMissKick(3);

            if (this.kickHitbox != null)
                this.kickHitbox.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            if (this.animations != null)
                this.animations.DemonRageKickMissKick(2);

            if (this.kickHitbox != null)
                this.kickHitbox.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.01f);


            /*if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);*/

            //yield return new WaitForSeconds(0.1f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        float newY = 0f;

        float currentTime = 0;
        float testTime = 0f;
        float duration = 0.05f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        float startUser = this.user.transform.position.y;

        float startPosX = player.animations.body.localPosition.x;


        float xForward = 2.25f;
        float victimStartPosX = player.transform.position.x;
        float victimEndPosX = this.user.transform.position.x + (this.user.transform.forward.z * xForward);

        float shakeDistance = 0.02f;

        float forwardZ = this.user.transform.forward.z;
        float pos = this.user.transform.position.x;
        
        float xMax = 14f;
        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
            xMax = 11f;

        float maxX = xMax - xForward;

        Vector3 particleYZPos = new Vector3(0f, 2.15f, -0.5f);

        if (pos > maxX && forwardZ == 1 || pos < -maxX && forwardZ == -1)
        {
            /*float currentTime2 = 0;
            float duration2 = 0.15f;
            while (currentTime2 < duration2)
            {
                this.user.transform.position = new Vector3(Mathf.Lerp(this.user.transform.position.x, maxX * forwardZ, currentTime2 / duration2), this.user.transform.position.y, 0f);
                //player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), player.transform.position.y, 0f);

                currentTime2 += Time.deltaTime;
                yield return null;
            }*/
            //this.user.transform.position = new Vector3(maxX * forwardZ, this.user.transform.position.y, 0f);
            this.user.transform.position = new Vector3(maxX * forwardZ, 0f, 0f);
            //player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), player.transform.position.y, 0f);
            player.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * xForward), 0f, 0f);

            if (this.hitParticle != null)
            {
                GameObject hitParticlePrefab = this.hitParticle;
                hitParticlePrefab = Instantiate(hitParticlePrefab, new Vector3(player.transform.position.x + (player.transform.forward.z * 1f), particleYZPos.y, particleYZPos.z), Quaternion.Euler(0, 0, 0));
            }
        }
        else
        {
            if (this.hitParticle != null)
            {
                GameObject hitParticlePrefab = this.hitParticle;
                hitParticlePrefab = Instantiate(hitParticlePrefab, new Vector3(player.transform.position.x + (player.transform.forward.z * -0.5f), particleYZPos.y, particleYZPos.z), Quaternion.Euler(0, 0, 0));
            }

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                /*testTime += Time.deltaTime;
                newY = Mathf.Sin(testTime * 100f);
                player.animations.body.localPosition = new Vector3(startPosX + (newY * shakeDistance), player.animations.body.localPosition.y, player.animations.body.localPosition.z);*/

                player.transform.position = new Vector3(Mathf.Lerp(victimStartPosX, victimEndPosX, currentTime / duration), Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
                this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startUser, targetPosition, currentTime / duration), 0);
                yield return null;
            }
        }

        

        //player.transform.position = new Vector3(victimEndPosX, 0f, 0);

        currentTime = 0;
        duration = 0.7f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * shakeDistance), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            yield return null;
        }





        if (this.animations != null)
            this.animations.DemonRageKickLaunch(2);
        currentTime = 0;
        duration = 0.1f;
        pos = this.user.transform.position.x;
        float newPosDistance = forwardZ * 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * shakeDistance), player.animations.body.localPosition.y, player.animations.body.localPosition.z);

            this.user.transform.position = new Vector3(Mathf.Lerp(pos, pos + newPosDistance, currentTime / duration), this.user.transform.position.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.DemonRageKickLaunch(3);

        currentTime = 0;
        duration = 0.01f;
        pos = this.user.transform.position.x;
        newPosDistance = forwardZ * 0.05f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * shakeDistance), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            this.user.transform.position = new Vector3(Mathf.Lerp(pos, pos + newPosDistance, currentTime / duration), this.user.transform.position.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.DemonRageKickLaunch(4);

        currentTime = 0;
        duration = 0.2f;
        pos = this.user.transform.position.x;
        newPosDistance = forwardZ * 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;
            newY = Mathf.Sin(testTime * 100f);
            player.animations.body.localPosition = new Vector3(startPosX + (newY * shakeDistance), player.animations.body.localPosition.y, player.animations.body.localPosition.z);
            this.user.transform.position = new Vector3(Mathf.Lerp(pos, pos + newPosDistance, currentTime / duration), this.user.transform.position.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();






        //yield return new WaitForSeconds(1);

        this.StopGrab(player);

        yield return new WaitForSeconds(0.1f);

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

            player.TakeDamage(this.user.transform.position, 5f + this.extraDamage, 0f, 0f, 0f, false, true, false, true);

            this.extraDamage = 0f;

            /*if (this.hitParticle != null)
            {
                GameObject hitParticlePrefab = this.hitParticle;
                hitParticlePrefab = Instantiate(hitParticlePrefab, new Vector3(player.transform.position.x + (player.transform.forward.z * -0.5f), 2f, -0.5f), Quaternion.Euler(0, 0, 0));
            }*/

            this.moving = false;

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

                //player.animations.SetDefaultPose();

                player.TakeDamage(this.user.transform.position, 45f, 1f, this.user.transform.forward.z * 1000f, 600f, true, true, false, false, true, false, true, true);

                /*player.TakeDamage(this.user.transform.position, 5f, 1f, this.user.transform.forward.z * 1000f, 1000f, true, true, false, false, true, false, true);

                if (!player.dead)
                    player.animations.SetDefaultPose();*/
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

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(false);

        if (this.rageSymbol != null)
            this.rageSymbol.SetActive(false);

        this.user.knockbackInvounrability = false;
        this.user.rb.isKinematic = false;

        this.moving = false;


        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.rb.isKinematic = false;
            this.grabbedPlayer.knockbackInvounrability = false;

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            //this.grabbedPlayer.animations.SetDefaultPose();

            if (this.grabbedPlayer.health > 0f)
                this.grabbedPlayer.animations.SetDefaultPose();

            if (this.onGoing && this.grabbedPlayer.health <= 0f)
            {
                this.grabbedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false, false, false, true, false, true);
            }

            this.grabbedPlayer = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
