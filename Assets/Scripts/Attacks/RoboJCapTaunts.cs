using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboJCapTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

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
                    this.StartCoroutine(this.DownTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                    //this.StartCoroutine(this.DownTauntCoroutine());
                }
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

        //CHANGE THIS LATER!!!!!!!!

        if (this.animations != null)
        {
            this.animations.upperBody.localPosition = new Vector3(0f, 0f, 0f);
            this.animations.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (this.animations.neutralEyes != null && this.animations.happyEyes != null)
            {
                this.animations.happyEyes.gameObject.SetActive(false);
                this.animations.neutralEyes.gameObject.SetActive(true);
            }
        }
        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.FlameGrabHitPose();*/
        if (this.animations != null)
            this.animations.SetDefaultPose();

        float currentTime = 0;
        float duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.upperBody.localEulerAngles = new Vector3(0, Mathf.Lerp(this.animations.upperBody.localEulerAngles.y, /*this.user.transform.forward.z * */90f, currentTime / duration), 0);
            yield return null;
        }


        if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(true);
            this.animations.neutralEyes.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);

        currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.upperBody.localPosition = new Vector3(0, Mathf.Lerp(this.animations.upperBody.localPosition.y, -0.2f, currentTime / duration), 0);
            yield return null;
        }

        currentTime = 0;
        //duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.upperBody.localPosition = new Vector3(0, Mathf.Lerp(this.animations.upperBody.localPosition.y, 0f, currentTime / duration), 0);
            yield return null;
        }



        yield return new WaitForSeconds(0.1f);

        if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(false);
            this.animations.neutralEyes.gameObject.SetActive(true);
        }


        currentTime = 0;
        duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.upperBody.localEulerAngles = new Vector3(0, Mathf.Lerp(this.animations.upperBody.localEulerAngles.y, 0, currentTime / duration), 0);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator DownTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.AddStun(0.2f, true);

        if (this.animations != null)
            this.animations.Sentai(0);
        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.Sentai(1);
        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.Sentai(2);
        yield return new WaitForSeconds(0.3f);
        if (this.animations != null)
            this.animations.Sentai(3);
        yield return new WaitForSeconds(0.3f);
        /*if (this.animations != null)
            this.animations.Sentai(5);
        yield return new WaitForSeconds(0.3f);*/
        if (this.animations != null)
            this.animations.Sentai(4);
        yield return new WaitForSeconds(0.7f);

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ForwardTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose3();

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

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator BackTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose2();

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

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
