using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public GameObject hitbox;
    public GameObject knife;
    public GameObject throwEffectParticle;

    public KnifeProjectile knifeProjectile;

    /*public GameObject gunTip;
    public GameObject gunTipLit;*/

    public GameObject handHitbox;

    /*public float shootTime = 0.65f;
    public float airShootTime = 0.25f;*/

    /*[Space]
    public GameObject shootEffectParticleP2;
    public LaserProjectile laserProjectileP2;
    public GameObject gunTipLitP2;*/
    //public Transform shootEffectTransform;
    //public Transform bulletVisual;

    //private Vector3 hitboxLocalPosition;

    /*public FireBall gunThrowProjectile;
    public Attack fireBallThrow;*/
    /*public float gunThrowXForce;
    public float gunThrowYForce;*/

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;
    }
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
    private void Update()
    {
        if (this.onGoing && this.user != null)
        {
            /*if (this.transform.position.y == 0)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                //this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);

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
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.KnifeThrowCoroutine());
                //this.StartCoroutine(this.ThrowGunCorutine());
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.KnifeThrowAirCoroutine());
            }

            //this.user.AddStun(0.2f, true);
            //this.user.AddStun(0.2f, false);
            //this.StartCoroutine(this.GunShootCoroutine());
        }
    }

    private IEnumerator KnifeThrowCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.EnableGun(true);
        if (this.animations != null)
            this.animations.KnifeThrowStart(false);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(-0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        this.EnableKnife(true);

        /*yield return new WaitForSeconds(0.1f);

        this.EnableKnife(true);
        yield return new WaitForSeconds(0.15f);*/

        yield return new WaitForSeconds(0.25f);

        if (this.handHitbox != null)
            this.handHitbox.SetActive(true);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }


        float currentTime = 0;
        float duration = 0.15f;
        //float targetRotation = -245f;
        float targetRotation = -250f;
        float startRotation = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(startRotation, targetRotation, currentTime / duration));
            yield return null;
        }

        if (this.animations != null)
            this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, -270f);


        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);


        int knifeAngle = 0;

        if (this.user.input.moveInput.y < 0f)
            knifeAngle = -1;
        else if (this.user.input.jumping)
            knifeAngle = 1;

        this.ThrowKnife(false, knifeAngle);

        this.EnableKnife(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.5f);
        //this.user.AddStun(0f, true);


        //this.EnableGun(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator KnifeThrowAirCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.EnableGun(true);
        if (this.animations != null)
            this.animations.KnifeThrowStart(true);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(-0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        this.EnableKnife(true);

        /*yield return new WaitForSeconds(0.1f);

        this.EnableKnife(true);
        yield return new WaitForSeconds(0.15f);*/

        /*this.user.AddStun(0.05f, true);
        this.user.AddKnockback(0f, 150f);*/
        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.25f);
        this.user.rb.isKinematic = false;

        if (this.handHitbox != null)
            this.handHitbox.SetActive(true);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }

        //this.user.rb.AddForce(0f, 300f, 0f);
        //this.user.AddStun(0.05f, true);
        this.user.AddKnockback(0f, 150f);
        

        float currentTime = 0;
        float duration = 0.15f;
        //float targetRotation = -245f;
        float targetRotation = -250f;
        float startRotation = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(startRotation, targetRotation, currentTime / duration));
            yield return null;
        }

        if (this.animations != null)
            this.animations.rightArm.localEulerAngles = new Vector3(0f, 0f, -270f);

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        int knifeAngle = 0;

        if (this.user.input.moveInput.y < 0f)
            knifeAngle = -1;
        else if (this.user.input.jumping)
            knifeAngle = 1;


        this.ThrowKnife(true, knifeAngle);

        this.EnableKnife(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.5f);
        //this.user.AddStun(0f, true);


        //this.EnableGun(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        

        this.user.rb.isKinematic = false;

        /*this.EnableKnife(false);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        //this.LightTip(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        this.EnableKnife(false);

        if (this.knife != null)
        {
            this.knife.transform.localPosition = new Vector3(0.15f, -0.64f, 0f);
            this.knife.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }
    }

    private void EnableKnife(bool enable = false)
    {
        if (this.knife != null)
        {
            if (enable)
                this.knife.SetActive(true);
            else
                this.knife.SetActive(false);
        }
    }

    public void ThrowKnife(bool inAir = false, int yAngle = 0)
    {
        if (this.knifeProjectile != null)
        {
            KnifeProjectile knifePrefab = this.knifeProjectile;

            float forward = this.transform.forward.z;

            //knifePrefab = Instantiate(knifePrefab, new Vector3(this.user.transform.position.x + (forward * 1.8f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);

            if (inAir)
            {
                float knifeRotatation = -30f;
                if (yAngle == -1)
                    knifeRotatation = -60f;
                else if (yAngle == 1)
                    knifeRotatation = 0f;

                knifePrefab = Instantiate(
                knifePrefab,
                new Vector3(this.user.transform.position.x + (forward * 1.7f), this.user.transform.position.y + 2.15f, -0.12f),
                Quaternion.Euler(0f, this.user.transform.eulerAngles.y, knifeRotatation));

                knifePrefab.speed = 20f;
            }
            else
            {
                float knifeRotatation = 0f;
                if (yAngle == -1)
                    knifeRotatation = -20f;
                else if (yAngle == 1)
                    knifeRotatation = 20f;

                knifePrefab = Instantiate(
                knifePrefab,
                new Vector3(this.user.transform.position.x + (forward * 1.7f), this.user.transform.position.y + 2f /*+ 2.15f*/, -0.12f),
                Quaternion.Euler(0f, this.user.transform.eulerAngles.y, knifeRotatation));

                knifePrefab.speed = 20f;
            }

            if (this.user != null)
                knifePrefab.SetOwner(this.user);

        }

        /*if (this.throwEffectParticle != null)
        {
            GameObject throwEffectPrefab = this.throwEffectParticle;

            float forward = this.transform.forward.z;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            throwEffectPrefab = Instantiate(throwEffectPrefab, new Vector3(this.user.transform.position.x + (forward * 1.9f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);


            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
        }*/
    }
}
