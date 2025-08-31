using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeAirKickAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public TestHitbox hitbox3;

    private bool moving = false;
    private float xSpeed = 0f;
    private float ySpeed = 0f;

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
        /*if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }*/

        if(this.onGoing && this.moving && this.user != null)
        {
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.xSpeed * Time.deltaTime, this.ySpeed * Time.deltaTime, 0);
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
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KickCoroutine());
            }
            else
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KickAirCoroutine());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator KickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        //this.animations.SetDefaultPose();
        this.animations.RoadRollerEndLand();
        float inbetweenTime = 0.025f;
        float kickTime = 0.15f;

        /*float time = 0.025f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 10f, 4, 0);
            yield return null;
        }*/

        yield return new WaitForSeconds(0.3f);
        this.moving = true;
        this.xSpeed = 1000f;
        this.ySpeed = 200f;

        this.animations.JoekukyakuKicksInbetweens(0);


        yield return new WaitForSeconds(0.05f);

        this.animations.JoekukyakuKicks(0);
        this.EnableHitbox(1);
        yield return new WaitForSeconds(kickTime);
        this.animations.JoekukyakuKicksInbetweens(1);
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicks(1);
        this.EnableHitbox(2);
        yield return new WaitForSeconds(kickTime);
        this.animations.JoekukyakuKicksInbetweens(0);

        this.xSpeed = 800f;
        this.ySpeed = -100f;
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicks(2);
        this.EnableHitbox(3);
        yield return new WaitForSeconds(kickTime);
        this.EnableHitbox(0);

        this.animations.JoekukyakuKicksInbetweens(0);
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicksInbetweens(1);
        yield return new WaitForSeconds(inbetweenTime);

        //this.animations.SetDefaultPose();

        this.user.rb.velocity = new Vector3(this.user.rb.velocity.x, -10f, 0);

        if (this.animations != null)
            this.animations.DemonCradle(5);
        this.moving = false;
        this.xSpeed = 0f;
        this.ySpeed = 0f;

        float waitTime = 2f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        

        if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
            this.animations.SetDefaultPose();
        else
            this.animations.RoadRollerEndLand();

        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        yield return new WaitForSeconds(0.2f);

        this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator KickAirCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        //this.animations.SetDefaultPose();
        this.animations.SetKickUppercutStartAnim();
        this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 30f, -12f);
        //this.animations.RoadRollerEndLand();
        float inbetweenTime = 0.025f;
        float kickTime = 0.15f;

        /*float time = 0.025f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 10f, 4, 0);
            yield return null;
        }*/

        yield return new WaitForSeconds(0.2f);
        this.moving = true;
        this.xSpeed = 800f;
        this.ySpeed = 200f;

        /*yield return new WaitForSeconds(0.001f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 500f, 500f, 0);*/

        this.animations.JoekukyakuKicksInbetweens(0);


        yield return new WaitForSeconds(0.05f);

        this.animations.JoekukyakuKicks(0);
        this.EnableHitbox(1);
        yield return new WaitForSeconds(kickTime);
        this.animations.JoekukyakuKicksInbetweens(1);
        //yield return new WaitForSeconds(inbetweenTime);


        this.animations.JoekukyakuKicksInbetweens(1);
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicks(1);
        this.EnableHitbox(2);
        yield return new WaitForSeconds(kickTime);
        this.xSpeed = 400f;
        this.ySpeed = -100f;

        this.EnableHitbox(0);

        this.animations.JoekukyakuKicksInbetweens(0);
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicksInbetweens(1);
        this.EnableHitbox(0);
        yield return new WaitForSeconds(inbetweenTime);
        this.moving = false;
        this.xSpeed = 0f;
        this.ySpeed = 0f;

        /*this.xSpeed = 800f;
        this.ySpeed = -100f;
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicks(2);
        this.EnableHitbox(3);
        yield return new WaitForSeconds(kickTime);
        this.EnableHitbox(0);

        this.animations.JoekukyakuKicksInbetweens(0);
        yield return new WaitForSeconds(inbetweenTime);
        this.animations.JoekukyakuKicksInbetweens(1);
        yield return new WaitForSeconds(inbetweenTime);*/

        //this.animations.SetDefaultPose();

        //this.user.rb.velocity = new Vector3(this.user.rb.velocity.x, -10f, 0);

        if (this.animations != null)
            this.animations.DemonCradle(5);
        /*this.moving = false;
        this.xSpeed = 0f;
        this.ySpeed = 0f;*/

        float waitTime = 2f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }



        if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
            this.animations.SetDefaultPose();
        else
            this.animations.RoadRollerEndLand();

        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        yield return new WaitForSeconds(0.2f);

        this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        this.moving = false;
        this.xSpeed = 0f;
        this.ySpeed = 0f;

        this.EnableHitbox(0);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void EnableHitbox(int hitboxId = 0)
    {
        if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(hitboxId == 1);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(hitboxId == 2);

        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(hitboxId == 3);



    }
}
