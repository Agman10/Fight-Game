using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolentMikeStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public ParticleSystem electricity;

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
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        this.user.LookAtTarget();

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(true);
        if (this.electricity != null)
            this.electricity.Play();

        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);

        this.user.transform.position = new Vector3(this.user.transform.position.x, 50f, 0f);



        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);

        yield return new WaitForSeconds(0.15f);
        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);


        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);


        if (this.animations != null)
            this.animations.ViolentBalletStartAnim(1);

        yield return new WaitForSeconds(0.1f);

        if (this.electricity != null)
            this.electricity.Stop();

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        /*if (!this.user.dead)
            this.user.rb.isKinematic = false;*/

        if (this.electricity != null)
            this.electricity.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
