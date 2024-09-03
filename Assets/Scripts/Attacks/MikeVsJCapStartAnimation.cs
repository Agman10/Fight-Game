using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MikeVsJCapStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool alternate = false;
    //[Space]
    public VisualEffect flame1, flame2, flame3;

    //[Space]
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
            this.user.AddStun(0.2f, true);
            if (this.alternate)
                this.StartCoroutine(this.TemplateCoroutineOld());
            else
                this.StartCoroutine(this.TemplateCoroutine());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        this.user.transform.position = new Vector3(this.user.transform.position.x, 250f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.TestPose5();


        float currentTime = 0;
        float duration = 0.9f;

        float targetPosition = 0f;
        float start = 16;

        /*float targetPosition = this.user.transform.position.x;
        float start = this.user.transform.position.x + (this.transform.forward.z * 16);*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            

            if (this.animations != null)
            {
                this.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.animations.body.transform.localPosition.y, 0);
                //this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
            }
                

            yield return null;
        }

        //this.user.rb.isKinematic = false;

        //yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SpinEnd();

        

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;

        this.PlayFire(false);

        if (this.electricity != null)
            this.electricity.Stop();

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator TemplateCoroutineOld()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        this.user.transform.position = new Vector3(this.user.transform.position.x, 250f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        //this.user.rb.isKinematic = false;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;

        if (this.animations != null)
            this.animations.TestPose5();


        this.PlayFire(true);

        if (this.electricity != null)
            this.electricity.Play();

        float currentTime = 0;
        float duration = 0.9f;

        /*float targetPosition = 0f;
        float start = 16;*/

        float targetPosition = this.user.transform.position.x;
        float start = this.user.transform.position.x + (this.transform.forward.z * 16);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;



            if (this.animations != null)
            {
                //this.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.animations.body.transform.localPosition.y, 0);
                this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
            }


            yield return null;
        }

        this.user.rb.isKinematic = false;

        //yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SpinEnd();

        this.PlayFire(false);

        if (this.electricity != null)
            this.electricity.Stop();



        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public void PlayFire(bool playing)
    {
        if (this.flame1 != null && this.flame2 != null && this.flame3 != null)
        {
            if (playing)
            {
                this.flame1.Play();
                this.flame2.Play();
                this.flame3.Play();
            }
            else
            {
                this.flame1.Stop();
                this.flame2.Stop();
                this.flame3.Stop();
            }
                
        }
    }
}
