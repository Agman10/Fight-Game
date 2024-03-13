using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeVsJCapStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

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
        this.user.rb.isKinematic = true;

        this.user.transform.position = new Vector3(this.user.transform.position.x, 50f, 0f);

        this.user.LookAtTarget();

        yield return new WaitForSeconds(0.01f);
        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.TestPose5();


        float currentTime = 0;
        float duration = 0.9f;

        float targetPosition = 0f;
        float start = 16;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            

            if (this.animations != null)
            {
                this.animations.body.transform.localPosition = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.animations.body.transform.localPosition.y, 0);
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
            }
                

            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SpinEnd();

        

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
