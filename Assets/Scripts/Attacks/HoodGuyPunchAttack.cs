using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodGuyPunchAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public bool onGoingNeutral;
    public TestHitbox hitbox;

    public TestHitbox airHitbox;

    public TestHitbox neutralHitbox2;
    public TestHitbox backHitbox;
    public TestHitbox forwardHitbox;
    public TestHitbox forwardHitbox2;

    public GameObject scyte;
    public GameObject scyteTrail;

    //private IEnumerator hitboxCoroutine;
    //private IEnumerator animationCoroutine;

    /*public override void OnEnable()
    {
        base.OnEnable();

        this.animationCoroutine = this.PunchAnimationCoroutine();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }*/


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }


        /*if (!this.user.dead && this.onGoing)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());
        }*/
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
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
            }

        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.1f, true);
            else
                this.user.AddStun(0.1f, false);

            this.StartCoroutine(this.PunchCoroutine());*/


            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.PunchCoroutine2());*/


            if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.AddStun(0.2f, true);
                else
                    this.user.AddStun(0.2f, false);

                this.StartCoroutine(this.ForwardPunchCoroutine());
            }
            else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.AddStun(0.2f, true);
                else
                    this.user.AddStun(0.2f, false);

                this.StartCoroutine(this.BackPunchCoroutine());
            }
            else //Neutral
            {
                /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.AddStun(0.2f, true);
                else
                    this.user.AddStun(0.2f, false);

                this.StartCoroutine(this.PunchCoroutine3());*/

                if (Mathf.Abs(this.user.transform.position.y) < 0.2f) //not in air
                {
                    if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                        this.user.AddStun(0.1f, true);
                    else
                        this.user.AddStun(0.1f, false);

                    this.StartCoroutine(this.PunchCoroutine());
                }
                else //in air
                {
                    this.user.AddStun(0.2f, false);
                    this.StartCoroutine(this.AirPunchCoroutine());
                }


                /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.user.AddStun(0.1f, true);
                else
                    this.user.AddStun(0.1f, false);

                this.StartCoroutine(this.PunchCoroutine());*/
            }

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.PunchHitboxCoroutine());

            this.animationCoroutine = this.PunchAnimationCoroutine();

            if (this.animationCoroutine != null)
            {
                this.StartCoroutine(this.animationCoroutine);
            }*/

        }
    }

    IEnumerator PunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestHoodGuyPunch(0);
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.TestHoodGuyPunch(1);

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null)
            this.animations.TestHoodGuyPunch(2);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.TestHoodGuyPunch(0);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.01f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator AirPunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);*/

        if (this.animations != null)
            this.animations.AirPunch(0);

        yield return new WaitForSeconds(0.1f);
        //yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.AirPunch(1);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.2f;
        //float duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator BackPunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ScyteSwing();

        if (this.scyte != null)
        {
            this.scyte.gameObject.SetActive(true);
            this.scyte.gameObject.transform.localPosition = new Vector3(0f, -0.56f, 0f);
            this.scyte.gameObject.transform.localEulerAngles = new Vector3(-5f, 0f, -90f);
        }

        if (this.animations != null)
            this.animations.upperBody.localEulerAngles = new Vector3(0f, 55f, 0f);

        yield return new WaitForSeconds(0.15f);

        if (this.backHitbox != null)
            this.backHitbox.gameObject.SetActive(true);

        if (this.scyteTrail != null)
            this.scyteTrail.SetActive(true);

        float currentTime = 0;
        float duration = 0.1f;
        //float targetVolume = 0.1f;
        float targetRotation = -65f;
        //float targetRotation = 360f;
        float startRotation = this.animations.upperBody.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.upperBody.localEulerAngles = new Vector3(0f, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 0f);
            yield return null;
        }

        if (this.backHitbox != null)
            this.backHitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.scyte != null)
        {
            this.scyte.gameObject.SetActive(false);
            this.scyte.gameObject.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        if (this.scyteTrail != null)
            this.scyteTrail.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ForwardPunchCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ScyteSpin();

        if (this.scyte != null)
        {
            this.scyte.gameObject.SetActive(true);
            this.scyte.gameObject.transform.localPosition = new Vector3(-0.3f, -0.56f, 0f);
            this.scyte.gameObject.transform.localEulerAngles = new Vector3(-80f, 0f, -90f);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.forwardHitbox2 != null)
            this.forwardHitbox2.gameObject.SetActive(true);

        if (this.scyteTrail != null)
            this.scyteTrail.SetActive(true);

        float currentTime = 0;
        float duration = 0.1f;
        //float targetRotation = this.user.transform.forward.z * 270f;
        float targetRotation = this.user.transform.forward.z * 360f;
        float startRotation = this.animations.body.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(0f, Mathf.Lerp(startRotation, targetRotation / 2, currentTime / duration), 0f);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 4f, 0f, 0f);
            yield return null;
        }

        if (this.forwardHitbox2 != null)
            this.forwardHitbox2.gameObject.SetActive(false);

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(true);



        currentTime = 0;
        duration = 0.1f;
        startRotation = this.user.transform.forward.z * (360f / 2);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(0f, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 0f);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 4f, 0f, 0f);
            yield return null;
        }

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);


        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();


        if (this.scyte != null)
        {
            this.scyte.gameObject.SetActive(false);
            this.scyte.gameObject.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        if (this.scyteTrail != null)
            this.scyteTrail.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.StartCoroutine(this.HitCoroutine());

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(false);*/

        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/

        if (this.backHitbox != null)
            this.backHitbox.gameObject.SetActive(false);

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);

        if (this.forwardHitbox2 != null)
            this.forwardHitbox2.gameObject.SetActive(false);

        if (this.scyte != null)
        {
            this.scyte.gameObject.SetActive(false);
            this.scyte.gameObject.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }


        /*if (this.animationCoroutine != null)
        {
            this.StopCoroutine(this.animationCoroutine);
            this.animationCoroutine = null;
        }*/

        if (this.scyteTrail != null)
            this.scyteTrail.SetActive(false);

        //this.onGoingNeutral = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);

        if (!this.user.dead && this.onGoing)
        {
            /*if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            if (this.neutralHitbox2 != null)
                this.neutralHitbox2.gameObject.SetActive(false);*/

            /*this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }

    IEnumerator PunchCoroutine3()
    {
        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.TestHoodGuyPunch(0);
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.TestHoodGuyPunch(1);

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.TestHoodGuyPunch(2);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.TestHoodGuyPunch(0);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.neutralHitbox2 != null)
            this.neutralHitbox2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();
    }

    IEnumerator PunchCoroutine2()
    {
        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartPunchPose0();
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartPunchPose();

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetPunchPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }


    IEnumerator PunchCoroutineOld()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartPunchPose0();
        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.SetStartPunchPose();

        yield return new WaitForSeconds(0.04f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}