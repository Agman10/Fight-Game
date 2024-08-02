using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MCapTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject fireBalls;

    public VisualEffect spinFlame;

    public VisualEffect handFlameBlue, handFlameOrange;

    public TestHitbox spinFlameHitbox;

    public VisualEffect flame1, flame2, flame3;

    public GameObject fireBall1, fireBall2, fireBall3;

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.user != null)
        {
            this.user.OnAttack += this.Cancel;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.user != null)
        {
            this.user.OnAttack -= this.Cancel;
        }
    }
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

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.input.moveInput.y < 0f) //Down
                {
                    //this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.DownTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    //this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.FireBallTauntCoroutine());
                    this.StartCoroutine(this.ForwardTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.FireBallTauntCoroutine());
                    //this.StartCoroutine(this.LayDownTauntCoroutine());
                    //this.StartCoroutine(this.RagingTauntCoroutine());
                    this.StartCoroutine(this.HoldFlamesTauntCoroutine());
                    //this.StartCoroutine(this.BackTauntCoroutine());
                }
                else //Neutral
                {
                    //this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.FireBallTauntCoroutine());
                }

                /*else if (this.user.input.moveInput.y > 0f) //Up
                {
                    //this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                }*/
            }
        }
    }

    public void Cancel()
    {
        if (!this.user.dead && this.onGoing)
        {
            //this.user.AddStun(0.1f, true);
            if (this.animations != null)
                this.animations.SetDefaultPose();
            //Debug.Log("Cancel");
            this.Stop();

        }
    }

    public override void Stop()
    {
        base.Stop();
        //this.user.rb.isKinematic = false;

        //DO SOMETING WITH THIS LATER

        /*if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(false);
            this.animations.neutralEyes.gameObject.SetActive(true);
        }*/

        if(this.fireBalls != null)
        {
            this.fireBalls.SetActive(false);
        }

        if (this.fireBall1 != null)
            this.fireBall1.SetActive(true);

        if (this.fireBall2 != null)
            this.fireBall2.SetActive(true);

        if (this.fireBall3 != null)
            this.fireBall3.SetActive(true);

        this.PlayFire(false);

        this.PlayHandFlames(false);

        if (this.spinFlameHitbox != null)
            this.spinFlameHitbox.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayFire(bool playing)
    {
        if (this.spinFlame != null)
        {
            if (playing)
                this.spinFlame.Play();
            else
                this.spinFlame.Stop();

            /*if (playing)
                this.spinFlame.gameObject.SetActive(true);
            else
                this.spinFlame.gameObject.SetActive(false);*/
        }
    }

    public void PlayHandFlames(bool playing)
    {
        if(this.handFlameBlue != null)
        {
            if (playing)
                this.handFlameBlue.Play();
            else
                this.handFlameBlue.Stop();
        }

        if (this.handFlameOrange != null)
        {
            if (playing)
                this.handFlameOrange.Play();
            else
                this.handFlameOrange.Stop();
        }
    }


    private IEnumerator FireBallTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.TestPose5();*/

        if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
        {
            this.animations.NinjaTeleport();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }

        if (this.fireBalls != null)
        {
            this.fireBalls.SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);

        float currentTime = 0;
        float duration = 1.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            /*if (this.animations != null)
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);*/

            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            //Debug.Log(currentTime);
            yield return null;
        }

        if (this.flame1 != null)
            this.flame1.Stop();

        if (this.flame2 != null)
            this.flame2.Stop();

        if (this.flame3 != null)
            this.flame3.Stop();

        if (this.fireBall1 != null)
            this.fireBall1.SetActive(false);

        if (this.fireBall2 != null)
            this.fireBall2.SetActive(false);

        if (this.fireBall3 != null)
            this.fireBall3.SetActive(false);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }

        //yield return new WaitForSeconds(0.2f);

        currentTime = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.fireBalls != null)
        {
            this.fireBalls.SetActive(false);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.fireBall1 != null)
            this.fireBall1.SetActive(true);

        if (this.fireBall2 != null)
            this.fireBall2.SetActive(true);

        if (this.fireBall3 != null)
            this.fireBall3.SetActive(true);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator DownTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose5();

        this.PlayFire(true);

        if (this.spinFlameHitbox != null)
            this.spinFlameHitbox.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);

            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            //Debug.Log(currentTime);
            yield return null;
        }

        this.PlayFire(false);

        if (this.spinFlameHitbox != null)
            this.spinFlameHitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    

    private IEnumerator ForwardTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose3();

        /*if (this.animations != null)
            this.animations.FlameGrabHitPose();*/

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HoldFlamesTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.HoldingTwoFlames();

        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.PlayHandFlames(true);

        currentTime = 0;
        duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        this.PlayHandFlames(false);

        currentTime = 0;
        duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator LayDownTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.TestPose();*/

        if (this.animations != null)
        {
            this.animations.SetKickUppercutStartAnim();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -12f);
        }
            

        //yield return new WaitForSeconds(0.1f);

        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.LayingDownSassy();

        currentTime = 0;
        duration = 1f;

        /*float currentTime = 0;
        float duration = 1f;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        if (this.animations != null)
        {
            this.animations.SetKickUppercutStartAnim();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -12f);
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator RagingTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.RagingBeastStartMidPose();

        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.RagingBeastStartPose();

        currentTime = 0;
        duration = 1.5f;

        float testTime = 0f;
        float startPosY = this.animations.body.localPosition.y;
        while (currentTime < duration)
        {
            testTime += Time.deltaTime;

            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);


            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator BackTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose();

        /*if (this.animations != null)
            this.animations.RagingBeastStartPose();*/

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }





    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.HappyJumping();*/

        if (this.animations != null)
            this.animations.FlameGrabHitPose();

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator NeutralTaunt2Coroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.HappyJumping();*/

        if (this.animations != null)
            this.animations.HappyJumping(0);

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        int amount = 25;
        int animStageId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.1f);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (this.animations != null)
            {
                this.animations.HappyJumping(animStageId);
            }


            animStageId += 1;
            amount -= 1;
            //Debug.Log(amount);
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
