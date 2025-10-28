using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRoadRollerAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public bool canBeCanceled;
    

    public RoadRoller roadRoller;
    public RoadRoller activeRoadRoller;

    public GameObject punchEffect;

    public SoundEffect fallingSfx;
    public SoundEffect finalPunchSfx;

    [HideInInspector] public bool anvilVulnerability;
    [HideInInspector] public bool fallingDown;
    //public SoundEffect landingSfx;

    //public AudioSource explosionSfx;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && this.canBeCanceled)
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
                    this.StartCoroutine(this.RoadRollerCoroutine());
                }
            }

            
        }
    }

    private IEnumerator RoadRollerCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.canBeCanceled = true;

        /*if (this.animations != null)
            this.animations.Wry();*/

        this.animations.KnifePunishmentWryPoseAnim(0);

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        this.anvilVulnerability = true;

        yield return new WaitForSeconds(0.05f);
        this.animations.KnifePunishmentWryPoseAnim(1);

        float testTime = 0f;
        float time = 0.3f;
        float startPosY = this.animations.body.localPosition.y;
        while (time > 0)
        {
            time -= Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (testTime > 0.2f)
                this.anvilVulnerability = false;
            yield return null;
        }

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        if (this.animations != null)
            this.animations.RoadRollerRiseUp();

        this.anvilVulnerability = false;

        float currentTime = 0;
        float duration = 0.4f;
        //float targetVolume = 0.1f;
        float targetPositionY = 20f;
        float startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }

        this.user.knockbackInvounrability = true;
        this.canBeCanceled = false;

        yield return new WaitForSeconds(0.2f);

        /*if(GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = true;
        }*/

        float maxRollerXPos = 11.65f;

        /*if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
        {
            maxRollerXPos = 8.65f;
        }*/

        if(GameManager.Instance != null)
        {
            if (GameManager.Instance.gameMode == 1)
                maxRollerXPos = 8.65f;
            else if (GameManager.Instance.gameCamera != null)
                maxRollerXPos = GameManager.Instance.gameCamera.maxX + 5.15f;
        }

        //Debug.Log(maxRollerXPos);

        if (this.roadRoller != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            if (summonPos > maxRollerXPos && this.user.transform.forward.z <= -1f)
                summonPos = maxRollerXPos;
            else if (summonPos < -maxRollerXPos && this.user.transform.forward.z > -1f)
                summonPos = -maxRollerXPos;

            RoadRoller roadRollerPrefab = this.roadRoller;
            roadRollerPrefab = Instantiate(roadRollerPrefab, new Vector3(summonPos, 17.4f, 0), this.transform.rotation);
            roadRollerPrefab.SetOwner(this.user);

            this.activeRoadRoller = roadRollerPrefab;

            this.user.transform.position = new Vector3(summonPos - (this.user.transform.forward.z * 2.35f), targetPositionY, 0);
            //bigSpherePrefab.belongsTo = this.user;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.animations != null)
            this.animations.RoadRollerFall();

        //this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x - (this.user.transform.forward.z * 2.35f), targetPositionY, 0);

        this.fallingDown = true;
        yield return new WaitForSeconds(0.4f);

        this.fallingSfx.PlaySound();

        currentTime = 0;
        duration = 0.3f;
        //float targetVolume = 0.1f;
        targetPositionY = 2.6f;
        //float targetPositionX = this.user.tempOpponent.transform.position.x - (this.user.transform.forward.z * 2.35f);
        startPositionY = this.user.transform.position.y;
        //float startPositionX = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);

            if (this.activeRoadRoller != null)
                this.activeRoadRoller.transform.position = new Vector3(this.activeRoadRoller.transform.position.x, Mathf.Lerp(startPositionY - 2.6f, 0f, currentTime / duration), 0);
            yield return null;
        }
        if (this.activeRoadRoller != null)
        {
            this.activeRoadRoller.LandEffect();
            this.activeRoadRoller.DisableHitbox();

            //this.landingSfx.PlaySound();
        }
        this.fallingSfx.StopSound();


        yield return new WaitForSeconds(0.1f);

        /*yield return new WaitForSeconds(0.2f);

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }*/

        this.fallingDown = false;
        float rollSpeed = 2000f;

        //AFTER ROAD ROLLER LANDS
        if (this.activeRoadRoller != null)
        {
            //IF HIT
            if (this.activeRoadRoller.victim != null)
            {
                //yield return new WaitForSeconds(0.1f);

                if (this.animations != null)
                    this.animations.RoadRollerWry();

                if (this.activeRoadRoller.victim.animations != null)
                    this.activeRoadRoller.victim.animations.LayDown();

                testTime = 0f;
                time = 1f;
                startPosY = this.animations.body.localPosition.y;
                while (time > 0)
                {
                    time -= Time.deltaTime;
                    testTime += Time.deltaTime;

                    float newY = Mathf.Sin(testTime * 100f);
                    this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

                    /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/
                    yield return null;
                }


                int amount = 40;
                int animId = 0;
                //bool idForward = true;
                while (amount > 0)
                {
                    //currentTime += Time.deltaTime;
                    if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

                    //yield return new WaitForSeconds(0.05f);

                    if (amount <= 1)
                    {
                        yield return new WaitForSeconds(0.2f);
                        if (this.animations != null)
                            this.animations.RoadRollerPunchMid();
                        yield return new WaitForSeconds(0.001f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.05f);
                    }

                    if(this.activeRoadRoller.victim != null && !this.activeRoadRoller.victim.dead)
                    {
                        //this.activeRoadRoller.victim.TakeDamage(this.user.transform.position, 0.5f, 0f, 0f, 0f, true, true, false, true);
                        this.activeRoadRoller.victim.TakeDamage(this.user.transform.position, 0.75f, 0f, 0f, 0f, true, true, false, true);

                        if (this.activeRoadRoller.victim.soundEffects != null)
                            this.activeRoadRoller.victim.soundEffects.hitSound.PlaySound();

                        /*if (this.activeRoadRoller.victim.animations != null)
                            this.activeRoadRoller.victim.animations.LayDown();*/
                    }

                    float minXPos = -0.75f;
                    float maxXPos = 0.75f;
                    float minYPos = 0.5f;
                    float maxYPos = 2.5f;
                    this.PunchEffect(new Vector3(this.user.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomX(minYPos, maxYPos), -2.5f));

                    if (this.animations != null)
                        this.animations.RoadRollerPunch(animId);

                    float posOffset = 0.05f;
                    if (animId == 0)
                    {
                        this.user.animations.body.transform.position = new Vector3(this.user.animations.body.transform.position.x - posOffset, this.user.animations.body.transform.position.y, 0f);
                        this.activeRoadRoller.transform.position = new Vector3(this.activeRoadRoller.transform.position.x - posOffset, this.activeRoadRoller.transform.position.y, 0f);

                        this.PunchEffect(new Vector3(this.user.animations.rightArmJoint.position.x - (this.user.transform.forward.z * 0.05f), this.user.animations.rightArmJoint.position.y - 0.5f, this.user.animations.rightArmJoint.position.z - 0.3f));


                        this.activeRoadRoller.victim.animations.body.transform.position = new Vector3(this.activeRoadRoller.victim.animations.body.transform.position.x - 0.01f, this.activeRoadRoller.victim.animations.body.transform.position.y, 0f);

                        animId = 1;
                    }
                    else
                    {
                        this.user.animations.body.transform.position = new Vector3(this.user.animations.body.transform.position.x + posOffset, this.user.animations.body.transform.position.y, 0f);
                        this.activeRoadRoller.transform.position = new Vector3(this.activeRoadRoller.transform.position.x + posOffset, this.activeRoadRoller.transform.position.y, 0f);
                        this.PunchEffect(new Vector3(this.user.animations.leftArmJoint.position.x - (this.user.transform.forward.z * 0.05f), this.user.animations.leftArmJoint.position.y - 0.5f, this.user.animations.leftArmJoint.position.z - 0.3f));


                        this.activeRoadRoller.victim.animations.body.transform.position = new Vector3(this.activeRoadRoller.victim.animations.body.transform.position.x + 0.01f, this.activeRoadRoller.victim.animations.body.transform.position.y, 0f);

                        animId = 0;
                    }
                        

                    amount -= 1;

                    yield return null;
                }

                if (this.activeRoadRoller != null)
                {
                    //this.activeRoadRoller.StartExploding();
                    this.activeRoadRoller.EnableElectricity();
                }

                //make the last punch so he chargest it longer and the release

                this.activeRoadRoller.victim.TakeDamage(this.user.transform.position, 5f, 0f, 0f, 0f, true, true, false, true);
                this.finalPunchSfx.PlaySound();

                this.PunchEffect(new Vector3(this.user.transform.position.x + this.RandomX(-0.75f, 0.75f), this.RandomX(0.5f, 2.5f), -2.5f));
                this.PunchEffect(new Vector3(this.user.transform.position.x + this.RandomX(-0.75f, 0.75f), this.RandomX(0.5f, 2.5f), -2.5f));
                this.PunchEffect(new Vector3(this.user.transform.position.x + this.RandomX(-0.75f, 0.75f), this.RandomX(0.5f, 2.5f), -2.5f));

                amount = 10;
                float posOffsett = 0.025f;
                while (amount > 0)
                {
                    this.OffsetRoadRoller(animId, posOffsett);
                    if (animId == 0)
                        animId = 1;
                    else
                        animId = 0;

                    yield return new WaitForSeconds(0.025f);

                    this.OffsetRoadRoller(animId, posOffsett);
                    if (animId == 0)
                        animId = 1;
                    else
                        animId = 0;

                    amount -= 1;
                    yield return null;
                }

                /*if (this.animations != null)
                    this.animations.SetEyes(0);
                yield return new WaitForSeconds(0.1f);*/


                //this.user.LookAtTarget();

                if (this.animations != null)
                    this.animations.SetDefaultPose();

                if (this.animations != null)
                    this.animations.RoadRollerEndLand();
                //this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 30f, 0f);
                yield return new WaitForSeconds(0.1f);

                if (this.animations != null)
                    this.animations.RollAnimation();

                float currentTime2 = 0;
                float duration2 = 0.7f;
                float targetRotation = -360f * 4f;

                currentTime = 0;
                duration = 0.3f;
                float targetPositionX = this.user.transform.position.x - (this.user.transform.forward.z * 2f);
                float startPositionX = this.user.transform.position.x;

                targetPositionY = 8f;
                startPositionY = this.user.transform.position.y;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    currentTime2 += Time.deltaTime;
                    //this.user.transform.position = new Vector3(Mathf.Lerp(startPositionX, targetPositionX, currentTime / duration), Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);
                    this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);

                    /*if (this.animations != null)
                        this.animations.body.transform.Rotate(new Vector3(0f, 0f, rollSpeed * Time.deltaTime));*/

                    if (this.animations != null)
                        this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0, targetRotation, currentTime2 / duration2));
                    yield return null;
                }

                if (this.activeRoadRoller != null)
                {
                    this.activeRoadRoller.Explode();
                    //this.activeRoadRoller.StartExploding();

                    /*if (this.activeRoadRoller.victim != null && !this.activeRoadRoller.victim.dead)
                        this.activeRoadRoller.victim.animations.SetDefaultPose();*/
                }

                currentTime = 0;
                duration = 0.4f;

                targetPositionX = this.user.transform.position.x - (this.user.transform.forward.z * 2f);
                startPositionX = this.user.transform.position.x;

                targetPositionY = 0f;
                startPositionY = this.user.transform.position.y;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    currentTime2 += Time.deltaTime;
                    //this.user.transform.position = new Vector3(Mathf.Lerp(startPositionX, targetPositionX, currentTime / duration), Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);
                    this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);

                    /*if (this.animations != null)
                        this.animations.body.transform.Rotate(new Vector3(0f, 0f, rollSpeed * Time.deltaTime));*/

                    if (this.animations != null)
                        this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0, targetRotation, currentTime2 / duration2));
                    yield return null;
                }

                this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

                /*if (this.animations != null)
                    this.animations.SetDefaultPose();*/

                if (this.animations != null)
                    this.animations.RoadRollerEndLand();

                //yield return new WaitForSeconds(0.3f);
                /*if (this.animations != null)
                    this.animations.SetDefaultPose();*/

                /*if (this.activeRoadRoller != null)
                {
                    this.activeRoadRoller.Explode();
                }*/

                this.user.knockbackInvounrability = false;
                this.canBeCanceled = true;
                this.user.rb.isKinematic = false;

                yield return new WaitForSeconds(0.1f);


                /*if (this.activeRoadRoller != null)
                {
                    this.activeRoadRoller.gameObject.SetActive(false);
                    this.activeRoadRoller = null;

                    *//*if (this.activeRoadRoller.victim != null)
                        this.activeRoadRoller.StopRoadRoller(this.activeRoadRoller.victim);*//*
                }*/

                if (this.animations != null)
                    this.animations.SetDefaultPose();

                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                yield return new WaitForSeconds(0.1f);

                this.onGoing = false;
                this.user.attackStuns.Remove(this.gameObject);
            }
            else //IF MISS
            {
                //yield return new WaitForSeconds(0.3f);

                /*if (this.activeRoadRoller != null)
                {
                    //this.activeRoadRoller.StartExploding();
                    this.activeRoadRoller.EnableElectricity();
                }*/

                this.user.LookAtTarget();

                if (this.animations != null)
                    this.animations.RoadRollerEndLand();
                yield return new WaitForSeconds(0.1f);

                /*this.user.knockbackInvounrability = false;
                this.canBeCanceled = true;*/

                if (this.animations != null)
                    this.animations.RollAnimation();

                float currentTime2 = 0;
                float duration2 = 0.7f;
                float targetRotation = -360f * 4f;

                currentTime = 0;
                duration = 0.3f;
                float targetPositionX = this.user.transform.position.x - (this.user.transform.forward.z * 2f);
                float startPositionX = this.user.transform.position.x;

                targetPositionY = 8f;
                startPositionY = this.user.transform.position.y;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    currentTime2 += Time.deltaTime;
                    //this.user.transform.position = new Vector3(Mathf.Lerp(startPositionX, targetPositionX, currentTime / duration), Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);
                    this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);

                    /*if (this.animations != null)
                        this.animations.body.transform.Rotate(new Vector3(0f, 0f, rollSpeed * Time.deltaTime));*/

                    if (this.animations != null)
                        this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0, targetRotation, currentTime2 / duration2));
                    yield return null;
                }

                if (this.activeRoadRoller != null)
                {
                    /*if (this.activeRoadRoller.victim != null && !this.activeRoadRoller.victim.dead)
                        this.activeRoadRoller.victim.animations.SetDefaultPose();*/

                    this.activeRoadRoller.Explode();

                    
                }

                this.anvilVulnerability = true;

                this.user.knockbackInvounrability = false;
                this.canBeCanceled = true;

                currentTime = 0;
                duration = 0.4f;

                targetPositionX = this.user.transform.position.x - (this.user.transform.forward.z * 2f);
                startPositionX = this.user.transform.position.x;

                targetPositionY = 0f;
                startPositionY = this.user.transform.position.y;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    currentTime2 += Time.deltaTime;
                    //this.user.transform.position = new Vector3(Mathf.Lerp(startPositionX, targetPositionX, currentTime / duration), Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);
                    this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0f);

                    /*if (this.animations != null)
                        this.animations.body.transform.Rotate(new Vector3(0f, 0f, rollSpeed * Time.deltaTime));*/

                    if (this.animations != null)
                        this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0, targetRotation, currentTime2 / duration2));

                    if (currentTime >= 0.2f)
                        this.anvilVulnerability = false;
                    yield return null;
                }

                this.anvilVulnerability = false;

                this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

                /*if (this.animations != null)
                    this.animations.SetDefaultPose();*/

                if (this.animations != null)
                    this.animations.RoadRollerEndLand();

                //this.user.knockbackInvounrability = false;
                //this.canBeCanceled = true;
                this.user.rb.isKinematic = false;

                yield return new WaitForSeconds(0.1f);

                if (this.animations != null)
                    this.animations.SetDefaultPose();

                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                yield return new WaitForSeconds(0.1f);


                /*if (this.animations != null)
                    this.animations.SetDefaultPose();

                yield return new WaitForSeconds(0.3f);



                if (this.activeRoadRoller != null)
                {
                    this.activeRoadRoller.gameObject.SetActive(false);
                    this.activeRoadRoller = null;

                    *//*if (this.activeRoadRoller.victim != null)
                        this.activeRoadRoller.StopRoadRoller(this.activeRoadRoller.victim);*//*
                }*/



                this.onGoing = false;
                this.user.attackStuns.Remove(this.gameObject);
            }
        }
        else
        {
            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.3f);


            this.user.knockbackInvounrability = false;
            this.canBeCanceled = true;
            this.user.rb.isKinematic = false;

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }



        
    }

    public void PunchEffect(Vector3 position)
    {
        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            punchEffectPrefab = Instantiate(punchEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }
    }

    private float RandomX(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }

    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;
        this.user.knockbackInvounrability = false;

        this.canBeCanceled = false;

        this.fallingSfx.StopSound();
        if (this.activeRoadRoller != null)
        {
            this.activeRoadRoller.Explode();

            /*if (this.activeRoadRoller.victim != null && !this.activeRoadRoller.victim.dead)
                this.activeRoadRoller.victim.animations.SetDefaultPose();

            this.activeRoadRoller.gameObject.SetActive(false);*/

            this.activeRoadRoller = null;

            /*if (this.activeRoadRoller.victim != null)
                this.activeRoadRoller.StopRoadRoller(this.activeRoadRoller.victim);*/
        }

        /*if(GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }*/

        this.anvilVulnerability = false;
        this.fallingDown = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void OffsetRoadRoller(int animId = 0, float posOffsett = 0.025f)
    {
        if (animId == 0)
        {
            this.user.animations.body.transform.position = new Vector3(this.user.animations.body.transform.position.x - posOffsett, this.user.animations.body.transform.position.y, 0f);
            this.activeRoadRoller.transform.position = new Vector3(this.activeRoadRoller.transform.position.x - posOffsett, this.activeRoadRoller.transform.position.y, 0f);
            this.activeRoadRoller.victim.animations.body.transform.position = new Vector3(this.activeRoadRoller.victim.animations.body.transform.position.x - 0.01f, this.activeRoadRoller.victim.animations.body.transform.position.y, 0f);
        }
        else
        {
            this.user.animations.body.transform.position = new Vector3(this.user.animations.body.transform.position.x + posOffsett, this.user.animations.body.transform.position.y, 0f);
            this.activeRoadRoller.transform.position = new Vector3(this.activeRoadRoller.transform.position.x + posOffsett, this.activeRoadRoller.transform.position.y, 0f);
            this.activeRoadRoller.victim.animations.body.transform.position = new Vector3(this.activeRoadRoller.victim.animations.body.transform.position.x + 0.01f, this.activeRoadRoller.victim.animations.body.transform.position.y, 0f);
        }
    }
}
