using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmageddonMeteorSpawner : MonoBehaviour
{
    public TestPlayer user;

    public Transform middleTransform;
    public ArmageddonMeteor armageddonMeteor;
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
        this.StartCoroutine(this.SpawnMeteorsCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    public void SetOwner(TestPlayer owner)
    {
        if (owner != null)
            this.user = owner;
    }

    private IEnumerator SpawnMeteorsCoroutine()
    {
        yield return new WaitForSeconds(0.01f);

        int amount = 10;
        while (amount > 0)
        {
            //yield return new WaitForSeconds(0.15f);
            //yield return new WaitForSeconds(0.01f);
            
            if (this.armageddonMeteor != null)
            {
                ArmageddonMeteor armageddonMeteorPrefab = this.armageddonMeteor;
                //armageddonMeteorPrefab = Instantiate(armageddonMeteorPrefab, new Vector3((this.transform.forward.z * 1.5f) + this.transform.position.x, 10f, 0), this.transform.rotation);
                armageddonMeteorPrefab = Instantiate(armageddonMeteorPrefab, new Vector3(Random.Range(0f, this.transform.forward.z * 5f) + this.transform.position.x, 10f, 0), this.transform.rotation, this.transform);
                //armageddonMeteorPrefab = Instantiate(armageddonMeteorPrefab, new Vector3(Random.Range(0f, this.transform.forward.z * 0.5f) + this.transform.position.x, 10f, 0), this.transform.rotation);

                if(this.middleTransform != null)
                {
                    if (armageddonMeteorPrefab.hitbox1 != null)
                        armageddonMeteorPrefab.hitbox1.explosionHitboxOrigin = this.middleTransform;

                    if (armageddonMeteorPrefab.hitbox2 != null)
                        armageddonMeteorPrefab.hitbox2.explosionHitboxOrigin = this.middleTransform;

                    if (armageddonMeteorPrefab.groundHitbox != null)
                        armageddonMeteorPrefab.groundHitbox.explosionHitboxOrigin = this.middleTransform;
                }

                //armageddonMeteorPrefab.direction = new Vector2(2f, Random.Range(-10f, -12f));
                

                if (this.user != null)
                    armageddonMeteorPrefab.SetOwner(this.user);
            }

            yield return new WaitForSeconds(0.15f);

            amount -= 1;

            yield return null;
        }

        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }

    public void StopMeteorSpawner()
    {
        this.StopAllCoroutines();
        //this.gameObject.SetActive(false);
        if (this.gameObject.active)
            this.StartCoroutine(this.StopMeteorSpawnerCoroutine());
    }

    private IEnumerator StopMeteorSpawnerCoroutine()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
