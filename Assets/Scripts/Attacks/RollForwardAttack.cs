using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollForwardAttack : Attack
{
    public TempPlayerAnimations animations;
    public TestHitbox hitbox;
    public bool onGoing;
    public bool rolling;
    public bool rollingBack;

    public float damage = 13f;
    public float superCharge = 8f;

    public AudioSource rollSfx;

    [Space]
    public float startDelay = 0.2f;
    public float rollDuration = 0.75f;
    public float rollSpeed = 1000f;
    public float missEndLag = 0.5f;
    public float hitEndLag = 0.2f;

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.RollBack;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.RollBack;
    }
    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            //this.Hit();

            this.Stop();
            //this.StartCoroutine(this.HitCoroutine());
            //this.RollBack(this.user);
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


        if (this.animations != null)
            this.animations.RollAnimation();
        /*float currentTime = 0;
        float duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, *//*this.transform.forward.z * *//*-1000f * Time.deltaTime));

            yield return null;
        }*/

        yield return new WaitForSeconds(this.startDelay);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.RollAnimation();

        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            capsuleCollider.radius = 0.65f;
            capsuleCollider.height = 0.65f;

            capsuleCollider.center = new Vector3(0f, 1.65f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
        }
        //this.user.transform.position = new Vector3(this.transform.position.x, 0f, 0f);

        this.rolling = true;

        if (this.rollSfx != null)
            this.rollSfx.Play();

        float currentTime = 0;
        float duration = this.rollDuration;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.rollSpeed * Time.deltaTime, 0f, 0f);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-3000f * Time.deltaTime));

            yield return null;
        }
        this.user.rb.velocity = new Vector3(0, 0, 0);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleColliderr)
        {
            capsuleColliderr.radius = 0.65f;
            capsuleColliderr.height = 2.25f;

            capsuleColliderr.center = new Vector3(0f, 1.125f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
        }
        this.rolling = false;
        yield return new WaitForSeconds(this.missEndLag);

        

        

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void RollBack(TestPlayer player)
    {
        if (this.onGoing /*&& player.damageMitigation < 1f && !player.knockbackInvounrability*/)
        {
            this.StopAllCoroutines();

            bool countered = false;

            /*if (!player.countering)
            {
                
            }
            else
            {
                player.OnHitFromPlayer?.Invoke(this.user);
            }*/
            if (player != null)
            {
                if (!player.countering)
                {
                    if (/*player.characterId == 3 &&*/
                    player.attacks.backwardSpecial2 != null &&
                    player.attacks.backwardSpecial2 is RollForwardAttack rollattack &&
                    rollattack.onGoing &&
                    rollattack.rolling)
                    {
                        //rollattack.onGoing = true;
                        player.TakeDamage(this.user.transform.position, this.damage, 0.2f);
                        this.user.TakeDamage(this.user.transform.position, this.damage, 0.2f);
                        this.user.GiveSuperCharge(this.superCharge);
                        this.user.GiveSuperCharge(this.superCharge / 2);
                        player.GiveSuperCharge(this.superCharge);
                        player.GiveSuperCharge(this.superCharge / 2);
                        rollattack.RollBack(null);

                        if (player.soundEffects != null)
                            player.soundEffects.PlayHitSound();

                        if (this.user.soundEffects != null)
                            this.user.soundEffects.PlayHitSound();
                    }
                    else
                    {
                        player.OnHit?.Invoke();
                        //player.TakeDamage(this.user.transform.position, this.damage, 0.2f, this.transform.forward.z * 600, 900);
                        player.TakeDamage(this.user.transform.position, this.damage, 0.2f, this.transform.forward.z * 600, 900, true, true, true, false, true, false, false, false, 0f, 0.5f, this.user, true);
                        //player.OnHit?.Invoke();
                        player.OnHitFromPlayer?.Invoke(this.user);
                        this.user.GiveSuperCharge(this.superCharge);
                        player.GiveSuperCharge(this.superCharge / 2);

                        if (player.soundEffects != null)
                            player.soundEffects.PlayHitSound();
                    }
                }
                else
                {
                    player.OnHitFromPlayer?.Invoke(this.user);
                    countered = true;
                }




                //player.AddStun(0.2f);
            }



            //this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
            this.user.rb.velocity = new Vector3(0, 0, 0);

            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            /*if (this.animations != null)
                this.animations.RollAnimation();*/

            if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
            {
                capsuleCollider.radius = 0.65f;
                capsuleCollider.height = 2.25f;

                capsuleCollider.center = new Vector3(0f, 1.125f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }

            this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            if (!countered)
            {
                if (this.animations != null)
                    this.animations.RollAnimation();

                this.user.AddKnockback(this.transform.forward.z * -300f, 900f);
                //this.user.knockbackInvounrability = true;

                this.StartCoroutine(this.RollBackCoroutine());
            }
            

        }
        
    }

    IEnumerator RollBackCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.rollingBack = true;
        /*yield return new WaitForSeconds(0.01f);
        this.user.AddKnockback(this.transform.forward.z * -300f, 900f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/

        yield return new WaitForSeconds(0.05f);
        this.rolling = false;
        float currentTime = 0;
        float duration = 0.5f;
        while (/*currentTime < duration*/ Mathf.Abs(this.user.rb.velocity.y) > 0f)
        {
            currentTime += Time.deltaTime;
            //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 1000f * Time.deltaTime, 0f, 0f);
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.transform.forward.z * */-4000f * Time.deltaTime));

            yield return null;
        }
        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/
        this.user.rb.velocity = new Vector3(0, 0, 0);

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.rolling = false;

        yield return new WaitForSeconds(this.hitEndLag);

        //this.user.knockbackInvounrability = false;
        this.rollingBack = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    

    

    public override void Stop()
    {
        base.Stop();
        this.rolling = false;
        this.rollingBack = false;

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            capsuleCollider.radius = 0.65f;
            capsuleCollider.height = 2.25f;
            
            capsuleCollider.center = new Vector3(0f, 1.125f, 0f);
            //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
        }

        if (this.rollSfx != null)
            this.rollSfx.Stop();

        /*if (!this.user.dead)
            this.user.rb.isKinematic = false;*/

        //this.user.knockbackInvounrability = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Hit()
    {
        if (this.user.tempOpponent != null && this.user.tempOpponent.characterId == 3 && this.user.tempOpponent.attacks.backwardSpecial2 != null && this.user.tempOpponent.attacks.backwardSpecial2 is RollForwardAttack rollattack && rollattack.onGoing)
        {
            /*this.user.knockbackInvounrability = true;
            this.RollBack(this.user);*/

            /*this.user.knockbackInvounrability = true;
            this.user.rb.velocity = new Vector3(0, 0, 0);*/
            this.StartCoroutine(this.HitCoroutine());
        }
        else
        {
            this.Stop();
            //this.StartCoroutine(this.HitCoroutine());
            //this.RollBack(this.user);
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    public void ChangeCollision(bool round = false)
    {
        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            if (round)
            {
                capsuleCollider.radius = 0.65f;
                capsuleCollider.height = 0.65f;

                capsuleCollider.center = new Vector3(0f, 1.65f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }
            else
            {
                capsuleCollider.radius = 0.65f;
                capsuleCollider.height = 2.25f;

                capsuleCollider.center = new Vector3(0f, 1.125f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }

        }
    }

    IEnumerator HitCoroutine()
    {
        this.user.rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        //this.RollBack(this.user);
        //this.Stop();
        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/
        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/
    }
}
