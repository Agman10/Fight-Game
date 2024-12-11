using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MikeBallerStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public float fallDuration = 0.2f;
    //public VisualEffect fire;
    public GameObject landingParticle;
    /*public GameObject propeller;
    public GameObject propellerBlade;*/

    public GameObject exclamationMark;

    //public Attack vsDarkJCap;
    public Attack vsJCap;

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
            //Debug.Log(number);
            if(this.vsJCap != null && this.user.characterId == 3 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 0 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsJCap.Initiate();
            }
            else if (this.user.characterId == 3 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 4 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.StartCoroutine(this.VsViolentMikeStartAnimation());
            }
            else
            {
                if (number == 0)
                    this.StartCoroutine(this.TemplateCoroutine());
                else
                    this.StartCoroutine(this.GroundRollCoroutine());

                //this.StartCoroutine(this.VsViolentMikeStartAnimation());
            }

            
            /*if (this.vsDarkJCap != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 1 && this.user.tempBall == null)
            {
                this.vsDarkJCap.Initiate();
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }*/
        }
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
        if (this.animations != null)
            this.animations.RollAnimation();

        float currentTime = 0;
        float duration = this.fallDuration;
        //float targetVolume = 0.1f;
        float targetPosition = 0f;
        //float targetPosition = -1f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-3000f * Time.deltaTime));

            yield return null;
        }


        /*currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        targetPosition = 2f;
        start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, this.transform.forward.z * -3000f * Time.deltaTime));

            yield return null;
        }



        currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        targetPosition = 0f;
        start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, this.transform.forward.z * -3000f * Time.deltaTime));

            yield return null;
        }*/




        if (this.animations != null)
            this.animations.SetDefaultPose();

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
        }*/


        //yield return new WaitForSeconds(1f - this.fallDuration - 0.2f);
        yield return new WaitForSeconds(1f - this.fallDuration);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.PlayFire(false);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator GroundRollCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 20f, 0f);
        /*if (this.animations != null)
            this.animations.RollAnimation();
        if (this.animations != null)
            this.animations.body.transform.localPosition = new Vector3(-5, 0.7f, 0f);*/
        //this.PlayFire(true);
        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);


        if (this.animations != null)
            this.animations.RollAnimation();
        if (this.animations != null)
            this.animations.body.transform.localPosition = new Vector3(-5, 0.8f, 0f);

        //yield return new WaitForSeconds(0.2f);

        float currentTime = 0;
        float duration = 0.7f;
        //float targetVolume = 0.1f;
        float targetPosition = 0f;
        //float targetPosition = -1f;
        float start = -5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0.8f, 0);
            if (this.animations != null)
            {
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-2000f * Time.deltaTime));
            }
                

            yield return null;
        }
        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

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

        if (this.exclamationMark != null)
            this.exclamationMark.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }



    private IEnumerator VsViolentMikeStartAnimation()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        //this.user.rb.isKinematic = false;

        /*if (this.animations != null)
            this.animations.RollAnimation();*/

        /*if (this.animations != null)
            this.animations.MikeBallerVsViolentStartPose();*/

        yield return new WaitForSeconds(0.3f);
        if (this.animations != null)
            this.animations.MikeBallerVsViolentStartPose2();
        /*if (this.animations != null)
        {
            if (this.animations.eyes != null)
                this.animations.eyes.localEulerAngles = new Vector3(0f, 25f, 0f);

            if (this.animations.upperBody != null *//*&& this.animations.lowerBody != null*//*)
            {
                this.animations.upperBody.localEulerAngles = new Vector3(0f, 50f, 0f);
                //this.animations.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }*/
            

        if (this.exclamationMark != null)
            this.exclamationMark.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.exclamationMark != null)
            this.exclamationMark.SetActive(false);

        //yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.RollAnimation();

        float currentTime = 0;
        float duration = 0.3f;

        float targetPosition = 4f;
        float start = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
                this.animations.body.transform.Rotate(0f, 0f, -3000f * Time.deltaTime);
            }


            yield return null;
        }

        currentTime = 0;
        duration = 0.3f;

        targetPosition = 0f;
        start = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
                this.animations.body.transform.Rotate(0f, 0f, -3000f * Time.deltaTime);
            }


            yield return null;
        }


        //yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        yield return new WaitForSeconds(0.5f);

        //SYNCED HERE!!!

        if (this.animations != null)
            this.animations.MikeBallerAngry();

        float time = 1f;
        float time2 = 0f;
        int laughId = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;

            time2 += Time.deltaTime;

            if(time2 > 0.025f)
            {
                time2 = 0f;
                if (this.animations != null)
                    this.animations.MikeBallerAngry(laughId);

                if (laughId == 0)
                    laughId = 1;
                else
                    laughId = 0;
            }

            yield return null;
        }

        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();


        //this.user.transform.position = new Vector3(this.user.transform.position.x, 3f, 0f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        yield return new WaitForSeconds(0.1f);

        //this.user.rb.isKinematic = false;

        //this.user.rb.isKinematic = false;
        //Debug.Log("baller");
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
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
