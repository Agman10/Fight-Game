using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperUppercut : Attack
{
    public bool onGoing;
    public TempPlayerAnimations animations;
    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public TestHitbox hitbox3;
    public TestHitbox hitbox4;
    public VisualEffect fire;
    public GameObject startParticle;

    public float endLag1 = 0.3f;
    public float endLag2 = 0.25f;

    public override void OnHit()
    {
        base.OnHit();
        if (this.animations != null && !this.user.dead && this.onGoing)
        {
            this.Stop();
            //Debug.Log("Test");
            this.animations.SetDefaultPose();
        }

        if (this.user.ragdoll != null /*&& !this.user.dead*/ && this.onGoing)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);
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
                    this.StartCoroutine(this.SuperUppercutCoroutine());

                }
            }
            
        }
    }
    IEnumerator SuperUppercutCoroutine()
    {
        //FIRST
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.35f), this.user.transform.position.y + 1.5f, -0.8f), Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(0.25f);
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim2();
        yield return new WaitForSeconds(0.05f);
        this.PlayFire(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 150, 500, 0);

        if (this.animations != null)
        {
            this.animations.SetPunchUpercutAnim();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        if (this.hitbox4 != null)
            this.hitbox4.gameObject.SetActive(true);

        /*if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);*/

        this.PlayFire(false);

        if (this.user.rb != null)
            this.user.rb.AddForce(0f, -200, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/


        /*float time = 0.05f;
        while (time > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time -= Time.deltaTime;

            yield return null;
        }
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            //this.animations.SetPunchUppercutStartAnim1();
        }
        float time2 = 0.05f;
        while (time2 > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time2 -= Time.deltaTime;

            yield return null;
        }
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        yield return new WaitForSeconds(0.015f);
        if (this.hitbox4 != null)
            this.hitbox4.gameObject.SetActive(false);

        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -125f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
        yield return new WaitForSeconds(0.015f);

        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 145f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 35f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 0f);


        yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);



        //SECOND
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim2();
        yield return new WaitForSeconds(0.05f);
        this.PlayFire(true);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 150, 600, 0);

        if (this.animations != null)
        {
            this.animations.SetPunchUpercutAnim();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }

        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);

        if (this.hitbox4 != null)
            this.hitbox4.gameObject.SetActive(true);

        /*if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);*/

        this.PlayFire(false);

        if (this.user.rb != null)
            this.user.rb.AddForce(0f, -200, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/


        /*time = 0.05f;
        while (time > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time -= Time.deltaTime;

            yield return null;
        }
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            //this.animations.SetPunchUppercutStartAnim1();
        }
        time2 = 0.05f;
        while (time2 > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time2 -= Time.deltaTime;

            yield return null;
        }
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/
        yield return new WaitForSeconds(0.015f);
        if (this.hitbox4 != null)
            this.hitbox4.gameObject.SetActive(false);

        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -125f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
        yield return new WaitForSeconds(0.015f);

        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 145f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 35f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 0f);

        yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);




        //THIRD
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim2();
        yield return new WaitForSeconds(0.05f);
        this.PlayFire(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 300, 1000, 0);

        if (this.animations != null)
        {
            this.animations.SetPunchUpercutAnim();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(false);

        this.PlayFire(false);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/


        /*time = 0.05f;
        while (time > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time -= Time.deltaTime;

            yield return null;
        }
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            //this.animations.SetPunchUppercutStartAnim1();
        }
        time2 = 0.05f;
        while (time2 > 0)
        {
            this.user.ragdoll.transform.Rotate(Vector3.up, Time.deltaTime * (this.user.transform.forward.z * -2000));
            time2 -= Time.deltaTime;

            yield return null;
        }
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -125f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
        yield return new WaitForSeconds(0.015f);

        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 145f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 35f, 0f);
        yield return new WaitForSeconds(0.015f);
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 0f);

        yield return new WaitForSeconds(this.endLag1);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);


        yield return new WaitForSeconds(this.endLag2);
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
    public override void Stop()
    {
        base.Stop();
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
        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(false);

        if (this.hitbox4 != null)
            this.hitbox4.gameObject.SetActive(false);
        this.PlayFire(false);
        this.onGoing = false;

        this.user.attackStuns.Remove(this.gameObject);
    }
}
