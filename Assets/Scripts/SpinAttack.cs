using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public Movement movement;
    public GameObject rightArmTrail;
    public GameObject leftArmTrail;

    public ParticleSystem dizzyParticle;
    public bool spinning = false;
    public float dizzyMeter = 0f;

    [Space]

    public float dissyRemovalSpeed = 20f;
    public float moveSpeed = 0.1f;

    public float stunTime1 = 0.2f;
    public float stunTime2 = 0.2f;

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

    private void Update()
    {
        if (!this.spinning)
        {
            if (this.dizzyMeter > 0f)
                this.dizzyMeter -= Time.deltaTime * this.dissyRemovalSpeed;
            else
                this.dizzyMeter = 0f;
        }
        
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);

            if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
            {
                this.user.AddStun(0.2f, true);
                this.user.rb.AddForce(0f, 200f, 0f);
            }
            else
            {
                this.user.AddStun(0.2f, true);
            }

            this.StartCoroutine(this.SpinAttackCoroutine());
        }
    }

    private IEnumerator SpinAttackCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        //this.user.rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SpinAttackStart();
        yield return new WaitForSeconds(0.1f);

        //this.dizzyMeter += 22f;
        this.spinning = true;

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SpinAttack();

        if (this.rightArmTrail != null && this.leftArmTrail != null)
        {
            this.rightArmTrail.SetActive(true);
            this.leftArmTrail.SetActive(true);
        }
            

        float time = 1f;
        Vector3 eyeRotation = this.animations.eyes.transform.eulerAngles;
        while (time > 0)
        {
            if (this.animations != null)
            {
                //this.animations.body.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -1500));

                this.animations.body.transform.Rotate(Vector3.up, -1500 * Time.deltaTime);

                /*this.animations.upperBody.transform.Rotate(Vector3.up, -1500 * Time.deltaTime);
                this.animations.eyes.transform.eulerAngles = eyeRotation;*/
            }

            this.dizzyMeter += Time.deltaTime * 40f;

            time -= Time.deltaTime;

            if (this.user.movement != null)
                this.user.movement.Move(this.user.movement.playerInput.moveInput * this.moveSpeed);

            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x, this.user.rb.velocity.y * 0.5f, 0f);

            yield return null;
        }

        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = this.stunTime1;
        //float targetVolume = 0.1f;
        float targetRotation = 0f;
        float start = this.animations.body.transform.localEulerAngles.y;
        //float start = this.animations.upperBody.transform.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.animations.body.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(start, targetRotation, currentTime / duration), 0);
            /*this.animations.upperBody.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(start, targetRotation, currentTime / duration), 0);
            this.animations.eyes.transform.eulerAngles = eyeRotation;*/

            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x, this.user.rb.velocity.y * 0.75f, 0f);
            yield return null;
        }
        if (this.animations != null)
            this.animations.body.localEulerAngles = new Vector3(0, 0, 0);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.rightArmTrail != null && this.leftArmTrail != null)
        {
            this.rightArmTrail.SetActive(false);
            this.leftArmTrail.SetActive(false);
        }
        this.spinning = false;

        /*if (this.animations != null)
            this.animations.SpinAttackStart();
        yield return new WaitForSeconds(0.1f);*/

        //yield return new WaitForSeconds(1f);
        //this.spinning = false;
        if (this.dizzyMeter >= 100)
        {
            this.spinning = false;

            if (this.animations != null)
                this.animations.Dizzy();

            if (this.dizzyParticle != null)
                this.dizzyParticle.gameObject.SetActive(true);

            while (this.dizzyMeter > 0)
            {
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                yield return null;
            }

            if (this.animations != null)
                this.animations.SetDefaultPose();

            if (this.dizzyParticle != null)
                this.dizzyParticle.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
        else
        {
            yield return new WaitForSeconds(this.stunTime1);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(this.stunTime2);

            this.spinning = false;

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }

        /*yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        this.spinning = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }
    public override void Stop()
    {
        base.Stop();
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);


        if (this.rightArmTrail != null && this.leftArmTrail != null)
        {
            this.rightArmTrail.SetActive(false);
            this.leftArmTrail.SetActive(false);
        }

        this.spinning = false;

        if (this.dizzyParticle != null)
            this.dizzyParticle.gameObject.SetActive(false);


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
