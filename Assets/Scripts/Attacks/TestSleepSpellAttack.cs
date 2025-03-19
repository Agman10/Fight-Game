using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSleepSpellAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject wand;

    public SleepSpellProjectile sleepSpellProjectile;
    public float sleepTime = 2f;

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
            //this.user.AddStun(0.2f, true);
            this.user.AddStun(0.2f, false);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

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

        

        //this.animations.SetStartThrowFirePose();

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.rightArm.localEulerAngles = new Vector3(15f, 0f, 0f);
            this.animations.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.wand != null)
            this.wand.SetActive(true);

        //yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
        }
        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.rightArm.localEulerAngles = new Vector3(0f, -15f, -120f);
            //this.animations.rightArmJoint.localEulerAngles = new Vector3(0f, -32f, 0f);
        }

        yield return new WaitForSeconds(0.5f);

        /*if (this.animations != null)
            this.animations.SetPunchPose();*/

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, 37f);
            this.animations.rightArmJoint.localEulerAngles = new Vector3(0f, -32f, 0f);
        }
            

        if (this.sleepSpellProjectile != null)
        {
            SleepSpellProjectile sleepSpellPrefab = this.sleepSpellProjectile;

            //sleepSpellPrefab = Instantiate(sleepSpellPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.8f), this.user.transform.position.y + 2f, 0), this.user.transform.rotation);
            sleepSpellPrefab = Instantiate(sleepSpellPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.5f), this.user.transform.position.y + 2f, 0), this.user.transform.rotation);
            sleepSpellPrefab.belongsTo = this.user;
            sleepSpellPrefab.sleepTime = this.sleepTime;
        }

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.5f);

        if (this.wand != null)
            this.wand.SetActive(false);

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.rightArm.localEulerAngles = new Vector3(15f, 0f, 0f);
            this.animations.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f);

        /*if (this.wand != null)
            this.wand.SetActive(false);*/

        //yield return new WaitForSeconds(0.5f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        if (this.wand != null)
            this.wand.SetActive(false);
    }
}
