using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcismSuper : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    //public GameObject beam;
    public HitboxSetOwner beam;

    public GameObject crosses;

    public GameObject cross1, cross2, cross3, cross4;

    public GameObject bible;

    public GameObject godLight;

    public CharStartToEndPos[] crossMovers;
    public ObjectScaleLerp[] crossScalers;

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
                if (this.user.superCharge >= this.user.maxSuperCharge)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TemplateCoroutine());
                }
                
            }

            
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.GrandFlameStart();

        if (this.animations != null)
            this.animations.Exorcism();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.user.soundEffects != null)
        {
            this.user.soundEffects.PlaySuperSfx();
        }

        //yield return new WaitForSeconds(0.2f);
        if (this.crosses != null)
            this.crosses.SetActive(true);

        if (this.bible != null)
            this.bible.SetActive(true);

        if (this.godLight != null)
            this.godLight.SetActive(true);

        float testTime2 = 0f;
        float currentTime = 0;
        float duration = 0.25f;
        float startPosY2 = this.animations.body.localPosition.y;

        float bibleStartPosY2 = this.bible.transform.localPosition.y;
        while (currentTime < duration)
        {
            //time -= Time.deltaTime;
            currentTime += Time.deltaTime;

            testTime2 += Time.deltaTime;

            float newY = Mathf.Sin(2 * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY2 + (newY * 0.01f), this.animations.body.localPosition.z);

            this.bible.transform.localPosition = new Vector3(this.bible.transform.localPosition.x, bibleStartPosY2 + (newY * 0.01f), this.bible.transform.localPosition.z);

            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.cross1 != null)
                this.cross1.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(0f, 1.25f, currentTime / duration));

            if (this.cross2 != null)
                this.cross2.transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(0f, -1.25f, currentTime / duration));

            if (this.cross3 != null)
                this.cross3.transform.localPosition = new Vector3(Mathf.Lerp(0f, 1.25f, currentTime / duration), 0f, 0f);

            if (this.cross4 != null)
                this.cross4.transform.localPosition = new Vector3(Mathf.Lerp(0f, -1.25f, currentTime / duration), 0f, 0f);

            yield return null;
        }


        float testTime = 0f;
        float time = 3;
        float startPosY = this.animations.body.localPosition.y;

        float bibleStartPosY = this.bible.transform.localPosition.y;
        while (time > 0)
        {
            time -= Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            this.bible.transform.localPosition = new Vector3(this.bible.transform.localPosition.x, bibleStartPosY + (newY * 0.01f), this.bible.transform.localPosition.z);

            if (this.crosses != null)
                this.crosses.transform.Rotate(new Vector3(0f, 200 * Time.deltaTime, 0f));

            yield return null;
        }

        if(this.beam != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            HitboxSetOwner beamPrefab = this.beam;
            //beamPrefab = Instantiate(beamPrefab, new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f), Quaternion.Euler(0, 0, 0));

            //beamPrefab = Instantiate(beamPrefab, new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f), this.user.tempOpponent.transform.rotation);
            beamPrefab = Instantiate(beamPrefab, new Vector3(summonPos, 0f, 0f), this.user.tempOpponent.transform.rotation);
            beamPrefab.SetOwner(this.user);
        }

        foreach (CharStartToEndPos crossMover in this.crossMovers)
        {
            if (crossMover != null)
                crossMover.Move();
        }

        foreach (ObjectScaleLerp crossScaler in this.crossScalers)
        {
            if (crossScaler != null)
                crossScaler.ScaleUp();
        }


        testTime = 0f;
        time = 0.5f;
        startPosY = this.animations.body.localPosition.y;

        bibleStartPosY = this.bible.transform.localPosition.y;
        while (time > 0)
        {
            time -= Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);

            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            this.bible.transform.localPosition = new Vector3(this.bible.transform.localPosition.x, bibleStartPosY + (newY * 0.01f), this.bible.transform.localPosition.z);

            /*if (this.crosses != null)
                this.crosses.transform.Rotate(new Vector3(0f, 200 * Time.deltaTime, 0f));*/
            yield return null;
        }

        //yield return new WaitForSeconds(1f);

        if (this.crosses != null)
        {
            this.crosses.SetActive(false);
            this.crosses.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        foreach (CharStartToEndPos crossMover in this.crossMovers)
        {
            if (crossMover != null)
                crossMover.SetStartPos();
        }

        foreach (ObjectScaleLerp crossScaler in this.crossScalers)
        {
            if (crossScaler != null)
                crossScaler.SetStartScale();
        }


        if (this.bible != null)
        {
            this.bible.transform.localPosition = new Vector3(this.bible.transform.localPosition.x, 3.5f, this.bible.transform.localPosition.z);
            this.bible.SetActive(false);
        }

        if (this.godLight != null)
            this.godLight.SetActive(false);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 45f, 0f);
        }
            

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.crosses != null)
            this.crosses.SetActive(false);

        foreach (CharStartToEndPos crossMover in this.crossMovers)
        {
            if (crossMover != null)
                crossMover.SetStartPos();
        }

        foreach (ObjectScaleLerp crossScaler in this.crossScalers)
        {
            if (crossScaler != null)
                crossScaler.SetStartScale();
        }

        if (this.bible != null)
        {
            this.bible.transform.localPosition = new Vector3(this.bible.transform.localPosition.x, 3.5f, this.bible.transform.localPosition.z);
            this.bible.SetActive(false);
        }

        if (this.godLight != null)
            this.godLight.SetActive(false);


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
