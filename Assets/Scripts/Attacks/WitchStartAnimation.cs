using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject broom;

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
        this.StartCoroutine(this.TemplateCoroutine());
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        float startXPos = this.user.transform.position.x;


        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        //this.user.transform.position = new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 4f), 0f, 0f);
        this.user.LookAtCenter();
        if (this.animations != null)
            this.animations.WitchBroomSit();

        //this.animations.eyes.localEulerAngles = new Vector3(0f, -25f, 0f);


        this.user.animations.body.transform.localPosition = new Vector3(-4f, this.user.animations.body.transform.localPosition.y, this.user.animations.body.transform.localPosition.z);

        if (this.broom != null)
            this.broom.SetActive(true);

        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;*/

        yield return new WaitForSeconds(0.01f);
        /*if (this.animations != null)
            this.animations.DarkJCapStartAnimation();*/

        //this.user.transform.position = new Vector3(startXPos - (this.user.transform.forward.z * 4f), 0f, 0f);

        float currentTime = 0;
        float duration = 0.2f;

        float targetPosition = startXPos;
        float start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                //this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);

                //this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);

                this.user.animations.body.transform.localPosition = new Vector3(
                    Mathf.Lerp(-4f, 0.1f, currentTime / duration), 
                    this.user.animations.body.transform.localPosition.y, 
                    this.user.animations.body.transform.localPosition.z);
            }


            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                this.user.animations.body.transform.localPosition = new Vector3(
                    Mathf.Lerp(0.1f, 0f, currentTime / duration),
                    this.user.animations.body.transform.localPosition.y,
                    this.user.animations.body.transform.localPosition.z);
            }


            yield return null;
        }

        //this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        //this.user.transform.position = new Vector3(startXPos, 0f, 0f);

        /*yield return new WaitForSeconds(1f - this.fallDuration - 0.1f);
        if (this.animations != null)
            this.animations.JCapLandingStartAnimation2();
        yield return new WaitForSeconds(0.1f);*/

        //yield return new WaitForSeconds(0.7f);

        float testTime = 0f;
        float time = 0.55f;
        float startPosY = this.animations.body.localPosition.y;
        while (time > 0)
        {
            time -= Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 10f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.005f), this.animations.body.localPosition.z);

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/
            yield return null;
        }


        if (this.broom != null)
            this.broom.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;*/

        //this.PlayFire(false);

        yield return new WaitForSeconds(0.05f);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;
        //this.PlayFire(false);

        if (this.broom != null)
            this.broom.SetActive(false);

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
}
