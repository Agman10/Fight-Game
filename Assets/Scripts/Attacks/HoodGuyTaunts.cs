using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodGuyTaunts : Attack
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
                    this.StartCoroutine(this.CaramellDance());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    //this.StartCoroutine(this.SpinTauntCoroutine());
                    this.StartCoroutine(this.TestStupidWalkCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    //this.StartCoroutine(this.SpinTauntCoroutine());
                    this.StartCoroutine(this.StupidTauntCoroutine2());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.SpinTauntCoroutine());
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
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

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

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator SpinTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SpinTaunt(0);

        float currentTime = 0;
        //float duration = 0.6f;
        float duration = 0.45f;
        //float targetRotation = this.animations.body.localEulerAngles.y + (360f * 2f);
        float targetRotation = this.animations.body.localEulerAngles.y + (360f * 2f) -90f;
        float startRotation = this.animations.body.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(
                    this.animations.body.localEulerAngles.x,
                    Mathf.Lerp(startRotation, targetRotation, currentTime / duration), 
                    this.animations.body.localEulerAngles.z);

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }



        currentTime = 0;
        duration = 0.2f;
        //float targetRotation = -245f;
        //targetRotation = (360f * 2f);
        targetRotation = this.animations.body.localEulerAngles.y + 90f;
        startRotation = this.animations.body.localEulerAngles.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(
                    this.animations.body.localEulerAngles.x,
                    Mathf.Lerp(startRotation, targetRotation, currentTime / duration),
                    this.animations.body.localEulerAngles.z);

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }



        if (this.animations != null)
            this.animations.SpinTaunt(1);


        //float testTime = 0f;
        currentTime = 0;
        duration = 0.5f;
        float startPosY = this.animations.body.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            float newY = Mathf.Sin(currentTime * 50f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

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

    private IEnumerator CaramellDance()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.CaramelDance(0);

        int amount = 20;
        int animationId = 1;
        bool idForward = true;

        float longWaitTime = 0.25f;
        float shortWaitTime = 0.05f;

        float waitTime = shortWaitTime;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(waitTime);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            /*if (this.animations != null)
            {
                this.animations.CaramelDance(animationId);
                Debug.Log(animationId);
            }*/

            if (animationId == 0)
            {
                this.animations.CaramelDance(0);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 1)
            {
                if (this.transform.forward.z < 0)
                    this.animations.CaramelDance(1);
                else
                    this.animations.CaramelDance(2);


                //this.animations.CaramelDance(1);
                waitTime = longWaitTime;
                animationId += 1;
            }
            else if (animationId == 2)
            {
                this.animations.CaramelDance(0);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 3)
            {
                if (this.transform.forward.z < 0)
                    this.animations.CaramelDance(2);
                else
                    this.animations.CaramelDance(1);


                //this.animations.CaramelDance(2);
                waitTime = longWaitTime;
                animationId = 0;
            }



            amount -= 1;
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }
        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator StupidTauntCoroutine2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;


        int number = Random.Range(1, 11);

        /*int amount = 20;
        float waitTime = 0.05f;*/

        /*int amount = 12;
        float waitTime = 0.08f;*/

        int amount = 14;
        float waitTime = 0.07f;

        /*int amount = 16;
        float waitTime = 0.06f;*/

        if (number == 1)
        {
            amount = 30;

            waitTime = 0.025f;

            /*amount = 80;

            waitTime = 0.01f;*/
        }


        if (this.animations != null)
            this.animations.StupidDance(1);

        /*if (this.animations != null)
            this.animations.StupidDance(0);*/

        //int amount = 20;
        int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(waitTime);

            if (this.animations != null)
                this.animations.StupidDance(animId);
            if (animId == 0)
                animId = 1;
            else
                animId = 0;


            /*if (animId == 0)
            {
                
                this.animations.SetDefaultPose();
                animId = 1;
            }
            else
            {
                this.animations.StupidDance(0);
                animId = 0;
            }*/

            //Debug.Log(amount);
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


    private IEnumerator StupidTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.StupidDance(0);

        int amount = 20;
        //int amount = 70;

        int animationId = 1;
        bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            float waitTime = 0.07f;

            if (animationId == 1)
                waitTime = 0.14f;

            /*float waitTime = 0.02f;

            if (animationId == 1)
                waitTime = 0.04f;*/

            yield return new WaitForSeconds(waitTime);

            if (this.animations != null)
            {
                this.animations.StupidDance(animationId);
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
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator TestStupidWalkCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestStupidWalk(0);

        //int number = Random.Range(1, 11);
        int number = Random.Range(1, 11);

        int amount = 10;
        float waitTime = 0.1f;

        /*if (number == 1)
        {
            amount = 20;

            waitTime = 0.025f;
        }*/


        int animationId = 1;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(waitTime);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            /*if (this.animations != null)
            {
                this.animations.CaramelDance(animationId);
                Debug.Log(animationId);
            }*/

            if (animationId == 0)
            {
                this.animations.TestStupidWalk(0);
                //waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 1)
            {
                this.animations.TestStupidWalk(1);
                //waitTime = longWaitTime;
                animationId += 1;
            }
            else if (animationId == 2)
            {
                this.animations.TestStupidWalk(2);
                //waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 3)
            {
                this.animations.TestStupidWalk(1);
                //waitTime = longWaitTime;
                animationId = 0;
            }



            amount -= 1;
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }
        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
