using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public TestPlayer belongsTo;
    public GameObject model;
    private Collider collision;
    private TestHitbox hitbox;

    public GameObject hitEffect;

    public TrailRenderer trail;

    public float speed = 10f;

    private bool disabled;

    public bool isP2 = false;
    public Material p1Material;
    public Material p2Material;
    public GameObject hitEffectP2;

    //public float damage = 5f;

    void OnEnable()
    {
        this.collision = GetComponent<Collider>();
        this.hitbox = GetComponent<TestHitbox>();

        //this.MoveTrail();

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.collision != null)
            this.collision.enabled = true;

        this.disabled = false;

        /*if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);*/
    }
    void OnDisable()
    {
        //this.StopAllCoroutines();
    }

    void Update()
    {
        if (!this.disabled)
            this.transform.Translate(new Vector3(this.speed * Time.deltaTime, 0f, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        LaserProjectile laser = other.GetComponent<LaserProjectile>();
        FireBall fireBall = other.GetComponent<FireBall>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();
        KnifeProjectile knife = other.GetComponent<KnifeProjectile>();
        Ball ball = other.GetComponent<Ball>();

        if (player != null)
        {
            if (this.belongsTo == null || player != this.belongsTo)
            {
                /*if (this.hitbox != null)
                    this.hitbox.gameObject.SetActive(false);*/

                /*if (this.collision != null)
                    this.collision.enabled = false;*/

                this.Disable();

                //Debug.Log("test");
            }
        }
        //Debug.Log(Vector3.Distance(this.trail.startWidth, this.trail.endWidth));
        //Debug.Log(this.trail.startWidth);
        if(laser != null || bigFireBall != null || knife != null)
        {
            this.Disable();
            //Debug.Log("test");
        }

        if (fireBall != null)
        {
            this.Disable();
            fireBall.SpawnFire();
        }

        if(ball != null)
        {
            this.Disable();
            ball.KnockBack(new Vector3(this.transform.forward.z * 100f, 100f, 0f));
        }
            

        if (other.tag == "Wall" || other.tag == "Ground")
        {
            this.Disable();
        }
    }


    public void Disable()
    {
        if (this.collision != null)
            this.collision.enabled = false;

        if (this.model != null)
            this.model.gameObject.SetActive(false);

        if (!this.isP2)
        {
            if (this.hitEffect != null)
            {
                GameObject hitEffectPrefab = this.hitEffect;
                hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (this.transform.forward.z * 0.25f), this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));
            }
        }
        else
        {
            if (this.hitEffectP2 != null)
            {
                GameObject hitEffectPrefab = this.hitEffectP2;
                hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (this.transform.forward.z * 0.25f), this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));
            }
        }

        //this.gameObject.SetActive(false);
        this.StartCoroutine(this.DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
    }
    
    /*public void MoveTrail()
    {
        this.StartCoroutine(this.MoveTrailCoroutine());
    }

    private IEnumerator MoveTrailCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        if (this.trail != null)
            this.trail.gameObject.transform.localPosition = new Vector3(0.5f, 0f, 0f);
    }*/

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.hitbox != null)
                this.hitbox.belongsTo = player;
        }
    }

    public void SetP2(bool p2 = true)
    {
        this.isP2 = p2;
        if (p2)
        {
            if(this.p2Material != null)
            {
                if (this.trail != null)
                    this.trail.material = p2Material;
            }
            
        }
        else
        {
            if (this.p1Material != null)
            {
                if (this.trail != null)
                    this.trail.material = p1Material;
            }
        }
        
    }
}
