using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DemonTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    //public VisualEffect handFlame;

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
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.SpinTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.SpinTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.SpinTauntCoroutine());
                }
                else //Neutral
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.SpinTauntCoroutine());
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

        //this.PlayHandFlame(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    /*public void PlayHandFlame(bool playing)
    {
        if (this.handFlame != null)
        {
            if (playing)
                this.handFlame.Play();
            else
                this.handFlame.Stop();
        }
    }*/

    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.TestPose3();*/

        if (this.animations != null)
            this.animations.FlameGrabHitPose();

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

    private IEnumerator SpinTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SpinTaunt(0);

        float currentTime = 0;
        //float duration = 0.6f;
        float duration = 0.45f;
        //float targetRotation = this.animations.body.localEulerAngles.y + (360f * 2f);
        float targetRotation = this.animations.body.localEulerAngles.y + (360f * 2f) - 90f;
        float startRotation = this.animations.body.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(
                    this.animations.body.localEulerAngles.x,
                    Mathf.Lerp(startRotation, targetRotation, currentTime / duration),
                    this.animations.body.localEulerAngles.z);

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }



        currentTime = 0;
        duration = 0.2f;
        //float targetRotation = -245f;
        //targetRotation = (360f * 2f);
        targetRotation = this.animations.body.localEulerAngles.y + 90f;
        startRotation = this.animations.body.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(
                    this.animations.body.localEulerAngles.x,
                    Mathf.Lerp(startRotation, targetRotation, currentTime / duration),
                    this.animations.body.localEulerAngles.z);

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }



        if (this.animations != null)
            this.animations.SpinTaunt(1);


        //float testTime = 0f;
        currentTime = 0;
        duration = 0.5f;
        float startPosY = this.animations.body.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            float newY = Mathf.Sin(currentTime * 50f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

}
