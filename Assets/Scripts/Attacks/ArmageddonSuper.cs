using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmageddonSuper : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public GameObject armageddonBody;

    public ArmageddonMeteorSpawner armageddonMeteorSpawner;
    public ArmageddonMeteorSpawner currentArmageddonMeteorSpawner;

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

    public override void OnReset()
    {
        if (this.currentArmageddonMeteorSpawner != null)
        {
            //this.currentGrandFlame.Stop();
            this.currentArmageddonMeteorSpawner.gameObject.SetActive(false);
            this.currentArmageddonMeteorSpawner = null;
        }
        base.OnReset();
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
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge /** 0.5f*/)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge /** 0.5f*/);
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

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.user.soundEffects != null)
        {
            this.user.soundEffects.PlaySuperSfx();
        }

        if (this.animations != null)
            this.animations.Armageddon(0);

        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.Armageddon(1);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.Armageddon(2);

        yield return new WaitForSeconds(0.05f);

        /*if (this.animations != null)
            this.animations.Armageddon(1);

        yield return new WaitForSeconds(0.1f);*/

        this.Disappear(true);

        float maxXPos = 9f;

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.gameMode == 1)
                maxXPos = 4f;
            else if (GameManager.Instance.gameCamera != null)
                maxXPos = GameManager.Instance.gameCamera.maxX + 2.5f;
        }

        if (this.armageddonMeteorSpawner != null)
        {
            ArmageddonMeteorSpawner armageddonMeteorSpawnerPrefab = this.armageddonMeteorSpawner;
            //armageddonMeteorPrefab = Instantiate(armageddonMeteorPrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, 10f, 0), this.transform.rotation);

            float xPos = this.user.transform.position.x;

            if (this.user.transform.position.x > maxXPos && this.user.transform.forward.z == 1)
                xPos = maxXPos;
            else if (this.user.transform.position.x < -maxXPos && this.user.transform.forward.z == -1)
                xPos = -maxXPos;

            //Debug.Log(xPos);

            //armageddonMeteorSpawnerPrefab = Instantiate(armageddonMeteorSpawnerPrefab, new Vector3(this.user.transform.position.x, 0f, 0), this.transform.rotation);
            armageddonMeteorSpawnerPrefab = Instantiate(armageddonMeteorSpawnerPrefab, new Vector3(xPos, 0f, 0), this.transform.rotation);
            if (this.user != null)
                armageddonMeteorSpawnerPrefab.SetOwner(this.user);

            this.currentArmageddonMeteorSpawner = armageddonMeteorSpawnerPrefab;
        }


        yield return new WaitForSeconds(2.5f);

        this.Disappear(false);

        if (this.animations != null)
            this.animations.Armageddon(3);

        yield return new WaitForSeconds(0.1f);

        this.user.rb.isKinematic = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public void Disappear(bool disappear = true)
    {
        /*if (this.user.rb != null)
            this.user.rb.isKinematic = disappear;*/

        /*if (this.animations != null)
            this.animations.body.gameObject.SetActive(!disappear);*/

        if (this.armageddonBody != null)
            this.armageddonBody.SetActive(disappear);

        if (disappear)
        {
            if (this.animations != null)
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);
        }
        else
        {
            if (this.animations != null && this.user != null)
                this.animations.body.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);
        }

        
    }

    public override void Stop()
    {
        base.Stop();

        this.Disappear(false);

        this.user.rb.isKinematic = false;

        if (this.currentArmageddonMeteorSpawner != null)
        {
            this.currentArmageddonMeteorSpawner.StopMeteorSpawner();
            this.currentArmageddonMeteorSpawner = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
