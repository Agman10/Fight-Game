using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BigFireBall : MonoBehaviour
{
    public TestPlayer owner;
    public float speed = 10f;
    private float speedMultiplier = 1f;

    private bool hasHitPlayer;
    private Collider mainCollider;

    public TestHitbox hitbox1;
    public TestHitbox hitbox2;

    public GameObject impactEffect;

    public VisualEffect flame;
    public GameObject model;
    public Action OnPlayerCollision;

    private void OnEnable()
    {
        this.OnPlayerCollision += this.PlayerCollisionInvoke;
        this.hasHitPlayer = false;

        this.mainCollider = this.GetComponent<Collider>();

        if (this.mainCollider != null)
            this.mainCollider.enabled = true;

        this.speedMultiplier = 1f;
        this.StartCoroutine(this.DisableTimer());

        //this.SetOwner(this.owner);
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.OnPlayerCollision -= this.PlayerCollisionInvoke;
        this.hasHitPlayer = false;
        this.EnableHitbox(0);

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.mainCollider != null)
            this.mainCollider.enabled = true;

        this.speedMultiplier = 1f;
    }

    void Update()
    {
        /*if (!this.hasHitPlayer)
        {
            //this.transform.Translate(this.transform.forward.z * 10f * Time.deltaTime, 0f, 0f, Space.World);
            this.transform.Translate(this.speed * Time.deltaTime, 0f, 0f);
        }*/
        this.transform.Translate((this.speed * this.speedMultiplier) * Time.deltaTime, 0f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();

        Ball ball = other.GetComponent<Ball>();

        if (player != null && !player.dead && !this.hasHitPlayer)
        {
            if (this.owner != player)
            {
                this.OnPlayerCollision?.Invoke();
            }
        }

        if(bigFireBall != null && bigFireBall.owner != this.owner)
        {
            //this.StartCoroutine(this.ExplodeCoroutine());
            this.Explode();
            bigFireBall.Explode();
        }

        if (ball != null)
        {
            ball.KnockBack(new Vector3(this.transform.forward.z * 350f, 300f, 0f));
        }


        if (other.tag == "Wall" || other.tag == "Ground")
        {
            this.Explode();
        }

        if (this.transform.position.y <= 1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.Explode();


    }

    [ContextMenu("Stop")]
    public void PlayerCollisionInvoke()
    {
        this.hasHitPlayer = true;
        if (this.mainCollider != null)
            this.mainCollider.enabled = false;

        this.speedMultiplier = 0.05f;

        this.StartCoroutine(this.HitPlayer());
    }

    IEnumerator HitPlayer()
    {
        yield return new WaitForSeconds(1f);

        this.EnableHitbox(1);

        if (this.model != null)
            this.model.gameObject.SetActive(false);
        if (this.flame != null)
            this.flame.Stop();

        this.speedMultiplier = 0f;

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.1f);
        this.EnableHitbox(2);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer user)
    {
        if (user != null)
        {
            this.owner = user;
            if (this.hitbox1 != null)
                this.hitbox1.belongsTo = user;
            if (this.hitbox2 != null)
                this.hitbox2.belongsTo = user;
        }
    }

    public void EnableHitbox(int id = 0)
    {
        if (id == 0) //enable hitbox1
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(true);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);
        }
        else if (id == 1) //enable hitbox2
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(true);
        }
        else //disable all hitboxes
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);
        }
    }

    public void Explode()
    {
        if (!this.hasHitPlayer)
        {
            this.StopAllCoroutines();
            this.hasHitPlayer = true;

            if (this.mainCollider != null)
                this.mainCollider.enabled = false;

            this.StartCoroutine(this.ExplodeCoroutine());
        }
        
    }
    private IEnumerator ExplodeCoroutine()
    {
        this.EnableHitbox(1);

        if (this.model != null)
            this.model.gameObject.SetActive(false);
        if (this.flame != null)
            this.flame.Stop();

        this.speedMultiplier = 0f;

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x /*+ (this.transform.forward.z * 0.7f)*/, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.1f);
        this.EnableHitbox(2);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(5f);
        if (!this.hasHitPlayer)
        {
            if (this.model != null)
                this.model.gameObject.SetActive(false);
            if (this.flame != null)
                this.flame.Stop();

            this.hasHitPlayer = true;
            if (this.mainCollider != null)
                this.mainCollider.enabled = false;

            this.speedMultiplier = 0f;

            this.EnableHitbox(2);

            yield return new WaitForSeconds(0.8f);
            this.gameObject.SetActive(false);
        }
    }
}
