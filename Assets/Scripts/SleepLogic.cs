using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepLogic : MonoBehaviour
{
    public TestPlayer user;
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool wakingUp;

    public float timer = 0f;

    public ParticleSystem sleepParticle;
    public GameObject noseBubble;
    public Transform noseBubbleTransform;

    public ParticleSystem noseBubblePopEffect;

    private void Update()
    {
        if (this.onGoing || this.wakingUp)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }

        if (this.timer > 0f)
            this.timer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
            this.user.OnReset += OnReset;
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
            this.user.OnReset -= OnReset;
        }
    }

    public void AddTime(float time)
    {
        if (this.timer < time)
            this.timer = time;
    }

    private IEnumerator SleepTimerCoroutine()
    {
        while (this.timer > 0f)
        {
            yield return null;
        }

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.sleepParticle != null)
            this.sleepParticle.Stop();

        if (this.noseBubble != null)
            this.noseBubble.SetActive(false);

        this.timer = 0f;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/


        this.StopAllCoroutines();
        this.StartCoroutine(this.WakingUpCoroutine());
    }

    private IEnumerator WakingUpCoroutine()
    {
        this.wakingUp = true;
        this.onGoing = false;

        this.animations.SetEyes(0);

        if (this.sleepParticle != null)
            this.sleepParticle.Stop();

        if (this.noseBubble != null)
            this.noseBubble.SetActive(false);

        if (this.noseBubblePopEffect != null)
            this.noseBubblePopEffect.Play();

        this.timer = 0f;

        yield return new WaitForSeconds(0.2f);
        //yield return new WaitForSeconds(2.2f);

        if(this.user.characterId == 2)
        {
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/

            this.animations.SetEyes(5);

            yield return new WaitForSeconds(0.05f);

            //this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.animations.SetEyes(0);

            yield return new WaitForSeconds(0.05f);

            this.animations.SetEyes(5);

            yield return new WaitForSeconds(0.05f);

            this.animations.SetEyes(0);

            /*if (this.animations != null)
                this.animations.SetDefaultPose();

            float currentTime = 0;
            float duration = 0.4f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                //this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
                if (this.animations.eyes != null)
                {
                    //this.animations.eyes.localEulerAngles = new Vector3(this.animations.eyes.localEulerAngles.x, Mathf.Lerp(20f, 360f, currentTime / duration), this.animations.eyes.localEulerAngles.z);
                    this.animations.upperBody.localEulerAngles = new Vector3(this.animations.eyes.localEulerAngles.x, Mathf.Lerp(0f, 360f, currentTime / duration), this.animations.eyes.localEulerAngles.z);
                }
                yield return null;
            }*/

            this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 5f);

            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            this.animations.eyes.localEulerAngles = new Vector3(0f, -10f, 0f);

            yield return new WaitForSeconds(0.05f);

            this.animations.eyes.localEulerAngles = new Vector3(0f, 10f, 0f);

            yield return new WaitForSeconds(0.05f);

            this.animations.eyes.localEulerAngles = new Vector3(0f, -10f, 0f);

            yield return new WaitForSeconds(0.05f);

            this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(0.2f);
        }
        





        if (this.animations != null)
            this.animations.SetDefaultPose();
        this.wakingUp = false;

        

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    //[ContextMenu("Initiate")]
    public void Sleep(float time = 2f)
    {
        if(this.user != null && !this.user.dead && !this.user.knockbackInvounrability && !this.user.countering)
        {
            this.AddTime(time);
        }

        if (!this.onGoing && this.user != null && !this.user.dead && !this.user.knockbackInvounrability && !this.user.countering)
        {
            //this.AddTime(time);

            //this.user.OnHit?.Invoke();

            if (this.wakingUp)
            {
                this.StopAllCoroutines();
                this.wakingUp = false;
            }
            else
            {
                this.user.attackStuns.Add(this.gameObject);

                /*if (this.sleepParticle != null)
                    this.sleepParticle.Play();*/
            }

            this.user.OnHit?.Invoke();

            this.user.AddStun(0.05f, true);
            //this.user.attackStuns.Add(this.gameObject);
            this.onGoing = true;

            if (this.sleepParticle != null)
                this.sleepParticle.Play();

            

            if (this.user.characterId == 3 || this.user.characterId == 4 || this.user.characterId == 7)
            {
                if (this.noseBubble != null)
                    this.noseBubble.SetActive(true);

                if (this.animations != null)
                    this.animations.MikeSleeping();

                this.StartCoroutine(this.MikeSleepingCoroutine());
            }
            else if (this.user.characterId == 2)
            {
                if (this.animations != null)
                    this.animations.RoboJCapSleeping();
            }
            else if (this.user.characterId == 8)
            {
                this.StartCoroutine(this.DemonSleepCoroutine());
            }
            else
            {
                /*if (this.animations != null)
                    this.animations.JCapSleeping(0);*/
                this.StartCoroutine(this.JCapSleepCoroutine());
            }
                

            this.StartCoroutine(this.SleepTimerCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }
        }
    }

    

    private IEnumerator JCapSleepCoroutine()
    {

        if (this.animations != null)
            this.animations.JCapSleeping(0);

        yield return new WaitForSeconds(0.6f);

        if (this.animations != null)
            this.animations.JCapSleeping(1);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.JCapSleeping(2);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.JCapSleeping(3);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.JCapSleeping(2);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.JCapSleeping(1);

        yield return new WaitForSeconds(0.2f);

        this.StartCoroutine(this.JCapSleepCoroutine());
    }

    private IEnumerator MikeSleepingCoroutine()
    {

        /*if (this.noseBubble != null)
            this.noseBubble.SetActive(true);

        if (this.animations != null)
            this.animations.MikeSleeping();*/

        float currentTime = 0;
        float duration = 0.5f;
        float startScale = 0.5f;
        float endScale = 1f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.noseBubbleTransform.localScale = new Vector3(
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration));


            if (this.animations.eyes != null)
            {
                this.animations.eyes.localEulerAngles = new Vector3(this.animations.eyes.localEulerAngles.x, this.animations.eyes.localEulerAngles.y, Mathf.Lerp(3f, 0f, currentTime / duration));
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);

        currentTime = 0;
        duration = 0.5f;
        startScale = 1f;
        endScale = 0.5f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.noseBubbleTransform.localScale = new Vector3(
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration));

            if (this.animations.eyes != null)
            {
                this.animations.eyes.localEulerAngles = new Vector3(this.animations.eyes.localEulerAngles.x, this.animations.eyes.localEulerAngles.y, Mathf.Lerp(0f, 3f, currentTime / duration));
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        this.StartCoroutine(this.MikeSleepingCoroutine());
    }


    private IEnumerator DemonSleepCoroutine()
    {

        if (this.animations != null)
            this.animations.DemonSleeping(0);

        yield return new WaitForSeconds(0.6f);

        if (this.animations != null)
            this.animations.DemonSleeping(1);

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.DemonSleeping(2);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.DemonSleeping(1);

        yield return new WaitForSeconds(0.2f);

        this.StartCoroutine(this.DemonSleepCoroutine());
    }

    public void OnHit()
    {
        if (/*!this.user.dead &&*/ this.onGoing || this.wakingUp)
        {
            if (!this.user.dead)
                this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }
    public void OnDeath()
    {
        if (this.onGoing || this.wakingUp)
            this.Stop();
    }
    public void OnReset()
    {
        this.Stop();
    }

    public void Stop()
    {
        this.StopAllCoroutines();
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.wakingUp = false;

        this.timer = 0f;

        if (this.noseBubble != null)
            this.noseBubble.SetActive(false);

        if (this.sleepParticle != null)
            this.sleepParticle.Stop();
    }







    private IEnumerator MikeSleepingCoroutineOld(float time)
    {
        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;*/

        /*if (this.sleepParticle != null)
            this.sleepParticle.Play();*/

        if (this.noseBubble != null)
            this.noseBubble.SetActive(true);

        if (this.animations != null)
            this.animations.MikeSleeping();

        yield return new WaitForSeconds(2f);

        this.StartCoroutine(this.MikeSleepingCoroutineOld(time));

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.sleepParticle != null)
            this.sleepParticle.Stop();

        if (this.noseBubble != null)
            this.noseBubble.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }


    private IEnumerator TemplateCoroutine(float time)
    {
        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;*/

        /*if (this.sleepParticle != null)
            this.sleepParticle.Play();*/



        if (this.animations != null)
            this.animations.JCapSleeping(0);

        yield return new WaitForSeconds(2f);

        this.StartCoroutine(this.TemplateCoroutine(time));

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.sleepParticle != null)
            this.sleepParticle.Stop();

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }
}
