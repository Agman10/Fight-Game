using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject model;

    public TestHitbox hitbox;
    public TestHitbox constantHitbox;

    public TestPlayer belongsTo;

    public bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        this.StartCoroutine(this.DiableTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.rb != null && Mathf.Abs(this.transform.position.y) > 0.6f && !this.hasCollided)
        if (this.rb != null && this.transform.position.y > 0.6f && !this.hasCollided)
            this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y - 0.3f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(this.transform.position.y <= 0.6f)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.hasCollided = true;
        }
        

        //Debug.Log("test");
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.hitbox != null)
                this.hitbox.belongsTo = player;

            if (this.constantHitbox != null)
                this.constantHitbox.belongsTo = player;
        }
    }


    private IEnumerator DiableTimer()
    {
        yield return new WaitForSeconds(2f);

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
