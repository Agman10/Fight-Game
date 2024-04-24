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

    public Attack vsJCap;

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

        

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
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

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
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

    private IEnumerator RagingBeastStartAnimationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -10f, this.user.transform.position.y, 0f);
        
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
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.025f);
            this.PunchEffect(new Vector3(this.user.transform.forward.z * 1f + this.RandomXY(-1f, 1f), this.RandomXY(0, 3), 0));
            amount -= 1;
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

        if (this.animations != null)
            this.animations.SetDefaultPose();



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PunchEffect(Vector3 position)
    {
        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            punchEffectPrefab = Instantiate(punchEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }
    }

    private float RandomXY(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }
}
