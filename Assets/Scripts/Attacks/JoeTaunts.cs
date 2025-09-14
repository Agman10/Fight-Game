using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JoeTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public VisualEffect handFlame;
    public VisualEffect handSmoke;

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
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                //this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y, 0);
            }
                
        }
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
                    this.StartCoroutine(this.ButtSlapTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.AngryTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.HoldFlameTauntCoroutine());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.AngryTauntCoroutine());
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
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

        this.PlayHandFlame(false);
        this.PlayHandSmoke(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayHandFlame(bool playing)
    {
        if (this.handFlame != null)
        {
            if (playing)
                this.handFlame.Play();
            else
                this.handFlame.Stop();
        }
    }

    public void PlayHandSmoke(bool playing)
    {
        if (this.handFlame != null)
        {
            if (playing)
                this.handSmoke.Play();
            else
                this.handSmoke.Stop();
        }
    }


    private IEnumerator HoldFlameTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.HoldingOneFlame();

        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.PlayHandFlame(true);

        /*currentTime = 0;
        duration = 0.8f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


            *//*if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;*//*
            yield return null;
        }*/

        int amount = 8;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            this.user.TakeDamage(this.user.transform.position, 0.1f, 0f, 0f, 0f, false, true);

            yield return new WaitForSeconds(0.1f);

            this.user.TakeDamage(this.user.transform.position, 0.1f, 0f, 0f, 0f, false, true);

            amount -= 1;

            /*if (amount <= 0 && this.user.input.taunting)
                amount = 1;*/

            yield return null;
        }

        this.animations.SetEyes(2);
        this.animations.eyes.localEulerAngles = new Vector3(0f, 12f, 0f);

        /*currentTime = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


            *//*if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;*//*
            yield return null;
        }*/

        amount = 2;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            this.user.TakeDamage(this.user.transform.position, 0.1f, 0f, 0f, 0f, false, true);

            yield return new WaitForSeconds(0.1f);

            this.user.TakeDamage(this.user.transform.position, 0.1f, 0f, 0f, 0f, false, true);

            amount -= 1;

            /*if (amount <= 0 && this.user.input.taunting)
                amount = 1;*/

            yield return null;
        }

        this.PlayHandFlame(false);
        this.PlayHandSmoke(true);

        if (this.animations != null)
            this.animations.HoldingOneFlameHurt();

        currentTime = 0;
        duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }
        this.PlayHandSmoke(false);

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


    private IEnumerator AngryTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.TestPose();

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/


        if (this.animations != null)
            this.animations.JoeTauntAngry();

        float time = 0.7f;
        float time2 = 0f;
        int laughId = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;

            time2 += Time.deltaTime;

            if (time2 > 0.025f)
            {
                time2 = 0f;
                if (this.animations != null)
                    this.animations.JoeTauntAngry(laughId);

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

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ButtSlapTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(0);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(2);





        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(2);


        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(0);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(2);


        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.JoeButtSlapTaunt(0);

        yield return new WaitForSeconds(0.2f);

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/


        //yield return new WaitForSeconds(1f);

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
        //yield return new WaitForSeconds(1f);

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

        if (this.animations != null)
            this.animations.TestPose();

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
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
        //yield return new WaitForSeconds(1f);

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
        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
