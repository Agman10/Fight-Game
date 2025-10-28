using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperPunchGhostAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public PunchGhost punchGhost;

    public PunchGhost currentPunchGhost;

    

    public VisualEffect aura;

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
            

            

            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
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
            this.animations.PunchGhostSummoning2();

        if (this.aura != null)
            this.aura.Play();

        //yield return new WaitForSeconds(0.2f);

        float forwardZ = this.user.transform.forward.z;
        float pos = this.user.transform.position.x;

        float maxX = 10.5f;

        /*if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
            maxX = 7.5f;*/

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.gameMode == 1)
                maxX = 7.5f;
            else if (GameManager.Instance.gameCamera != null)
                maxX = GameManager.Instance.gameCamera.maxX + 4f;
        }

        //Debug.Log(maxX);

        if (pos > maxX && forwardZ == 1 || pos < -maxX && forwardZ == -1)
        {
            float currentTime2 = 0;
            float duration2 = 0.15f;
            while (currentTime2 < duration2)
            {
                this.user.transform.position = new Vector3(Mathf.Lerp(this.user.transform.position.x, maxX * forwardZ, currentTime2 / duration2), this.user.transform.position.y, 0f);

                currentTime2 += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
        }

        this.SpawnPunchGhost();


        //yield return new WaitForSeconds(5f);

        float range = 19f;

        if(GameManager.Instance != null && GameManager.Instance.gameMode == 1)
            range = 24f;

        float maxGhostX = 18f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 0 && GameManager.Instance.gameCamera != null)
            maxGhostX = GameManager.Instance.gameCamera.maxX + 11.5f;

        //Debug.Log(maxGhostX);

        float currentTime = 0;
        float duration = 5f;
        while (currentTime < duration && this.currentPunchGhost != null && Mathf.Abs(this.user.transform.position.x - this.currentPunchGhost.transform.position.x) < range && Mathf.Abs(this.currentPunchGhost.transform.position.x) < maxGhostX)
        {
            //currentTime += Time.deltaTime;

            //Debug.Log("Time: " + currentTime + " Dist: " + Mathf.Abs(this.user.transform.position.x - this.currentPunchGhost.transform.position.x));

            currentTime += Time.deltaTime;
            yield return null;
        }

        this.RemovePunchGhost();

        if (this.aura != null)
            this.aura.Stop();

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void SpawnPunchGhost()
    {
        if(this.punchGhost != null && this.user != null)
        {
            PunchGhost punchGhostPrefab = this.punchGhost;
            punchGhostPrefab = Instantiate(punchGhostPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.1f), 0f, 0f), this.user.transform.rotation);
            punchGhostPrefab.SetOwner(this.user);
            this.currentPunchGhost = punchGhostPrefab;
        }
    }

    public void RemovePunchGhost()
    {
        if (this.currentPunchGhost != null)
        {
            //this.currentPunchGhost.Stop();

            if (this.currentPunchGhost.gameObject.active)
                this.currentPunchGhost.Stop();
            this.currentPunchGhost = null;
        }
    }

    public override void Stop()
    {
        base.Stop();

        if (this.aura != null)
            this.aura.Stop();

        if (this.currentPunchGhost != null)
        {
            if (this.currentPunchGhost.gameObject.active)
                this.currentPunchGhost.Stop();
            this.currentPunchGhost = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
