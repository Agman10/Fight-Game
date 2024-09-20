using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DarkJCapTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public Spinner bombJuggle;

    public GameObject handCigg;
    public GameObject mouthCigg;
    public VisualEffect ciggLitFlame;
    public VisualEffect ciggRemoverFlame;
    public ParticleSystem ciggDestroySmoke;

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

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y, 0);
            }

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
                    //this.StartCoroutine(this.NeutralTauntCoroutine());
                    this.StartCoroutine(this.SmokingCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    this.StartCoroutine(this.BackTauntCoroutine());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
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

        if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(false);
            this.animations.neutralEyes.gameObject.SetActive(true);
        }

        if (this.bombJuggle != null)
            this.bombJuggle.speedMultiplier = 0f;

        /*if (this.bombJuggle != null)
        {
            this.bombJuggle.SetActive(false);
            this.bombJuggle.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        if (this.ciggLitFlame != null)
            this.ciggLitFlame.Stop();

        if (this.ciggRemoverFlame != null)
            this.ciggRemoverFlame.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.bombJuggle != null)
        {
            this.bombJuggle.gameObject.SetActive(false);
            this.bombJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.bombJuggle.speedMultiplier = 1f;
        }

        if (this.handCigg != null)
        {
            this.handCigg.SetActive(false);
            this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
        }

        if (this.mouthCigg != null)
            this.mouthCigg.SetActive(false);
    }

    private IEnumerator NeutralTauntCoroutine()
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

    private IEnumerator DownTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.Wry();*/

        if (this.animations != null)
            this.animations.LDance(0);

        int amount = 20;
        int animationId = 1;
        bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.125f);
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
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }

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
            this.animations.Juggle(0);

        if (this.bombJuggle != null)
        {
            this.bombJuggle.gameObject.SetActive(true);
            this.bombJuggle.speedMultiplier = 1f;
            //this.bombJuggle.transform.eulerAngles = new Vector3(this.bombJuggle.transform.rotation.x, 0f, this.bombJuggle.transform.rotation.z);
            //this.bombJuggle.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            this.bombJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/


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
            /*if (idForward)
                animationId += 1;
            else if (!idForward)
                animationId -= 1;

            if (animationId == 0)
                idForward = true;
            else if (animationId == 2)
                idForward = false;*/

            if (animationId == 3)
                animationId = 0;
            else
                animationId += 1;

            amount -= 1;
            if (amount <= 0 && this.user.input.taunting)
                amount = 1;
            //Debug.Log(amount);
        }

        if (this.bombJuggle != null)
        {
            this.bombJuggle.gameObject.SetActive(false);
            this.bombJuggle.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator SmokingCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        this.animations.Smoking(0);

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.handCigg.SetActive(true);
        }
            

        this.animations.Smoking(1);

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, -132f, 0f);
        }

        this.animations.Smoking(2);

        yield return new WaitForSeconds(0.1f);

        this.animations.Smoking(3);

        yield return new WaitForSeconds(0.1f);

        this.animations.Smoking(4);

        yield return new WaitForSeconds(0.2f);

        if (this.ciggLitFlame != null)
            this.ciggLitFlame.Play();

        yield return new WaitForSeconds(0.1f);

        if (this.ciggLitFlame != null)
            this.ciggLitFlame.Stop();

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, 2.8f, 0f);
        }

        this.animations.Smoking(5);

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            //this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.handCigg.SetActive(false);
        }
            

        if (this.mouthCigg != null)
            this.mouthCigg.SetActive(true);

        this.animations.Smoking(6);

        int amount = 4;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(1f);

            this.user.TakeDamage(this.user.transform.position, 0.1f, 0f, 0f, 0f, false, true);

            amount -= 1;

            if (amount <= 0 && this.user.input.taunting)
                amount = 1;

            yield return null;
        }

        if (this.mouthCigg != null)
            this.mouthCigg.SetActive(false);

        if (this.handCigg != null)
        {
            //this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.handCigg.SetActive(true);
        }

        this.animations.Smoking(5, true);

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, 48f, 0f);
            //this.handCigg.SetActive(true);
        }

        this.animations.Smoking(3, true);

        yield return new WaitForSeconds(0.1f);

        this.animations.Smoking(2, true);

        yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            //this.handCigg.SetActive(true);
        }

        //this.animations.Smoking(1, true);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*yield return new WaitForSeconds(0.1f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.handCigg.SetActive(false);
        }*/


        

        if (this.ciggRemoverFlame != null)
            this.ciggRemoverFlame.Play();
        yield return new WaitForSeconds(0.05f);

        if (this.handCigg != null)
        {
            this.handCigg.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            this.handCigg.SetActive(false);
        }
        if (this.ciggDestroySmoke != null)
            this.ciggDestroySmoke.Play();

        if (this.ciggRemoverFlame != null)
            this.ciggRemoverFlame.Stop();

        yield return new WaitForSeconds(0.05f);



        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

    }
}
