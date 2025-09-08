using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    //public float fallDuration = 0.2f;
    public ParticleSystem landingParticle;

    public override void OnHit()
    {
        base.OnHit();
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
            this.StartCoroutine(this.TemplateCoroutine());

            //this.user.AddStun(0.2f, true);
            /*if (this.vsDarkJCap != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 1 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsDarkJCap.Initiate();
            }
            else if (this.vsMikeBaller != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 3 && GameManager.Instance != null && GameManager.Instance.gameMode == 0)
            {
                this.vsMikeBaller.Initiate();
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 9f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();
        if (this.animations != null)
            this.animations.JoeStartAnimationFalling(0);

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 9f, 0f);

        //FALL ANIM 1 HERE
        if (this.animations != null)
            this.animations.JoeStartAnimationFalling(0);

        float currentTime = 0;
        float duration = 0.25f;
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
            this.landingParticle.Play();
        }

        this.animations.JoeStartAnimationFalling(1);
        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        //int number = Random.Range(1, 1001);
        int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number == 1)
        {
            /*this.user.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            this.user.Suicide();*/

            //this.user.Die(this.user.transform.position, false, true, true, false, 0);

            //this.user.Die(this.user.transform.position, false, false, true, false, 0);
            this.user.Die(new Vector3(this.user.transform.position.x /*+ (this.transform.forward.z * 1f)*/, -9.5f, 0), true, false, true, false, 0);
        }
        else if (this.animations != null)
        {
            //this.animations.JCapLandingStartAnimation();
            this.animations.JoeStartAnimationFalling(1);
            //FALL ANIM 2 HERE
        }

        yield return new WaitForSeconds(1f);

        //FALL ANIM 3 HERE
        if (this.animations != null)
            this.animations.JoeStartAnimationFalling(2);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.RoadRollerEndLand();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.PlayFire(false);
        yield return new WaitForSeconds(0.025f);

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
        //this.PlayFire(false);

        /*if (this.animations != null)
            this.animations.rightArm.localScale = new Vector3(1f, 1f, 1f);*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }
}
