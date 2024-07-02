using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolentMikeTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject electricGuitar;

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.user != null)
        {
            this.user.OnAttack += this.Cancel;
        }

        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.user != null)
        {
            this.user.OnAttack -= this.Cancel;
        }

        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;
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

    private void Update()
    {
        if (this.onGoing && this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
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
                    this.StartCoroutine(this.ElectricGuitarTauntCoroutine());
                    //this.StartCoroutine(this.LDanceCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    this.StartCoroutine(this.LaughTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.StartCoroutine(this.LaughTauntCoroutine());
                    this.StartCoroutine(this.BackTauntCoroutine());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.LaughTauntCoroutine());
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

        //DO SOMETING WITH THIS LATER

        if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(false);
            this.animations.neutralEyes.gameObject.SetActive(true);

            if (this.animations.angryEyes != null)
                this.animations.angryEyes.gameObject.SetActive(false);
        }

        if (this.animations != null && this.animations.rightArm != null && this.animations.leftArm != null)
        {
            this.animations.rightArm.localPosition = new Vector3(0f, 0.15f, -0.6f);
            this.animations.leftArm.localPosition = new Vector3(0f, 0.15f, 0.6f);
        }


        /*if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);
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
            this.animations.Shrug(0);

        float currentTime = 0;
        float duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.animations != null)
                this.animations.upperBody.localEulerAngles = new Vector3(0, Mathf.Lerp(this.animations.upperBody.localEulerAngles.y, 55f, currentTime / duration), 0);
            yield return null;
        }
        //yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.Shrug(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.Shrug(2);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null && this.animations.rightArm != null && this.animations.leftArm != null)
        {
            this.animations.rightArm.localPosition = new Vector3(0f, 0.2f, -0.6f);
            this.animations.leftArm.localPosition = new Vector3(0f, 0.2f, 0.6f);
        }

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null && this.animations.rightArm != null && this.animations.leftArm != null)
        {
            this.animations.rightArm.localPosition = new Vector3(0f, 0.15f, -0.6f);
            this.animations.leftArm.localPosition = new Vector3(0f, 0.15f, 0.6f);
        }

        yield return new WaitForSeconds(0.5f);

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
            this.animations.Laughing(1);

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
                this.animations.Laughing(laughId);
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

    private IEnumerator ElectricGuitarTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ElectricGuitar(0);

        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(true);

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        int amount = 16;
        int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.05f);
            if (this.animations != null)
                this.animations.ElectricGuitar(animId);
            if (animId == 0)
                animId = 1;
            else
                animId = 0;

            amount -= 1;

            if (amount <= 0 && this.user.input.taunting)
                amount = 1;

            yield return null;
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator LDanceCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.Wry();*/

        if (this.animations != null)
            this.animations.LDance(0);

        int amount = 30;
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
                this.animations.LDance(animationId);
            }
            if (idForward)
                animationId += 1;
            else if (!idForward)
                animationId -= 1;

            if (animationId == 0)
                idForward = true;
            else if (animationId == 2)
                idForward = false;



            amount -= 1;
            //Debug.Log(amount);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
