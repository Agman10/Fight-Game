using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MCapStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public float fallDuration = 0.2f;
    public VisualEffect fire;
    public GameObject landingParticle;
    /*public GameObject propeller;
    public GameObject propellerBlade;*/

    /*public Attack vsDarkJCap;
    public Attack vsMikeBaller;*/

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
            /*if (this.vsDarkJCap != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 1 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsDarkJCap.Initiate();
            }
            else if (this.vsMikeBaller != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 3 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsMikeBaller.Initiate();
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }*/

            //this.StartCoroutine(this.TemplateCoroutine());
            this.StartCoroutine(this.MasterJcapStart1());
        }
    }

    private IEnumerator MasterJcapStart1()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;

        float startXPos = this.user.transform.position.x;

        this.user.transform.position = new Vector3(this.user.transform.position.x, 250f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();

        if (this.animations != null)
        {
            this.animations.NinjaTeleport();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }

        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
        {
            this.animations.NinjaTeleport();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }
            

        yield return new WaitForSeconds(0.1f);

        this.user.transform.position = new Vector3(startXPos, 0f, 0f);

        //float startPos = this.user.transform.position.x;
        float pos1 = this.user.transform.position.x + (this.user.transform.forward.z * 1f);
        float pos2 = this.user.transform.position.x + (this.user.transform.forward.z * -1f);

        float currentTime = 0;
        float duration = 0.4f;
        float time2 = 0f;

        //float test = 1f;

        int laughId = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            time2 += Time.deltaTime;

            float test = Mathf.Lerp(1f, 0.1f, currentTime / duration);

            if (time2 > 0.01f)
            {
                time2 = 0f;
                /*if (this.animations != null)
                    this.animations.MikeBallerAngry(laughId);*/

                if (laughId == 0)
                {
                    laughId = 1;
                    //this.user.transform.position = new Vector3(pos1, 0f, 0f);
                    this.user.transform.position = new Vector3(startXPos + (this.user.transform.forward.z * test), 0f, 0f);
                }
                else
                {
                    laughId = 0;
                    //this.user.transform.position = new Vector3(pos2, 0f, 0f);
                    this.user.transform.position = new Vector3(startXPos + (this.user.transform.forward.z * -test), 0f, 0f);
                }
            }

            yield return null;
        }

        this.user.transform.position = new Vector3(startXPos, 0f, 0f);

        

        yield return new WaitForSeconds(0.4f);

        /*yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        }

        yield return new WaitForSeconds(0.1f);*/


        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.1f);

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        //this.PlayFire(false);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 7f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        /*if (this.animations != null)
            this.animations.DarkJCapStartAnimation();*/

        float currentTime = 0;
        float duration = this.fallDuration;
        //float targetVolume = 0.1f;
        float targetPosition = 0f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);

            yield return null;
        }

        if (this.landingParticle != null)
        {
            GameObject landingParticlePrefab = this.landingParticle;
            landingParticlePrefab = Instantiate(landingParticlePrefab, new Vector3(this.user.transform.position.x, 0.01f, 0f), Quaternion.Euler(0, 0, 0));
        }


        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        /*int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number == 1)
        {
            this.user.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            this.user.Suicide();
        }
        else if (this.animations != null)
        {
            this.animations.JCapLandingStartAnimation();
        }*/

        this.animations.JCapLandingStartAnimation();

        /*yield return new WaitForSeconds(1f - this.fallDuration - 0.1f);
        if (this.animations != null)
            this.animations.JCapLandingStartAnimation2();
        yield return new WaitForSeconds(0.1f);*/

        yield return new WaitForSeconds(1f - this.fallDuration - 0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.1f);

        //this.PlayFire(false);

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

        if (this.animations != null)
            this.animations.rightArm.localScale = new Vector3(1f, 1f, 1f);

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }
}
