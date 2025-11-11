using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public string characterName;
    public float nameSize = 100f;
    public int characterId;
    public float health = 100f;
    public float maxHealth = 100f;
    public float superCharge = 0f;
    public float maxSuperCharge = 100f;
    public bool dead = false;
    public bool knockbackInvounrability;
    public bool preventDeath = false;
    public bool stopAnimationOnHit = false;
    public bool countering = false;
    public int playerNumber;

    public bool hasBeenHit = false;

    [Range(-1f, 1f)]
    public float damageMitigation = 0f;

    public Rigidbody rb;
    public TestRagdoll ragdoll;
    public Collider collision;
    public GameObject ghost;
    public GameObject hitboxes;

    public PlayerInput input;

    [HideInInspector] public TempPlayerAnimations animations;
    [HideInInspector] public Movement movement;
    [HideInInspector] public TempAttacks attacks;
    [HideInInspector] public CharacterSkinTest skin;
    public CharacterSoundEffects soundEffects;

    public List<float> stuns = new List<float>();
    public List<GameObject> attackStuns = new List<GameObject>();

    public Action OnHit;
    public Action OnTakeDamage;
    public Action OnDeath;
    public Action<int, int> OnKO;
    //public Action<bool> OnKO;
    public Action OnReset;
    public Action OnAttack;

    public Action OnDisableItems;

    public Action<int> OnEntranceDone;

    public Action<TestPlayer> OnHitFromPlayer;
    private Vector3 startPos;

    public SleepLogic sleepLogic;
    public KnockDownLogic knockDownLogic;
    public HitAnimationLogic hitAnimLogic;
    //public bool sleeping = false;

    //Remove this later
    public TestPlayer tempOpponent;
    public GameObject tempBall;
    public bool tempLookAtBall = false;
    public GameObject iceCube;
    public SkelletonBody skelletonBody;
    public HitEffectLogic hitEffectLogic;

    [HideInInspector] public float hits;
    [HideInInspector] public float hitsTimer = 0f;

    void Awake()
    {
        this.animations = GetComponent<TempPlayerAnimations>();
        this.movement = GetComponent<Movement>();
        this.startPos = this.transform.position;

        this.attacks = GetComponent<TempAttacks>();
        this.skin = GetComponent<CharacterSkinTest>();
    }
    private void OnEnable()
    {
        if (this.input != null)
            this.SetInput(this.input);

        this.OnHit += this.Hit;


        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.ragdoll != null)
            this.ragdoll.owner = this;
        /*if (this.input != null)
        {
            if(GameManager.Instance != null)
            {
                this.input.StartInput += GameManager.Instance.PauseGame;
            }
        }*/
            
    }

    private void OnDisable()
    {
        this.OnHit -= this.Hit;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stuns.Count);
        //remove this solution later
        if(!this.dead && this.stuns.Count <= 0 && this.attackStuns.Count <= 0)
        {
            this.LookAtTarget();
        }
        /*if (this.tempOpponent != null)
        {
            Debug.Log(Mathf.Abs(this.transform.position.x - this.tempOpponent.transform.position.x));

            Debug.Log(Mathf.Abs(this.transform.position.y - this.tempOpponent.transform.position.y));

            //Debug.Log(this.playerNumber);
            if (this.playerNumber == 2)
            {
                //Debug.Log(this.transform.position.y - this.tempOpponent.transform.position.y);
                Debug.Log(Mathf.Abs(this.transform.position.y - this.tempOpponent.transform.position.y));
                Debug.Log(this.tempOpponent.transform.position.y - this.transform.position.y);
            }

            //Debug.Log(Vector2.Distance(this.transform.position, this.tempOpponent.transform.position));
            *//*if (this.playerNumber == 2)
                Debug.Log(Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.y + 2.9f), this.tempOpponent.transform.position));*//*
        }*/


        if (this.dead && this.ragdoll != null && !this.ragdoll.rb.isKinematic)
        {
            this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        /*if(this.dead && this.ragdoll != null)
        {
            this.transform.position = new Vector3(this.ragdoll.transform.position.x, this.transform.position.y, 0f);
        }*/

        if (this.hitsTimer > 0f && this.hits > 0)
        {
            this.hitsTimer -= 2f * Time.deltaTime;

            if (this.hitsTimer <= 0f)
                this.hits = 0;
        }
    }


    public void TakeDamage(Vector3 position, float amount = 1f, float stun = 0f, float horizontalKnockback = 0f, float verticalKnockback = 0f, bool ragdollForce = true, bool ghost = true, bool changeDir = false, bool dontKill = false, bool stopMomentumOnStun = true, bool preventDeathSound = false, bool super = false, bool knockDown = false, float impactStunDuration = 0f, float knockDownSitDuration = 0.5f, TestPlayer damageOwner = null/*, bool delayDeath = false*/)
    {
        if(this.damageMitigation != 0f && amount > 0f)
        {
            float mitigation = 1f - this.damageMitigation;

            /*if (this.damageMitigation < 0)
                mitigation = Mathf.Abs(this.damageMitigation * 2f);*/

            //Debug.Log(mitigation);

            this.health -= amount * mitigation;
        }
        else
        {
            this.health -= amount;
        }

        if (this.health < 0f)
            this.health = 0f;


        if (amount > 0)
        {
            
            if (this.damageMitigation < 1f)
            {
                this.OnTakeDamage?.Invoke();
                this.hasBeenHit = true;
            }
                
        }

        if(stun > 0f)
        {
            this.AddStun(stun, stopMomentumOnStun);
        }

        if(Mathf.Abs(horizontalKnockback) > 0f || Mathf.Abs(verticalKnockback) > 0f)
        {
            if (!knockDown)
                this.AddKnockback(horizontalKnockback, verticalKnockback);
        }
        if(changeDir && stun > 0f && !this.knockbackInvounrability)
        {
            if(horizontalKnockback > 0f)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
            }
            else if (horizontalKnockback < 0f)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        
        if(this.health <= 0f && !this.preventDeath /*&& !dontKill*/)
        {
            int koType = 0;

            if (damageOwner == this)
            {
                if (super)
                    koType = 4; //HyperSuicide
                else
                    koType = 3; //Suicide

                /*koType = 3; //Suicide
                koType = 4; //HyperSuicide*/

            }
            else if (super)
            {
                koType = 1; //HyperKo
            }
                

            //MAKE AN OPTION SO NOT ALL SELF DAMAGE ATTACKS TRIGGER SUICIDE

            if (!dontKill)
                this.Die(position, ragdollForce, ghost, true, preventDeathSound, koType);

            /*if (!dontKill)
                this.Die(position, ragdollForce, ghost, true, preventDeathSound, super);*/

            //this.Die(position, ragdollForce, ghost);

            /*if (!delayDeath)
                this.Die(position, ragdollForce, ghost);
            else
                this.DelayedDeath(position, ragdollForce, ghost);*/
        }



        if (amount < 0)
        {
            if (this.health >= this.maxHealth)
                this.health = this.maxHealth;
        }
        if (knockDown)
            this.KnockDown(horizontalKnockback, verticalKnockback, stun, impactStunDuration, knockDownSitDuration);
        /*else
            this.TriggerHitAnim();*/
        
        //Debug.Log("ouch");
    }

    public void AddStun(float lenght = 0.5f, bool stopMomentum = true)
    {
        this.stuns.Add(lenght);
        if (stopMomentum)
            this.rb.velocity = Vector3.zero;

        this.StartCoroutine(this.RemoveStunCoroutine(lenght));
        //this.RemoveStun(lenght);
    }
    public void AddKnockback(float horizontalKnockback = 0f, float verticalKnockback = 0f)
    {
        if (!this.knockbackInvounrability)
            this.rb.AddForce(horizontalKnockback, verticalKnockback, 0);
    }

    public void RemoveStun(float lenght)
    {
        //this.StartCoroutine(this.RemoveStunCoroutine(lenght));
        this.stuns.Remove(lenght);

        /*if (this.stuns.Contains(lenght))
            this.stuns.Remove(lenght);*/
    }
    IEnumerator RemoveStunCoroutine(float lenght)
    {
        yield return new WaitForSeconds(lenght);
        this.RemoveStun(lenght);
        //this.stuns.Remove(lenght);
    }

    public void GiveSuperCharge(float amount = 1f)
    {
        if (amount > 0f)
        {
            if (this.superCharge < this.maxSuperCharge)
            {
                //this.superCharge += amount;

                //remove the 1.5 multiflier its there right now for the charge to go up faster
                this.superCharge += amount * 1.5f;
                if (this.superCharge >= this.maxSuperCharge)
                    this.superCharge = this.maxSuperCharge;
            }
        }
        else if (amount < 0f)
        {
            if(this.superCharge > 0f)
            {
                this.superCharge += amount;
                if (this.superCharge <= 0f)
                    this.superCharge = 0f;
            }
        }
    }

    public void KnockDown(float xForce = 300f, float yForce = 600f, float stunTime = 2f, float impactStunDuration = 0f, float sitDuration = 0.5f)
    {
        if (this.knockDownLogic != null)
            this.knockDownLogic.KnockDown(xForce, yForce, stunTime, impactStunDuration, sitDuration);
    }

    public void TriggerHitAnim()
    {
        if (this.hitAnimLogic != null)
            this.hitAnimLogic.TriggerHitAnimation();
    }

    public void Die(Vector3 position, bool ragdollforce = true, bool ghost = true, bool ragdoll = true, bool preventDeathSound = false, int deathType = 0/*bool super = false*/)
    {
        if (!this.dead)
        {
            this.dead = true;
            this.health = 0f;
            this.OnDeath?.Invoke();
            //this.OnKO?.Invoke(super);
            this.OnKO?.Invoke(deathType, this.playerNumber);
            this.hasBeenHit = true;

            if (!preventDeathSound && this.soundEffects != null)
                this.soundEffects.PlayDeathSound();

            /*if (this.ragdoll != null)
                this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);*/

            if (this.hitboxes != null)
            {
                this.hitboxes.gameObject.SetActive(false);
            }

            if (this.collision != null)
            {
                this.collision.enabled = false;
            }

            if (this.rb != null)
            {
                this.rb.isKinematic = true;
            }

            if (this.ragdoll != null && ragdoll)
            {
                if (this.transform.forward.z <= -1)
                    this.animations.InversePose();

                this.ragdoll.EnableRagdoll();
                if (ragdollforce)
                    this.ragdoll.RagdollForce(position);
            }

            if (this.ghost != null && ghost)
            {
                GameObject ghostPrefab = this.ghost;
                //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
                ghostPrefab = Instantiate(ghostPrefab, this.transform.position, Quaternion.Euler(0, 90, 0));

                CharacterSkinTest ghostSkin = ghostPrefab.GetComponent<CharacterSkinTest>();
                if(ghostSkin != null)
                {
                    if (this.skin != null && this.skin.skin != null)
                        ghostSkin.SetSkin(this.skin.skin);
                }
                
            }
        }
    }

    [ContextMenu("Die")]
    public void Suicide()
    {
        if (!this.dead && !this.preventDeath)
        {
            this.dead = true;
            this.health = 0f;
            this.OnDeath?.Invoke();
            //this.OnKO?.Invoke(false);
            this.OnKO?.Invoke(3, this.playerNumber);
            this.hasBeenHit = true;

            if (this.soundEffects != null)
                this.soundEffects.PlayDeathSound();

            if (this.hitboxes != null)
            {
                this.hitboxes.gameObject.SetActive(false);
            }

            if (this.collision != null)
            {
                this.collision.enabled = false;
            }

            if (this.rb != null)
            {
                this.rb.isKinematic = true;
            }

            if (this.ragdoll != null)
            {
                //this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
                if (this.transform.forward.z <= -1)
                    this.animations.InversePose();

                this.ragdoll.EnableRagdoll();

                //backflip
                //this.ragdoll.RagdollForce(new Vector3(this.transform.position.x + (this.transform.forward.z * 1f), this.transform.position.y - 1.5f, this.transform.position.z), 70000);

                //this.ragdoll.RagdollForce(new Vector3(this.transform.position.x + (this.transform.forward.z * 1.5f), this.transform.position.y - 1.5f, this.transform.position.z), 5000);

            }

            /*if(this.ghost != null)
            {
                GameObject ghostPrefab = this.ghost;
                //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
                ghostPrefab = Instantiate(ghostPrefab, this.transform.position, Quaternion.Euler(0, 90, 0));
            }*/

            
        }
        
    }

    [ContextMenu("FreezeToDeath")]
    public void FreezeToDeath()
    {
        if (!this.dead)
        {
            this.dead = true;
            this.health = 0f;
            this.OnDeath?.Invoke();
            //this.OnKO?.Invoke(false);
            this.OnKO?.Invoke(0, this.playerNumber);

            if (this.hitboxes != null)
            {
                this.hitboxes.gameObject.SetActive(false);
            }

            if (this.collision != null)
            {
                this.collision.enabled = false;
            }

            if (this.rb != null)
            {
                this.rb.isKinematic = true;
            }

            if (this.ragdoll != null)
            {
                this.ragdoll.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            }

            if (this.animations != null)
                this.animations.SetFreezePose();

            if (this.iceCube != null)
                this.iceCube.SetActive(true);
        }

    }

    [ContextMenu("Reset")]
    public void ResetPlayer()
    {
        this.OnHit?.Invoke();
        this.OnReset?.Invoke();
        this.dead = false;
        this.health = this.maxHealth;
        this.preventDeath = false;

        this.hasBeenHit = false;
            

        if (this.hitboxes != null)
        {
            this.hitboxes.gameObject.SetActive(true);
        }

        if (this.collision != null)
        {
            this.collision.enabled = true;
        }

        if (this.rb != null)
        {
            this.rb.isKinematic = false;
            this.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            this.rb.velocity = new Vector3(0f, 0f, 0f);
        }

        if (this.ragdoll != null)
        {
            //this.ragdoll.gameObject.SetActive(true);
            this.ragdoll.StopRagdoll();
            this.ragdoll.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            this.ragdoll.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.ResetRigPos();
        }
        this.transform.position = this.startPos;
    }
    public void LookAtTarget()
    {
        if (this.tempOpponent != null /*&& !this.dead && this.stuns.Count <= 0 && this.attackStuns.Count <= 0*/ && !this.tempLookAtBall && !this.dead)
        {
            if (!this.tempOpponent.dead)
            {
                if (this.tempOpponent.transform.position.x > this.transform.position.x + 0.2f)
                {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    if (this.ragdoll != null)
                        this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);

                    /*if (this.ragdoll != null)
                        this.ragdoll.transform.localScale = new Vector3(this.ragdoll.transform.localScale.x, this.ragdoll.transform.localScale.y, 1f);*/
                }
                else if (this.tempOpponent.transform.position.x < this.transform.position.x - 0.2f)
                {
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                    if (this.ragdoll != null)
                        this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
                }
            }
            else if (this.tempOpponent.ragdoll != null)
            {
                if (this.tempOpponent.ragdoll.transform.position.x > this.transform.position.x)
                {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    if (this.ragdoll != null)
                        this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                    if (this.ragdoll != null)
                        this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
                }
            }
        }
        else if (this.tempBall != null)
        {
            if (this.tempBall.transform.position.x > this.transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
            }
        }
    }
    [ContextMenu("FlipDirection")]
    public void FlipDirection()
    {
        if (this.ragdoll != null)
        {
            if (this.ragdoll.transform.localScale.z > -1f)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
            
    }


    public void lookAtPlayer()
    {
        if (this.tempOpponent.transform.position.x > this.transform.position.x + 0.2f)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            if (this.ragdoll != null)
                this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);

            /*if (this.ragdoll != null)
                this.ragdoll.transform.localScale = new Vector3(this.ragdoll.transform.localScale.x, this.ragdoll.transform.localScale.y, 1f);*/
        }
        else if (this.tempOpponent.transform.position.x < this.transform.position.x - 0.2f)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            if (this.ragdoll != null)
                this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
        }
    }

    public void EntranceDone()
    {
        this.OnEntranceDone?.Invoke(this.playerNumber);
    }

    public void SetInput(PlayerInput input)
    {
        if (input != null)
        {
            if (this.attacks != null)
                this.attacks.playerInput = input;

            if (this.movement != null)
                this.movement.playerInput = input;
        }
        
    }


    public void Hit()
    {
        if (!this.knockbackInvounrability)
        {
            if (this.animations != null && !this.stopAnimationOnHit)
            {
                this.animations.SetDefaultPose();
                //this.animations.ResetRigPos();

                //DO THIS AFTER FIXING SOME SUPERS
                /*if(this.attackStuns.Count <= 0)
                    this.animations.SetDefaultPose();*/
            }
        }
        
        //Debug.Log("test");
    }


    public void LookAtCenter()
    {
        if (this.ragdoll != null)
        {
            if (this.transform.position.x > 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

    }

    public void LookAtDirection(float xDir = 0f)
    {
        if (this.ragdoll != null)
        {
            if (xDir > 0f)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, -1f);
            }
            else if (xDir < 0f)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                if (this.ragdoll != null)
                    this.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

    }

    public void EnableCollision(bool enable = true)
    {
        if (!this.dead && this.collision != null)
            this.collision.enabled = enable;
    }

    public void ResetPosition()
    {
        this.transform.position = this.startPos;
    }

    /*public void DelayedDeath(Vector3 position, bool ragdollforce = false, bool ghost = false)
    {
        if (!this.dead)
        {
            this.dead = true;
            this.health = 0f;
            //this.OnDeath?.Invoke();

            if (this.hitboxes != null)
            {
                this.hitboxes.gameObject.SetActive(false);
            }

            if (this.collision != null)
            {
                this.collision.enabled = false;
            }

            if (this.rb != null)
            {
                this.rb.isKinematic = true;
            }

            this.StartCoroutine(this.DelayedDeathCoroutine(position, ragdollforce, ghost));
        }
    }
    IEnumerator DelayedDeathCoroutine(Vector3 position, bool ragdollforce = false, bool ghost = false)
    {
        yield return new WaitForSeconds(1f);
        this.OnDeath?.Invoke();

        if (this.ragdoll != null)
        {
            this.ragdoll.EnableRagdoll();
            if (ragdollforce)
                this.ragdoll.RagdollForce(position);
        }

        if (this.ghost != null && ghost)
        {
            GameObject ghostPrefab = this.ghost;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            ghostPrefab = Instantiate(ghostPrefab, this.transform.position, Quaternion.Euler(0, 90, 0));
        }
    }*/
}
