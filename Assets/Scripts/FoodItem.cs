using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public float healingAmount;
    public float chargeAmount;

    public GameObject model;
    public GameObject collision;

    private Rigidbody rb;
    private Collider coliderr;

    public bool hasCollided;

    public GameObject pickUpParticle;

    public float spinSpeed = -500f;

    public AudioSource pickupSfx;

    public GameObject breakVfx;
    public AudioSource breakSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rb != null && Mathf.Abs(this.transform.position.y) > 0.1f && !this.hasCollided)
            this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y - 0.3f, 0f);

        if (!this.hasCollided)
        {
            if (this.model != null)
                this.model.transform.Rotate(new Vector3(0f, 0f, this.spinSpeed * Time.deltaTime));
        }
    }
    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();

        this.coliderr = this.GetComponent<Collider>();

        /*if (this.rb != null)
            this.rb.isKinematic = false;

        if (this.coliderr != null)
            this.coliderr.enabled = true;

        if (this.collision != null)
            this.collision.SetActive(true);

        this.hasCollided = false;
        if (this.model != null)
            this.model.SetActive(true);*/

        this.StartCoroutine(this.DiableTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("test");

        this.hasCollided = true;

        if (this.model != null)
            this.model.transform.localEulerAngles = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if(player != null)
        {
            this.Heal(player);
            this.Disable();
        }
    }

    public void Heal(TestPlayer player)
    {
        if(this.healingAmount > 0f)
        {
            player.TakeDamage(this.transform.position, -this.healingAmount);
        }
        else if (this.healingAmount < 0f)
        {
            player.TakeDamage(this.transform.position, -this.healingAmount, 0f, 0f, 0f, false, true, false, false);
        }

        if(this.chargeAmount > 0f)
        {
            player.GiveSuperCharge(this.chargeAmount / 1.5f); //when fixig give super charge dont divide by 1.5
        }
        else if (this.chargeAmount < 0f)
        {
            player.GiveSuperCharge(this.chargeAmount);
        }

        if (this.pickupSfx != null)
            this.pickupSfx.Play();
    }


    private IEnumerator DiableTimer()
    {
        yield return new WaitForSeconds(8f);

        float time = 2f;
        float maxInterval = 0.1f;
        float minInterval = 0.05f;
        float currentTime = 0;
        float timer = time;
        while (timer > 0f)
        {
            float interval = minInterval + timer / time * (maxInterval - minInterval);
            timer -= Time.deltaTime;
            currentTime += Time.deltaTime;

            //Debug.Log(Time.time);
            if (timer < 0.0f) timer = 0.0f;

            if (this.model != null)
            {
                if (Mathf.PingPong(currentTime, interval) > (interval / 2.0f))
                    this.model.SetActive(false);
                else
                    this.model.SetActive(true);
            }



            //lamp.enabled = Mathf.PingPong(Time.time, interval) > (interval / 2.0f);
            //lamp.gameObject.SetActive(Mathf.PingPong(Time.time, interval) > (interval / 2.0f));
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    public void Disable()
    {
        if (this.pickUpParticle != null)
        {
            GameObject pickUpPrefab = this.pickUpParticle;
            pickUpPrefab = Instantiate(pickUpPrefab, new Vector3(transform.position.x, this.transform.position.y), Quaternion.Euler(0, 0, 0));
        }

        /*this.StopAllCoroutines();
        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.coliderr != null)
            this.coliderr.enabled = false;

        if (this.collision != null)
            this.collision.SetActive(false);

        this.hasCollided = true;
        if (this.model != null)
            this.model.SetActive(false);
        this.StartCoroutine(this.DisableCoroutine());*/

        this.gameObject.SetActive(false);
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            this.rb.AddForce(knockback /** 0.75f*/);
        }
    }

    public void Break()
    {
        this.StopAllCoroutines();
        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.coliderr != null)
            this.coliderr.enabled = false;

        if (this.collision != null)
            this.collision.SetActive(false);

        this.hasCollided = true;
        if (this.model != null)
            this.model.SetActive(false);

        if (this.breakSfx != null)
            this.breakSfx.Play();

        if (this.breakVfx != null)
            this.breakVfx.SetActive(true);

        this.StartCoroutine(this.DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }
}
