using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperFlameSpin : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public GameObject startParticle;

    public int animationId;

    public VisualEffect rightHandFlame;
    public VisualEffect leftHandFlame;
    public VisualEffect rightLegFlame;
    public VisualEffect leftLegFlame;

    public AudioSource spinSfx;
    public AudioSource flameSfx;

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
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SuperFlameSpinCoroutine());

                }
            }
            
        }
    }

    private IEnumerator SuperFlameSpinCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.TestPose3();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.35f), this.user.transform.position.y + 1.5f, -0.8f), Quaternion.Euler(0, 0, 0));
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2.4f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);
        if (this.animationId == 1 && this.animations != null)
        {
            this.animations.SuperFlameSpin2();
        }
        else
        {
            this.animations.SuperFlameSpin1();
        }
        


        this.PlayFire(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 400, 1200, 0);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        if (this.flameSfx != null)
            this.flameSfx.Play();

        float time = 0.6f;
        while (time > 0)
        {
            time -= Time.deltaTime;

            if (this.animationId == 1 && this.animations != null)
            {
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, 1500f * Time.deltaTime));
            }
            else
            {
                this.animations.body.transform.Rotate(new Vector3(this.user.transform.forward.z * -1500f * Time.deltaTime, 0f, 0f));
            }

            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(this.user.transform.forward.z * -1500f * Time.deltaTime, 0f, 0f));*/

            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, 1500f * Time.deltaTime));*/

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, this.upwardVelocity, 0);

            yield return null;
        }
        if (this.spinSfx != null)
            this.spinSfx.Stop();


        //yield return new WaitForSeconds(0.5f);
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        time = 0.1f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(1500f * Time.deltaTime, 0f, 0f));

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, this.upwardVelocity, 0);

            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        this.PlayFire(false);

        yield return new WaitForSeconds(0.3f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        this.PlayFire(false);
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
        
    }

    public void PlayFire(bool playing)
    {
        if (this.rightHandFlame != null && this.leftHandFlame != null && this.rightLegFlame != null && this.leftLegFlame != null)
        {
            if (playing)
            {
                this.rightHandFlame.Play();
                this.leftHandFlame.Play();
                this.rightLegFlame.Play();
                this.leftLegFlame.Play();
            }
            else
            {
                this.rightHandFlame.Stop();
                this.leftHandFlame.Stop();
                this.rightLegFlame.Stop();
                this.leftLegFlame.Stop();
            }
        }
    }
}
