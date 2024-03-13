using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAnvilAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public Anvil anvil;

    public float cooldownTimer;

    public GameObject confused;
    public GameObject rope;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    private void Update()
    {
        if (this.cooldownTimer > 0)
            this.cooldownTimer -= Time.deltaTime;

        if (this.onGoing && this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {


            /*if (this.user != null && this.user.stuns.Count <= 0 && this.user.attackStuns.Count <= 0)
            {
                
            }*/
            if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    if (this.cooldownTimer <= 0f)
                    {
                        this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.SummonAnvilCoroutine());
                    }
                    else
                    {
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.FailSummonAnvilCoroutine());
                    }
                    
                }
            }
        }
    }

    private IEnumerator SummonAnvilCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.GrandFlameStart();
        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.3f);

        //this.cooldownTimer = 3f;
        this.cooldownTimer = 1f;

        if (this.anvil != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            Anvil anvilPrefab = this.anvil;
            anvilPrefab = Instantiate(anvilPrefab, new Vector3(summonPos, 11f, 0), Quaternion.Euler(0, 0, 0));
            anvilPrefab.SetOwner(this.user);
            //bigSpherePrefab.belongsTo = this.user;
        }

        yield return new WaitForSeconds(0.45f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator FailSummonAnvilCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.GrandFlameStart();

        yield return new WaitForSeconds(0.2f);
        if (this.confused != null)
            this.confused.gameObject.SetActive(true);

        //this.cooldownTimer = 3f;

        yield return new WaitForSeconds(0.2f);
        if (this.confused != null)
            this.confused.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator SummonAnvilCoroutine2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.BallerPullRope(0);

        if (this.rope != null)
            this.rope.SetActive(true);

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.rope.transform.localScale = new Vector3(1f, Mathf.Lerp(0.1f, 1f, currentTime / duration), 1f);

            yield return null;
        }


        if (this.rope != null)
            this.rope.transform.localPosition = new Vector3(this.rope.transform.localPosition.x, 9.5f, this.rope.transform.localPosition.z);

        





        /*if (this.animations != null)
            this.animations.GrandFlameStart();*/

        

        yield return new WaitForSeconds(0.2f);
        if (this.animations != null)
            this.animations.BallerPullRope(1);

        if (this.rope != null)
            this.rope.transform.localPosition = new Vector3(this.rope.transform.localPosition.x, 9.25f, this.rope.transform.localPosition.z);

        this.cooldownTimer = 3f;

        if (this.anvil != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            Anvil anvilPrefab = this.anvil;
            anvilPrefab = Instantiate(anvilPrefab, new Vector3(summonPos, 11f, 0), Quaternion.Euler(0, 0, 0));
            anvilPrefab.SetOwner(this.user);
            //bigSpherePrefab.belongsTo = this.user;
        }

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.BallerPullRope(0);

        if (this.rope != null)
            this.rope.transform.localPosition = new Vector3(this.rope.transform.localPosition.x, 9.5f, this.rope.transform.localPosition.z);

        yield return new WaitForSeconds(0.3f);

        if (this.rope != null)
            this.rope.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();

        if (this.rope != null)
            this.rope.SetActive(false);

        if (this.confused != null)
            this.confused.gameObject.SetActive(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
