using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperKickBarrage : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitboxLower;
    public TestHitbox hitboxUpper;
    public TestHitbox hitboxEnd;
    public TestHitbox hitboxSpinKick;
    public TestHitbox hitboxSpinKickEnd;

    public TestHitbox firtsHitbox;

    public GameObject startParticle;

    public AudioSource kickSfx;
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

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {


            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.KickBarrageCoroutine());

                }
            }
        }
    }

    private IEnumerator KickBarrageCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float animSpeed = 0.01f;

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        if (this.user.soundEffects != null)
        {
            this.user.soundEffects.PlaySuperSfx();
        }

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 2);

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0f), this.user.transform.position.y + 1.62f, -1.25f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 2);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(true);

        if (this.firtsHitbox != null)
            this.firtsHitbox.gameObject.SetActive(true);

        this.PlayKickSfx(1.1f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(false);

        if (this.firtsHitbox != null)
            this.firtsHitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 2);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);

        yield return new WaitForSeconds(animSpeed);


        //2



        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 1);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 1);

        if (this.hitboxUpper != null)
            this.hitboxUpper.gameObject.SetActive(true);

        this.PlayKickSfx(1.2f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxUpper != null)
            this.hitboxUpper.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 1);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);


        //3


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 1);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 1);

        if (this.hitboxUpper != null)
            this.hitboxUpper.gameObject.SetActive(true);

        this.PlayKickSfx(1.1f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxUpper != null)
            this.hitboxUpper.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 1);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);


        //4



        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 2);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 2);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(true);

        this.PlayKickSfx(1.2f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 2);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);



        //5




        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 2);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 2);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(true);

        this.PlayKickSfx(1.1f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 2);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);



        //6


        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(1, 1);

        yield return new WaitForSeconds(animSpeed);

        this.user.rb.AddForce(new Vector3(this.user.transform.forward.z * 250f, 0f, 0f));

        if (this.animations != null)
            this.animations.ForwardKickShippu(2, 1);

        if (this.hitboxEnd != null)
            this.hitboxEnd.gameObject.SetActive(true);

        this.PlayKickSfx(1.2f);

        yield return new WaitForSeconds(animSpeed + 0.05f);

        if (this.hitboxEnd != null)
            this.hitboxEnd.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ForwardKickShippu(3, 1);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(5);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ForwardKickShippu(6);


        //Spin



        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetSpinKick2AnimPoseStart();

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.SetSpinKickAnimPose2();


        this.user.rb.AddForce(new Vector3(/*this.user.transform.forward.z * 200f*/ 0f, 1000, 0f));

        if (this.hitboxSpinKick != null)
            this.hitboxSpinKick.gameObject.SetActive(true);

        if (this.spinSfx != null)
            this.spinSfx.Play();

        float time = 0.35f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, -2000f * Time.deltaTime, 0));

            //this.user.rb.velocity = new Vector3(0f, 12f, 0);

            yield return null;
        }


        if (this.hitboxSpinKick != null)
            this.hitboxSpinKick.gameObject.SetActive(false);

        if (this.hitboxSpinKickEnd != null)
            this.hitboxSpinKickEnd.gameObject.SetActive(true);

        time = 0.3f;
        while (Mathf.Abs(this.user.transform.position.y) > 0f && time > 0)
        {
            time -= Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, -2000f * Time.deltaTime, 0));

            yield return null;
        }

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        if (this.hitboxSpinKickEnd != null)
            this.hitboxSpinKickEnd.gameObject.SetActive(false);



        yield return new WaitForSeconds(animSpeed);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.25f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayKickSfx(float pitch = 1f)
    {
        if (this.kickSfx != null)
        {
            this.kickSfx.time = 0.05f;
            this.kickSfx.pitch = pitch;
            this.kickSfx.Play();
        }
    }

    public override void Stop()
    {
        base.Stop();

        if (this.hitboxLower != null)
            this.hitboxLower.gameObject.SetActive(false);
        if (this.hitboxUpper != null)
            this.hitboxUpper.gameObject.SetActive(false);
        if (this.hitboxSpinKick != null)
            this.hitboxSpinKick.gameObject.SetActive(false);

        if (this.hitboxEnd != null)
            this.hitboxEnd.gameObject.SetActive(false);

        if (this.hitboxSpinKickEnd != null)
            this.hitboxSpinKickEnd.gameObject.SetActive(false);

        if (this.spinSfx != null)
            this.spinSfx.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
