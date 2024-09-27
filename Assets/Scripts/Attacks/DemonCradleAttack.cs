using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCradleAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;

    //public GameObject trail;
    public ParticleSystem[] trails;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            //this.Stop();
            this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());
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
            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());*/

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();*/

        if (this.animations != null)
            this.animations.DemonCradle(1);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.DemonCradle(2);

        yield return new WaitForSeconds(0.001f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 100f, 1000f, 0f);

        /*if (this.trail != null)
            this.trail.SetActive(true);*/

        foreach (ParticleSystem trail in this.trails)
        {
            if (trail != null)
                trail.Play();
        }

        /*if (this.animations != null)
            this.animations.DemonCradle(2);*/

        float currentTime = 0;
        float duration = 0.3f;
        float startRotation = this.transform.forward.z * -90f;
        //float stargetRotation = -this.transform.forward.z * 360f;
        //float targetRotation = -this.transform.forward.z * 450f;
        float targetRotation = -this.transform.forward.z * 520f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 0);
            yield return null;
        }

        /*if (this.trail != null)
            this.trail.SetActive(false);*/

        foreach (ParticleSystem trail in this.trails)
        {
            if (trail != null)
                trail.Stop();
        }

        if (this.animations != null)
            this.animations.DemonCradle(3);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);
        

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        /*currentTime = 0;
        duration = 0.05f;
        startRotation = 180f;
        //float stargetRotation = -this.transform.forward.z * 360f;
        //float targetRotation = -this.transform.forward.z * 450f;
        targetRotation = -this.transform.forward.z * 90f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 0);
            yield return null;
        }*/

        yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, 0, 0);

        if (this.animations != null)
            this.animations.DemonCradle(4);

        currentTime = 0;
        duration = 0.05f;
        startRotation = this.user.transform.forward.z * 110f;
        //float stargetRotation = -this.transform.forward.z * 360f;
        //float targetRotation = -this.transform.forward.z * 450f;
        targetRotation = this.transform.forward.z * 50f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.DemonCradle(5);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        //yield return new WaitForSeconds(0.4f);

        float waitTime = 0.5f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitCoroutine()
    {
        foreach (ParticleSystem trail in this.trails)
        {
            if (trail != null)
                trail.Stop();
        }

        yield return new WaitForSeconds(0.001f);
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        /*if (this.trail != null)
            this.trail.SetActive(false);*/

        foreach (ParticleSystem trail in this.trails)
        {
            if (trail != null)
                trail.Stop();
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
