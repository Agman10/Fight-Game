using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject broom;
    public Spinner potionJuggle;

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
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.input.moveInput.y < 0f) //Down
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.BroomTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    /*this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.BroomTauntCoroutine());*/

                    this.StartCoroutine(this.LaughTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    //this.StartCoroutine(this.BroomTauntCoroutine());
                    this.StartCoroutine(this.PotionJuggleCoroutine());
                }
                else //Neutral
                {
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.LaughTauntCoroutine());
                }
            }
            /*else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f)
            {
                this.StartCoroutine(this.ForwardTauntCoroutine());
            }*/

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

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        if (this.potionJuggle != null)
            this.potionJuggle.speedMultiplier = 0f;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.broom != null)
            this.broom.SetActive(false);

        if (this.potionJuggle != null)
        {
            this.potionJuggle.gameObject.SetActive(false);
            this.potionJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.potionJuggle.speedMultiplier = 1f;
        }
    }

    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.TestPose2();*/

        if (this.animations != null)
            this.animations.FlameGrabHitPose();

        /*if (this.animations != null)
            this.animations.RagingBeastPose();*/

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

    private IEnumerator BroomTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.WitchBroomSit();

        if (this.broom != null)
            this.broom.SetActive(true);

        float testTime = 0f;
        float currentTime = 0;
        float duration = 1f;
        float startPosY = this.animations.body.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            testTime += Time.deltaTime;

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


            //this.user.rb.velocity = new Vector3(0f, 0f, 0f);

            float newY = Mathf.Sin(testTime * 5f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);


            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        if (this.broom != null)
            this.broom.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

        yield return new WaitForSeconds(0.1f);

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


    private IEnumerator LaughTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.Laughing3(1);

        int amount = 16;
        int laughId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.05f);
            if (this.animations != null)
                this.animations.Laughing3(laughId);
            if (laughId == 0)
                laughId = 1;
            else
                laughId = 0;

            amount -= 1;

            if (amount <= 0 && this.user.input.taunting)
                amount = 1;

            yield return null;
        }
        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator PotionJuggleCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.Juggle(0);

        if (this.potionJuggle != null)
        {
            this.potionJuggle.gameObject.SetActive(true);
            this.potionJuggle.speedMultiplier = 1f;
            //this.bombJuggle.transform.eulerAngles = new Vector3(this.bombJuggle.transform.rotation.x, 0f, this.bombJuggle.transform.rotation.z);
            //this.bombJuggle.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            this.potionJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }


        int amount = 20;
        int animationId = 1;
        bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.1f);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (this.animations != null)
            {
                this.animations.Juggle(animationId);
            }

            if (animationId == 3)
                animationId = 0;
            else
                animationId += 1;

            amount -= 1;
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }

        if (this.potionJuggle != null)
        {
            this.potionJuggle.gameObject.SetActive(false);
            this.potionJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
