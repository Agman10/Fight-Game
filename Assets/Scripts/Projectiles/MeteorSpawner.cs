using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public TestPlayer user;

    public Transform middleTransform;
    public ArmageddonMeteor meteor;
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

        int amount = 15;
        while (amount > 0)
        {
            //yield return new WaitForSeconds(0.15f);
            //yield return new WaitForSeconds(0.01f);

            this.SpawnMeteor();

            yield return new WaitForSeconds(0.1f);

            amount -= 1;

            yield return null;
        }

        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }

    public void SpawnMeteor()
    {
        if (this.meteor != null)
        {
            ArmageddonMeteor armageddonMeteorPrefab = this.meteor;
            armageddonMeteorPrefab = Instantiate(armageddonMeteorPrefab, new Vector3(Random.Range(-2.5f, 2.5f) + this.transform.position.x, 12f, 0), Quaternion.Euler(0f, 0f, 0f), this.transform);

            //armageddonMeteorPrefab.direction = new Vector2(Random.Range(-0.5f, 0.5f), armageddonMeteorPrefab.direction.y);
            armageddonMeteorPrefab.direction = new Vector2(Random.Range(-0.5f, 0.5f), -20f);

            if (this.middleTransform != null)
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
