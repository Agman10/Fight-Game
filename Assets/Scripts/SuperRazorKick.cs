using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRazorKick : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public TestHitbox hitbox3;
    public GameObject startParticle;
    public GameObject trail;

    public AudioSource uppcutSfx;
    public AudioSource kickDownSfx;
    public AudioSource startSfx;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            //Debug.Log("Test");
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }

        //this.user.RemoveStun(1.3f);
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
                    //this.user.AddStun(1.3f, true);
                    this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.KickUppercutCoroutine());
                    this.StartCoroutine(this.SuperRazorKickCoroutine());

                }
            }
            
        }
    }

    IEnumerator SuperRazorKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        float animSpeed = 0.05f;
        //Debug.Log(0.3f + (0.3f - (animSpeed * 3)));

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.startSfx != null)
        {
            //this.startSfx.time = 0.05f;
            this.startSfx.Play();
        }

        //FIRST
        /*if (this.animations != null)
            this.animations.SetKickUppercutStartAnim();
        yield return new WaitForSeconds(0.05f);*/

        /*if (this.animations != null)
            this.animations.SuperRazorKickStart();*/

        if (this.animations != null)
            this.animations.SetKickUppercutStartAnim();

        yield return new WaitForSeconds(0.25f);

        /*if (this.animations != null)
            this.animations.SetKickUppercutStartAnim();
        yield return new WaitForSeconds(0.05f);*/


        if (this.animations != null)
            this.animations.SetKickUppercutStartAnim2();

        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.uppcutSfx != null)
        {
            this.uppcutSfx.time = 0.25f;
            this.uppcutSfx.Play();
        }
        yield return new WaitForSeconds(0.05f);

        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 200, 750, 0);
        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, this.user.transform.forward.z * 90, 50);*/

        if (this.animations != null)
            this.animations.SetKickUpercutAnim0();

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);
        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim();


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim2();


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim3();

        yield return new WaitForSeconds(animSpeed);
        //SECOND

        if (this.uppcutSfx != null)
        {
            this.uppcutSfx.time = 0.2f;
            this.uppcutSfx.Play();
        }

        if (this.trail != null)
            this.trail.SetActive(false);
        yield return new WaitForSeconds(0.05f);

        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.animations != null)
            this.animations.SetKickUppercutStartAnim2();

        yield return new WaitForSeconds(animSpeed);

        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 100, 500, 0);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim0();

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);
        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim();


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim2();


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetKickUpercutAnim3();

        yield return new WaitForSeconds(0.1f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);
        if (this.trail != null)
            this.trail.SetActive(false);

        //THIRD

        if (this.kickDownSfx != null)
        {
            //this.kickDownSfx.time = 0.1f;
            this.kickDownSfx.time = 0.2f;
            this.kickDownSfx.Play();
        }

        yield return new WaitForSeconds(animSpeed);
        if (this.trail != null)
            this.trail.SetActive(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 200, 500, 0);
        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd1();
        yield return new WaitForSeconds(animSpeed);
        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(true);
        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd2();
        yield return new WaitForSeconds(animSpeed);
        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd3();

        yield return new WaitForSeconds(animSpeed);
        if (this.trail != null)
            this.trail.SetActive(false);
        this.animations.SetDefaultPose();
        //this.animations.SuperRazorKickEnd();
        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(false);


        /*yield return new WaitForSeconds(0.25f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.25f);*/

        yield return new WaitForSeconds(0.5f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);


        yield return new WaitForSeconds(0.25f);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();
        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        if (this.animations != null /*&& !this.user.dead*/)
        {
            //this.animations.SetDefaultPose();
            if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);
        }

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.trail != null)
            this.trail.SetActive(false);
        this.onGoing = false;

        this.user.attackStuns.Remove(this.gameObject);
    }
}
