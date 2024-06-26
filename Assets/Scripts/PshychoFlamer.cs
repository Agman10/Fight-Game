using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PshychoFlamer : Attack
{
    public TestHitbox hitbox;
    public TempPlayerAnimations animations;
    //public bool spinning;
    public bool onGoing;
    public bool canBeCanceled;
    public float duration = 1f;
    public float speed = 10f;
    public int startPoseId;

    public Transform flameTransform;

    public VisualEffect fire;
    public GameObject startParticle;


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && this.canBeCanceled)
        {
            this.Stop();
            //Debug.Log("Test");
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }

        /*if (this.user.ragdoll != null *//*&& !this.user.dead*//* && this.onGoing)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge / 2)
            {
                this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                //this.user.AddStun(1.3f, true);
                this.user.AddStun(0.2f, true);
                //this.StartCoroutine(this.KickUppercutCoroutine());
                this.StartCoroutine(this.PshychoFlamerCoroutine());
                /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                    //this.user.AddStun(1.3f, true);
                    this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.KickUppercutCoroutine());
                    this.StartCoroutine(this.PshychoFlamerCoroutine());

                }*/
            }
            
        }
    }

    IEnumerator PshychoFlamerCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.canBeCanceled = true;

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            if (this.startPoseId == 1)
                this.animations.SetPshychoFlamerStartPose2();
            else
                this.animations.SetPshychoFlamerStartPose();
        }

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();


        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));

            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.35f), this.user.transform.position.y + 1.5f, -0.8f), Quaternion.Euler(0, 0, 0));

            if (this.startPoseId == 1)
                startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x/* + (this.user.transform.forward.z * 0.35f)*/, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            else
                startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.35f), this.user.transform.position.y + 1.5f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        

        yield return new WaitForSeconds(0.3f);

        this.user.rb.isKinematic = false;

        this.user.knockbackInvounrability = true;
        this.canBeCanceled = false;
        this.PlayFire(true);
        yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.SetPshychoFlamerPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if(this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            capsuleCollider.height = 2.5f;
            capsuleCollider.center = new Vector3(0f, 1.25f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
        }



        //Testing angle
        float time = this.duration;

        if (this.user.transform.position.y > 0f)
        {
            this.animations.body.transform.localEulerAngles = new Vector3(0f, 0f, -120f);

            if (this.flameTransform != null)
                this.flameTransform.localEulerAngles = new Vector3(0f, 0f, -120f);
        }
        else
        {
            //this.animations.body.transform.localEulerAngles = new Vector3(0f, 0f, -90f);

            if (this.flameTransform != null)
                this.flameTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        while (time > 0 && this.user.transform.position.y > 0f)
        {
            if (this.user.animations != null)
            {
                this.user.ragdoll.transform.Rotate(new Vector3(0, (this.user.transform.forward.z * -2000f) * Time.deltaTime, 0));
                this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.speed, -10f, 0);
            }
            time -= Time.deltaTime;

            if (time <= 0.2f)
            {
                this.PlayFire(false);
                //Debug.Log(time);
            }


            yield return null;
        }
        //Debug.Log(time);
        if(time > 0f)
        {
            this.animations.body.transform.localEulerAngles = new Vector3(0f, 0f, -90f);

            if (this.flameTransform != null)
                this.flameTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }
        //Testing angle end


        //float time = this.duration;
        while (time > 0)
        {
            if(this.user.animations != null)
            {
                this.user.ragdoll.transform.Rotate(new Vector3(0, (this.user.transform.forward.z * -2000) * Time.deltaTime, 0));
                this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.speed, 0, 0);
            }
            time -= Time.deltaTime;

            if (time <= 0.2f)
            {
                this.PlayFire(false);
                //Debug.Log(time);
            }
                

            yield return null;
        }
        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleColliderr)
        {
            capsuleColliderr.height = 3f;
            capsuleColliderr.center = new Vector3(0f, 1.5f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y + 0.5f, 0f);
        }

        this.user.rb.velocity = new Vector3(0, 0, 0);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        

        

        this.user.knockbackInvounrability = false;
        this.canBeCanceled = true;

        if (this.animations != null)
            this.animations.PshychoFlamerEndPose();

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);
        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
        this.canBeCanceled = false;
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
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.PlayFire(false);
        //this.spinning = false;
        this.user.knockbackInvounrability = false;
        this.onGoing = false;
        this.canBeCanceled = false;

        this.user.rb.isKinematic = false;

        if (this.flameTransform != null)
            this.flameTransform.localEulerAngles = new Vector3(0f, 0f, -90f);


        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            capsuleCollider.height = 3f;
            capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y + 0.5f, 0f);
        }

        this.user.attackStuns.Remove(this.gameObject);
    }
}
