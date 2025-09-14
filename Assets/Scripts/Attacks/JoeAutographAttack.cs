using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeAutographAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;

    public GameObject paper;
    public GameObject pen;
    public Transform paperModelTransform;
    public Transform[] paperTransforms;

    public FireBall autographProjectile;


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

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.paper != null)
            this.paper.SetActive(true);

        this.ChangePaperPosition(0);

        if (this.pen != null)
            this.pen.SetActive(true);

        if (this.animations != null)
            this.animations.WritingAutograph(0);

        int amount = 16;
        int armId = 1;
        //bool idForward = true;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;

            yield return new WaitForSeconds(0.05f);
            if (this.animations != null)
                this.animations.WritingAutograph(armId);
            if (armId == 0)
                armId = 1;
            else
                armId = 0;

            amount -= 1;

            yield return null;
        }

        

        if (this.pen != null)
            this.pen.SetActive(false);



        if (this.animations != null)
            this.animations.ThrowAutoGraph(0);

        this.ChangePaperPosition(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.ThrowAutoGraph(1);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.ChangePaperPosition(2);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);


        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.ThrowAutoGraph(2);

        this.ChangePaperPosition(3);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.ThrowAutoGraph(3);

        this.ChangePaperPosition(4);

        yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.JokeSuperFireBall(2);

        this.ThrowAutograph();

        if (this.paper != null)
            this.paper.SetActive(false);

        this.ChangePaperPosition(0);

        yield return new WaitForSeconds(0.5f);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.paper != null)
            this.paper.SetActive(false);

        if (this.pen != null)
            this.pen.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.ChangePaperPosition(0);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ChangePaperPosition(int paperId = 0)
    {
        if(this.paperModelTransform != null)
        {
            this.paperModelTransform.localPosition = this.paperTransforms[paperId].localPosition;
            this.paperModelTransform.localEulerAngles = this.paperTransforms[paperId].localEulerAngles;
            this.paperModelTransform.localScale = new Vector3(1f, this.user.transform.forward.z, 1f);
        }
    }

    public void ThrowAutograph()
    {
        if (this.autographProjectile != null)
        {
            FireBall autographPrefab = this.autographProjectile;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            autographPrefab = Instantiate(autographPrefab, new Vector3(this.user.transform.position.x + (forward * 1.37f), this.user.transform.position.y + 2.1f, 0), this.user.transform.rotation);
            if (this.user != null)
                autographPrefab.belongsTo = this.user;

            autographPrefab.KnockBack(new Vector3(forward * 400f, 100f, 0));
        }
    }
}
