using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSelfAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject gun;
    public GameObject shootEffectParticle;
    public GameObject bloodEffect;
    public Transform shootEffectTransform;

    public GameObject gunModelRb;

    public bool canBeCanceled;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && this.canBeCanceled)
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
        this.canBeCanceled = true;

        if (this.animations != null)
            this.animations.ShootSelf();

        this.EnableGun(true);

        yield return new WaitForSeconds(0.2f);
        this.EnableGun(true);
        yield return new WaitForSeconds(0.3f);
        this.user.knockbackInvounrability = true;
        this.canBeCanceled = false;
        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        if (this.shootEffectParticle != null && this.shootEffectTransform != null)
        {
            GameObject shoootEffectPrefab = this.shootEffectParticle;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.Euler(this.shootEffectTransform.rotation.x, this.transform.forward.z * 90f, this.shootEffectTransform.rotation.z));
        }

        if (this.bloodEffect != null)
        {
            GameObject bloodEffectPrefab = this.bloodEffect;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            bloodEffectPrefab = Instantiate(bloodEffectPrefab, new Vector3(this.user.transform.position.x + (this.transform.forward.z * 0.4f), this.user.transform.position.y + 2.5f, 0f), Quaternion.Euler(0f, this.transform.forward.z * 90f/*this.user.transform.rotation.y*/, 0f));
        }

        yield return new WaitForSeconds(0.05f);

        this.user.Die(new Vector3(this.user.transform.position.x + (this.transform.forward.z * -5), this.user.transform.position.y + 2f, 0f));
        //this.user.TakeDamage(new Vector3(this.user.transform.position.x + (this.transform.forward.z * -5), this.user.transform.position.y + 2f, 0f), 500);
        //this.user.TakeDamage(new Vector3(this.user.transform.position.x + (this.transform.forward.z * -4), this.user.transform.position.y + 1f, 0f), 500);

        this.EnableGun(false);

        if(this.gunModelRb != null)
        {
            GameObject gunModelPrefab = this.gunModelRb;
            //gunModelPrefab = Instantiate(gunModelPrefab, this.gun.transform.position, this.gun.transform.rotation);
            gunModelPrefab = Instantiate(gunModelPrefab, new Vector3(this.user.transform.position.x + (this.transform.forward.z * -0.97f), this.user.transform.position.y + 2.28f, -0.35f), this.user.transform.rotation/*Quaternion.Euler(0f, this.user.transform.rotation.y, 0f)*/);
        }
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private void EnableGun(bool enable = false)
    {
        if (this.gun != null)
        {
            if (enable)
            {
                this.gun.transform.localEulerAngles = new Vector3(170f, -0.5f, 90f);
                this.gun.SetActive(true);
            }
            else
            {
                this.gun.transform.localEulerAngles = new Vector3(90f, 0f, -90f);
                this.gun.SetActive(false);
            }
                
        }
    }
    public override void Stop()
    {
        base.Stop();

        this.EnableGun(false);
        this.canBeCanceled = false;
        this.user.knockbackInvounrability = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
