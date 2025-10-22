using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovProjectile : Projectile
{
    private Rigidbody rb;
    private Collider collision;
    public GameObject model;
    public AudioSource breakSfx;
    public ParticleSystem breakVfx;

    public MolotovFlameFalling molotovFlameFalling;
    public MolotovFlames molotovFlame;

    // Start is called before the first frame update
    public override void OnEnable()
    {
        base.OnEnable();
        this.rb = GetComponent<Rigidbody>();
        this.collision = GetComponent<Collider>();

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.collision != null)
            this.collision.enabled = true;


        //this.KnockBack(new Vector3(140f, 700f, 0));
        //this.KnockBack(new Vector3(240f, 500f, 0));
        //this.KnockBack(new Vector3(260f, 600f, 0));
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    void Update()
    {
        if (this.rb != null)
            this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y - 0.1f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        LaserProjectile laser = other.GetComponent<LaserProjectile>();
        FireBall fireBall = other.GetComponent<FireBall>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();
        KnifeProjectile knife = other.GetComponent<KnifeProjectile>();
        Projectile projectile = other.GetComponent<Projectile>();

        if (player != null)
        {
            if (this.owner == null || player != this.owner)
            {
                player.TakeDamage(this.transform.position, 5f, 0.2f, this.transform.forward.z * 10f, 100f);
                this.BreakBottle();

                if (this.owner != null)
                {
                    this.owner.GiveSuperCharge(0.5f);
                    player.GiveSuperCharge(0.25f);
                    player.OnHitFromPlayer?.Invoke(this.owner);
                }
            }
        }

        TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();

        if (ragdoll != null)
            ragdoll.KnockBack(new Vector3(this.transform.forward.z * (10f * 100f), 100f * 100f, 0f));

        /*TestHitbox hitbox = other.GetComponent<TestHitbox>();

        if (hitbox != null && hitbox.fireProperty)
            this.BreakBottle();*/


        FoodItemDestroyHitbox foodItemDestroyHitbox = other.GetComponent<FoodItemDestroyHitbox>();
        if (foodItemDestroyHitbox != null)
            this.BreakBottle();

        if (laser != null || bigFireBall != null || knife != null)
        {
            this.BreakBottle();
            //Debug.Log("test");
        }

        if (fireBall != null)
        {
            this.BreakBottle();
            fireBall.SpawnFire();
        }

        if (projectile != null && !projectile.ignoreOtherProjectiles && projectile.priority >= this.priority)
            this.BreakBottle();


        if (other.tag == "Wall" || other.tag == "Ground" /*|| other.tag == "Untagged"*/)
        {
            this.BreakBottle();
        }

        /*if (this.transform.position.y <= 1f && this.transform.position.y > -1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.BreakBottle();*/
    }

    public void BreakBottle()
    {
        this.StopAllCoroutines();
        /*if (other.tag == "Ground")
        {
            this.DisableFire(true);
        }

        if (this.transform.position.y <= 1f && this.transform.position.y > -1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.DisableFire(true);*/

        if (this.collision != null)
            this.collision.enabled = false;

        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.model != null)
            this.model.SetActive(false);

        if (this.breakSfx != null)
            this.breakSfx.Play();

        if (this.breakVfx != null)
            this.breakVfx.Play();

        if (this.transform.position.y <= 1f && this.transform.position.y > -1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
        {
            this.SpawnMolotovFire();
        }
        else
        {
            this.SpawnFallingFlame();
        }

        this.StartCoroutine(this.DisableCoroutine());
    }

    public void SpawnFallingFlame()
    {
        if (this.molotovFlame != null)
        {
            MolotovFlameFalling firePrefab = this.molotovFlameFalling;

            firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), this.transform.rotation);
            if (this.owner != null)
                firePrefab.SetOwner(this.owner);
        }
    }
    public void SpawnMolotovFire()
    {
        if (this.molotovFlame != null)
        {
            MolotovFlames firePrefab = this.molotovFlame;

            firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, 0f, 0f), this.transform.rotation);
            if (this.owner != null)
                firePrefab.SetOwner(this.owner);
        }


    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    public override void SetOwner(TestPlayer user)
    {
        base.SetOwner(user);

        if (user != null)
        {

        }
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            this.rb.AddForce(knockback /** 0.75f*/);
        }
    }
}
