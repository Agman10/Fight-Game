using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireBall : MonoBehaviour
{
    public Fire fire;
    public TestPlayer belongsTo;
    public float damage = 5f;

    public VisualEffect flame;
    public GameObject model;
    private Rigidbody rb;
    private Collider hitbox;

    [Space]
    public GameObject hitEffect;
    public float stun = 0f;
    public float horizontalKnockack = 0f;
    public float verticalKnockack = 0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();
        this.hitbox = GetComponent<Collider>();


        if (this.model != null)
            this.model.gameObject.SetActive(true);
        /*if (this.flame != null)
            this.flame.Stop();*/

        if (this.rb != null)
            this.rb.isKinematic = false;
        if (this.hitbox != null)
            this.hitbox.enabled = true;
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            //Debug.Log(knockback.magnitude);
            this.rb.AddForce(knockback);
            //this.rb.AddTorque(knockback);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.tag == "Reflector")
        {
            
            this.rb.AddForce(this.transform.forward.z * -1500, 200, 0);
            //this.transform.localEulerAngles = new Vector3(this.transform.rotation.x, this.transform.forward.z * this.transform.rotation.y, this.transform.rotation.z);
            //this.transform.localEulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y -1, this.transform.rotation.z);
            //this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y + 180, this.transform.rotation.z);
            //Debug.Log(this.transform.rotation);
            this.belongsTo = null;
            return;
        }*/

        TestPlayer player = other.GetComponent<TestPlayer>();
        TestHitbox hitbox = other.GetComponent<TestHitbox>();
        HeadStandingPrevention headStandingPrevention = other.GetComponent<HeadStandingPrevention>();

        Ball ball = other.GetComponent<Ball>();

        if (hitbox != null)
            return;

        if (headStandingPrevention != null)
            return;

        if(player == null /*|| this.belongsTo == null*/)
        {
            //this.gameObject.SetActive(false);
            this.Disable();

            if (this.fire != null)
            {
                Fire firePrefab = this.fire;

                float yPos = this.transform.position.y;
                if (this.transform.position.y < 0.5f)
                    yPos = 0.5f;

                firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, yPos, 0f), this.transform.rotation);
                if (this.belongsTo != null)
                    firePrefab.SetOwner(this.belongsTo);

            }

            if (ball != null)
            {
                ball.KnockBack(new Vector3(this.transform.forward.z * 100f, 100f, 0f));
            }
        }
        else
        {
            if(this.belongsTo == null || player != this.belongsTo)
            {
                //this.gameObject.SetActive(false);
                this.Disable();

                //player.TakeDamage(this.transform.position, this.damage);
                player.TakeDamage(this.transform.position, this.damage, this.stun, this.transform.forward.z * this.horizontalKnockack, this.verticalKnockack);
                //player.TakeDamage(this.transform.position, this.damage, 0.1f, this.transform.forward.z * 300f, 300f);
                player.OnHit?.Invoke();
                if (this.belongsTo != null)
                {
                    this.belongsTo.GiveSuperCharge(5f);
                    player.GiveSuperCharge(2.5f);

                    player.OnHitFromPlayer?.Invoke(this.belongsTo);
                }

                if (this.fire != null)
                {
                    Fire firePrefab = this.fire;

                    float yPos = this.transform.position.y;
                    if (this.transform.position.y < 0.5f)
                        yPos = 0.5f;

                    //firePrefab = Instantiate(firePrefab, new Vector3(player.transform.position.x, yPos, 0f), this.transform.rotation);
                    firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, yPos, 0f), this.transform.rotation);
                    if (this.belongsTo != null)
                        firePrefab.SetOwner(this.belongsTo);
                }
            }
        }
        
    }

    public void Disable()
    {
        if (this.model != null)
            this.model.gameObject.SetActive(false);
        if (this.flame != null)
            this.flame.Stop();

        if (this.rb != null)
            this.rb.isKinematic = true;
        if (this.hitbox != null)
            this.hitbox.enabled = false;


        if (this.hitEffect != null)
        {
            GameObject hitEffectPrefab = this.hitEffect;
            hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }

        this.StartCoroutine(this.DisableCoroutine());
    }

    public void SpawnFire()
    {
        this.Disable();

        if (this.fire != null)
        {
            Fire firePrefab = this.fire;

            float yPos = this.transform.position.y;
            if (this.transform.position.y < 0.5f)
                yPos = 0.5f;

            firePrefab = Instantiate(firePrefab, new Vector3(this.transform.position.x, yPos, 0f), this.transform.rotation);
            if (this.belongsTo != null)
                firePrefab.SetOwner(this.belongsTo);

        }
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }
}
