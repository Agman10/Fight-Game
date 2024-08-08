using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollUpAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox;
    public TestHitbox hitbox2;

    public ParticleSystem electricity;
    public GameObject glowingEyes;

    public float xForce = 300f;
    public float yForce = 1000f;

    public AudioSource rollSfx;

    public int rollUpId = 0;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());
            /*this.Stop();
            if (this.animations != null)
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
            this.user.AddStun(0.2f, true);
            //this.StartCoroutine(this.RollUpCoroutine());

            if(this.rollUpId == 1)
                this.StartCoroutine(this.RollUpImprovedCoroutine());
            else
                this.StartCoroutine(this.RollUpCoroutine());
        }
    }

    private IEnumerator RollUpCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.RollAnimation();

        /*if (this.electricity != null)
            this.electricity.gameObject.SetActive(true);*/

        yield return new WaitForSeconds(0.1f);

        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * this.xForce, this.yForce, 0);
        //yield return new WaitForSeconds(1f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.ChangeCollision(true);

        if (this.electricity != null)
            this.electricity.Play();

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);*/

        if (this.rollSfx != null)
            this.rollSfx.Play();

        float currentTime = 0;
        float duration = 0.6f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-3000f * Time.deltaTime));

            yield return null;
        }
        if (this.electricity != null)
            this.electricity.Stop();

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);*/

        //yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.ChangeCollision(false);

        yield return new WaitForSeconds(0.1f);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        yield return new WaitForSeconds(0.45f);
        /*if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        this.ChangeCollision(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        if (this.electricity != null)
            this.electricity.Stop();

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);*/

        /*if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);*/

    }


    public void ChangeCollision(bool round = false)
    {
        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            if (round)
            {
                capsuleCollider.radius = 0.65f;
                capsuleCollider.height = 0.65f;

                capsuleCollider.center = new Vector3(0f, 1.65f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }
            else
            {
                capsuleCollider.radius = 0.65f;
                capsuleCollider.height = 2.25f;

                capsuleCollider.center = new Vector3(0f, 1.125f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }

        }
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }


    private IEnumerator RollUpImprovedCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.RollAnimation();

        /*if (this.electricity != null)
            this.electricity.gameObject.SetActive(true);*/



        yield return new WaitForSeconds(0.05f);

        float extraYForce = 0f;
        if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
            extraYForce = 200f;

        //Debug.Log(extraYForce);

        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * this.xForce, this.yForce + extraYForce, 0);
        //yield return new WaitForSeconds(1f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.ChangeCollision(true);

        if (this.electricity != null)
            this.electricity.Play();

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);*/

        if (this.rollSfx != null)
            this.rollSfx.Play();

        float currentTime = 0;
        float duration = 0.6f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-3000f * Time.deltaTime));

            yield return null;
        }
        if (this.electricity != null)
            this.electricity.Stop();

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);*/

        //yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        /*if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);*/

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.ChangeCollision(false);

        //yield return new WaitForSeconds(0.1f);
        /*if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);*/
        //yield return new WaitForSeconds(0.1f);

        this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y/* / 2f*/, 0);

        yield return new WaitForSeconds(0.05f);
        /*if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
