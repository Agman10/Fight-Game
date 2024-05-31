using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFlameAttack : Attack
{
    public TempPlayerAnimations animations;
    public GrandFlame grandFlame;
    public GrandFlame currentGrandFlame;
    //public bool spinning;
    public bool onGoing;
    public bool canBeCanceled;

    public GameObject startParticle;
    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && this.canBeCanceled)
        {
            this.Stop();
            //Debug.Log("Test");
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }

        /*if (this.user.ragdoll != null *//*&& !this.user.dead*//* && this.onGoing)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);*/
    }
    public override void OnReset()
    {
        if (this.currentGrandFlame != null)
        {
            //this.currentGrandFlame.Stop();
            this.currentGrandFlame.gameObject.SetActive(false);
            this.currentGrandFlame = null;
        }
        base.OnReset();
    }
    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    //this.user.AddStun(1.3f, true);
                    this.user.AddStun(0.2f, true);
                    //this.StartCoroutine(this.KickUppercutCoroutine());
                    this.StartCoroutine(this.GrandFlameCoroutine());
                }
            }
            
        }
    }
    IEnumerator GrandFlameCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.canBeCanceled = true;
        if (this.animations != null)
            this.animations.GrandFlameStart();
        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }


        //yield return new WaitForSeconds(0.2f);

        float testTime = 0f;
        float time = 0.2f;
        float startPosY = this.animations.body.localPosition.y;
        while (time > 0)
        {
            time -= Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 100f);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);
            yield return null;
        }


        if (this.animations != null)
            this.animations.GrandFlameMid();

        yield return new WaitForSeconds(0.05f);

        this.user.rb.constraints = RigidbodyConstraints.FreezeAll;
        this.user.knockbackInvounrability = true;




        float currentTime = 0;
        float duration = 0.15f;
        //float targetVolume = 0.1f;
        float targetPosition = 3.5f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.25f);
        /*if (this.animations != null)
            this.animations.GrandFlameMid();*/

        if (this.grandFlame != null)
        {
            GrandFlame grandFlamePrefab = this.grandFlame;
            grandFlamePrefab = Instantiate(grandFlamePrefab, new Vector3(this.user.transform.position.x, 0f, 0f), Quaternion.Euler(0, 0, 0));
            grandFlamePrefab.SetOwner(this.user);
            this.currentGrandFlame = grandFlamePrefab;
        }

        this.canBeCanceled = false;

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.GrandFlame();
        //this.canBeCanceled = false;
        

        float currentTime2 = 0;
        float duration2 = 3f;

        while (currentTime2 < duration2)
        {
            currentTime2 += Time.deltaTime;
            float newY = Mathf.Sin(Time.time * 40);
            this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, this.animations.body.localPosition.y + (newY * 0.05f), this.animations.body.localPosition.z);

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        //yield return new WaitForSeconds(3f);
        
        if (this.currentGrandFlame != null)
        {
            this.currentGrandFlame.Stop();
            this.currentGrandFlame = null;
        }
        /*this.user.LookAtTarget();
        if (this.animations != null)
            this.animations.GrandFlame();*/
        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.knockbackInvounrability = false;

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
        this.canBeCanceled = false;
    }

    public override void Stop()
    {
        base.Stop();

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.PlayFire(false);*/
        //this.spinning = false;
        if (!this.user.dead)
            this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        this.user.knockbackInvounrability = false;
        this.onGoing = false;
        this.canBeCanceled = false;
        if (this.currentGrandFlame != null)
        {
            this.currentGrandFlame.Stop();
            this.currentGrandFlame = null;
        }

        /*if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            capsuleCollider.height = 3f;
            capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y + 0.5f, 0f);
        }*/

        this.user.attackStuns.Remove(this.gameObject);
    }
}
