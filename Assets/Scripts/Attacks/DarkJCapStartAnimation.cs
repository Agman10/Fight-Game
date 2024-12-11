using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DarkJCapStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public VisualEffect fire;

    public GameObject trail;
    public GameObject punchEffect;

    //public GameObject ragingFlame;

    //public BigFireBall bigFireBall;

    public Attack vsJCap;
    public Attack vsMaster;

    public AudioSource ragingBeastPunchSfx;
    public AudioSource ragingBeastSwooshSfx;

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

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            if (this.user.tempOpponent != null && this.user.tempOpponent.characterId == 1 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
                this.StartCoroutine(this.RagingBeastStartAnimationCoroutine());
            else if (this.vsJCap != null && this.user.characterId == 1 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 0 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
                this.vsJCap.Initiate();
            else if (this.vsMaster != null && this.user.characterId == 1 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 6 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
                this.vsMaster.Initiate();
            else
                this.StartCoroutine(this.StartAnimationCoroutine());
        }
    }

    private IEnumerator StartAnimationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 5f, 0f);
        this.PlayFire(true);
        this.user.LookAtTarget();
        if (this.animations != null)
            this.animations.DarkJCapStartAnimation();
        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.DarkJCapStartAnimation();

        float currentTime = 0;
        float duration = 0.8f;
        float targetPosition = 1f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }
        this.PlayFire(false);

        currentTime = 0;
        duration = 0.2f;
        targetPosition = 0f;
        start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();


        yield return new WaitForSeconds(0.1f);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;

        this.PlayFire(false);

        if (this.trail != null)
            this.trail.SetActive(false);

        if (GameManager.Instance != null && this.onGoing)
            GameManager.Instance.RagingBeastEffect(0);

        /*if (this.ragingFlame != null)
            this.ragingFlame.SetActive(false);*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
    public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }


    private IEnumerator StartAnimation2Coroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        this.user.LookAtTarget();

        if (this.animations != null)
            this.animations.RagingBeastPose();

        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.RagingBeastPose();


        yield return new WaitForSeconds(0.25f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/

        if (this.animations != null)
            this.animations.RagingBeastStartMidPose();

        yield return new WaitForSeconds(0.05f);

        /*if (this.ragingFlame != null)
            this.ragingFlame.SetActive(true);*/

        if (this.animations != null)
            this.animations.RagingBeastStartPose();

        float currentTime = 0;
        float duration = 0.6f;

        float testTime = 0f;
        float startPosY = this.animations.body.localPosition.y;
        while (currentTime < duration)
        {
            testTime += Time.deltaTime;

            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);


            if (currentTime >= duration - 0.1f && this.user.input.taunting)
                currentTime = duration - 0.1f;
            yield return null;
        }

        /*if (this.ragingFlame != null)
            this.ragingFlame.SetActive(false);*/

        

        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);


        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator RagingBeastStartAnimationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -10f, this.user.transform.position.y, 0f);

        if (this.ragingBeastSwooshSfx != null)
        {
            this.ragingBeastSwooshSfx.time = 0.06f;
            this.ragingBeastSwooshSfx.Play();
        }

        yield return new WaitForSeconds(0.01f);
        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.animations != null)
            this.animations.RagingBeastDash();

        float currentTime = 0;
        float duration = 0.4f;
        float targetPosition = this.user.transform.forward.z * -0.7f;
        //float start = this.user.transform.position.x;
        //float start = this.user.transform.forward.z * -7f;
        float start = this.user.transform.forward.z * -10f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }
        if (this.animations != null)
            this.animations.StartAnimationRagingBeastGrab();

        yield return new WaitForSeconds(0.2f);
        if (this.trail != null)
            this.trail.SetActive(false);


        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(1);

        this.user.transform.position = new Vector3(this.user.transform.forward.z * -1f, this.user.transform.position.y, 0);

        if (this.animations != null)
            this.animations.StartAnimationRagingBeastBlock();
        //int amount = 18;
        yield return new WaitForSeconds(0.1f);
        int amount = 30;
        int amount2 = 0;
        if (this.user.playerNumber == 2)
            amount2 = 1;
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.025f);
            bool playSfx = false;
            if (amount2 == 0)
                playSfx = true;
            else
                playSfx = false;

            this.PunchEffect(new Vector3(this.user.transform.forward.z * 1f + this.RandomXY(-1f, 1f), this.RandomXY(0, 3), 0), playSfx);
            amount -= 1;

            amount2++;
            if (amount2 > 1)
                amount2 = 0;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(0);



        //yield return new WaitForSeconds(0.1f);

        currentTime = 0;
        duration = 0.15f;
        targetPosition = this.user.transform.forward.z * -5f;
        start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }

        /*if (this.animations != null)
            this.animations.StartAnimationRagingBeastBlock2();*/

        currentTime = 0;
        duration = 0.2f;
        targetPosition = this.user.transform.forward.z * -7f;
        start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        /*this.animations.SuperFireBallCharge();

        yield return new WaitForSeconds(0.3f);

        if (this.bigFireBall != null)
        {
            BigFireBall bigFireBallPrefab = this.bigFireBall;
            bigFireBallPrefab = Instantiate(bigFireBallPrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, this.transform.position.y + 1.56f, 0), this.transform.rotation);
            bigFireBallPrefab.SetOwner(this.user);
        }

        this.animations.SuperFireBallShoot();

        yield return new WaitForSeconds(0.5f);*/


        if (this.animations != null)
            this.animations.SetDefaultPose();



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    public void PunchEffect(Vector3 position, bool playSfx)
    {
        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            punchEffectPrefab = Instantiate(punchEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }

        if (this.ragingBeastPunchSfx != null && playSfx)
        {
            this.ragingBeastPunchSfx.time = 0.01f;
            this.ragingBeastPunchSfx.pitch = Random.Range(0.9f, 1.1f);
            //this.punchSfx.pitch = Random.Range(0.85f, 0.95f);
            //this.punchSfx.pitch = 0.9f;
            this.ragingBeastPunchSfx.Play();
        }
    }

    private float RandomXY(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }
}
