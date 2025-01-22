using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLaserHumpAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public GameObject laser;

    public Transform hitboxOrigin;

    public CharacterSoundEffect laserSfx;

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

            if(this.hitboxOrigin != null && this.user != null && this.user.tempOpponent != null)
            {
                this.hitboxOrigin.position = new Vector3(this.user.tempOpponent.transform.position.x - (this.transform.forward.z * 1.25f), this.user.tempOpponent.transform.position.y + 0.5f, 0f);
            }
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
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
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

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.animations != null)
            this.animations.StupidDance(1);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.StupidDance(0);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.StupidDance(1);

        this.laserSfx.PlaySound();

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.StupidDance(3);

        //this.laserSfx.PlaySound();

        /*if (this.objectToScale != null)
            this.objectToScale.gameObject.SetActive(true);*/

        if (this.laser != null)
        {
            this.laser.SetActive(true);
            this.laser.transform.localScale = new Vector3(1f, 0.01f, 0.01f);

            float currentTime = 0;
            float duration = 0.1f;
            //float targetVolume = 0.1f;
            /*float targetPosition = 3.5f;
            float start = this.transform.position.y;*/
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                this.laser.transform.localScale = new Vector3(1f, Mathf.Lerp(0.01f, 1f, currentTime / duration), Mathf.Lerp(0.01f, 1f, currentTime / duration));
                yield return null;
            }
        }

        //yield return new WaitForSeconds(0.45f);
        yield return new WaitForSeconds(0.35f);

        /*if (this.objectToScale != null)
            this.objectToScale.ScaleDown2(0.05f, true);*/

        if (this.laser != null)
        {
            //this.laser.transform.localScale = new Vector3(1f, 0.01f, 0.01f);

            float currentTime = 0;
            float duration = 0.05f;
            //float targetVolume = 0.1f;
            /*float targetPosition = 3.5f;
            float start = this.transform.position.y;*/
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                this.laser.transform.localScale = new Vector3(1f, Mathf.Lerp(1f, 0.01f, currentTime / duration), Mathf.Lerp(1f, 0.01f, currentTime / duration));
                yield return null;
            }

            this.laser.SetActive(false);
            this.laser.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.05f);


        if (this.animations != null)
            this.animations.StupidDance(1);



        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (this.laser != null)
        {
            this.laser.SetActive(false);
            this.laser.transform.localScale = new Vector3(1f, 1f, 1f);
        }
            

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
