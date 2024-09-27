using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperScissorKick : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;
    public TestHitbox hitbox2;
    public TestHitbox hitbox3;

    public GameObject startParticle;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();

            /*this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());*/

            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
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
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.ScissorKickCoroutine());
                }
            }

            
        }
    }

    private IEnumerator ScissorKickCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float animSpeed = 0.05f;

        /*if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);*/

        if (this.animations != null)
            this.animations.ScissorKick(0);

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.4f), this.user.transform.position.y + 2.1f, -1.25f), Quaternion.Euler(0, 0, 0));
        }

        if (this.user.soundEffects != null)
        {
            this.user.soundEffects.PlaySuperSfx();
        }

        yield return new WaitForSeconds(0.2f);


        if (this.animations != null)
            this.animations.ScissorKick(1);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        /*if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(1);*/

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(2);

        yield return new WaitForSeconds(animSpeed);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(3);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 4f, this.user.rb.velocity.y, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ScissorKick(0);


        //2



        /*yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.ScissorKick(0);*/

        //this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.ScissorKick(1);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);
        if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        /*if (this.user.rb != null)
            this.user.rb.AddForce(this.user.transform.forward.z * 1000f, 0f, 0f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(1);*/

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(2);

        yield return new WaitForSeconds(animSpeed);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.ScissorKick(3);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(4);

        yield return new WaitForSeconds(animSpeed);

        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 4f, this.user.rb.velocity.y, 0);

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ScissorKick(0);

        yield return new WaitForSeconds(animSpeed);

        if (this.animations != null)
            this.animations.ScissorKick(5);

        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(true);

        float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 15, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.ScissorKick(0);

        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 6f, this.user.rb.velocity.y, 0);

        yield return new WaitForSeconds(0.15f);



        if (this.user.rb != null)
            this.user.rb.velocity = new Vector3(0, this.user.rb.velocity.y, 0);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);



        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        if (this.hitbox3 != null)
            this.hitbox3.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }
}
