using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AttackAnimationTests : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject scyte;

    private bool moving = false;

    public GameObject objectToEnable;
    public GameObject objectToEnable2;

    public VisualEffect fire;

    /*public GameObject objectToEnable;

    public Explosion explosion;
    public Transform objectTransform;*/

    //public ParticleSystem particle;

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
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/


            if (this.moving)
            {
                //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 20f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 22f, this.user.rb.velocity.y, 0f);
                //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 25f, this.user.rb.velocity.y, 0f);
            }


            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
            }
        }

        /*if (this.onGoing && this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }*/
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);

            this.user.AddStun(0.2f, true);
            //this.StartCoroutine(this.TestButtAttack());
            //this.StartCoroutine(this.Fly());
            this.StartCoroutine(this.TestParry());

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TestShockwaveSuper());
            }*/


            /*if (Mathf.Abs(this.user.transform.position.y) < 0.2f)
            {
                //Debug.Log(Mathf.Abs(this.user.transform.position.y));
                *//*this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KickCoroutine());*//*
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.AnimationTestCoroutine());
            }*/

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TestButtAttack()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


        if (this.animations != null)
            this.animations.HoodGuyButtAttack();

        yield return new WaitForSeconds(0.1f);

        float maxSpeed = 1500f;

        float currentTime = 0;
        float duration = 0.05f;
        float targetVelocity = maxSpeed;
        float startVelocity = 500f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f/*100f * Time.deltaTime*/, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * maxSpeed * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        currentTime = 0;
        duration = 0.15f;
        targetVelocity = 500f;
        startVelocity = maxSpeed;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);
        this.user.rb.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.1f);


        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);


        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator TestShockwaveSuper()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ShockwavePunchStart();

        yield return new WaitForSeconds(0.3f);
        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(true);

        this.PlayFire(true);

        if (this.animations != null)
            this.animations.ShockwavePunch();

        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.5f);

        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(false);

        this.PlayFire(false);

        

        if (this.objectToEnable2 != null)
            this.objectToEnable2.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        this.user.rb.isKinematic = false;

        if (this.objectToEnable2 != null)
            this.objectToEnable2.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    
    private IEnumerator AnimationTestCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;



        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            this.animations.TestStupidWalk(0);
            //this.animations.StupidDance(0);
        }

        //int amountt = 15;
        int amountt = 10;
        int animationIdd = 1;
        while (amountt > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.025f);

            if (animationIdd == 0)
            {
                this.animations.TestStupidWalk(0);
                //this.animations.StupidDance(0);

                animationIdd += 1;
            }
            else if (animationIdd == 1)
            {
                this.animations.TestStupidWalk(1);
                //this.animations.StupidDance(1);
  
                animationIdd += 1;
            }
            else if (animationIdd == 2)
            {
                this.animations.TestStupidWalk(2);
                //this.animations.StupidDance(2);

                animationIdd += 1;
            }
            else if (animationIdd == 3)
            {
                this.animations.TestStupidWalk(1);
                //this.animations.StupidDance(1);

                animationIdd = 0;
            }
            amountt -= 1;
            if (amountt <= 0 && this.user.input.super)
                amountt = 1;
        }







        /*if (this.animations != null)
            this.animations.TestStupidWalk(0);

        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.3f);*/

        this.user.rb.isKinematic = false;

        this.ChangeCollision(true);


        if (Mathf.Abs(this.user.transform.position.x) >= 14f)
        {
            //Debug.Log(-(this.transform.forward.z * 13.99f));

            //this.user.transform.position = new Vector3(this.user.transform.position.x + this.user.transform.forward.z * 0.01f, this.user.transform.position.y, 0f);
            this.user.transform.position = new Vector3(-(this.transform.forward.z * 13.99f), this.user.transform.position.y, 0f);
        }
            

        if (this.animations != null)
            this.animations.Crawl(0);

        int amount = 60;
        int animationId = 1;

        float longWaitTime = 0.25f;
        float shortWaitTime = 0.05f;

        float waitTime = shortWaitTime;



        this.moving = true;

        //NOTE FOR WHEN MAKING IT FOR A SUPER IS MAYBE TRY TO MAKE IT BASED ON THE CAMERA INSTEAD (camera x pos * 6.5)

        while (amount > 0 && Mathf.Abs(this.user.transform.position.x) < 14f /*&& this.user.transform.position.x < this.user.transform.forward.z * 13.9f*/)
        {

            //Debug.Log(Mathf.Abs(this.user.transform.forward.z * this.user.transform.position.x));
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            yield return new WaitForSeconds(0.025f);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.transform.forward.z * 20f, this.user.rb.velocity.y, 0f);*/

            /*if (this.animations != null)
            {
                this.animations.CaramelDance(animationId);
                Debug.Log(animationId);
            }*/

            if (animationId == 0)
            {
                this.animations.Crawl(0);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 1)
            {
                this.animations.Crawl(1);
                waitTime = longWaitTime;
                animationId += 1;
            }
            else if (animationId == 2)
            {
                this.animations.Crawl(2);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 3)
            {
                this.animations.Crawl(1);
                waitTime = longWaitTime;
                animationId = 0;
            }

            amount -= 1;
            /*if (amount <= 0 && this.user.input.taunting)
                amount = 1;*/
            //Debug.Log(amount);
        }
        this.moving = false;
        this.ChangeCollision(false);

        if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();



        this.moving = false;
        this.user.rb.isKinematic = false;
        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //this.ChangeCollision(false);

        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(false);

        if (this.objectToEnable2 != null)
            this.objectToEnable2.SetActive(false);

        this.PlayFire(false);

        /*if (this.particle != null)
            this.particle.Stop();*/

        /*if (this.objectToEnable != null)
            this.objectToEnable.SetActive(false);

        if (this.objectTransform != null)
            this.objectTransform.localPosition = new Vector3(0f, 1.7f, 0f);*/


        if (this.scyte != null)
        {
            this.scyte.SetActive(false);
            this.scyte.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }


    public void ChangeCollision(bool crawl = false)
    {
        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            if (crawl)
            {
                capsuleCollider.radius = 0.5f;
                capsuleCollider.height = 2f;

                capsuleCollider.center = new Vector3(0f, 1f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }
            else
            {
                capsuleCollider.radius = 0.5f;
                capsuleCollider.height = 3f;

                capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }

        }
    }








    private IEnumerator AnimationTestCoroutine2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.scyte != null)
        {
            this.scyte.SetActive(true);
            this.scyte.transform.localPosition = new Vector3(0f, -0.56f, 0.8f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, -90f, -90f);
        }

        if (this.animations != null)
            this.animations.HoodGuyBackKick(0, 0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(1, 0);

        yield return new WaitForSeconds(0.05f);




        if (this.animations != null)
        {
            this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, -20f);
            this.animations.body.localPosition = new Vector3(-0.255f, this.animations.defaultYPos - 0.04f, 0f);
        }

        yield return new WaitForSeconds(0.025f);




        /*if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(true);*/

        if (this.animations != null)
            this.animations.HoodGuyBackKick(2, 1);

        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.1f);

        /*if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(1, 0);

        //yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
        {
            this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, -20f);
            this.animations.body.localPosition = new Vector3(-0.255f, this.animations.defaultYPos - 0.04f, 0f);
        }

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(1, 0);


        yield return new WaitForSeconds(0.025f);




        /*if (this.animations != null)
            this.animations.HoodGuyBackKick(0, 0);

        yield return new WaitForSeconds(0.025f);*/

        if (this.scyte != null)
        {
            this.scyte.SetActive(false);
            this.scyte.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.175f);


        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }



    private IEnumerator AnimationTestCoroutine3()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        //yield return new WaitForSeconds(0.1f);


        //yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.AirPunch(0);

        //yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.1f);



        if (this.animations != null)
            this.animations.AirPunch(1);

        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);*/

        float currentTime = 0;
        float duration = 0.2f;
        //float duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/



        /*if (this.animations != null)
            this.animations.SexKickStart();

        yield return new WaitForSeconds(0.05f);*/


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);
        //yield return new WaitForSeconds(0.05f);


        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    private IEnumerator AnimationTestCoroutine4()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float animSpeed = 0.06f;

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 0);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 150f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 0);

        yield return new WaitForSeconds(animSpeed + 0.025f);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);

        yield return new WaitForSeconds(animSpeed);




        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator Fly()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


        if (this.animations != null)
            this.animations.TestFly();

        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(true);

        if (this.objectToEnable2 != null)
            this.objectToEnable2.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        float maxSpeed = 1500f;

        float currentTime = 0;
        float duration = 0.8f;
        float targetVelocity = maxSpeed;
        float startVelocity = 500f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            float newY = Mathf.Sin(currentTime * 20f);

            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f/*100f * Time.deltaTime*/, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 500f * Time.deltaTime, 0f, 0f);

            if (this.animations != null)
            {
                /*this.animations.rightArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));
                this.animations.leftArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));*/

                this.animations.rightArm.localEulerAngles = new Vector3(90f, 0f, newY * 45);
                this.animations.leftArm.localEulerAngles = new Vector3(-90f, 0f, newY * 45);

                /*this.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, newY * -40);
                this.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, newY * 40);*/
            }

            yield return null;
        }

        /*currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * maxSpeed * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        currentTime = 0;
        duration = 0.15f;
        targetVelocity = 500f;
        startVelocity = maxSpeed;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);

            yield return null;
        }*/


        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //yield return new WaitForSeconds(0.5f);
        this.user.rb.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.1f);


        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(false);

        if (this.objectToEnable2 != null)
            this.objectToEnable2.SetActive(false);

        yield return new WaitForSeconds(0.1f);


        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }



    private IEnumerator TestParry()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.Parry();

        yield return new WaitForSeconds(0.1f);

        /*float currentTime = 0;
        float duration = 0.2f;
        //float duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/


        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
