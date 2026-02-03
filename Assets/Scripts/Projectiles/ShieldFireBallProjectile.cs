using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShieldFireBallProjectile : Projectile
{
    public VisualEffect flame;
    public GameObject model;
    private Collider collision;
    public TestHitbox damageHitbox;
    public override void OnEnable()
    {
        base.OnEnable();
        this.collision = GetComponent<Collider>();

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.collision != null)
            this.collision.enabled = true;

        if (this.damageHitbox != null)
            this.damageHitbox.gameObject.SetActive(true);


        //this.KnockBack(new Vector3(140f, 700f, 0));
        //this.KnockBack(new Vector3(240f, 500f, 0));
        //this.KnockBack(new Vector3(260f, 600f, 0));
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        LaserProjectile laser = other.GetComponent<LaserProjectile>();
        FireBall fireBall = other.GetComponent<FireBall>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();
        KnifeProjectile knife = other.GetComponent<KnifeProjectile>();
        Projectile projectile = other.GetComponent<Projectile>();
        Ball ball = other.GetComponent<Ball>();
        Bomb bomb = other.GetComponent<Bomb>();
        ElectricBallProjectile electricBall = other.GetComponent<ElectricBallProjectile>();


        if (player != null)
        {
            if (this.owner == null || player != this.owner)
            {
                //player.TakeDamage(this.transform.position, 5f, 0.2f, this.transform.forward.z * 10f, 100f, true, true, false, false, true, false, false, false, 0f, 0.5f, this.owner, true);
                this.Disable();

                /*if (this.owner != null)
                {
                    this.owner.GiveSuperCharge(0.5f);
                    player.GiveSuperCharge(0.25f);
                    player.OnHitFromPlayer?.Invoke(this.owner);
                }*/
            }
        }

        TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();

        if (ragdoll != null || bomb != null || ball != null)
        {
            this.Disable();
            //ragdoll.KnockBack(new Vector3(this.transform.forward.z * (10f * 100f), 100f * 100f, 0f));
        }

        FoodItemDestroyHitbox foodItemDestroyHitbox = other.GetComponent<FoodItemDestroyHitbox>();
        if (foodItemDestroyHitbox != null)
            this.Disable();

        if (bigFireBall != null || knife != null || electricBall != null)
        {
            this.Disable();
            //Debug.Log("test");
        }

        if (fireBall != null)
        {
            this.Disable();
            fireBall.SpawnFire();
        }

        if (laser != null)
        {
            this.Disable();
            laser.Disable();
        }

        if (projectile != null && !projectile.ignoreOtherProjectiles && projectile.priority >= this.priority)
            this.Disable();


        if (other.tag == "Wall" /*|| other.tag == "Ground"*/)
        {
            this.Disable();
        }
    }

    public void Disable()
    {
        this.StopAllCoroutines();

        if (this.collision != null)
            this.collision.enabled = false;

        if (this.model != null)
            this.model.SetActive(false);

        if (this.flame != null)
            this.flame.Stop();

        this.StartCoroutine(this.DisableHitBoxCoroutine());
        this.StartCoroutine(this.DisableCoroutine());
    }

    

    

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator DisableHitBoxCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        if (this.damageHitbox != null)
            this.damageHitbox.gameObject.SetActive(false);
    }
}
