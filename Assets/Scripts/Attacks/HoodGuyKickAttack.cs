using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodGuyKickAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public TestHitbox hitbox;
    public TestHitbox airHitbox;
    public TestHitbox forwardHitbox;

    public GameObject scyte;


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
            //this.user.AddStun(0.2f, true);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.AddStun(0.2f, true);
            else
                this.user.AddStun(0.2f, false);

            this.StartCoroutine(this.KickCoroutine());*/


            if (Mathf.Abs(this.user.transform.position.y) < 0.2f /*Mathf.Abs(this.user.rb.velocity.y) <= 0f*/)
            {
                //Debug.Log(Mathf.Abs(this.user.transform.position.y));

                /*this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KickCoroutine());*/

                if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.ForwardKickCoroutine());
                }
                else
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.KickCoroutine());
                }
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.AirKickCoroutine());
            }
                

            

        }
    }

    IEnumerator KickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.HoodGuyKick(0);

        yield return new WaitForSeconds(0.05f);

        //this.user.rb.AddForce(new Vector3(100f, 0f, 0f));

        if (this.animations != null)
            this.animations.HoodGuyKick(1);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.HoodGuyKick(2);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.HoodGuyKick(3);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);


        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);*/




        if (this.animations != null)
            this.animations.HoodGuyKick(2);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.175f);


        /*if (this.animations != null)
            this.animations.HoodGuyKick(2);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.HoodGuyKick(1);

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null)
            this.animations.HoodGuyKick(0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/




        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator AirKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SexKickStart();

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.025f);


        if (this.animations != null)
            this.animations.SexKick();

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.2f;
        //float duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;

            /*if(this.airHitbox != null)
            {
                if (currentTime > 0.05f && currentTime <= 0.15f)
                    this.airHitbox.gameObject.SetActive(true);
                else
                    this.airHitbox.gameObject.SetActive(false);
            }*/
            

            yield return null;
        }

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);


        if (this.animations != null)
            this.animations.SexKickStart();

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.025f);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);



        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(true);
        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);

        currentTime = 0;
        duration = 0.05f;
        while (currentTime < duration && Mathf.Abs(this.user.transform.position.y) >= 0.05f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }*/


        /*while (currentTime < duration && Mathf.Abs(this.user.rb.velocity.y) >= 1f && this.user.transform.position.y > 0f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }*/

        /*while (currentTime < duration && Mathf.Abs(this.user.rb.velocity.y) > 0f)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }*/

        /*if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/

        //yield return new WaitForSeconds(0.6f);

        //End
    }

    IEnumerator ForwardKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.scyte != null)
        {
            this.scyte.SetActive(true);
            this.scyte.transform.localPosition = new Vector3(0f, -0.56f, 0.8f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, -90f, -90f);
        }

        if (this.animations != null)
            this.animations.HoodGuyBackKick(0, 0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(1, 0);

        yield return new WaitForSeconds(0.05f);

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(2, 1);

        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.1f);

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);

        /*if (this.animations != null)
            this.animations.rightLeg.localScale = new Vector3(1f, 1f, 1f);*/

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.HoodGuyBackKick(1, 0);

        yield return new WaitForSeconds(0.05f);

        /*if (this.animations != null)
            this.animations.HoodGuyBackKick(0, 0);

        yield return new WaitForSeconds(0.025f);*/

        if (this.scyte != null)
        {
            this.scyte.SetActive(false);
            this.scyte.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.175f);




        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public override void Stop()
    {
        base.Stop();

        this.StartCoroutine(this.HitCoroutine());

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);*/

        if (this.forwardHitbox != null)
            this.forwardHitbox.gameObject.SetActive(false);

        if (this.scyte != null)
        {
            this.scyte.SetActive(false);
            this.scyte.transform.localPosition = new Vector3(0.4f, -0.56f, 0f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        /*if (this.scyte != null)
        {
            this.scyte.SetActive(true);
            this.scyte.transform.localPosition = new Vector3(0f, -0.56f, 0.8f);
            this.scyte.transform.localEulerAngles = new Vector3(0f, -90f, -90f);
        }*/


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.airHitbox != null)
            this.airHitbox.gameObject.SetActive(false);
    }

    IEnumerator KickCoroutine2()
    {
        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;*/

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartKickPose0();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetStartKickPose();

        yield return new WaitForSeconds(0.025f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetKickPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && !this.user.dead && !this.user.rb.isKinematic && this.user.attackStuns.Count <= 0)
            this.animations.SetDefaultPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(0.2f);



        /*this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }
}