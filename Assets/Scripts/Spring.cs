using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject activeModel;
    public GameObject unactiveModel;
    public TestHitbox hitbox;
    private Collider collision;

    public GameObject triggerCollison;

    public bool activated = false;


    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();
        this.collision = GetComponent<Collider>();

        this.StartCoroutine(this.ActivateSpringTimerCoroutine());
    }

    private void OnDisable()
    {
        this.activated = false;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.activeModel != null)
            this.activeModel.gameObject.SetActive(false);

        if (this.unactiveModel != null)
            this.unactiveModel.gameObject.SetActive(true);

        if (this.triggerCollison != null)
            this.triggerCollison.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= 0.1f)
        {
            if (this.rb != null)
                this.rb.isKinematic = true;

            if (this.collision != null)
                this.collision.enabled = false;

            this.transform.position = new Vector3(this.transform.position.x, 0f, 0f);

            //this.hasCollided = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("test");
            this.PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        if (!this.activated)
        {
            this.StopAllCoroutines();
            this.activated = true;
            this.StartCoroutine(this.ActivateSpringCoroutine());
        }
        
        
    }

    private IEnumerator ActivateSpringCoroutine()
    {
        /*float randomTime = Random.Range(2f, 3f);
        Debug.Log(randomTime);*/
        yield return new WaitForSeconds(0.1f);
        this.activated = true;

        if (this.activeModel != null)
            this.activeModel.gameObject.SetActive(true);

        if (this.unactiveModel != null)
            this.unactiveModel.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);


        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        if (this.activeModel != null)
            this.activeModel.gameObject.SetActive(false);

        if (this.unactiveModel != null)
            this.unactiveModel.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator ActivateSpringTimerCoroutine()
    {
        yield return new WaitForSeconds(1.6f);

        if (this.triggerCollison != null)
            this.triggerCollison.SetActive(true);

        float randomTime = Random.Range(1f, 4f);
        Debug.Log(randomTime);
        yield return new WaitForSeconds(randomTime);
        this.activated = true;

        if (this.activeModel != null)
            this.activeModel.gameObject.SetActive(true);

        if (this.unactiveModel != null)
            this.unactiveModel.gameObject.SetActive(false);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);


        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        if (this.activeModel != null)
            this.activeModel.gameObject.SetActive(false);

        if (this.unactiveModel != null)
            this.unactiveModel.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            //Debug.Log(knockback.magnitude);
            this.rb.AddForce(knockback);

            //this.rb.AddTorque(new Vector3(0f, 0f, knockback.x * 500000f));
            //this.rb.AddTorque(knockback);
        }
    }
}
