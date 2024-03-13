using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDownAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;
    public GameObject landingParticle;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
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
            

            //Debug.Log(Mathf.Abs(this.user.rb.velocity.y));
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine(true));
            }
            else
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine(false));
            }
        }
    }

    private IEnumerator TemplateCoroutine(bool onGround = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (onGround)
        {
            this.user.rb.AddForce(0f, 600f, 0f);
            if (this.animations != null)
                this.animations.RollAnimation();
            //Debug.Log(onGround);
        }
        else
        {
            this.user.rb.AddForce(0f, 300f, 0f);
            if (this.animations != null)
                this.animations.RollAnimation();

            //Debug.Log(onGround);
        }

        

        yield return new WaitForSeconds(0.2f);

        this.ChangeCollision(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.user.rb.AddForce(0f, -800f, 0f);

        while (Mathf.Abs(this.user.rb.velocity.y) > 0f)
        {
            //currentTime += Time.deltaTime;
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-3000f * Time.deltaTime));

            yield return null;
        }

        if (this.landingParticle != null)
        {
            GameObject landingParticlePrefab = this.landingParticle;
            landingParticlePrefab = Instantiate(landingParticlePrefab, new Vector3(this.user.transform.position.x, 0.01f, 0f), Quaternion.Euler(0, 0, 0));
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.ChangeCollision(false);

        yield return new WaitForSeconds(0.2f);

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        this.onGoing = false;

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.ChangeCollision(false);

        this.user.attackStuns.Remove(this.gameObject);
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
}
