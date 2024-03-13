using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject hitbox;
    public GameObject gun;
    public GameObject shootEffectParticle;
    public Transform shootEffectTransform;
    public Transform bulletVisual;

    public GameObject handHitbox;

    private Vector3 hitboxLocalPosition;

    public FireBall gunThrowProjectile;
    public Attack fireBallThrow;

    [Space]
    private int bullets = 17;
    private int maxBullets = 17;
    public GameObject magazineHole;
    public GameObject magazineTransform;
    public GameObject magazineRb;
    public GameObject handMagazine;
    public bool hasNoMagaznine = false;
    public bool reloading = false;
    /*public float gunThrowXForce;
    public float gunThrowYForce;*/

    public override void OnEnable()
    {
        base.OnEnable();
        this.hitboxLocalPosition = this.hitbox.transform.localPosition;
    }
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
                this.StartCoroutine(this.GunShootCoroutine(0.5f));
                //this.StartCoroutine(this.ThrowGunCorutine());
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.GunShootCoroutine(0f));
            }

            //this.user.AddStun(0.2f, true);
            //this.user.AddStun(0.2f, false);
            //this.StartCoroutine(this.GunShootCoroutine());
        }
    }

    private IEnumerator GunShootCoroutine(float time = 0.5f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.EnableGun(true);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (!this.gun.active)
        {
            if (this.animations != null)
                this.animations.SetStartThrowBombPose();
            yield return new WaitForSeconds(0.125f);
            this.EnableGun(true);

            if (this.animations != null)
                this.animations.SetDefaultPose();
            yield return new WaitForSeconds(0.125f);
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
        }

        if(this.user.input != null)
        {
            if(this.user.input.moveInput == new Vector3(/*this.user.transform.forward.z * -1f*/0f, -1f, 0f) && this.user.input.special)
            {
                this.StopAllCoroutines();
                this.user.attackStuns.Remove(this.gameObject);
                this.StartCoroutine(this.ThrowGunCorutine(0.5f, time));
                yield return new WaitForSeconds(0.1f);
            }
        }


        //DO RELOAD HERE
        if (this.bullets <= 0)
        {
            this.StopAllCoroutines();
            this.user.attackStuns.Remove(this.gameObject);

            if(this.hasNoMagaznine)
                this.StartCoroutine(this.ReloadGun2());
            else
                this.StartCoroutine(this.ReloadGun());
        }



        if (this.user.tempOpponent != null && this.hitbox != null)
        {
            if (this.user.tempOpponent.characterId == 3 || this.user.tempOpponent.characterId == 4)
                this.hitbox.transform.localEulerAngles = new Vector3(0f, 0f, -2f);
            else
                this.hitbox.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            this.hitbox.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        //yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(time / 2f);

        if (this.shootEffectParticle != null && this.shootEffectTransform != null)
        {
            GameObject shoootEffectPrefab = this.shootEffectParticle;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.Euler(this.shootEffectTransform.rotation.x, this.transform.forward.z * 90f, this.shootEffectTransform.rotation.z));


            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
        }

        if (this.hitbox != null)
            this.hitbox.SetActive(true);

        if (this.animations != null)
            this.animations.GunFiredPose();

        if (this.handHitbox != null)
            this.handHitbox.SetActive(true);

        this.user.AddKnockback(this.transform.forward.z * -50, 50f);

        this.bullets--;

        float currentTime = 0;
        float duration = 0.05f;
        Vector3 hitboxPos = this.hitbox.transform.position;
        float bulletvisualZpos = 5f * (-this.user.transform.forward.z);
        this.bulletVisual.transform.localPosition = new Vector3(0.5f, 0f, bulletvisualZpos);
        //Vector3 hitbox
        //this.hitbox.transform.position = this.hitbox.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.hitbox.transform.position = hitboxPos;
            
            yield return null;
        }

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        //yield return new WaitForSeconds(0.05f);

        this.hitbox.transform.localPosition = this.hitboxLocalPosition;

        if (this.hitbox != null)
            this.hitbox.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();


        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(time / 2f);
        //this.user.AddStun(0f, true);
        

        //this.EnableGun(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        if (this.transform.position.y == 0)
            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

        yield return new WaitForSeconds(0.2f);
        if (!this.onGoing)
            this.EnableGun(false);
        //this.onGoing = false;
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.SetActive(false);

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        this.EnableGun(false);
        this.hitbox.transform.localPosition = this.hitboxLocalPosition;

        if (this.magazineTransform != null)
        {
            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            this.magazineTransform.gameObject.SetActive(false);
        }

        if (this.handMagazine != null)
            this.handMagazine.gameObject.SetActive(false);

        if (this.reloading)
        {
            if (this.magazineRb != null && this.magazineTransform != null)
            {
                GameObject magazinePrefab = this.magazineRb;
                //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
                magazinePrefab = Instantiate(magazinePrefab, this.magazineTransform.transform.position, this.magazineTransform.transform.rotation);

                this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
            }
            this.reloading = false;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private void EnableGun(bool enable = false)
    {
        if (this.gun != null)
        {
            if (enable)
                this.gun.SetActive(true);
            else
                this.gun.SetActive(false);
        }
    }

    IEnumerator ThrowGunCorutine(float time = 0.5f, float stunTime = 0.5f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.EnableGun(true);

        if (this.animations != null)
        {
            this.animations.SetStartThrowFirePose();
        }


        yield return new WaitForSeconds(time);
        if (this.animations != null)
            this.animations.SetPunchPose();
        this.ThrowGun();
        this.EnableGun(false);

        if (this.user != null && this.user.attacks != null && this.fireBallThrow != null)
            this.user.attacks.forwardSpecial = this.fireBallThrow;

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(stunTime);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ReloadGun()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetPunchPose();

        yield return new WaitForSeconds(0.1f);

        if (this.magazineHole != null)
            this.magazineHole.gameObject.SetActive(true);

        if(this.magazineTransform != null)
        {
            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            this.magazineTransform.SetActive(true);
        }
        this.hasNoMagaznine = true;
        this.reloading = true;
        float currentTime = 0;
        float duration = 0.1f;
        float startPos = -0.1f;
        float endPos = -0.39f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.magazineTransform != null)
                this.magazineTransform.transform.localPosition = new Vector3(0f, Mathf.Lerp(startPos, endPos, currentTime / duration), 0f);
            yield return null;
        }

        if (this.magazineTransform != null)
        {
            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.39f, 0f);
            this.magazineTransform.gameObject.SetActive(false);
        }
            

        if (this.magazineRb != null && this.magazineTransform != null)
        {
            GameObject magazinePrefab = this.magazineRb;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            magazinePrefab = Instantiate(magazinePrefab, this.magazineTransform.transform.position, this.magazineTransform.transform.rotation);

            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
        }
        this.reloading = false;

        //yield return new WaitForSeconds(0.1f);
        if (this.handMagazine != null)
            this.handMagazine.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.GunReload();

        yield return new WaitForSeconds(0.2f);

        if (this.handMagazine != null)
            this.handMagazine.gameObject.SetActive(false);

        if (this.magazineTransform != null)
        {
            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            this.magazineTransform.gameObject.SetActive(false);
        }
        this.bullets = this.maxBullets;
        this.hasNoMagaznine = false;

        if (this.magazineHole != null)
            this.magazineHole.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        yield return new WaitForSeconds(0.2f);
        if (!this.onGoing)
            this.EnableGun(false);
    }

    private IEnumerator ReloadGun2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetPunchPose();

        yield return new WaitForSeconds(0.1f);


        if (this.handMagazine != null)
            this.handMagazine.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.GunReload();

        yield return new WaitForSeconds(0.2f);

        if (this.handMagazine != null)
            this.handMagazine.gameObject.SetActive(false);

        if (this.magazineTransform != null)
        {
            this.magazineTransform.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            this.magazineTransform.gameObject.SetActive(false);
        }
        this.bullets = this.maxBullets;
        this.hasNoMagaznine = false;

        if (this.magazineHole != null)
            this.magazineHole.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetPunchPose();

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        yield return new WaitForSeconds(0.2f);
        if (!this.onGoing)
            this.EnableGun(false);
    }

    public void ThrowGun()
    {
        if (this.gunThrowProjectile != null)
        {
            FireBall gunThrowPrefab = this.gunThrowProjectile;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            gunThrowPrefab = Instantiate(gunThrowPrefab, new Vector3(forward + this.user.transform.position.x, this.user.transform.position.y + 2, 0), this.user.transform.rotation);
            if (this.user != null)
                gunThrowPrefab.belongsTo = this.user;


            if (this.user.tempOpponent != null && this.hitbox != null)
            {
                if (this.user.tempOpponent.characterId == 3 || this.user.tempOpponent.characterId == 4)
                    gunThrowPrefab.KnockBack(new Vector3(forward * 700f, 250f, 0));
                else
                    gunThrowPrefab.KnockBack(new Vector3(forward * 700f, 350f, 0));
            }
            else
            {
                gunThrowPrefab.KnockBack(new Vector3(forward * 700f, 350f, 0));
            }
            //gunThrowPrefab.KnockBack(new Vector3(forward * 700f, 350f, 0));
        }
    }
}
