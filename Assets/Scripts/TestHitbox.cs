using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    //TestPlayer[] playersCollided;
    public float damage = 1f;
    public float stun = 0f;
    public float horizontalKnockback = 0f;
    public float verticalKnockback = 0f;
    public bool explosionKnockback = false;
    public bool continousDamage = false;
    public float damageDelay = 0.1f;
    public List<TestPlayer> players = new List<TestPlayer>();
    //public List<TestRagdoll> ragdolls = new List<TestRagdoll>();
    public List<GameObject> others = new List<GameObject>();
    public TestPlayer belongsTo;
    public bool damageOwner = false;
    public float superChargeAmount;
    public bool instaKill;
    public bool avoidOnHitInvoke;

    public bool avoidDamagingPlayer;

    public Action<TestPlayer> OnPlayerCollision;
    public Action<Ball> OnBallCollision;

    public bool fireProperty = false;
    public bool electricProperty = false;
    public float physicsKnockbackMultiplier = 1f;
    public bool changeTargetDir;
    public bool preventDeath = false;

    public bool ragdollForce = true;

    public bool preventMomentumStop = false;

    public Transform hitboxOrigin;

    public bool isSuper = false;
    public bool knockDown = false;
    //public float knockDownImpactDuration = 0f;
    //public float knockDownSitDuration = 0.5f;

    public bool preventDeathSound = false;
    public bool doHitSound = true;
    public float hitSoundCooldown = 0f;

    public Transform explosionHitboxOrigin;
    public CharacterSoundEffect customHitSound;

    public Transform hitEffectTransformOrigin;

    public bool hitEffectRandomPos = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.forward);
    }
    private void OnEnable()
    {
        this.players.Clear();
        this.OnPlayerCollision += this.PlayerCollisionInvoke;
        this.OnBallCollision += this.BallCollisionInvoke;
    }
    private void OnDisable()
    {
        this.OnPlayerCollision -= this.PlayerCollisionInvoke;
        this.StopAllCoroutines();
        this.players.Clear();
        this.others.Clear();

        this.OnBallCollision -= this.BallCollisionInvoke;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!this.continousDamage)
        {
            TestPlayer player = other.GetComponent<TestPlayer>();
            //Debug.Log(player);
            //Debug.Log(other);
            if (player != null /*&& this.belongsTo != player*/ && !player.dead && !this.players.Contains(player))
            {
                if(this.belongsTo != player || this.damageOwner)
                {
                    this.OnPlayerCollision.Invoke(player);
                    if (!this.avoidDamagingPlayer)
                    {
                        /*if (other == player)
                        this.OnPlayerCollision.Invoke(player);*/

                        //this.OnPlayerCollision.Invoke(player);

                        if (this.instaKill)
                            player.Die(this.transform.position, false, false, true, this.preventDeathSound, this.isSuper);

                        this.players.Add(player);
                        if (!this.avoidOnHitInvoke)
                        {
                            player.OnHit?.Invoke();

                            /*if (TimeScaler.Instance != null)
                            {
                                TimeScaler.Instance.SetTimeScale(0.05f, 0.025f);
                            }*/

                            if (this.belongsTo != null)
                                player.OnHitFromPlayer?.Invoke(this.belongsTo);
                        }

                        if (this.doHitSound && player.damageMitigation < 1f)
                        {
                            //player.TestSoundEffect();

                            if (this.customHitSound.sound != null)
                            {
                                this.customHitSound.PlaySound();
                            }
                            else
                            {
                                if (player.soundEffects != null)
                                {
                                    player.soundEffects.PlayHitSound();
                                }
                            }
                        }

                        if (this.electricProperty)
                        {
                            if (player.skelletonBody != null /*&& player.skelletonBody.enabled*/)
                                player.skelletonBody.EnableAndDisableSkelleton();
                        }

                        /*if (this.hitEffectTransformOrigin != null && player.hitEffectLogic != null && this.belongsTo != null)
                            player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, -this.belongsTo.transform.forward.z);*/

                        if (this.hitEffectTransformOrigin != null && player.hitEffectLogic != null && this.belongsTo != null)
                        {
                            float forwardZ = 1f;
                            if (player.transform.position.x > this.belongsTo.transform.position.x)
                                forwardZ = -1f;

                            player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, forwardZ, this.hitEffectTransformOrigin.position.z);
                            //player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, -this.belongsTo.transform.forward.z);
                        }

                        //Debug.Log(player.transform.forward);
                        //float distance = Vector3.Distance(player.transform.position, this.transform.position);
                        Vector3 dir = (player.transform.position - this.transform.position).normalized;

                        if (this.explosionHitboxOrigin != null)
                            dir = (player.transform.position - this.explosionHitboxOrigin.position).normalized;

                        //Debug.Log(dir.x);
                        float direction = 1f;
                        if (dir.x < 0f)
                            direction = -1f;
                        else if (dir.x == 0f)
                            direction = -player.transform.forward.z;

                        //Debug.Log(direction);

                        Vector3 posOrigin = this.transform.position;
                        if (this.hitboxOrigin != null)
                            posOrigin = this.hitboxOrigin.position;

                        if (!this.explosionKnockback)
                            player.TakeDamage(posOrigin, this.damage, this.stun, this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback, this.ragdollForce, true, this.changeTargetDir, this.preventDeath, !this.preventMomentumStop, this.preventDeathSound, this.isSuper, this.knockDown);
                        else
                            player.TakeDamage(posOrigin, this.damage, this.stun, direction * this.horizontalKnockback, this.verticalKnockback, this.ragdollForce, true, this.changeTargetDir, this.preventDeath, !this.preventMomentumStop, this.preventDeathSound, this.isSuper, this.knockDown);

                        if (this.belongsTo != null && this.superChargeAmount != 0f && this.belongsTo != player)
                        {
                            float mitigation = 1f - player.damageMitigation;

                            this.belongsTo.GiveSuperCharge(this.superChargeAmount * mitigation);
                            player.GiveSuperCharge((this.superChargeAmount / 2) * mitigation);
                        }
                        //Debug.Log("collision: " + player);
                    }


                }

            }

            TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();

            if(ragdoll != null && ragdoll.rb != null)
            {
                Vector3 dir = (ragdoll.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    ragdoll.KnockBack(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                else
                    ragdoll.KnockBack(new Vector3(direction * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));

                /*if (ragdoll.owner != null && ragdoll.owner.soundEffects != null)
                    ragdoll.owner.soundEffects.PlayHitSound();*/

                if (this.doHitSound && ragdoll.canBeAttacked)
                {
                    if (this.customHitSound.sound != null)
                    {
                        this.customHitSound.PlaySound();
                    }
                    else if (ragdoll.owner != null && ragdoll.owner.soundEffects != null)
                    {
                        ragdoll.owner.soundEffects.PlayHitSound();
                    }
                }

                if (this.electricProperty)
                {
                    if (ragdoll.owner != null && ragdoll.owner.skelletonBody != null /*&& ragdoll.owner.skelletonBody.enabled*/)
                    {
                        ragdoll.owner.skelletonBody.EnableAndDisableSkelleton();
                    }
                }

            }


            Bomb bomb = other.GetComponent<Bomb>();

            if (bomb != null)
            {
                if (this.fireProperty)
                    bomb.Explode();

                Vector3 dir = (bomb.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    bomb.KnockBack(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 2f), this.verticalKnockback * 2f, 0));
                else
                    bomb.KnockBack(new Vector3(direction * (this.horizontalKnockback * 2f), this.verticalKnockback * 2f, 0));
            }

            Ball ball = other.GetComponent<Ball>();
            if(ball != null)
            {
                /*if (player == null)
                    this.OnBallCollision?.Invoke(ball);*/

                this.OnBallCollision?.Invoke(ball);

                Vector3 dir = (ball.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    ball.KnockBack(new Vector3(this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback, 0));
                else
                    ball.KnockBack(new Vector3(direction * this.horizontalKnockback, this.verticalKnockback, 0));
            }


            DamagableEntity damagableEntity = other.GetComponent<DamagableEntity>();
            if (damagableEntity != null && !damagableEntity.dead)
            {
                if (this.belongsTo != damagableEntity.owner || this.damageOwner)
                    this.HitDamagableEntity(damagableEntity);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.continousDamage)
        {
            TestPlayer player = other.GetComponent<TestPlayer>();
            //Debug.Log(player);
            //Debug.Log(other);
            if (player != null /*&& this.belongsTo != player*/ && !player.dead && !this.players.Contains(player))
            {
                if(this.belongsTo != player || this.damageOwner)
                {
                    this.players.Add(player);
                    if (this.instaKill)
                        player.Die(this.transform.position, false, false, true, this.preventDeathSound, this.isSuper);

                    //player.TakeDamage(this.transform.position, this.damage, this.stun, this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback);
                    if (!this.avoidOnHitInvoke)
                    {
                        player.OnHit?.Invoke();

                        /*if (TimeScaler.Instance != null)
                        {
                            TimeScaler.Instance.SetTimeScale(0.05f, 0.025f);
                        }*/

                        if (this.belongsTo != null)
                            player.OnHitFromPlayer?.Invoke(this.belongsTo);
                    }

                    if (this.doHitSound && player.damageMitigation < 1f)
                    {
                        //player.TestSoundEffect();

                        if(this.customHitSound.sound != null)
                        {
                            this.customHitSound.PlaySound();
                        }
                        else
                        {
                            if (player.soundEffects != null)
                            {
                                player.soundEffects.PlayHitSound(this.hitSoundCooldown);
                            }
                        }
                    }

                    if (this.electricProperty)
                    {
                        if (player.skelletonBody != null /*&& player.skelletonBody.enabled*/)
                            player.skelletonBody.EnableAndDisableSkelleton();
                    }

                    /*if (this.hitEffectTransformOrigin != null && player.hitEffectLogic != null && this.belongsTo != null)
                    {
                        float forwardZ = 1f;
                        if (player.transform.position.x > this.belongsTo.transform.position.x)
                            forwardZ = -1f;

                        player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, forwardZ);
                        //player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, -this.belongsTo.transform.forward.z);
                    }*/

                    

                    if (this.hitEffectRandomPos)
                    {
                        if (this.hitEffectTransformOrigin != null && player.hitEffectLogic != null)
                        {
                            float forwardZ = 1f;
                            if (player.transform.position.x > this.hitEffectTransformOrigin.position.x)
                                forwardZ = -1f;

                            float yPos = this.hitEffectTransformOrigin.transform.position.y + UnityEngine.Random.Range(-1f, 1f);

                            if (yPos < player.transform.position.y)
                                yPos = player.transform.position.y;

                            //player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y + UnityEngine.Random.Range(-1f, 1f), forwardZ, UnityEngine.Random.Range(-1f, 1f));

                            player.hitEffectLogic.DoHitEffect(yPos, forwardZ, UnityEngine.Random.Range(-1f, 1f));
                            //player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, -this.belongsTo.transform.forward.z);
                        }
                    }
                    else
                    {
                        if (this.hitEffectTransformOrigin != null && player.hitEffectLogic != null && this.belongsTo != null)
                        {
                            float forwardZ = 1f;
                            if (player.transform.position.x > this.belongsTo.transform.position.x)
                                forwardZ = -1f;

                            player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, forwardZ);
                            //player.hitEffectLogic.DoHitEffect(this.hitEffectTransformOrigin.transform.position.y, -this.belongsTo.transform.forward.z);
                        }
                    }


                    //Debug.Log("collision: " + player);
                    float knockbackMultiplier = 1f;
                    if (player.stuns.Count > 0 || player.attackStuns.Count > 0 || player.rb.velocity.y > 0f)
                        knockbackMultiplier = this.physicsKnockbackMultiplier;

                    Vector3 dir = (player.transform.position - this.transform.position).normalized;

                    if (this.explosionHitboxOrigin != null)
                        dir = (player.transform.position - this.explosionHitboxOrigin.position).normalized;

                    float direction = 1f;
                    if (dir.x <= 0f)
                        direction = -1f;

                    Vector3 posOrigin = this.transform.position;
                    if (this.hitboxOrigin != null)
                        posOrigin = this.hitboxOrigin.position;

                    if (!this.explosionKnockback)
                        player.TakeDamage(posOrigin, this.damage, this.stun, this.transform.forward.z * (this.horizontalKnockback * knockbackMultiplier), this.verticalKnockback * knockbackMultiplier, this.ragdollForce, true, this.changeTargetDir, this.preventDeath, !this.preventMomentumStop, this.preventDeathSound, this.isSuper);
                    else
                        player.TakeDamage(posOrigin, this.damage, this.stun, direction * (this.horizontalKnockback * knockbackMultiplier), this.verticalKnockback * knockbackMultiplier, this.ragdollForce, true, this.changeTargetDir, this.preventDeath, !this.preventMomentumStop, this.preventDeathSound, this.isSuper);


                    if (this.belongsTo != null && this.superChargeAmount != 0f && this.belongsTo != player)
                    {
                        float mitigation = 1f - player.damageMitigation;

                        this.belongsTo.GiveSuperCharge(this.superChargeAmount * mitigation);
                        player.GiveSuperCharge((this.superChargeAmount / 2) * mitigation);
                    }

                    if (this.gameObject.active)
                        this.StartCoroutine(this.RemoveCoroutine(this.damageDelay, player));
                }

                
            }

            Bomb bomb = other.GetComponent<Bomb>();

            if (bomb != null && !this.others.Contains(bomb.gameObject))
            {
                if (this.fireProperty)
                    bomb.Explode();

                this.others.Add(bomb.gameObject);
                Vector3 dir = (bomb.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    bomb.KnockBack(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 2f * this.physicsKnockbackMultiplier), this.verticalKnockback * 2f * this.physicsKnockbackMultiplier, 0));
                else
                    bomb.KnockBack(new Vector3(direction * (this.horizontalKnockback * 2f * this.physicsKnockbackMultiplier), this.verticalKnockback * 2f * this.physicsKnockbackMultiplier, 0));

                this.StartCoroutine(this.RemoveGameObjectCoroutine(this.damageDelay, bomb.gameObject));
            }

            TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();
            if (ragdoll != null && ragdoll.rb != null && ragdoll.canBeAttacked && !this.others.Contains(ragdoll.gameObject))
            {
                this.others.Add(ragdoll.gameObject);
                Vector3 dir = (ragdoll.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    ragdoll.KnockBack(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f * this.physicsKnockbackMultiplier), this.verticalKnockback * 150f * this.physicsKnockbackMultiplier, 0));
                else
                    ragdoll.KnockBack(new Vector3(direction * (this.horizontalKnockback * 150f * this.physicsKnockbackMultiplier), this.verticalKnockback * 150f * this.physicsKnockbackMultiplier, 0));

                if (this.doHitSound && ragdoll.canBeAttacked)
                {
                    if (this.customHitSound.sound != null)
                    {
                        this.customHitSound.PlaySound();
                    }
                    else if (ragdoll.owner != null && ragdoll.owner.soundEffects != null)
                    {
                        ragdoll.owner.soundEffects.PlayHitSound(this.hitSoundCooldown);
                    }
                }

                if (this.electricProperty)
                {
                    if (ragdoll.owner != null && ragdoll.owner.skelletonBody != null /*&& ragdoll.owner.skelletonBody.enabled*/)
                    {
                        ragdoll.owner.skelletonBody.EnableAndDisableSkelleton();
                    }
                }


                this.StartCoroutine(this.RemoveGameObjectCoroutine(this.damageDelay, ragdoll.gameObject));

            }

            Ball ball = other.GetComponent<Ball>();
            if (ball != null && !this.others.Contains(ball.gameObject))
            {
                this.others.Add(ball.gameObject);
                Vector3 dir = (ball.transform.position - this.transform.position).normalized;
                float direction = 1f;
                if (dir.x <= 0f)
                    direction = -1f;
                //ragdoll.rb.AddForce(new Vector3(this.transform.forward.z * (this.horizontalKnockback * 150f), this.verticalKnockback * 150f, 0));
                if (!this.explosionKnockback)
                    ball.KnockBack(new Vector3(this.transform.forward.z * (this.horizontalKnockback * this.physicsKnockbackMultiplier), this.verticalKnockback * this.physicsKnockbackMultiplier, 0));
                else
                    ball.KnockBack(new Vector3(direction * (this.horizontalKnockback * this.physicsKnockbackMultiplier), this.verticalKnockback  * this.physicsKnockbackMultiplier, 0));

                this.StartCoroutine(this.RemoveGameObjectCoroutine(this.damageDelay, ball.gameObject));
            }

            DamagableEntity damagableEntity = other.GetComponent<DamagableEntity>();
            if (damagableEntity != null && !damagableEntity.dead && !this.others.Contains(damagableEntity.gameObject))
            {
                if (this.belongsTo != damagableEntity.owner || this.damageOwner)
                {
                    this.others.Add(damagableEntity.gameObject);

                    this.HitDamagableEntity(damagableEntity);

                    this.StartCoroutine(this.RemoveGameObjectCoroutine(this.damageDelay, damagableEntity.gameObject));
                }
            }
        }
    }

    public void HitDamagableEntity(DamagableEntity damagableEntity)
    {
        Vector3 dir = (damagableEntity.transform.position - this.transform.position).normalized;
        float direction = 1f;
        if (dir.x < 0f)
            direction = -1f;
        else if (dir.x == 0f)
            direction = -damagableEntity.transform.forward.z;

        Vector3 posOrigin = this.transform.position;
        if (this.hitboxOrigin != null)
            posOrigin = this.hitboxOrigin.position;

        float knockbackDir = this.transform.forward.z * this.horizontalKnockback;
        if (this.explosionKnockback)
            knockbackDir = direction * this.horizontalKnockback;

        damagableEntity.TakeDamage(posOrigin, this.damage, this.stun, knockbackDir, this.verticalKnockback, this.changeTargetDir);



    }

    public void PlayerCollisionInvoke(TestPlayer player)
    {

    }

    public void BallCollisionInvoke(Ball ball)
    {

    }

    IEnumerator RemoveCoroutine(float delay, TestPlayer playerToRemove)
    {
        yield return new WaitForSeconds(delay);
        this.players.Remove(playerToRemove);
    }

    /*IEnumerator RemoveRagdollCoroutine(float delay, TestRagdoll ragdollToRemove)
    {
        yield return new WaitForSeconds(delay);
        this.ragdolls.Remove(ragdollToRemove);
    }*/

    IEnumerator RemoveGameObjectCoroutine(float delay, GameObject gameObjectToRemove)
    {
        yield return new WaitForSeconds(delay);
        this.others.Remove(gameObjectToRemove);
    }
}
