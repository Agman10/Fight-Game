using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardRunAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public bool isSuper;

    public TestHitbox hitbox1;
    public TestHitbox hitbox2;

    //public float superStartTime = 0.3f;
    public float superEndlagTime = 0.05f;

    //private float testTime = 0f;

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
            if (this.isSuper)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    {
                        this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.SuperCowardRunCoroutine());
                    }
                }
            }
            else
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TemplateCoroutine());
                }
            }
            
            
        }
    }

    private IEnumerator SuperCowardRunCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.CowardStart();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }


        //do this so they doen't glide when walking towards them
        float currentTime = 0;
        float duration = 0.3f;
        //float duration = this.superStartTime;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.animations != null)
        {
            this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.animations.leftArm.localEulerAngles = new Vector3(0f, 0f, 180f);
            this.animations.SetEyes(2);
        }

        //yield return new WaitForSeconds(0.3f);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        float testTime = 0f;
        float time = 2f;
        while (time > 0)
        {
            time -= Time.deltaTime;

            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 70f);
            //float newY = Mathf.Sin(testTime * 20f);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 8f, 0f, 0f);

            if (this.animations != null)
            {
                this.animations.rightArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));
                this.animations.leftArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));

                this.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, newY * 40);
                this.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, newY * -40);

                /*this.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, newY * -40);
                this.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, newY * 40);*/
            }
            yield return null;
        }
        //testTime = 0f;

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.AddStun(0.1f, true);
        yield return new WaitForSeconds(0.05f);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);




        //do this so they doen't glide when walking towards them
        /*currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        currentTime = 0;
        duration = this.superEndlagTime;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.05f);

        //yield return new WaitForSeconds(0.3f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        
        if (this.animations != null)
            this.animations.CowardStart();


        //do this so they doen't glide when walking towards them
        float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.animations != null)
        {
            this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.animations.leftArm.localEulerAngles = new Vector3(0f, 0f, 180f);
            this.animations.SetEyes(2);
        }

        //yield return new WaitForSeconds(0.3f);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        float testTime = 0f;
        float time = 1f;
        while (time > 0)
        {
            time -= Time.deltaTime;

            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 70f);
            //float newY = Mathf.Sin(testTime * 20f);

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 8f, 0f, 0f);

            if (this.animations != null)
            {
                this.animations.rightArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));
                this.animations.leftArm.transform.Rotate(new Vector3(0f, 0f, -1500 * Time.deltaTime));

                this.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, newY * 40);
                this.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, newY * -40);

                /*this.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, newY * -40);
                this.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, newY * 40);*/
            }
            yield return null;
        }
        //testTime = 0f;

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.AddStun(0.1f, true);
        yield return new WaitForSeconds(0.05f);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);




        //do this so they doen't glide when walking towards them
        currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.3f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
