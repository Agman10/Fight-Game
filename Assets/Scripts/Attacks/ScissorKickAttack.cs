using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorKickAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;
    public TestHitbox hitbox2;

    public TestHitbox footDiveHitbox;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();

            /*this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());*/

            /*if (this.animations != null)
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
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.ScissorKickCoroutine());
            }
            else if (Mathf.Abs(this.user.transform.position.y) >= 0.2f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.FootDiveCoroutine());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator ScissorKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float animSpeed = 0.075f;

        /*if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);*/

        if (this.animations != null)
            this.animations.ScissorKick(0);

        yield return new WaitForSeconds(0.1f);


        if (this.animations != null)
            this.animations.ScissorKick(1);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        /*if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(1);*/

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(2);

        yield return new WaitForSeconds(animSpeed);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(3);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 4f, this.user.rb.velocity.y, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ScissorKick(0);

        yield return new WaitForSeconds(0.15f);

        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator FootDiveCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.FootDive(0);

        float time = 0.2f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));*/

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(0f, 0.85f, 0f);

            yield return null;
        }

        if (this.footDiveHitbox != null)
            this.footDiveHitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.FootDive(1);

        time = 1f;
        while (time > 0 && /*Mathf.Abs(this.user.transform.position.y) >= 0.05f*/ Mathf.Abs(this.user.rb.velocity.y) > 0f)
        {
            time -= Time.deltaTime;
            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));*/

            //this.user.ragdoll.transform.Rotate(new Vector3(0, this.spinRotationSpeed * Time.deltaTime, 0));
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 15f, -15f, 0f);

            yield return null;
        }

        if (this.footDiveHitbox != null)
            this.footDiveHitbox.gameObject.SetActive(false);

        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

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

        if (this.footDiveHitbox != null)
            this.footDiveHitbox.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
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
}