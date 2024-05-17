using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UppercutAttack : Attack
{
    //public TestPlayer user;
    public TestHitbox kickHitbox;
    public TestHitbox punchHitbox1;
    public TestHitbox punchHitbox2;
    public TempPlayerAnimations animations;

    public bool isPunch;
    public int uppercutId;
    public VisualEffect fire;
    public GameObject trail;
    public bool onGoing;

    public float kickAnimSpeed = 0.075f;
    public float kickEndTime = 0.4f;
    public float kickXForce = 200f;
    public float kickYForce = 1000f;
    // Start is called before the first frame update
    public override void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
        }
            
    }
    public override void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
        }
            
    }

    public override void OnHit()
    {
        
        if (/*this.animations != null && */!this.user.dead && this.onGoing)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());

            /*this.Stop();
            //Debug.Log("Test");
            if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
            
        /*if (this.user.ragdoll != null *//*&& !this.user.dead*//* && this.onGoing)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        //this.user.RemoveStun(1.3f);
    }
    public override void OnDeath()
    {
        this.Stop();
    }
    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        if(this.user != null)
        {
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.AddStun(1.3f, true);
                this.user.AddStun(0.2f, true);
                //this.StartCoroutine(this.KickUppercutCoroutine());

                if (this.uppercutId == 1)
                    this.StartCoroutine(this.PunchUppercutCoroutine());
                else if (this.uppercutId == 2)
                    this.StartCoroutine(this.FlameKickCoroutine());
                else
                    this.StartCoroutine(this.KickUppercutCoroutine());

                *//*if (this.isPunch)
                    this.StartCoroutine(this.PunchUppercutCoroutine());
                else
                    this.StartCoroutine(this.KickUppercutCoroutine());*//*
                
            }*/

            if (this.uppercutId == 1)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.StartCoroutine(this.PunchUppercutCoroutine());
                    this.user.AddStun(0.2f, true);
                }
            }
            else if (this.uppercutId == 2)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.StartCoroutine(this.FlameKickCoroutine());
                    this.user.AddStun(0.2f, true);
                }
            }
            else if (this.uppercutId == 3)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.StartCoroutine(this.PunchUppercutCoroutine2(1000f));
                else
                    this.StartCoroutine(this.PunchUppercutCoroutine2(1300f));

                this.user.AddStun(0.2f, true);
            }
            else if (this.uppercutId == 4)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    this.StartCoroutine(this.KickUppercutCoroutine(this.kickYForce));
                else
                    this.StartCoroutine(this.KickUppercutCoroutine(this.kickYForce + 200f));

                this.user.AddStun(0.2f, true);
            }
            else
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.StartCoroutine(this.KickUppercutCoroutine(this.kickYForce));
                    this.user.AddStun(0.2f, true);
                }
            }

        }
    }
    IEnumerator KickUppercutCoroutine(float yForce = 1000f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //float animSpeed = 0.075f;
        float animSpeed = this.kickAnimSpeed;
        //Debug.Log(0.3f + (0.3f - (animSpeed * 3)));

        if (this.animations != null)
            this.animations.SetKickUppercutStartAnim();

        /*if (this.trail != null)
            this.trail.SetActive(true);*/
        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetKickUppercutStartAnim2();

        if (this.trail != null)
            this.trail.SetActive(true);
        yield return new WaitForSeconds(0.05f);

        /*if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 200, 1000, 0);*/

        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * this.kickXForce, yForce, 0);

        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, this.user.transform.forward.z * 90, 50);*/

        if (this.animations != null)
        {
            this.animations.SetKickUpercutAnim0();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
        {
            this.animations.SetKickUpercutAnim();
        }


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
        {
            this.animations.SetKickUpercutAnim2();
        }


        yield return new WaitForSeconds(animSpeed);

        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, this.user.transform.forward.z * 90, 30);*/

        if (this.animations != null)
        {
            this.animations.SetKickUpercutAnim3();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);*/
        }






        //yield return new WaitForSeconds(0.3f + (0.3f -(animSpeed * 3)));

        yield return new WaitForSeconds(0.2f);

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.3f -(animSpeed * 3));
        yield return new WaitForSeconds(0.1f + (0.3f - (animSpeed * 3)));



        /*if (this.animations != null)
        {
            this.animations.SetKickUpercutEndAnim();
        }*/

        if (this.trail != null)
            this.trail.SetActive(false);
        //yield return new WaitForSeconds(0.1f);

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.4f);
        yield return new WaitForSeconds(this.kickEndTime);

        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);*/
        }
        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);


        yield return new WaitForSeconds(0.25f);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    IEnumerator PunchUppercutCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim2();
        yield return new WaitForSeconds(0.05f);
        this.PlayFire(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 200, 1000, 0);

        if (this.animations != null)
        {
            this.animations.SetPunchUpercutAnim();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }

        if (this.punchHitbox1 != null)
            this.punchHitbox1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        if (this.punchHitbox1 != null)
            this.punchHitbox1.gameObject.SetActive(false);

        if (this.punchHitbox2 != null)
            this.punchHitbox2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        if (this.punchHitbox2 != null)
            this.punchHitbox2.gameObject.SetActive(false);

        this.PlayFire(false);

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




        /*yield return new WaitForSeconds(0.025f);
        this.animations.body.localEulerAngles = new Vector3(0f, -125f, 0f);
        yield return new WaitForSeconds(0.025f);
        this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, 145f, 0f);
        yield return new WaitForSeconds(0.025f);
        //this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, 55f, 0f);
        yield return new WaitForSeconds(0.025f);
        //this.animations.SetDefaultPose();
        this.animations.body.localEulerAngles = new Vector3(0f, 0f, 0f);*/


        /*yield return new WaitForSeconds(0.025f);
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, -90, 0);
        yield return new WaitForSeconds(0.025f);
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
        }
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(0.025f);
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 90, 0);
        yield return new WaitForSeconds(0.025f);
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/



        /*if (this.animations != null)
        {
            this.animations.SetDefaultPose();
        }*/
        yield return new WaitForSeconds(0.3f);
        
        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        yield return new WaitForSeconds(0.25f);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);


    }


    IEnumerator PunchUppercutCoroutine2(float upForce = 1000f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim2();
        yield return new WaitForSeconds(0.05f);
        this.PlayFire(true);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 200, upForce, 0);

        if (this.animations != null)
        {
            this.animations.SetPunchUpercutAnim();
        }

        if (this.punchHitbox1 != null)
            this.punchHitbox1.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        if (this.punchHitbox1 != null)
            this.punchHitbox1.gameObject.SetActive(false);

        if (this.punchHitbox2 != null)
            this.punchHitbox2.gameObject.SetActive(true);

        //this.PlayFire(false);

        yield return new WaitForSeconds(0.1f);
        if (this.punchHitbox2 != null)
            this.punchHitbox2.gameObject.SetActive(false);

        this.PlayFire(false);

        yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);
        this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);
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
        this.StopAllCoroutines();

        /*if (this.user.ragdoll != null)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        if (this.animations != null /*&& !this.user.dead*/)
        {
            //this.animations.SetDefaultPose();
            if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);
        }

        if (this.punchHitbox1 != null)
            this.punchHitbox1.gameObject.SetActive(false);
        if (this.punchHitbox2 != null)
            this.punchHitbox2.gameObject.SetActive(false);
        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(false);

        if (this.trail != null)
            this.trail.SetActive(false);

        this.PlayFire(false);
        this.onGoing = false;

        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator FlameKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetPunchUppercutStartAnim1();

        yield return new WaitForSeconds(0.1f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 300, 900, 0);

        if (this.animations != null)
            this.animations.SuperFlameSpin2();

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(true);

        if (this.trail != null)
            this.trail.SetActive(true);

        float currentTime = 0;
        float duration = 0.25f;
        //float targetVolume = 0.1f;
        float targetRotation = 245f;
        //float targetRotation = 360f;
        float startRotation = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(startRotation, targetRotation, currentTime / duration));
            yield return null;
        }

        if (this.trail != null)
            this.trail.SetActive(false);

        if (this.kickHitbox != null)
            this.kickHitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        

        if (this.animations != null)
            this.animations.SetDefaultPose();

        

        yield return new WaitForSeconds(0.5f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        yield return new WaitForSeconds(0.25f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }
}
