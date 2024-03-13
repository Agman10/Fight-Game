using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrowAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public FireBall fireBall;
    public float fireBallXForce;
    public float fireBallYForce;
    public int animationId;

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
    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
            }

        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f) //on ground
            {
                //this.user.AddStun(1.1f, true);
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.ThrowFireBallCorutine(0.5f, 0.5f));
            }
            else
            {
                //this.user.AddStun(0.6f, false);
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.ThrowFireBallCorutine(0.5f, 0f));
            }
            //this.user.AddStun(0.2f, true);
            //this.StartCoroutine(this.ThrowFireBallCorutine(1));
        }
    }
    IEnumerator ThrowFireBallCorutine(float time = 0.5f, float stunTime = 0.5f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;


        if (this.animations != null)
        {
            if (this.animationId ==1)
                this.animations.ShootPose();
            else
                this.animations.SetStartThrowFirePose();
        }


        yield return new WaitForSeconds(time);
        if (this.animations != null)
            this.animations.SetPunchPose();
        this.ThrowFireBall();
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(stunTime);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ThrowFireBall()
    {
        if (this.fireBall != null)
        {
            FireBall fireBallPrefab = this.fireBall;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            fireBallPrefab = Instantiate(fireBallPrefab, new Vector3(forward + this.user.transform.position.x, this.user.transform.position.y + 2, 0), this.user.transform.rotation);
            if (this.user != null)
                fireBallPrefab.belongsTo = this.user;

            fireBallPrefab.KnockBack(new Vector3(forward * this.fireBallXForce, this.fireBallYForce, 0));
        }
    }

    public override void Stop()
    {
        base.Stop();
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
