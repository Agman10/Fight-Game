using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperElectricBallAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;
    public ElectricBallProjectile electricBall;
    public ElectricBallProjectile electricBallP2;
    public GameObject electricBallModel;
    public GameObject electricBallModelP1;
    public GameObject electricBallModelP2;

    public TestHitbox knockbackHitbox;

    public float ballMoveSpeed = 2f;
    public float ballWaveSpeed = 5f;

    public GameObject glowingEyes;

    public int animationId;

    public bool doubleElectricBall = false;

    public AudioSource electricitySfx;

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

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge / 2)
            {
                /*this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.SuperElectricBallCoroutine());*/

                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.SuperElectricBallCoroutine(false));

                    if (this.doubleElectricBall)
                        this.StartCoroutine(this.SuperElectricBallDoubleCoroutine(false));
                    else
                        this.StartCoroutine(this.SuperElectricBallCoroutine(false));
                }
                else
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.SuperElectricBallCoroutine(true));

                    if (this.doubleElectricBall)
                        this.StartCoroutine(this.SuperElectricBallDoubleCoroutine(true));
                    else
                        this.StartCoroutine(this.SuperElectricBallCoroutine(true));
                }
                /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFireBallCoroutine());

                }
                else
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFireBallAirCoroutine());
                }*/
            }

        }
    }

    private IEnumerator SuperElectricBallCoroutine(bool inAir = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SuperFireBallCharge();*/

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(0, inAir);

            /*if (inAir)
                this.animations.ElectricSphereThrow(0, true);
            else
                this.animations.ElectricSphereThrow(0, false);*/

            /*if (this.animationId == 1)
                this.animations.ShootPoseCrouch();
            else
                this.animations.SuperFireBallCharge();*/
        }

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.electricBallModel != null)
        {
            if (this.electricBallModelP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
            {
                if (this.electricBallModelP1 != null)
                    this.electricBallModelP1.SetActive(false);
                this.electricBallModelP2.SetActive(true);
            }
            else if (this.electricBallModelP1 != null)
            {
                this.electricBallModelP1.SetActive(true);

                if (this.electricBallModelP2 != null)
                    this.electricBallModelP2.SetActive(false);
            }
                

            this.electricBallModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            this.electricBallModel.SetActive(true);
        }

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        if (this.electricitySfx != null)
            this.electricitySfx.Play();

        float currentTime = 0;
        float duration = 1.25f;
        float targetScale = 1f;
        float startScale = 0.1f;
        while (currentTime < duration)
        {

            //this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.electricBallModel != null)
                this.electricBallModel.transform.localScale = new Vector3(
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration));

            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.SuperFireBallShoot();*/

        if (this.electricBallModel != null)
        {
            this.electricBallModel.SetActive(false);
            this.electricBallModel.transform.localScale = new Vector3(1f, 1f, 1f);

        }

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(1, inAir);

            /*if (this.animationId == 1)
                this.animations.ShootPoseCrouch2();
            else
                this.animations.SuperFireBallShoot();*/
        }

        if (this.electricBall != null)
        {
            ElectricBallProjectile electricBallPrefab = this.electricBall;

            if (this.electricBallP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                electricBallPrefab = this.electricBallP2;

            electricBallPrefab = Instantiate(electricBallPrefab, new Vector3((this.transform.forward.z * 1.35f) + this.transform.position.x, this.transform.position.y + 1.75f, 0), this.transform.rotation);
            electricBallPrefab.SetOwner(this.user);
            electricBallPrefab.moveSpeed = this.ballMoveSpeed;
            electricBallPrefab.waveSpeed = this.ballWaveSpeed;
        }

        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(true);

        /*yield return new WaitForSeconds(0.5f);

        if (this.electricBall != null)
        {
            ElectricBallProjectile electricBallPrefab = this.electricBall;

            if (this.electricBallP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                electricBallPrefab = this.electricBallP2;

            electricBallPrefab = Instantiate(electricBallPrefab, new Vector3((this.transform.forward.z * 1.35f) + this.transform.position.x, this.transform.position.y + 1.75f, 0), this.transform.rotation);
            electricBallPrefab.SetOwner(this.user);
            electricBallPrefab.moveSpeed = this.ballMoveSpeed;
            electricBallPrefab.waveSpeed = this.ballWaveSpeed;
        }*/


        yield return new WaitForSeconds(0.8f);

        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(false);

        /*if (this.animationId == 1)
        {
            yield return new WaitForSeconds(0.2f);

            if (this.animations != null)
                this.animations.ShootPoseCrouch();

            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }*/

        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();


        currentTime = 0;
        duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.electricBallModel != null)
        {
            this.electricBallModel.SetActive(false);
            this.electricBallModel.transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(false);

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator SuperElectricBallDoubleCoroutine(bool inAir = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SuperFireBallCharge();*/

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(0, inAir);
        }

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.electricBallModel != null)
        {
            if (this.electricBallModelP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
            {
                if (this.electricBallModelP1 != null)
                    this.electricBallModelP1.SetActive(false);
                this.electricBallModelP2.SetActive(true);
            }
            else if (this.electricBallModelP1 != null)
            {
                this.electricBallModelP1.SetActive(true);

                if (this.electricBallModelP2 != null)
                    this.electricBallModelP2.SetActive(false);
            }


            this.electricBallModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            this.electricBallModel.SetActive(true);
        }

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        if (this.electricitySfx != null)
            this.electricitySfx.Play();

        float currentTime = 0;
        float duration = 0.5f;
        float targetScale = 1f;
        float startScale = 0.1f;
        while (currentTime < duration)
        {

            //this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.electricBallModel != null)
                this.electricBallModel.transform.localScale = new Vector3(
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration));

            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.SuperFireBallShoot();*/

        if (this.electricBallModel != null)
        {
            this.electricBallModel.SetActive(false);
            this.electricBallModel.transform.localScale = new Vector3(1f, 1f, 1f);

        }

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(1, inAir);

            /*if (this.animationId == 1)
                this.animations.ShootPoseCrouch2();
            else
                this.animations.SuperFireBallShoot();*/
        }

        if (this.electricBall != null)
        {
            ElectricBallProjectile electricBallPrefab = this.electricBall;

            if (this.electricBallP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                electricBallPrefab = this.electricBallP2;

            electricBallPrefab = Instantiate(electricBallPrefab, new Vector3((this.transform.forward.z * 1.35f) + this.transform.position.x, this.transform.position.y + 1.75f, 0), this.transform.rotation);
            electricBallPrefab.SetOwner(this.user);
            electricBallPrefab.moveSpeed = this.ballMoveSpeed;
            electricBallPrefab.waveSpeed = this.ballWaveSpeed;
        }

        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(0, inAir);
        }


        if (this.electricBallModel != null)
        {
            if (this.electricBallModelP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
            {
                if (this.electricBallModelP1 != null)
                    this.electricBallModelP1.SetActive(false);
                this.electricBallModelP2.SetActive(true);
            }
            else if (this.electricBallModelP1 != null)
            {
                this.electricBallModelP1.SetActive(true);

                if (this.electricBallModelP2 != null)
                    this.electricBallModelP2.SetActive(false);
            }


            this.electricBallModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            this.electricBallModel.SetActive(true);
        }

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        if (this.electricitySfx != null)
            this.electricitySfx.Play();

        currentTime = 0;
        duration = 0.3f;
        targetScale = 1f;
        startScale = 0.1f;
        while (currentTime < duration)
        {

            //this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.electricBallModel != null)
                this.electricBallModel.transform.localScale = new Vector3(
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration),
                    Mathf.Lerp(startScale, targetScale, currentTime / duration));

            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.SuperFireBallShoot();*/

        if (this.electricBallModel != null)
        {
            this.electricBallModel.SetActive(false);
            this.electricBallModel.transform.localScale = new Vector3(1f, 1f, 1f);

        }

        if (this.animations != null)
        {
            this.animations.ElectricSphereThrow(1, inAir);
        }

        if (this.electricBall != null)
        {
            ElectricBallProjectile electricBallPrefab = this.electricBall;

            if (this.electricBallP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                electricBallPrefab = this.electricBallP2;

            electricBallPrefab = Instantiate(electricBallPrefab, new Vector3((this.transform.forward.z * 1.35f) + this.transform.position.x, this.transform.position.y + 1.75f, 0), this.transform.rotation);
            electricBallPrefab.SetOwner(this.user);
            electricBallPrefab.moveSpeed = this.ballMoveSpeed;
            electricBallPrefab.waveSpeed = this.ballWaveSpeed;
        }

        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(true);


        yield return new WaitForSeconds(0.8f);

        if (this.knockbackHitbox != null)
            this.knockbackHitbox.gameObject.SetActive(false);

        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();


        currentTime = 0;
        duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}

