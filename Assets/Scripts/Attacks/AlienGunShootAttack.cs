using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGunShootAttack: Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public GameObject hitbox;
    public GameObject gun;
    public GameObject shootEffectParticle;

    public LaserProjectile laserProjectile;

    public GameObject gunTip;
    public GameObject gunTipLit;

    public GameObject handHitbox;

    public float shootTime = 0.65f;
    public float airShootTime = 0.25f;

    public AudioSource shootSfx;

    [Space]
    public GameObject shootEffectParticleP2;
    public LaserProjectile laserProjectileP2;
    public GameObject gunTipLitP2;
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
                this.StartCoroutine(this.GunShootCoroutine(this.shootTime));
                //this.StartCoroutine(this.ThrowGunCorutine());
            }
            else
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.GunShootCoroutine(this.airShootTime));
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

        /*if (this.user.input != null)
        {
            if (this.user.input.moveInput == new Vector3(*//*this.user.transform.forward.z * -1f*//*0f, -1f, 0f) && this.user.input.special)
            {
                this.StopAllCoroutines();
                this.user.attackStuns.Remove(this.gameObject);
                this.StartCoroutine(this.ThrowGunCorutine(0.5f, time));
                yield return new WaitForSeconds(0.1f);
            }
        }*/

        //yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.AlienGunShoot(0);

        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(time / 2f);

        /*if (this.shootEffectParticle != null)
        {
            GameObject shoootEffectPrefab = this.shootEffectParticle;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.Euler(this.shootEffectTransform.rotation.x, this.transform.forward.z * 90f, this.shootEffectTransform.rotation.z));


            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
        }*/

        /*if (this.hitbox != null)
            this.hitbox.SetActive(true);*/

        this.LightTip(true);
        //yield return new WaitForSeconds(0.1f);

        this.ShootLaser();

        if (this.animations != null)
            this.animations.AlienGunShoot(1);

        if (this.handHitbox != null)
            this.handHitbox.SetActive(true);

        //this.user.AddKnockback(this.transform.forward.z * -50, 50f);

        //yield return new WaitForSeconds(0.05f);


        /*if (this.hitbox != null)
            this.hitbox.SetActive(false);*/

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.AlienGunShoot(0);

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        yield return new WaitForSeconds(0.125f);

        this.LightTip(false);

        yield return new WaitForSeconds(0.125f);

        //yield return new WaitForSeconds(0.25f);

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

        if (this.handHitbox != null)
            this.handHitbox.SetActive(false);

        this.EnableGun(false);

        this.LightTip(false);

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

    public void ShootLaser()
    {
        if (this.laserProjectile != null)
        {
            LaserProjectile laserPrefab = this.laserProjectile;

            if(this.laserProjectileP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                laserPrefab = this.laserProjectileP2;

            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            //laserPrefab = Instantiate(laserPrefab, new Vector3(this.user.transform.position.x + (forward * 2.35f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);

            //laserPrefab = Instantiate(laserPrefab, new Vector3(this.user.transform.position.x + (forward * 1.35f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);
            laserPrefab = Instantiate(laserPrefab, new Vector3(this.user.transform.position.x + (forward * 1.4f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);
            if (this.user != null)
                laserPrefab.SetOwner(this.user);

            /*if (this.user != null)
                fireBallPrefab.belongsTo = this.user;

            fireBallPrefab.KnockBack(new Vector3(forward * this.fireBallXForce, this.fireBallYForce, 0));*/
        }

        if (this.shootEffectParticle != null)
        {
            GameObject shoootEffectPrefab = this.shootEffectParticle;

            if (this.shootEffectParticleP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                shoootEffectPrefab = this.shootEffectParticleP2;

            float forward = this.transform.forward.z;
            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, this.shootEffectTransform.rotation);
            shoootEffectPrefab = Instantiate(shoootEffectPrefab, new Vector3(this.user.transform.position.x + (forward * 1.9f), this.user.transform.position.y + 1.83f, -0.12f), this.user.transform.rotation);


            //shoootEffectPrefab = Instantiate(shoootEffectPrefab, this.shootEffectTransform.position, Quaternion.EulerAngles(this.shootEffectTransform.rotation.x, this.shootEffectTransform.rotation.y, this.shootEffectTransform.rotation.z));
        }

        if(this.shootSfx != null)
        {
            this.shootSfx.Play();
        }
    }

    public void LightTip(bool glow = false)
    {
        if(this.gunTip != null && this.gunTipLit != null)
        {
            if (glow)
            {
                if (this.gunTipLitP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                    this.gunTipLitP2.SetActive(true);
                else
                    this.gunTipLit.SetActive(true);

                this.gunTip.SetActive(false);
            }
            else
            {
                this.gunTip.SetActive(true);
                this.gunTipLit.SetActive(false);
                if (this.gunTipLitP2 != null)
                    this.gunTipLitP2.SetActive(false);
            }
        }
    }

    /*IEnumerator ThrowGunCorutine(float time = 0.5f, float stunTime = 0.5f)
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
    }*/

    /*public void ThrowGun()
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
    }*/
}
