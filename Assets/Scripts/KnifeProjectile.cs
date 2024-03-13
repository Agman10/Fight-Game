using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    public TestPlayer belongsTo;
    public GameObject model;
    private Collider collision;
    private TestHitbox hitbox;

    public GameObject hitEffect;

    public Transform hitEffectTransform;

    private CharacterSkinTest skin;

    //public TrailRenderer trail;

    public float speed = 10f;

    private bool disabled;

    //public float damage = 5f;

    void OnEnable()
    {
        this.collision = GetComponent<Collider>();
        this.hitbox = GetComponent<TestHitbox>();

        this.skin = GetComponent<CharacterSkinTest>();

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
        this.StopAllCoroutines();
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
        if (laser != null || bigFireBall != null || knife != null)
        {
            this.Disable();
            //Debug.Log("test");
        }

        if (fireBall != null)
        {
            this.Disable();
            fireBall.SpawnFire();
        }


        if (other.tag == "Wall" || other.tag == "Ground")
        {
            this.Disable();
        }

        if (this.transform.position.y <= 1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.Disable();
    }


    public void Disable()
    {
        if (this.collision != null)
            this.collision.enabled = false;

        if (this.model != null)
            this.model.gameObject.SetActive(false);

        if (this.hitEffect != null)
        {
            GameObject hitEffectPrefab = this.hitEffect;
            if (this.hitEffectTransform != null)
                hitEffectPrefab = Instantiate(hitEffectPrefab, this.hitEffectTransform.position, Quaternion.Euler(0, 0, 0));
            else
                hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (this.transform.forward.z * 0.45f), this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));

        }

        //this.gameObject.SetActive(false);
        this.StartCoroutine(this.DisableCoroutine());
        //this.gameObject.SetActive(false);
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

            if (this.skin != null && player.skin != null && player.skin.skin != null)
                this.skin.SetSkin(player.skin.skin);
        }
    }
}
