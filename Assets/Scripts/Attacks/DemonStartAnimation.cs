using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DemonStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    //public float fallDuration = 0.2f;
    //public VisualEffect fire;
    //public GameObject landingParticle;

    public override void OnHit()
    {
        base.OnHit();
        /*if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }*/
    }

    /*public override void OnDeath()
    {
        if (this.onGoing)
            base.OnDeath();
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            int number = Random.Range(0, 2);

            this.StartCoroutine(this.StartAnimation2Coroutine());
            //Debug.Log(number);
            /*if (this.vsJCap != null && this.user.characterId == 7 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 6 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsJCap.Initiate();
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());

            }*/
        }
    }

    private IEnumerator StartAnimationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 5f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();
        if (this.animations != null)
            this.animations.DarkJCapStartAnimation();
        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.DarkJCapStartAnimation();

        float currentTime = 0;
        float duration = 0.8f;
        float targetPosition = 1f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }
        //this.PlayFire(false);

        currentTime = 0;
        duration = 0.2f;
        targetPosition = 0f;
        start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;
        //this.PlayFire(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator StartAnimation2Coroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        this.user.LookAtTarget();

        if (this.animations != null)
            this.animations.RagingBeastPose();

        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.RagingBeastPose();


        yield return new WaitForSeconds(0.25f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/

        if (this.animations != null)
            this.animations.RagingBeastStartMidPose();

        yield return new WaitForSeconds(0.05f);

        /*if (this.ragingFlame != null)
            this.ragingFlame.SetActive(true);*/

        if (this.animations != null)
            this.animations.RagingBeastStartPose();

        float currentTime = 0;
        float duration = 0.6f;

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

        /*if (this.ragingFlame != null)
            this.ragingFlame.SetActive(false);*/



        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);


        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }



    /*public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }*/
}
