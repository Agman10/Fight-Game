using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHoodGuySpin : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitboxStart;
    public TestHitbox hitbox;
    public TestHitbox hitbox2;
    public GameObject startParticle;

    public GameObject rightArmTrail;
    public GameObject leftArmTrail;


    public GameObject windHitboxes;

    public AudioSource spinSfx;

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
            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }*/

            if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.SpinSuperCoroutine());
            }
        }
    }

    private IEnumerator SpinSuperCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        this.animations.SetDefaultPose();
        this.animations.SetTPose();
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);

        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.2f);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);*/

        if (this.hitboxStart != null)
            this.hitboxStart.gameObject.SetActive(true);

        this.EnableArmTrails(true);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(true);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        float time = 0.2f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, this.user.transform.forward.z * 1000f * Time.deltaTime, 0f));

            //this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 0f, 10f, 0f);

            yield return null;
        }

        this.user.rb.isKinematic = false;

        if (this.hitboxStart != null)
            this.hitboxStart.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(false);

        time = 1.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, this.user.transform.forward.z * 1500f * Time.deltaTime, 0f));

            //this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 0f, 10f, 0f);

            yield return null;
        }

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        this.EnableArmTrails(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, 0, 0);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        

        yield return new WaitForSeconds(0.85f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitboxStart != null)
            this.hitboxStart.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(false);

        this.EnableArmTrails(false);

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void EnableArmTrails(bool enable)
    {
        if (this.rightArmTrail != null)
            this.rightArmTrail.SetActive(enable);

        if (this.leftArmTrail != null)
            this.leftArmTrail.SetActive(enable);
    }
}
