using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ArmageddonMeteor : MonoBehaviour
{
    public TestPlayer owner;

    public Vector2 direction;

    public bool broken = false;

    public VisualEffect flame;
    public GameObject model;

    public GameObject impactEffect;
    public GameObject impactEffectAir;

    private Collider mainCollider;
    public TestHitbox hitbox1;
    public TestHitbox hitbox2;
    public TestHitbox groundHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.mainCollider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!this.broken)
            this.transform.Translate(this.transform.forward.z * this.direction.x * Time.deltaTime, this.direction.y * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        BigFireBall bigFireBall = other.GetComponent<BigFireBall>();

        Ball ball = other.GetComponent<Ball>();

        ArmageddonMeteor meteor = other.GetComponent<ArmageddonMeteor>();

        /*if (player != null && !player.dead)
        {
            if (this.owner != player)
            {
                //this.OnPlayerCollision?.Invoke();
                this.Explode();
            }
        }*/

        /*if (bigFireBall != null && bigFireBall.owner != this.owner)
        {
            //this.StartCoroutine(this.ExplodeCoroutine());
            this.Explode();
            //bigFireBall.Explode();
        }*/

        /*if (ball != null)
        {
            ball.KnockBack(new Vector3(this.transform.forward.z * 350f, 300f, 0f));
        }*/
        //Debug.Log(other.tag);

        if(meteor != null && meteor.owner != this.owner)
        {
            this.Explode(false);
            meteor.Explode(false);
        }

        /*if (other.tag == "Wall" || other.tag == "Ground")
        {
            //Debug.Log("Test");
            this.Explode();
        }*/

        if (other.tag == "Wall")
        {
            //Debug.Log("Test");
            this.Explode(false);
        }

        if (other.tag == "Ground")
        {
            //Debug.Log("Test");
            this.Explode(true);
        }


        /*if (this.transform.position.y <= 1f) //REMOVE THIS WHEN ADDING GROUND TAG ON ALL STAGES
            this.Explode(true);*/


    }

    public void Explode(bool grounded = true)
    {
        if (!this.broken)
        {
            this.broken = true;
            if (this.mainCollider != null)
                this.mainCollider.enabled = false;

            this.StartCoroutine(this.ExplodeCoroutine(grounded));
        }
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

            if (this.groundHitbox != null)
                this.groundHitbox.belongsTo = user;
        }
    }

    private IEnumerator ExplodeCoroutine(bool grounded = true)
    {
        //this.EnableHitbox(1);

        /*if (this.hitbox1 != null)
            this.hitbox1.gameObject.SetActive(false);*/

        if (grounded)
        {
            if (this.hitbox2 != null)
                this.hitbox2.transform.position = new Vector3(this.transform.position.x, 0f, 0f);

            this.EnableHitbox(2);
        }
        else
        {
            this.EnableHitbox(1);
        }
        

        if (this.model != null)
            this.model.gameObject.SetActive(false);
        if (this.flame != null)
            this.flame.Stop();

        //this.speedMultiplier = 0f;
        this.broken = true;

        /*if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }*/

        if (grounded)
        {
            if (this.impactEffect != null)
            {
                this.impactEffect.SetActive(true);

                if (grounded)
                    this.impactEffect.transform.position = new Vector3(this.transform.position.x, 0.02f, 0f);
                /*GameObject impactPrefab = this.impactEffect;
                impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));*/
            }
        }
        else
        {
            if (this.impactEffectAir != null)
            {
                this.impactEffectAir.SetActive(true);
                /*GameObject impactPrefab = this.impactEffect;
                impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));*/
            }
        }

        /*if (this.impactEffect != null)
        {
            this.impactEffect.SetActive(true);

            if (grounded)
                this.impactEffect.transform.position = new Vector3(this.transform.position.x, 0.02f, 0f);
            //GameObject impactPrefab = this.impactEffect;
            //impactPrefab = Instantiate(impactPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.Euler(0, 0, 0));
        }*/

        yield return new WaitForSeconds(0.1f);
        this.EnableHitbox(3);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
    }

    public void EnableHitbox(int id = 0)
    {
        if (id == 0) //enable hitbox1
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(true);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);

            if (this.groundHitbox != null)
                this.groundHitbox.gameObject.SetActive(false);
        }
        else if (id == 1) //enable hitbox2
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(true);

            if (this.groundHitbox != null)
                this.groundHitbox.gameObject.SetActive(false);
        }
        else if (id == 2) //enable hitbox2
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(true);

            if (this.groundHitbox != null)
                this.groundHitbox.gameObject.SetActive(true);
        }
        else //disable all hitboxes
        {
            if (this.hitbox1 != null)
                this.hitbox1.gameObject.SetActive(false);
            if (this.hitbox2 != null)
                this.hitbox2.gameObject.SetActive(false);

            if (this.groundHitbox != null)
                this.groundHitbox.gameObject.SetActive(false);
        }
    }
}
