using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFireBallAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;
    public BigFireBall bigFireBall;

    public GameObject smoke;
    public GameObject confused;

    public float cooldownTimer;
    //private float cooldown = 1.35f;
    public float cooldown = 1.8f;

    public int animationId;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    private void Update()
    {
        if (this.cooldownTimer > 0)
            this.cooldownTimer -= Time.deltaTime;

        //Debug.Log(this.cooldownTimer);
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge / 2)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    if (this.cooldownTimer <= 0)
                        this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFireBallCoroutine(this.cooldownTimer > 0));


                    /*this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFireBallCoroutine());*/

                }
                else
                {
                    if (this.cooldownTimer <= 0)
                        this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFireBallAirCoroutine(this.cooldownTimer > 0));
                }
            }
            
        }
    }

    private IEnumerator SuperFireBallCoroutine(bool fail = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        //Debug.Log("Fail: " + fail);

        /*if (this.animations != null)
            this.animations.SuperFireBallCharge();*/

        if(this.animations != null)
        {
            if (this.animationId == 1)
                this.animations.ShootPoseCrouch();
            else
                this.animations.SuperFireBallCharge();
        }

        if (this.startParticle != null && !fail)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.SuperFireBallShoot();*/

        if (!fail)
        {
            if (this.animations != null)
            {
                if (this.animationId == 1)
                    this.animations.ShootPoseCrouch2();
                else
                    this.animations.SuperFireBallShoot();
            }

            if (this.bigFireBall != null)
            {
                BigFireBall bigFireBallPrefab = this.bigFireBall;
                bigFireBallPrefab = Instantiate(bigFireBallPrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, this.transform.position.y + 1.56f, 0), this.transform.rotation);
                bigFireBallPrefab.SetOwner(this.user);
            }
            this.cooldownTimer = this.cooldown;

            /*if (this.smoke != null)
            {
                GameObject smokePrefab = this.smoke;
                smokePrefab = Instantiate(smokePrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, this.transform.position.y + 1.56f, 0), Quaternion.Euler(0, 0, 0));
            }*/

            //yield return new WaitForSeconds(0.5f);

            if (this.animationId == 1)
            {
                yield return new WaitForSeconds(0.2f);

                if (this.animations != null)
                    this.animations.ShootPoseCrouch();

                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }




        }
        else
        {
            if (this.animations != null)
            {
                if (this.animationId == 1)
                    this.animations.ShootPoseCrouch();
                else
                    this.animations.SuperFireBallShoot();
            }

            if (this.smoke != null)
            {
                GameObject smokePrefab = this.smoke;
                smokePrefab = Instantiate(smokePrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, this.transform.position.y + 1.56f, 0), Quaternion.Euler(0, 0, 0));
            }

            if (this.confused != null)
                this.confused.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.35f);
            if (this.confused != null)
                this.confused.gameObject.SetActive(false);
        }
        


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.5f);

        if (!fail)
            yield return new WaitForSeconds(0.3f);
        else
            yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator SuperFireBallAirCoroutine(bool fail = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SuperFireBallCharge();*/

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            if (this.animationId == 1)
                this.animations.ShootPoseCrouch(true);
            else
                this.animations.SuperFireBallCharge(true);
        }

        if (this.startParticle != null && !fail)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.SuperFireBallShoot();*/

        if (!fail)
        {
            if (this.animations != null)
            {
                if (this.animationId == 1)
                    this.animations.ShootPoseCrouch2(true);
                else
                    this.animations.SuperFireBallShoot(true);
            }

            if (this.bigFireBall != null)
            {
                BigFireBall bigFireBallPrefab = this.bigFireBall;
                bigFireBallPrefab = Instantiate(bigFireBallPrefab, new Vector3((this.transform.forward.z * 1.495f) + this.transform.position.x, this.transform.position.y + 1.53f, 0), this.user.transform.rotation);
                //bigFireBallPrefab = Instantiate(bigFireBallPrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, this.transform.position.y + 1.56f, 0), this.user.transform.rotation);
                bigFireBallPrefab.transform.localEulerAngles = new Vector3(0f, this.user.transform.localEulerAngles.y, -20f);
                bigFireBallPrefab.SetOwner(this.user);
            }
            this.cooldownTimer = this.cooldown;

            /*if (this.smoke != null)
            {
                GameObject smokePrefab = this.smoke;
                smokePrefab = Instantiate(smokePrefab, new Vector3((this.transform.forward.z * 1.495f) + this.transform.position.x, this.transform.position.y + 1.53f, 0), Quaternion.Euler(0, 0, 0));
            }*/

            //yield return new WaitForSeconds(0.5f);

            if (this.animationId == 1)
            {
                yield return new WaitForSeconds(0.2f);

                if (this.animations != null)
                    this.animations.ShootPoseCrouch(true);

                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }




        }
        else
        {
            if (this.animations != null)
            {
                if (this.animationId == 1)
                    this.animations.ShootPoseCrouch(true);
                else
                    this.animations.SuperFireBallShoot(true);
            }
            if (this.smoke != null)
            {
                GameObject smokePrefab = this.smoke;
                smokePrefab = Instantiate(smokePrefab, new Vector3((this.transform.forward.z * 1.495f) + this.transform.position.x, this.transform.position.y + 1.53f, 0), Quaternion.Euler(0, 0, 0));
            }
            if (this.confused != null)
                this.confused.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.35f);
            if (this.confused != null)
                this.confused.gameObject.SetActive(false);
        }

        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.5f);

        if (!fail)
            yield return new WaitForSeconds(0.3f);
        else
            yield return new WaitForSeconds(0.1f);

        //this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;

        if (this.confused != null)
            this.confused.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
