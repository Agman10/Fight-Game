using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolentMikeStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public ParticleSystem electricity;

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

    /*private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            if (this.user.characterId == 4 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 3 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.StartCoroutine(this.VsMikeBallerStartAnimation());
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }

            
            //this.StartCoroutine(this.VsMikeBallerStartAnimation());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        this.user.LookAtTarget();

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(true);
        if (this.electricity != null)
            this.electricity.Play();

        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);

        this.user.transform.position = new Vector3(this.user.transform.position.x, 50f, 0f);



        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);

        yield return new WaitForSeconds(0.15f);
        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);


        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);


        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(1);

        yield return new WaitForSeconds(0.1f);

        if (this.electricity != null)
            this.electricity.Stop();

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        //yield return new WaitForSeconds(1f);
        this.user.EntranceDone();
    }


    private IEnumerator VsMikeBallerStartAnimation()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;

        float startXPos = this.user.transform.position.x;

        this.user.transform.position = new Vector3(this.user.transform.position.x, 250f, 0f);

        

        //this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(startXPos * -2f, 0f, 0f);
        this.user.LookAtTarget();

        //this.user.transform.position = new Vector3(this.user.transform.position.x * 2, 0f, 0f);
        //this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.RollAnimation();

        yield return new WaitForSeconds(0.4f);

        float currentTime = 0;
        float duration = 0.9f;

        float targetPosition = startXPos;
        float start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);
                this.animations.body.transform.Rotate(0f, 0f, -3000f * Time.deltaTime);
            }


            yield return null;
        }

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        //yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        yield return new WaitForSeconds(0.1f);
        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.2f);

        //SYNCED HERE!!!


        /*if (this.animations != null)
            this.animations.Laughing3(1);*/

        if (this.animations != null)
            this.animations.Laughing3(1);

        float time = 1f;
        float time2 = 0f;
        int laughId = 0;
        while (time > 0)
        {
            time -= Time.deltaTime;

            time2 += Time.deltaTime;

            if (time2 > 0.05f)
            {
                time2 = 0f;
                if (this.animations != null)
                    this.animations.Laughing3(laughId);

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

        //yield return new WaitForSeconds(0.3f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        yield return new WaitForSeconds(0.1f);

        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;*/

        //this.user.rb.isKinematic = false;
        //Debug.Log("violent");
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    public override void Stop()
    {
        base.Stop();
        /*if (!this.user.dead)
            this.user.rb.isKinematic = false;*/

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);


        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
}
