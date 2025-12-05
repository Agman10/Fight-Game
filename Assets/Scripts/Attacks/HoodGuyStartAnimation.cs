using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HoodGuyStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public ObjectScaleLerp cosmos;

    public VisualEffect smoke;

    public AudioSource throwSfx;
    public AudioSource rollSfx;
    public AudioSource growSfx;

    public AnimationCurve throwCurve;

    public GameObject pipe;
    public AnimationCurve pipeJumpCurve;

    //public float fallDuration = 0.2f;
    //public VisualEffect fire;
    //public GameObject landingParticle;
    /*public GameObject propeller;
    public GameObject propellerBlade;*/

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
            //this.StartCoroutine(this.TemplateCoroutine2());
            //this.StartCoroutine(this.SmokeStartAnimation());

            int number = Random.Range(0, 10);

            if (this.user.tempOpponent != null && this.user.tempOpponent.characterId == 5 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                if (this.user.playerNumber == 2)
                    this.StartCoroutine(this.ThrowP2());
                else
                    this.StartCoroutine(this.ThrowP1());
            }
            else
            {
                /*if (number == 0)
                    this.StartCoroutine(this.PipeCoroutine());
                else
                    this.StartCoroutine(this.TemplateCoroutine2());*/

                if (number == 0)
                {
                    this.StartCoroutine(this.PipeCoroutine());
                }
                else if (number == 1)
                {
                    int numberr = Random.Range(0, 3);
                    if (numberr == 0)
                        this.StartCoroutine(this.SlideStartAnimation());
                    else if (numberr == 1)
                        this.StartCoroutine(this.SpinStartAnimation(false));
                    else
                        this.StartCoroutine(this.SpinStartAnimation(true));

                    //Debug.Log(numberr);
                }
                else
                {
                    this.StartCoroutine(this.TemplateCoroutine2());
                }
                //Debug.Log(number);

                //this.StartCoroutine(this.TemplateCoroutine2());
                //this.StartCoroutine(this.PipeCoroutine());

                //this.StartCoroutine(this.SmokeStartAnimation());
                //this.StartCoroutine(this.SlideStartAnimation());
                //this.StartCoroutine(this.SpinStartAnimation());

            }
        }
    }

    private IEnumerator TemplateCoroutine2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0, 0f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        if(this.cosmos != null)
        {
            this.cosmos.gameObject.SetActive(true);
            //this.cosmos.ScaleUp();
            //this.cosmos.ScaleDown();
        }

        float currentTime = 0;
        float duration = 0.75f;
        /*float targetPosition = 0f;
        float start = this.transform.position.y;*/
        float targetPosition = this.user.animations.defaultYPos;
        float start = this.user.animations.body.transform.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }

        if (this.cosmos != null)
        {
            //this.cosmos.gameObject.SetActive(false);
            //this.cosmos.ScaleUp();
            this.cosmos.ScaleDown();
        }

        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);


        yield return new WaitForSeconds(0.25f);

        if (this.cosmos != null)
        {
            this.cosmos.gameObject.SetActive(false);
            //this.cosmos.ScaleUp();
            //this.cosmos.ScaleDown();
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        float currentTime = 0;
        float duration = 1f;
        float targetPosition = 0f;
        float start = this.transform.position.y;
        /*float targetPosition = this.user.animations.defaultYPos;
        float start = this.user.animations.body.transform.localPosition.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);


        //yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

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

        /*if (this.animations != null)
            this.animations.rightArm.localScale = new Vector3(1f, 1f, 1f);*/

        if (this.cosmos != null)
            this.cosmos.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        if (this.rollSfx != null)
            this.rollSfx.Stop();


        if (this.pipe != null)
        {
            this.pipe.SetActive(false);
        }

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        this.PlaySmoke(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator SmokeStartAnimation()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 250f, 0f);
        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);

        /*if (this.animations != null)
            this.animations.body.localScale = new Vector3(0f, 0f, 0f);*/

        this.user.transform.position = new Vector3(this.user.transform.position.x, -3f, 0f);
        this.PlaySmoke(true);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        float currentTime = 0;
        float duration = 0.5f;
        float targetPosition = 0f;
        float start = this.transform.position.y;
        /*float targetPosition = this.user.animations.defaultYPos;
        float start = this.user.animations.body.transform.localPosition.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        /*if (this.animations != null && this.user != null)
            this.animations.body.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);*/

        this.PlaySmoke(false);

        yield return new WaitForSeconds(0.2f);


        yield return new WaitForSeconds(0.25f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator SlideStartAnimation()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.LookAtCenter();
        //this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        this.user.animations.body.transform.localPosition = new Vector3(-4f, this.user.animations.defaultYPos, 0f);
        this.user.animations.body.transform.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

        /*this.user.animations.upperBody.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        this.user.animations.lowerBody.transform.localEulerAngles = new Vector3(0f, 90f, 0f);*/


        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        this.user.animations.body.transform.localPosition = new Vector3(-4f, this.user.animations.defaultYPos, 0f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        

        this.user.animations.body.transform.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

        /*this.user.animations.upperBody.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        this.user.animations.lowerBody.transform.localEulerAngles = new Vector3(0f, 90f, 0f);*/

        float currentTime = 0;
        float duration = 0.75f;
        /*float targetPosition = 0f;
        float start = this.transform.position.y;*/
        float targetPosition = 0f;
        float start = -4f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.animations.defaultYPos, 0f);
            //this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0, this.user.transform.forward.z * 1080f, currentTime / duration), 0f);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        currentTime = 0;
        duration = 0.1f;
        start = this.user.transform.forward.z * 90f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(start, 0f, currentTime / duration), 0f);
            yield return null;
        }


        /*currentTime = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.upperBody.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(90f, 0f, currentTime / duration), 0f);
            yield return null;
        }

        currentTime = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.lowerBody.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(90f, 0f, currentTime / duration), 0f);
            yield return null;
        }*/

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator SpinStartAnimation(bool alternate = false)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;
        this.user.LookAtCenter();
        //this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        this.user.animations.body.transform.localPosition = new Vector3(-4f, this.user.animations.defaultYPos, 0f);


        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        this.user.animations.body.transform.localPosition = new Vector3(-4f, this.user.animations.defaultYPos, 0f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        float currentTime = 0;
        float duration = 1f;
        /*float targetPosition = 0f;
        float start = this.transform.position.y;*/
        float targetPosition = 0f;
        float start = -4f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.animations.defaultYPos, 0f);

            if (alternate)
                this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0, this.user.transform.forward.z * -900f, currentTime / duration), 0f);
            else
                this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0, this.user.transform.forward.z * -1080f, currentTime / duration), 0f);



            //this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0, this.user.transform.forward.z * -1260f, currentTime / duration), 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);
        if (alternate)
        {
            this.user.animations.body.transform.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 180f, 0f);
            yield return new WaitForSeconds(0.6f);
            currentTime = 0;
            duration = 0.6f;
            //start = this.user.animations.body.transform.localEulerAngles.y;
            start = this.user.transform.forward.z * 180f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.user.animations.body.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(start, this.user.transform.forward.z * -360, currentTime / duration), 0f);
                yield return null;
            }
        }
        

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }


    private IEnumerator ThrowP1()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.body.localScale = new Vector3(0f, 0f, 0f);*/

        //this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        //this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);





        this.user.animations.ItemThrow(0);

        yield return new WaitForSeconds(0.2f);

        this.user.animations.ItemThrow(1);

        yield return new WaitForSeconds(0.05f);

        this.user.animations.ItemThrow(2);

        /*if (this.nothingSfx != null)
            this.nothingSfx.Play();*/

        if (this.throwSfx != null)
        {
            //this.throwSfx.time = 0.01f;
            this.throwSfx.Play();
        }

        yield return new WaitForSeconds(0.05f);

        this.user.animations.ItemThrow(3);

        //this.ThrowRandomItem();

        //HERE HE THROWS PLAYER 2

        yield return new WaitForSeconds(0.3f);

        /*float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }*/

        this.user.animations.ItemThrow(4);

        /*currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }*/

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*currentTime = 0;
        duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        currentTime = 0;
        //float currentTime2 = 0;
        duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);*/

        //Debug.Log("P1");

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }



    private IEnumerator ThrowP2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;

        this.user.rb.isKinematic = true;

        this.user.LookAtTarget();

        if (this.animations != null)
            this.animations.body.localScale = new Vector3(0f, 0f, 0f);

        this.user.transform.position = new Vector3(-6.2f, 3f, 0f);
        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        

        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();


        yield return new WaitForSeconds(0.2f);





        

        yield return new WaitForSeconds(0.2f);

        

        yield return new WaitForSeconds(0.05f);

        yield return new WaitForSeconds(0.05f);

        //this.ThrowRandomItem();

        //HERE HE THROWS PLAYER 2

        if (this.animations != null && this.user != null)
        {
            this.animations.RollAnimation();
            this.animations.body.localScale = new Vector3(0.5f, 0.5f, 0.5f * this.user.transform.forward.z);
            this.animations.body.localPosition = new Vector3(0f, 0.65f, 0f);
            //this.animations.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y / 2f, 0f);
        }

        if (this.rollSfx != null)
            this.rollSfx.Play();

        float currentTime = 0;
        //float duration = 0.6f;
        float duration = 0.85f;
        float targetPositionX = 7f;
        float startX = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), this.throwCurve.Evaluate(currentTime / duration), 0f);

            //Debug.Log(this.throwCurve.Evaluate(currentTime / duration));

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));
            yield return null;
        }

        if (this.rollSfx != null)
            this.rollSfx.Stop();


        /*float currentTime = 0;
        float duration = 0.3f;
        float targetPositionX = 0f;
        float targetPositionY = 6f;
        float startX = this.user.transform.position.x;
        float startY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0f);

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f,2000f * Time.deltaTime));
            yield return null;
        }

        currentTime = 0;
        //float currentTime2 = 0;
        duration = 0.3f;
        targetPositionX = 7f;
        targetPositionY = 0f;
        startX = this.user.transform.position.x;
        startY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //currentTime2 += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0f);
            //this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime2 / 0.35f), Mathf.Lerp(startY, targetPositionY, currentTime2 / 0.35f), 0f);

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f,2000f * Time.deltaTime));
            yield return null;
        }*/

        this.animations.SetPunchUppercutStartAnim1();
        this.animations.body.localPosition = new Vector3(0f, 0.72f, 0f);


        //yield return new WaitForSeconds(0.3f);

        

        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        /*currentTime = 0;
        //currentTime2 = 0;

        duration = 0.05f;
        targetPositionX = 7f;
        targetPositionY = 0f;
        startX = this.user.transform.position.x;
        startY = this.user.transform.position.y;
        while (currentTime2 < 0.35f)
        {
            currentTime += Time.deltaTime;
            currentTime2 += Time.deltaTime;

            //this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0f);
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime2 / 0.35f), Mathf.Lerp(startY, targetPositionY, currentTime2 / 0.35f), 0f);

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, *//*this.user.transform.forward.z **//* 2000f * Time.deltaTime));
            yield return null;
        }*/


        if (this.animations != null && this.user != null)
        {
            this.animations.SetDefaultPose();
            //this.animations.body.localScale = new Vector3(0.5f, 0.5f, 0.5f * this.user.transform.forward.z);
            //this.animations.body.localPosition = new Vector3(0f, 0.65f, 0f);
            this.animations.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y / 2f, 0f);
        }

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        yield return new WaitForSeconds(0.5f);

        if (this.growSfx != null)
        {
            //this.throwSfx.time = 0.01f;
            this.growSfx.Play();
        }

        currentTime = 0;
        //float currentTime2 = 0;
        duration = 0.2f;
        float targetScale = 1f;
        float startScale = 0.5f;

        float targetScaleZ = 1f * this.user.transform.forward.z;
        float startScaleZ = 0.5f * this.user.transform.forward.z;

        float startPos = 1.95f * 0.5f;
        float targetPos = 1.95f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            this.user.animations.body.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));

            this.user.animations.body.localPosition = new Vector3(0f, Mathf.Lerp(startPos, targetPos, currentTime / duration), 0f);

            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.5f);

        //Debug.Log("P2");

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }



    private IEnumerator PipeCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        this.animations.HoodGuyPipe(0);

        //this.user.transform.position = new Vector3(this.user.transform.position.x, -5f, 0f);
        this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);

        this.user.LookAtTarget();

        if (this.pipe != null)
        {
            this.pipe.SetActive(true);
            //this.pipe.transform.localPosition = new Vector3(0f, -1.8f, 0f);
        }
            

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0, 0f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.animations.HoodGuyPipe(0);

        //this.user.animations.body.transform.localPosition = new Vector3(0f, this.user.animations.defaultYPos - 5f, 0f);
        this.user.animations.body.transform.localPosition = new Vector3(0f, 0f, 0f);

        /*float currentTime = 0;
        float duration = 0.1f;
        float targetPosition = 0f;
        float start = -1.8f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.pipe != null)
                this.pipe.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }

        this.user.animations.body.transform.localPosition = new Vector3(0f, 0f, 0f);*/
        yield return new WaitForSeconds(0.1f);

        float currentTime = 0;
        //float duration = 0.75f;
        float duration = 0.45f;
        /*float targetPosition = 0f;
        float start = this.transform.position.y;*/
        //float targetPosition = this.user.animations.defaultYPos;
        float targetPosition = 3.45f;
        float start = this.user.animations.body.transform.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }

        /*if (this.cosmos != null)
        {
            //this.cosmos.gameObject.SetActive(false);
            //this.cosmos.ScaleUp();
            this.cosmos.ScaleDown();
        }*/

        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        this.animations.HoodGuyPipe(1);
        this.user.animations.body.transform.localPosition = new Vector3(0f, 3.45f, 0f);

        /*currentTime = 0;
        duration = 0.2f;

        targetPosition = 6f;
        start = this.user.animations.body.transform.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }*/

        /*currentTime = 0;
        duration = 0.1f;

        targetPosition = this.user.animations.defaultYPos;
        //targetPosition = 6f;
        start = this.user.animations.body.transform.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }*/




        /*currentTime = 0;
        duration = 0.4f;

        targetPosition = 6f;
        start = this.user.animations.body.transform.localPosition.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, this.pipeJumpCurve.Evaluate(currentTime / duration), 0f);
            yield return null;
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();



        currentTime = 0;
        duration = 0.25f;
        targetPosition = -1.8f;
        start = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.pipe != null)
                this.pipe.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }

        if (this.pipe != null)
            this.pipe.SetActive(false);*/



        float currentTime2 = 0;
        float duration2 = 0.4f;

        currentTime = 0;
        duration = 0.25f;
        /*float targetPosition = 0f;
        float start = this.transform.position.y;*/
        targetPosition = -1.8f;
        start = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            currentTime2 += Time.deltaTime;

            if (this.pipe != null)
                this.pipe.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);

            this.user.animations.body.transform.localPosition = new Vector3(0f, this.pipeJumpCurve.Evaluate(currentTime2 / duration2), 0f);

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            yield return null;
        }

        if (this.pipe != null)
            this.pipe.SetActive(false);

        currentTime = 0;
        duration = 0.4f;

        targetPosition = 6f;
        start = this.user.animations.body.transform.localPosition.y;
        while (currentTime2 < duration2)
        {
            currentTime2 += Time.deltaTime;
            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            //this.user.animations.body.transform.localPosition = new Vector3(0f, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);
            this.user.animations.body.transform.localPosition = new Vector3(0f, this.pipeJumpCurve.Evaluate(currentTime2 / duration2), 0f);
            yield return null;
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.25f);

        /*if (this.cosmos != null)
        {
            this.cosmos.gameObject.SetActive(false);
            //this.cosmos.ScaleUp();
            //this.cosmos.ScaleDown();
        }*/

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        yield return new WaitForSeconds(0.1f);

        this.user.EntranceDone();
    }



    /*private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 7f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        *//*if (this.animations != null)
            this.animations.DarkJCapStartAnimation();*//*

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
        int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number == 1)
        {
            this.user.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            this.user.Suicide();
        }
        else if (this.animations != null)
        {
            this.animations.JCapLandingStartAnimation();
        }

        *//*yield return new WaitForSeconds(1f - this.fallDuration - 0.1f);
        if (this.animations != null)
            this.animations.JCapLandingStartAnimation2();
        yield return new WaitForSeconds(0.1f);*//*

        yield return new WaitForSeconds(1f - this.fallDuration - 0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.1f);

        //this.PlayFire(false);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }*/

    public void PlaySmoke(bool playing)
    {
        if (this.smoke != null)
        {
            if (playing)
                this.smoke.Play();
            else
                this.smoke.Stop();
        }
    }
}
