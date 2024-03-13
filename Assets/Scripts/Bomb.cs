using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionTime = 3f;
    public float explosionDuration = 0.1f;
    public GameObject model;
    public GameObject explosionEffect;
    private Collider modelCollider;
    public TestHitbox explosionHitbox;
    public TestHitbox explosionHitbox2;
    public TestPlayer belongsTo;
    private Rigidbody rb;

    public Transform ropeTransform;
    // Start is called before the first frame update

    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();
        this.modelCollider = GetComponent<Collider>();

        if (this.modelCollider != null)
            this.modelCollider.enabled = true;

        if (this.rb != null)
            this.rb.isKinematic = false;

        if (this.model != null)
            this.model.SetActive(true);

        this.StartCoroutine(this.ExplosionTimer());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(false);

        if (this.model != null)
            this.model.SetActive(true);

        if (this.modelCollider != null)
            this.modelCollider.enabled = true;

        if (this.rb != null)
            this.rb.isKinematic = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExplosionTimer()
    {
        //yield return new WaitForSeconds(this.explosionTime);

        float currentTime = 0;
        float duration = this.explosionTime;
        //float startPosY = this.ropeTransform.localPosition.y;
        float startPosY = 0.695f;
        float endPosY = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.ropeTransform != null)
                this.ropeTransform.localPosition = new Vector3(0f, Mathf.Lerp(startPosY, endPosY, currentTime / duration), 0f);
            yield return null;
        }

        this.Explode();

        if (this.ropeTransform != null)
            this.ropeTransform.localPosition = new Vector3(0f, startPosY, 0f);

        /*if (this.model != null)
            this.model.SetActive(false);

        if (this.modelCollider != null)
            this.modelCollider.enabled = false;

        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(this.explosionDuration);
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(false);
        this.gameObject.SetActive(false);*/
    }

    public void Explode()
    {
        this.StopAllCoroutines();
        if (this.model != null)
            this.model.SetActive(false);

        if (this.modelCollider != null)
            this.modelCollider.enabled = false;

        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(true);

        if (this.explosionEffect != null)
        {
            GameObject explosionPrefab = this.explosionEffect;
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }
        this.StartCoroutine(this.ExplosionDuration());
    }

    IEnumerator ExplosionDuration()
    {
        yield return new WaitForSeconds(this.explosionDuration);
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(false);

        //yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            //Debug.Log(knockback.magnitude);
            this.rb.AddForce(knockback);
            //this.rb.AddTorque(knockback);
            this.rb.AddTorque(new Vector3(1000, 1000, 1000));
        }
    }

    public void SetOwner(TestPlayer player)
    {
        if(player != null)
        {
            this.belongsTo = player;

            if (this.explosionHitbox != null)
                this.explosionHitbox.belongsTo = player;

            if (this.explosionHitbox2 != null)
                this.explosionHitbox2.belongsTo = player;
        }
    }
}
