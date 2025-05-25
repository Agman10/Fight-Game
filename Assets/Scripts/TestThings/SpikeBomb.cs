using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBomb : MonoBehaviour
{
    public Explosion explosion;
    public AudioSource explosionSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Test");

        TestPlayer player = other.GetComponent<TestPlayer>();
        TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();

        if (player != null || ragdoll != null)
        {
            this.Explode();
        }
    }

    public void Explode()
    {
        if (this.explosion != null)
        {
            Explosion explosionPrefab = this.explosion;

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0), Quaternion.Euler(0, 0, 0));

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0), Quaternion.Euler(0, 0, 0));
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), this.transform.rotation);

            explosionPrefab.SetDamage(25f, 10f, 600f, 900f, 300f, 300f, 0f, 0f, 1f, 0.2f, true, true, false);

            //explosionPrefab.SetSize(1.5f);

            //explosionPrefab.SetSize(1.6f);
            explosionPrefab.SetSize(2f);

        }
        if (this.explosionSfx != null)
            this.explosionSfx.Play();

        this.gameObject.SetActive(false);
    }
}
