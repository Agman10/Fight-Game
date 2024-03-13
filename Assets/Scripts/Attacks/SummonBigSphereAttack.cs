using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBigSphereAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public TestHitbox bigSphere;

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
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
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
        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.2f);

        if (this.bigSphere != null && this.user.tempOpponent)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            TestHitbox bigSpherePrefab = this.bigSphere;
            bigSpherePrefab = Instantiate(bigSpherePrefab, new Vector3(summonPos, 11f, 0), Quaternion.Euler(0, 0, 0));
            bigSpherePrefab.belongsTo = this.user;
        }

        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
