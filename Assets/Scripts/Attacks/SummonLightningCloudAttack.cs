using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLightningCloudAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    //public GameObject startParticle;

    public StormCloud stormCloud;

    public GameObject confused;

    public GameObject glowingEyes;

    //public float cooldown = 5f;
    public float cooldownTimer;

    //public AudioSource lightningSfx;

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

    /*private void Update()
    {
        if (this.cooldownTimer > 0)
            this.cooldownTimer -= Time.deltaTime;
    }*/

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


            /*if (this.user != null && this.user.stuns.Count <= 0 && this.user.attackStuns.Count <= 0)
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                    {
                        this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.TemplateCoroutine());
                    }
                }
            }*/

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.SummonLightningCloudCoroutine());
                /*if (this.cooldownTimer <= 0f)
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.SummonLightningCloudCoroutine());
                }
                else
                {
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.FailSummonLightningCloudCoroutine());
                }*/
                
            }
        }
    }

    private IEnumerator SummonLightningCloudCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SummonLightningCloud(0);
        /*if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }*/

        yield return new WaitForSeconds(0.25f);
        /*if (this.animations != null)
            this.animations.SummonLightningCloud(1);*/

        float lightningPos = this.LightningPos(this.user.transform.position.x);

        /*float pos = this.user.transform.position.x;
        float lightningPos = pos + this.user.transform.forward.z * 6f;

        if (this.user.input != null && GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            float cameraPos = GameManager.Instance.gameCamera.transform.position.x;

            //Debug.Log(Mathf.Abs(pos - (cameraPos + 7)));
            //Debug.Log(pos + (((cameraPos + 7) - pos) / 2));

            if (this.user.transform.forward.z >= 0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = cameraPos + 7f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
                else if (this.user.input.moveInput.y < 0)
                    lightningPos = pos + this.user.transform.forward.z;
                else
                    lightningPos = pos + this.user.transform.forward.z * 7f;
            }
            else if (this.user.transform.forward.z <= -0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = cameraPos - 7f;
                else if (this.user.input.moveInput.y < 0)
                    lightningPos = pos + this.user.transform.forward.z;
                else
                    lightningPos = pos + this.user.transform.forward.z * 7f;
            }

            if (lightningPos > cameraPos + 7)
                lightningPos = cameraPos + 7f;
            else if (lightningPos < cameraPos - 7)
                lightningPos = cameraPos - 7f;
        }*/




        //this.cooldownTimer = 2f;
        if (this.stormCloud != null /*&& this.user.tempOpponent != null*/)
        {
            StormCloud stormCloudPrefab = this.stormCloud;
            //stormCloudPrefab = Instantiate(stormCloudPrefab, new Vector3(this.user.tempOpponent.transform.position.x, 6f, 0), Quaternion.Euler(0, 0, 0));
            stormCloudPrefab = Instantiate(stormCloudPrefab, new Vector3(lightningPos, 6f, 0), Quaternion.Euler(0, 0, 0));
            stormCloudPrefab.SetOwner(this.user);
        }
        yield return new WaitForSeconds(0.3f);
        if (this.animations != null)
            this.animations.SummonLightningCloud(1);

        yield return new WaitForSeconds(0.1f);

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        /*if (this.lightningSfx != null)
        {
            this.lightningSfx.Play();
        }*/

        yield return new WaitForSeconds(0.2f);
        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        //yield return new WaitForSeconds(0.4f);

        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator FailSummonLightningCloudCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SummonLightningCloud();
        /*if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }*/

        yield return new WaitForSeconds(0.2f);
        if (this.confused != null)
            this.confused.gameObject.SetActive(true);

        /*this.cooldownTimer = 3f;
        if (this.stormCloud != null && this.user.tempOpponent != null)
        {
            StormCloud stormCloudPrefab = this.stormCloud;
            stormCloudPrefab = Instantiate(stormCloudPrefab, new Vector3(this.user.tempOpponent.transform.position.x, 6f, 0), Quaternion.Euler(0, 0, 0));
            stormCloudPrefab.SetOwner(this.user);
        }*/

        yield return new WaitForSeconds(0.2f);
        if (this.confused != null)
            this.confused.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();
        if (this.confused != null)
            this.confused.gameObject.SetActive(false);

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public float LightningPos(float pos)
    {
        //float pos = this.user.transform.position.x;
        float lightningPos = pos + this.user.transform.forward.z * 6f;

        if(GameManager.Instance != null && GameManager.Instance.gameMode == 0)
        {
            if (this.user.input != null && GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            {
                float cameraPos = GameManager.Instance.gameCamera.transform.position.x;

                //Debug.Log(Mathf.Abs(pos - (cameraPos + 7)));
                //Debug.Log(pos + (((cameraPos + 7) - pos) / 2));

                if (this.user.transform.forward.z >= 0.5f)
                {
                    if (this.user.input.moveInput.x > 0)
                        lightningPos = cameraPos + 7f;
                    else if (this.user.input.moveInput.x < 0)
                        lightningPos = pos + this.user.transform.forward.z * 3f;
                    else if (this.user.input.moveInput.y < 0)
                        lightningPos = pos + this.user.transform.forward.z;
                    else
                        lightningPos = pos + this.user.transform.forward.z * 7f;
                }
                else if (this.user.transform.forward.z <= -0.5f)
                {
                    if (this.user.input.moveInput.x > 0)
                        lightningPos = pos + this.user.transform.forward.z * 3f;
                    else if (this.user.input.moveInput.x < 0)
                        lightningPos = cameraPos - 7f;
                    else if (this.user.input.moveInput.y < 0)
                        lightningPos = pos + this.user.transform.forward.z;
                    else
                        lightningPos = pos + this.user.transform.forward.z * 7f;
                }

                if (lightningPos > cameraPos + 7)
                    lightningPos = cameraPos + 7f;
                else if (lightningPos < cameraPos - 7)
                    lightningPos = cameraPos - 7f;
            }
        }
        else
        {
            if (this.user.input != null && GameManager.Instance != null)
            {
                //float cameraPos = GameManager.Instance.gameCamera.transform.position.x;

                //Debug.Log(Mathf.Abs(pos - (cameraPos + 7)));
                //Debug.Log(pos + (((cameraPos + 7) - pos) / 2));

                if (this.user.transform.forward.z >= 0.5f)
                {
                    if (this.user.input.moveInput.x > 0)
                        lightningPos = 10f;
                    else if (this.user.input.moveInput.x < 0)
                        lightningPos = pos + this.user.transform.forward.z * 4f;
                    else if (this.user.input.moveInput.y < 0)
                        lightningPos = pos + this.user.transform.forward.z;
                    else if (this.user.input.transform.position.x > -1f)
                        lightningPos = (10f + pos) / 2f; //pos + this.user.transform.forward.z;
                    else
                        lightningPos = 0f;
                }
                else if (this.user.transform.forward.z <= -0.5f)
                {
                    if (this.user.input.moveInput.x > 0)
                        lightningPos = pos + this.user.transform.forward.z * 4f;
                    else if (this.user.input.moveInput.x < 0)
                        lightningPos = -10f;
                    else if (this.user.input.moveInput.y < 0)
                        lightningPos = pos + this.user.transform.forward.z;
                    else if (this.user.input.transform.position.x < 1f)
                        lightningPos = (-10f + pos) / 2f; /*pos + this.user.transform.forward.z;*/
                    else
                        lightningPos = 0f;
                }

                if (lightningPos > 10f)
                    lightningPos = 10f;
                else if (lightningPos < -10f)
                    lightningPos = -10f;
            }
        }
        

        return lightningPos;
    }

    /*public void LightningPos()
    {
        float pos = this.user.transform.position.x;
        float lightningPos = pos + this.user.transform.forward.z * 6f;
        if (this.user.input != null)
        {
            if (this.user.transform.forward.z >= 0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = pos + this.user.transform.forward.z * 12f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
            }
            else if (this.user.transform.forward.z <= -0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = pos + this.user.transform.forward.z * 12f;
            }
        }
        if (this.user.input != null && GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            float cameraPos = GameManager.Instance.gameCamera.transform.position.x;

            if (this.user.transform.forward.z >= 0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = cameraPos + 7f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
                else
                    lightningPos = cameraPos;
            }
            else if (this.user.transform.forward.z <= -0.5f)
            {
                if (this.user.input.moveInput.x > 0)
                    lightningPos = pos + this.user.transform.forward.z * 3f;
                else if (this.user.input.moveInput.x < 0)
                    lightningPos = cameraPos + 7f;
                else
                    lightningPos = cameraPos;
            }
        }
    }*/
}
