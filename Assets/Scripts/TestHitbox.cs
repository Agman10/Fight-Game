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
    public float physicsKnockbackMultiplier = 1f;
    public bool changeTargetDir;
    public bool preventDeath = false;

    public bool ragdollForce = true;

    public Transform hitboxOrigin;
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
                            player.Die(this.transform.position, false, false);

                        this.players.Add(player);
                        if (!this.avoidOnHitInvoke)
                        {
                            player.OnHit?.Invoke();

                            if (this.belongsTo != null)
                                player.OnHitFromPlayer?.Invoke(this.belongsTo);
                        }
                            


                        //Debug.Log(player.transform.forward);
                        //float distance = Vector3.Distance(player.transform.position, this.transform.position);
                        Vector3 dir = (player.transform.position - this.transform.position).normalized;
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
                            player.TakeDamage(posOrigin, this.damage, this.stun, this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback, this.ragdollForce, true, this.changeTargetDir, this.preventDeath);
                        else
                            player.TakeDamage(posOrigin, this.damage, this.stun, direction * this.horizontalKnockback, this.verticalKnockback, this.ragdollForce, true, this.changeTargetDir, this.preventDeath);

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
                        player.Die(this.transform.position, false, false);

                    //player.TakeDamage(this.transform.position, this.damage, this.stun, this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback);
                    if (!this.avoidOnHitInvoke)
                    {
                        player.OnHit?.Invoke();

                        if (this.belongsTo != null)
                            player.OnHitFromPlayer?.Invoke(this.belongsTo);
                    }
                        
                    //Debug.Log("collision: " + player);
                    float knockbackMultiplier = 1f;
                    if (player.stuns.Count > 0 || player.attackStuns.Count > 0 || player.rb.velocity.y > 0f)
                        knockbackMultiplier = this.physicsKnockbackMultiplier;

                    Vector3 dir = (player.transform.position - this.transform.position).normalized;
                    float direction = 1f;
                    if (dir.x <= 0f)
                        direction = -1f;

                    Vector3 posOrigin = this.transform.position;
                    if (this.hitboxOrigin != null)
                        posOrigin = this.hitboxOrigin.position;

                    if (!this.explosionKnockback)
                        player.TakeDamage(posOrigin, this.damage, this.stun, this.transform.forward.z * (this.horizontalKnockback * knockbackMultiplier), this.verticalKnockback * knockbackMultiplier, this.ragdollForce, true, this.changeTargetDir, this.preventDeath);
                    else
                        player.TakeDamage(posOrigin, this.damage, this.stun, direction * (this.horizontalKnockback * knockbackMultiplier), this.verticalKnockback * knockbackMultiplier, this.ragdollForce, true, this.changeTargetDir, this.preventDeath);


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
        }
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
