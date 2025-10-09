using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MolotovFlameFalling : MonoBehaviour
{

    public GameObject molotovFlame;
    public VisualEffect flame;
    private Rigidbody rb;
    private Collider colision;
    public TestHitbox hitbox;

    public TestPlayer owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();
        this.colision = GetComponent<Collider>();

        if (this.rb != null)
        {
            this.rb.isKinematic = false;

            //this.rb.AddForce(0f, -500f, 0f);
           this.rb.velocity = new Vector3(0f, -10f, 0f);
        }
            

        if (this.colision != null)
            this.colision.enabled = true;

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.flame != null)
            this.flame.Play();

        this.StartCoroutine(this.DespawnCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            this.DisableFire(true);
        }

        if (this.transform.position.y <= 1f && this.transform.position.y > -1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.DisableFire(true);
    }

    public void DisableFire(bool spawnMolotov = true)
    {
        this.StopAllCoroutines();

        if (this.flame != null)
            this.flame.Stop();

        if (this.colision != null)
            this.colision.enabled = false;

        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        
        if (spawnMolotov)
        {
            this.SpawnMolotovFire();
        }

        this.StartCoroutine(this.DisableCoroutine());
    }

    public void SpawnMolotovFire()
    {
        

        if (this.molotovFlame != null)
        {
            GameObject firePrefab = this.molotovFlame;

            firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, 0f, 0f), this.transform.rotation);
            /*if (this.belongsTo != null)
                firePrefab.SetOwner(this.belongsTo);*/
        }


    }

    public void SetOwner(TestPlayer user)
    {
        if(user != null)
        {
            this.owner = user;

            if (this.hitbox != null)
                this.hitbox.belongsTo = user;
        }

        
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(5f);
        this.DisableFire(false);
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }
}
