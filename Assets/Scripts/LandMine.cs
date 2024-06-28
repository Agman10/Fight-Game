using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public bool isActive;
    public float inactiveDuration = 1f;
    public float explosionDuration = 0.1f;
    public GameObject model;
    public MeshRenderer lamp;
    public Material activeMaterial;
    public Material inactiveMaterial;
    private Collider landmineCollider;
    public TestHitbox explosionHitbox;
    public GameObject explosionEffect;
    public TestPlayer belongsTo;

    public CharacterSoundEffect explosionSfx;
    // Start is called before the first frame update
    void Awake()
    {
        this.landmineCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.MakeActive(false);
        if (this.model != null)
            this.model.SetActive(true);
        this.StartCoroutine(this.ActiveCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.isActive)
        {
            TestPlayer player = other.GetComponent<TestPlayer>();
            TestHitbox hitbox = other.GetComponent<TestHitbox>();
            TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();
            Bomb bomb = other.GetComponent<Bomb>();
            Ball ball = other.GetComponent<Ball>();
            if(player != null || hitbox != null || ragdoll != null || bomb != null || ball != null)
            {
                this.Explode();
            }
        }
    }

    public void Explode()
    {
        //this.gameObject.SetActive(false);
        this.MakeActive(false);
        if (this.model != null)
            this.model.SetActive(false);

        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(true);

        if (this.explosionEffect != null)
        {
            GameObject explosionPrefab = this.explosionEffect;
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }

        this.explosionSfx.PlaySound();

        this.StartCoroutine(this.ExplosionDuration());
    }

    public void MakeActive(bool active)
    {
        this.isActive = active;

        if (this.landmineCollider != null)
            this.landmineCollider.enabled = active;

        if (this.lamp != null)
        {
            if (active && this.activeMaterial != null)
                this.lamp.material = this.activeMaterial;
            else if(!active && this.inactiveMaterial != null)
                this.lamp.material = this.inactiveMaterial;
        }
        
    }

    IEnumerator ActiveCoroutine()
    {
        yield return new WaitForSeconds(1f);
        this.MakeActive(true);
    }

    IEnumerator ExplosionDuration()
    {
        yield return new WaitForSeconds(this.explosionDuration);
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.explosionHitbox != null)
                this.explosionHitbox.belongsTo = player;
        }
    }
}
