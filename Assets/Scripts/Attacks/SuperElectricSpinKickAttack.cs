using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperElectricSpinKickAttack : Attack
{
    //public TestPlayer user;
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox;
    public TestHitbox hitbox2;
    public GameObject startParticle;

    public bool spinning;
    public bool spinKick2;
    public float spinRotationSpeed = 2000f;
    public float upwardVelocity = 10f;
    public float forwardVelocity = 0f;
    public float duration = 0.9f;


    public ParticleSystem electricity1;
    public ParticleSystem electricity2;

    public GameObject windHitboxes;

    public AudioSource spinSfx;



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

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge / 2)
            {
                //this.user.AddStun(1.5f, true);
                this.user.GiveSuperCharge(-(this.user.maxSuperCharge / 2));
                this.user.AddStun(0.2f, true);
                if (this.spinKick2)
                    this.StartCoroutine(this.SpinningCoroutine2());
                else
                    this.StartCoroutine(this.SpinningCoroutine());
            }
            

            //this.StartCoroutine(this.TryGrabbingCorutine(this.grabSpeed));
        }
    }

    IEnumerator SpinningCoroutine()
    {
        //yield return new WaitForSeconds(0.05f);
        this.onGoing = true;
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.SetSpinKick2AnimPoseStart();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.8f, -0.8f), Quaternion.Euler(0, 0, 0));
        }
        this.user.rb.isKinematic = true;

        //yield return new WaitForSeconds(0.2f);


        yield return new WaitForSeconds(0.19f);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.01f);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(false);



        this.user.rb.isKinematic = false;

        if (this.animations != null)
        {
            this.animations.SetSpinKickAnimPose2();
        }
        //this.user.ragdoll.transform.localEulerAngles = new Vector3(180, 0, 0);
        this.spinning = true;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.PlayElectricity(true, 1);
        //this.user.rb.AddForce(1000, 1000, 0);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        float time = this.duration;
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
        this.spinning = false;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose2End();
        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose2End2();
        yield return new WaitForSeconds(0.05f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        this.PlayElectricity(false, 1);
        this.spinning = false;
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);

        this.user.rb.velocity = new Vector3(0, 0, 0);
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
        }



        yield return new WaitForSeconds(0.8f);
        //Debug.Log("end 1");
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    IEnumerator SpinningCoroutine2()
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

        //yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetSpinKickStartAnimPose();

        this.user.rb.isKinematic = true;

        //yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(0.19f);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.01f);

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(false);



        this.user.rb.isKinematic = false;

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose();

        this.spinning = true;

        this.PlayElectricity(true, 2);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        float time = this.duration;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));

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

        //yield return new WaitForSeconds(0.045f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);


        this.PlayElectricity(false, 2);
        this.spinning = false;
        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        //yield return new WaitForSeconds(0.05f);

        this.user.rb.velocity = new Vector3(0, 0, 0);
        if (this.animations != null)
            this.animations.SetDefaultPose();



        yield return new WaitForSeconds(0.8f);
        //Debug.Log("end 2");
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    

    public void PlayElectricity(bool playing, int amount)
    {
        if (amount == 1)
        {
            if (this.electricity1 != null)
            {
                if (playing)
                    this.electricity1.Play();
                else
                    this.electricity1.Stop();
            }
        }
        if (amount == 2)
        {
            if (playing)
            {
                if (this.electricity1 != null)
                    this.electricity1.Play();

                if (this.electricity2 != null)
                    this.electricity2.Play();
            }
            else
            {
                if (this.electricity1 != null)
                    this.electricity1.Stop();

                if (this.electricity2 != null)
                    this.electricity2.Stop();
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

        if (this.windHitboxes != null)
            this.windHitboxes.gameObject.SetActive(false);

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        this.user.rb.isKinematic = false;

        this.PlayElectricity(false, 2);
        this.spinning = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

    }
}
