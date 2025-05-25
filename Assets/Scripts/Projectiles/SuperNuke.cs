using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNuke : MonoBehaviour
{
    public NukeExplosion nukeExplosion;
    public TestPlayer belongsTo;
    public GameObject model;
    private void OnEnable()
    {
        this.StartCoroutine(this.FallDownCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.model != null)
            this.model.SetActive(true);
    }

    private IEnumerator FallDownCoroutine()
    {
        float currentTime = 0;
        float duration = 0.4f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.position.x, Mathf.Lerp(12f, 0f, currentTime / duration), 0f);
            yield return null;
        }
        this.Explode();
        //this.transform.localPosition = this.endPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null)
            this.Explode();
    }

    public void Explode()
    {
        if (this.model != null)
            this.model.SetActive(false);

        if (this.nukeExplosion != null)
        {
            NukeExplosion nukeExplosionPrefab = this.nukeExplosion;
            nukeExplosionPrefab = Instantiate(nukeExplosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
            nukeExplosionPrefab.SetOwner(this.belongsTo);
        }
        this.StopAllCoroutines();
        this.StartCoroutine(this.DisableCoroutine());
        //this.gameObject.SetActive(false);
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;
        }
    }
}
