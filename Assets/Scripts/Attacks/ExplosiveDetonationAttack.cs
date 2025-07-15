using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosiveDetonationAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject explosiveBox;

    public Explosion explosion;
    public Transform handle;

    public VisualEffect smoke;

    public AudioSource explosionSfx;

    [Space]

    public bool buttAttacking;
    public TestHitbox buttHitbox;
    public ParticleSystem buttExplosiveVfx;
    public AudioSource buttExplosionSfx;
    public AudioSource buttLaunchSfx;

    public AnimationCurve buttLaunchVelocity;
    public GameObject buttExplosionPrefab;


    //public ParticleSystem particle;

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

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;

        if (this.buttHitbox != null)
            this.buttHitbox.OnPlayerCollision += this.ButtHit;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;

        if (this.buttHitbox != null)
            this.buttHitbox.OnPlayerCollision -= this.ButtHit;
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
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.ExplosiveDetonationCoroutine());
            }
            else
            {
                this.user.AddStun(0.2f, true);
                //this.StartCoroutine(this.TestButtAttack2());
                this.StartCoroutine(this.ButtAttack());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator ExplosiveDetonationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.ExplosiveBox(0);

        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(true);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);

        yield return new WaitForSeconds(0.35f);
        if (this.animations != null)
            this.animations.ExplosiveBox(1);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.17f, 0f);


        yield return new WaitForSeconds(0.1f);
        if (this.explosion != null)
        {
            Explosion explosionPrefab = this.explosion;

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0), Quaternion.Euler(0, 0, 0));

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0), Quaternion.Euler(0, 0, 0));
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, 0), Quaternion.Euler(0, 0, 0));

            explosionPrefab.SetOwner(this.user);

            explosionPrefab.SetDamage(15f, 5f, 500f, 800f, 200f, 300f, 5f, 3f, 0.5f, 0.2f, true, false);

            //explosionPrefab.SetSize(1.5f);

            //explosionPrefab.SetSize(1.6f);
            explosionPrefab.SetSize(1.55f);

            if (this.explosionSfx != null)
            {
                //this.explosionSfx.time = 0.01f;
                this.explosionSfx.Play();
            }
                
        }

        if (this.smoke != null)
            this.smoke.Play();
        //this.user.TakeDamage(this.user.transform.position, 5);

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.ExplosiveBox(1, true);

        this.user.TakeDamage(this.user.transform.position, 5);

        //yield return new WaitForSeconds(0.7f);


        yield return new WaitForSeconds(0.4f);

        if (this.smoke != null)
            this.smoke.Stop();

        yield return new WaitForSeconds(0.3f);



        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(false);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);

        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        //yield return new WaitForSeconds(0.3f);

        float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();

        /*if (this.particle != null)
            this.particle.Stop();*/

        this.user.rb.isKinematic = false;

        /*if (this.explosiveBox != null)
            this.explosiveBox.SetActive(false);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        if (this.smoke != null)
            this.smoke.Stop();

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(false);

        this.buttAttacking = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(false);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);
    }





    

    private IEnumerator ButtAttack()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        
        //this.user.rb.isKinematic = true;

        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        float yVelocity = 0.5f;

        if (this.animations != null)
            this.animations.HoodGuyButtAttack();

        this.user.rb.AddForce(this.user.transform.forward.z * -50f, 300f, 0f);

        yield return new WaitForSeconds(0.2f);

        this.buttAttacking = true;

        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(true);

        if (this.buttLaunchSfx != null)
        {
            this.buttLaunchSfx.time = 0.06f;
            this.buttLaunchSfx.Play();
        }

        float currentTime = 0;
        //float duration = 0.6f;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.rb.velocity = new Vector3((this.transform.forward.z * (this.buttLaunchVelocity.Evaluate(currentTime / duration))) * Time.deltaTime, yVelocity, 0f);

            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        this.user.rb.AddForce(this.user.transform.forward.z * 50f, -300f, 0f);
        //this.user.rb.AddForce(0f, -300f, 0f);

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.HoodGuyButtAttack(1);

        this.buttAttacking = false;

        yield return new WaitForSeconds(0.1f);

        /*if (this.animations != null)
            this.animations.HoodGuyButtAttackMiss();*/


        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        float waitTime = 1f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if (waitTime > 0f)
        {
            if (this.animations != null)
                this.animations.HoodGuyButtAttackMissLand();
        }

        this.user.rb.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 155f, 0f);
        yield return new WaitForSeconds(0.05f);*/
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.05f);

        this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ButtHit(TestPlayer player)
    {
        this.StopAllCoroutines();
        this.user.rb.isKinematic = true;

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(false);

        /*if (this.buttExplosiveVfx != null)
            this.buttExplosiveVfx.Play();

        if (this.buttExplosionSfx != null)
        {
            this.buttExplosionSfx.Play();
        }*/

        bool countered = false;

        if (player != null && this.onGoing && this.buttAttacking /*&& !player.countering*/)
        {
            if(!player.countering)
            {

                if (/*player.attacks.neutralSpecial2 != null &&
                    player.attacks.neutralSpecial2 is ExplosiveDetonationAttack rollattack &&*/
                    player.attacks.backwardSpecial2 != null &&
                    player.attacks.backwardSpecial2 is ExplosiveDetonationAttack rollattack &&
                    rollattack.onGoing &&
                    rollattack.buttAttacking)
                {
                    player.TakeDamage(this.user.transform.position, 5, 0.3f);
                    this.user.TakeDamage(this.user.transform.position, 5, 0.3f);

                    this.user.GiveSuperCharge(2.5f);
                    player.GiveSuperCharge(2.5f);
                    /*this.user.GiveSuperCharge(this.superCharge);
                    this.user.GiveSuperCharge(this.superCharge / 2);
                    player.GiveSuperCharge(this.superCharge);
                    player.GiveSuperCharge(this.superCharge / 2);*/

                    /*if (!player.dead)
                        rollattack.RollBack();*/

                    //Debug.Log("test " + this.user.playerNumber);

                    if (player.soundEffects != null)
                        player.soundEffects.PlayHitSound();

                    if (this.user.soundEffects != null)
                        this.user.soundEffects.PlayHitSound();

                    
                    float distance = Mathf.Abs(this.user.transform.position.x - player.transform.position.x);
                    
                    //Debug.Log(distance);
                    
                    if(distance < 1.5f && !player.dead)
                    {
                        float difference = Mathf.Abs(1.5f - distance);
                        float halfDifference = difference / 2f;
                        Debug.Log(difference);


                        this.user.transform.position = new Vector3(this.user.transform.position.x - (halfDifference * this.user.transform.forward.z), this.user.transform.position.y, 0f);
                        player.transform.position = new Vector3(player.transform.position.x - (halfDifference * player.transform.forward.z), player.transform.position.y, 0f);

                        
                    }

                    distance = Mathf.Abs(this.user.transform.position.x - player.transform.position.x);
                    //Debug.Log(distance);

                    float midPoint = this.user.transform.position.x + (player.transform.position.x - this.user.transform.position.x) / 2f;
                    //Debug.Log(midPoint);


                    if (this.buttExplosionPrefab != null)
                    {
                        GameObject explosionPrefab = this.buttExplosionPrefab;
                        explosionPrefab = Instantiate(explosionPrefab, new Vector3(midPoint, this.user.transform.position.y + 1.6f, 0), Quaternion.Euler(0, 0, 0));
                        explosionPrefab.transform.localScale = Vector3.one;
                    }
                    if (this.buttExplosionSfx != null)
                    {
                        this.buttExplosionSfx.Play();
                    }


                    if (!player.dead)
                        rollattack.RollBack();
                }
                else
                {
                    player.OnHit?.Invoke();
                    player.OnHitFromPlayer?.Invoke(this.user);
                    player.TakeDamage(this.user.transform.position, 9f, 0.5f, this.transform.forward.z * 300f, 800f, true, true, false, false, true, false, false, true, 0.2f, 0.2f);
                    this.user.GiveSuperCharge(4f);
                    player.GiveSuperCharge(4f / 2f);
                    if (player.soundEffects != null)
                        player.soundEffects.PlayHitSound();

                    if (this.buttExplosiveVfx != null)
                        this.buttExplosiveVfx.Play();

                    if (this.buttExplosionSfx != null)
                    {
                        this.buttExplosionSfx.Play();
                    }
                }
            }
            else
            {
                player.OnHitFromPlayer?.Invoke(this.user);
                countered = true;
            }

            /*player.TakeDamage(this.user.transform.position, 8f, 0.5f, 300f, 800f, true, true, false, false, true, false, false, true, 0.2f, 0.2f);
            this.user.GiveSuperCharge(4f);
            player.GiveSuperCharge(4f / 2f);
            if (player.soundEffects != null)
                player.soundEffects.PlayHitSound();*/


            
        }

        this.buttAttacking = false;


        if (!countered && !this.user.dead)
            this.StartCoroutine(this.ButtHitCoroutine(player));
    }

    public void RollBack()
    {
        this.user.rb.isKinematic = true;
        this.StopAllCoroutines();
        this.buttAttacking = false;

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(false);

        this.StartCoroutine(this.ButtHitCoroutine(null));
    }

    private IEnumerator ButtHitCoroutine(TestPlayer player)
    {
        yield return new WaitForSeconds(0.1f);

        this.user.rb.isKinematic = false;
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        this.user.rb.AddForce(this.user.transform.forward.z * -300f, 600f, 0f);

        if (this.animations != null)
            this.animations.HoodGuyButtAttackHit();

        float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        //float targetRotation = this.user.transform.forward.z * 30f;
        float targetRotation = 0f;
        //float targetRotation = 360f;
        float startRotation = this.animations.body.localEulerAngles.y;

        if (this.user.transform.forward.z < 1f)
            targetRotation = 360f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, Mathf.Lerp(startRotation, targetRotation, currentTime / duration), this.animations.body.localEulerAngles.z);
            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        float waitTime = 1f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        /*if (this.animations != null)
            this.animations.RoadRollerEndLand();

        yield return new WaitForSeconds(0.1f);*/

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //Debug.Log("test");

        yield return new WaitForSeconds(0.2f);

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    /*private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
        }
    }*/








    private IEnumerator TestButtAttack2()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        float yVelocity = 0.5f;

        if (this.animations != null)
            this.animations.HoodGuyButtAttack();

        this.user.rb.AddForce(this.user.transform.forward.z * -50f, 300f, 0f);

        yield return new WaitForSeconds(0.2f);

        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(true);

        float maxSpeed = 1500f;

        float currentTime = 0;
        float duration = 0.05f;
        float targetVelocity = maxSpeed;
        float startVelocity = 500f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f/*100f * Time.deltaTime*/, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, yVelocity, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * maxSpeed * Time.deltaTime, 0f, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * maxSpeed * Time.deltaTime, yVelocity, 0f);

            yield return null;
        }

        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        currentTime = 0;
        duration = 0.15f;
        targetVelocity = 500f;
        startVelocity = maxSpeed;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;


            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, 0f, 0f);
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * Mathf.Lerp(startVelocity, targetVelocity, currentTime / duration) * Time.deltaTime, yVelocity, 0f);
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);

            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);
        this.user.rb.velocity = new Vector3(0f, 0f, 0f);

        this.user.rb.AddForce(this.user.transform.forward.z * 50f, -300f, 0f);
        //this.user.rb.AddForce(0f, -300f, 0f);

        if (this.buttHitbox != null)
            this.buttHitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        /*if (this.animations != null)
            this.animations.HoodGuyButtAttackMiss();*/


        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        float waitTime = 1f;
        while (Mathf.Abs(this.user.rb.velocity.y) > 0f && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if (this.animations != null)
            this.animations.HoodGuyButtAttackMissLand();

        this.user.rb.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(0.3f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 155f, 0f);
        yield return new WaitForSeconds(0.05f);*/
        this.animations.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.05f);

        this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
