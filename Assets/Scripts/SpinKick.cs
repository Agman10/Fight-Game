using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpinKick : Attack
{
    //public TestPlayer user;
    public TestHitbox hitbox;
    public TestHitbox hitbox2;
    public TempPlayerAnimations animations;
    public bool spinning;
    public bool spinKick2;
    public float spinRotationSpeed = 1200f;
    public float upwardVelocity = 0f;
    public float forwardVelocity = 10f;
    public VisualEffect fire1;
    public VisualEffect fire2;
    public bool onGoing;

    public AudioSource spinSfx;
    public AudioSource flameSfx;

    [Space]

    public float endStun = 0.4f;
    //private bool ongoing;

    //public float stunLength = 0.6f;
    //public Transform rightArm, rightArmJoint, leftArm, leftArmJoint, rightLeg, rightLegJoint, leftLeg, leftLegJoint;
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
        
        if (this.animations != null && !this.user.dead && this.onGoing)
        {
            this.Stop();
            //Debug.Log("spinkick");
            //this.animations.SetDefaultPose();
        }
            
        /*if (this.user.ragdoll != null *//*&& !this.user.dead*//* && this.onGoing)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/

        /*if (this.ongoing)
            this.user.RemoveStun(1.5f);*/

        /*if (this.user.stuns.Contains(1.5f))
            this.user.RemoveStun(1.5f);*/

        
    }
    public override void OnDeath()
    {
        this.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.spinning /*&& this.ongoing*/ && this.user != null)
        {
            /*this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, this.upwardVelocity, 0);*/
        }
    }
    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        if (this.user != null && !this.spinning /*&& !this.ongoing*/ && this.user.stuns.Count <= 0 && this.user.attackStuns.Count <= 0)
        {
            //this.user.AddStun(1.5f, true);
            this.user.AddStun(0.2f, true);
            if (this.spinKick2)
                this.StartCoroutine(this.SpinningCoroutine2());
            else
                this.StartCoroutine(this.SpinningCoroutine());

            //this.StartCoroutine(this.TryGrabbingCorutine(this.grabSpeed));
        }
    }

    IEnumerator SpinningCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        /*if (this.animations != null)
            this.animations.SetSpinKickAnimPoseStart0();*/
        /*if (this.animations != null)
            this.animations.SetSpinKickStartAnimPose1();*/
        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetSpinKickStartAnimPose();

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);*/

        yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose();
        //this.user.ragdoll.transform.localEulerAngles = new Vector3(180, 0, 0);
        this.spinning = true;

        this.PlayFire(true, 2);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        if (this.flameSfx != null)
            this.flameSfx.Play();

        float time = 0.9f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, this.upwardVelocity, 0);

            yield return null;
        }

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        //yield return new WaitForSeconds(0.9f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);


        this.spinning = false;
        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd1();
        yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd2();
        yield return new WaitForSeconds(0.025f);
        if (this.animations != null)
            this.animations.SetSpinKickAnimPoseEnd3();
        yield return new WaitForSeconds(0.025f);

        //yield return new WaitForSeconds(0.1f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);


        this.PlayFire(false, 2);
        this.spinning = false;
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        //yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, 0, 0);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        

        yield return new WaitForSeconds(this.endStun);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    IEnumerator SpinningCoroutine2()
    {
        //yield return new WaitForSeconds(0.05f);
        this.onGoing = true;
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.SetSpinKick2AnimPoseStart();

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
        {
            this.animations.SetSpinKickAnimPose2();
            //this.user.ragdoll.transform.localPosition = new Vector3(0, 2.25f, 0);
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1.4f, 1);*/
        }
        //this.user.ragdoll.transform.localEulerAngles = new Vector3(180, 0, 0);
        this.spinning = true;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.PlayFire(true, 1);
        //this.user.rb.AddForce(1000, 1000, 0);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        if (this.flameSfx != null)
            this.flameSfx.Play();

        float time = 0.9f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, this.upwardVelocity, 0);

            yield return null;
        }

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        /*time = 0.4f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.forwardVelocity, -2, 0);

            yield return null;
        }*/

        //yield return new WaitForSeconds(0.9f);
        this.spinning = false;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose2End();
        yield return new WaitForSeconds(0.05f);
        /*if (this.animations != null)
            this.animations.SetSpinKickAnimPose2();
        yield return new WaitForSeconds(0.025f);*/
        if (this.animations != null)
            this.animations.SetSpinKickAnimPose2End2();
        yield return new WaitForSeconds(0.05f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        this.PlayFire(false, 1);
        this.spinning = false;
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);

        this.user.rb.velocity = new Vector3(0, 0, 0);
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            /*if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);*/
        }
        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/
        //this.user.ragdoll.transform.localPosition = new Vector3(0, 1.95f, 0);

        //yield return new WaitForSeconds(0.05f);


        
        yield return new WaitForSeconds(this.endStun);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayFire(bool playing, int amount)
    {
        if (amount == 1)
        {
            if(this.fire1 != null)
            {
                if (playing)
                    this.fire1.Play();
                else
                    this.fire1.Stop();
            }
        }
        if (amount == 2)
        {
            if (playing)
            {
                if (this.fire1 != null)
                    this.fire1.Play();

                if (this.fire2 != null)
                    this.fire2.Play();
            }
            else
            {
                if (this.fire1 != null)
                    this.fire1.Stop();

                if (this.fire2 != null)
                    this.fire2.Stop();
            }
        }
    }
    public override void Stop()
    {
        this.StopAllCoroutines();
        if (this.animations != null /*&& !this.user.dead*/)
        {
            //this.animations.SetDefaultPose();
            if (this.animations.rightLeg != null)
                this.animations.rightLeg.localScale = new Vector3(1, 1f, 1);
        }
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        this.PlayFire(false, 2);
        this.spinning = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
        
    }
}
