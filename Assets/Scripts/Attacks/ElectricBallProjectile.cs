using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBallProjectile : MonoBehaviour
{
    private float startY = 3.5f;
    public bool waveing;

    public TestPlayer owner;

    private bool hasHitPlayer;
    private Collider mainCollider;

    public TestHitbox hitbox1;
    public TestHitbox hitbox2;

    public GameObject impactEffect;

    public ParticleSystem electricityEffect1;
    public ParticleSystem electricityEffect2;

    public GameObject model;

    private float speedMultiplier = 1f;

    public float moveSpeed = 2f;
    public float waveSpeed = 5f;

    public Action OnPlayerCollision;

    private float testTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.StartCoroutine(this.MoveToYCoroutine());

        this.OnPlayerCollision += this.PlayerCollisionInvoke;
        this.hasHitPlayer = false;

        this.mainCollider = this.GetComponent<Collider>();

        if (this.mainCollider != null)
            this.mainCollider.enabled = true;

        this.speedMultiplier = 1f;
        this.StartCoroutine(this.DisableTimer());

        this.testTime = 0f;
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.OnPlayerCollision -= this.PlayerCollisionInvoke;
        this.hasHitPlayer = false;
        this.EnableHitbox(0);

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.mainCollider != null)
            this.mainCollider.enabled = true;

        this.speedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasHitPlayer)
        {
            this.transform.Translate(this.moveSpeed * Time.deltaTime, 0f, 0f);
            //this.transform.Translate(this.moveSpeed * this.speedMultiplier * Time.deltaTime, this.transform.position.y, 0f);
        }
        //this.transform.Translate((this.moveSpeed * this.speedMultiplier) * Time.deltaTime, 0f, 0f);

        //this.transform.Translate(this.moveSpeed * Time.deltaTime, this.transform.position.y, 0f);

        /*if (!this.hasHitPlayer)
            this.transform.Translate(3f * Time.deltaTime, this.transform.position.y, 0f);*/

        if (this.waveing && !this.hasHitPlayer)
        {
            //float test = 0f;
            //this.testTime += Time.deltaTime;
            /*if (this.testTime >= 1f)
            {
                //this.testTime -= Time.deltaTime;
                this.testTime = 0f;
            }
            else
            {
                this.testTime += Time.deltaTime;
            }*/

            this.testTime += Time.deltaTime;
            float newY = Mathf.Sin(this.testTime * this.waveSpeed);
            //float newY = Mathf.Sin(this.testTime * (this.waveSpeed * this.speedMultiplier));
            //float newY = Mathf.Sin(this.testTime * 6f);
            //Debug.Log(this.testTime);

            this.transform.position = new Vector3(this.transform.position.x, this.startY + (newY * 3f), 0f);
        }
        
    }



    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();
        ElectricBallProjectile electricBall = other.GetComponent<ElectricBallProjectile>();
        if (player != null && !player.dead && !this.hasHitPlayer)
        {
            if (this.owner != player)
            {
                this.OnPlayerCollision?.Invoke();
            }
        }

        if (bigFireBall != null)
        {
            //this.StartCoroutine(this.ExplodeCoroutine());
            this.Explode();
            bigFireBall.Explode();
        }

        if (electricBall != null)
        {
            //this.StartCoroutine(this.ExplodeCoroutine());
            if(electricBall.owner != null && electricBall.owner == this.owner)
            {
                //do nothing
            }
            else
            {
                this.Explode();
                electricBall.Explode();
            }
            /*this.Explode();
            electricBall.Explode();*/
        }

        if (other.tag == "Wall" || other.tag == "Ground")
        {
            this.Explode();
        }

        /*if (this.transform.position.y <= 1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.Explode();*/


    }

    [ContextMenu("Stop")]
    public void PlayerCollisionInvoke()
    {
        this.StopAllCoroutines();
        this.hasHitPlayer = true;
        if (this.mainCollider != null)
            this.mainCollider.enabled = false;

        this.speedMultiplier = 0.025f;

        this.StartCoroutine(this.HitPlayer());
    }

    IEnumerator HitPlayer()
    {
        /*yield return new WaitForSeconds(0.01f);
        this.hasHitPlayer = true;*/

        if (this.electricityEffect1 != null)
            this.electricityEffect1.Stop();
        yield return new WaitForSeconds(1f);

        this.EnableHitbox(1);

        if (this.model != null)
            this.model.gameObject.SetActive(false);
        if (this.electricityEffect2 != null)
            this.electricityEffect2.Stop();

        this.speedMultiplier = 0f;

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.1f);
        this.EnableHitbox(2);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    public void Explode()
    {
        if (!this.hasHitPlayer)
        {
            this.StopAllCoroutines();
            this.hasHitPlayer = true;

            if (this.mainCollider != null)
                this.mainCollider.enabled = false;

            this.StartCoroutine(this.ExplodeCoroutine());
        }

    }
    private IEnumerator ExplodeCoroutine()
    {
        this.EnableHitbox(1);

        if (this.model != null)
            this.model.gameObject.SetActive(false);

        if (this.electricityEffect1 != null)
            this.electricityEffect1.Stop();
        if (this.electricityEffect2 != null)
            this.electricityEffect2.Stop();

        this.speedMultiplier = 0f;

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.1f);
        this.EnableHitbox(2);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer user)
    {
        if (user != null)
        {
            this.owner = user;
            if (this.hitbox1 != null)
                this.hitbox1.belongsTo = user;
            if (this.hitbox2 != null)
                this.hitbox2.belongsTo = user;
        }
    }

    public void EnableHitbox(int id = 0)
    {
        if (id == 0) //enable hitbox1
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(true);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);
        }
        else if (id == 1) //enable hitbox2
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(true);
        }
        else //disable all hitboxes
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);
        }
    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(20f);
        if (!this.hasHitPlayer)
        {
            if (this.model != null)
                this.model.gameObject.SetActive(false);


            if (this.electricityEffect1 != null)
                this.electricityEffect1.Stop();

            if (this.electricityEffect2 != null)
                this.electricityEffect2.Stop();

            this.hasHitPlayer = true;

            if (this.mainCollider != null)
                this.mainCollider.enabled = false;

            this.speedMultiplier = 0f;

            this.EnableHitbox(2);

            yield return new WaitForSeconds(0.8f);
            this.gameObject.SetActive(false);
        }
    }


    private IEnumerator MoveToYCoroutine()
    {
        float currentTime = 0;
        float duration = 0.2f;
        float targetPosition = this.startY;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        this.transform.position = new Vector3(this.transform.position.x, this.startY, 0f);
        this.waveing = true;
    }
}
