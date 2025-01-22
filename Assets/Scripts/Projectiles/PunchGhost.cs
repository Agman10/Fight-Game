using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PunchGhost : MonoBehaviour
{
    public TestPlayer owner;
    public Vector3 direction;
    public float speedMultiplier = 1;

    public TestHitbox punchHitbox;
    public TestHitbox hitbox2;

    //public Collider colliderCheck;

    public ParticleSystem smoke;
    public VisualEffect aura;

    public bool stay;
    public bool stopped;

    public LayerMask layerMask;

    public Transform bandana;

    public TempPlayerAnimations animations;
    public TempPlayerAnimations animations2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        /*if (this.animations != null && this.animations2 != null)
            this.StartCoroutine(this.TestOraOra());

        if (this.smoke != null)
            this.smoke.Play();*/


        this.Entrance();

        if (this.owner != null)
            this.SetOwner(this.owner);

        //this.StartCoroutine(this.DisableCoroutine(2f));
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.stay && !this.stopped)
            this.transform.Translate(this.direction * Time.deltaTime * this.speedMultiplier);

        //Debug.Log(this.colliderCheck)
        //bool dirRay1 = Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0f), this.transform.TransformDirection(1f, 0f, 0f), out hit, 1.2f);

        RaycastHit boxHit;
        bool testBoxCast = Physics.BoxCast(this.transform.position, new Vector3(1f, 1f, 1f), this.transform.forward, this.transform.rotation, 1f);

        if (
            Physics.BoxCast(new Vector3(this.transform.position.x - (this.transform.forward.z * 0.6f), this.transform.position.y, 0f),
            new Vector3(0.5f, 2.5f, 1f),
            this.transform.TransformDirection(1f, 0f, 0f),
            out boxHit,
            this.transform.rotation,
            1.6f,
            this.layerMask))
        {
            TestPlayer player = boxHit.transform.gameObject.GetComponent<TestPlayer>();

            if (player != null && !player.dead && player != this.owner)
            {
                //Debug.Log("Did Hit");
                this.stay = true;
            }
            else
            {
                this.stay = false;
            }
        }
        else
        {
            this.stay = false;
        }

        /*RaycastHit hit;
        if (Physics.Raycast(new Vector3(this.transform.position.x *//*+ this.transform.forward.z*//*, this.transform.position.y + 1f, 0f), this.transform.TransformDirection(1f, 0f, 0f), out hit, 1.2f))

        {
            Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0f), this.transform.TransformDirection(1f, 0f, 0f) * hit.distance, Color.yellow);

            TestPlayer player = hit.transform.gameObject.GetComponent<TestPlayer>();

            if (player != null && !player.dead && player != this.owner)
            {
                //Debug.Log("Did Hit");
                this.stay = true;
            }
            else
            {
                this.stay = false;
            }
        }
        else
        {
            this.stay = false;
        }*/
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.owner = player;

            if (this.punchHitbox != null)
                this.punchHitbox.belongsTo = player;

            if (this.hitbox2 != null)
                this.hitbox2.belongsTo = player;
        }
    }

    public void Entrance()
    {
        this.stopped = true;
        this.StartCoroutine(this.AppearCoroutine());
    }

    public void Stop()
    {
        if (!this.stopped)
        {
            this.StopAllCoroutines();
            this.stopped = true;

            this.StartCoroutine(this.DissapearCoroutine());
        }
        

    }

    private IEnumerator TestOraOra(/*bool startLeft = false*/)
    {
        if (this.animations != null)
        {
            float randomPunchZRange = 30f;
            float randomPunchYRange = 20f;

            float timeMultiplier = 0.5f;

            //Debug.Log(startLeft);

            this.animations.NewPunch(0, false, true);
            this.animations2.NewPunch(0, true);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, 2.5f, 0f);

            yield return new WaitForSeconds(0.025f * timeMultiplier);

            this.animations.NewPunch(5, false, true);
            this.animations2.NewPunch(5, true);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(0.01f * timeMultiplier);

            this.animations.NewPunch(4, false, true);
            this.animations2.NewPunch(4, true);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, 4f, 0f);

            //this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f, Random.Range(70f, 110f));

            //this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f, 90f + Random.Range(-randomPunchZRange, randomPunchZRange));


            //this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            /*if (!startLeft)
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            else
                this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));*/


            this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            this.animations2.leftArm.localEulerAngles = new Vector3(0f, -40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));

            yield return new WaitForSeconds(0.05f * timeMultiplier);

            /*this.animations.NewPunch(3);

            yield return new WaitForSeconds(0.02f);*/




            this.animations.NewPunch(0, true, true);
            this.animations2.NewPunch(0, false);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, -2.5f, 0f);

            yield return new WaitForSeconds(0.025f * timeMultiplier);

            this.animations.NewPunch(5, true, true);
            this.animations2.NewPunch(5, false);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, 0f, 0f);


            yield return new WaitForSeconds(0.01f * timeMultiplier);

            this.animations.NewPunch(4, true, true);
            this.animations2.NewPunch(4, false);

            if (this.bandana != null) this.bandana.localEulerAngles = new Vector3(0f, -4f, 0f);

            //this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f, Random.Range(70f, 110f));

            //this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f, 90f + Random.Range(-randomPunchZRange, randomPunchZRange));


            //this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            /*if (!startLeft)
                this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            else
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));*/

            this.animations.leftArm.localEulerAngles = new Vector3(0f, -40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));
            this.animations2.rightArm.localEulerAngles = new Vector3(0f, 40f + Random.Range(-randomPunchYRange, randomPunchYRange), 90f + Random.Range(-randomPunchZRange, randomPunchZRange));

            yield return new WaitForSeconds(0.05f * timeMultiplier);

            /* this.animations.NewPunch(3, true);

             yield return new WaitForSeconds(0.02f);*/


            //this.animations.SetDefaultPose();

            this.StartCoroutine(this.TestOraOra());
        }


    }


    private IEnumerator DissapearCoroutine()
    {
        //yield return new WaitForSeconds(1);
        if (this.smoke != null)
            this.smoke.Play();

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
        }

        if (this.animations2 != null)
        {
            this.animations2.gameObject.SetActive(false);
        }

        if (this.punchHitbox != null)
            this.punchHitbox.gameObject.SetActive(false);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(false);

        //yield return new WaitForSeconds(7f);

        if (this.aura != null)
            this.aura.Stop();


        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            //currentTime += Time.deltaTime;

            currentTime += Time.deltaTime;
            if(this.animations != null)
            {
                this.animations.body.localScale = new Vector3(Mathf.Lerp(1f, 0f, currentTime / duration), 1f, Mathf.Lerp(1f, 0f, currentTime / duration));
            }

            /*this.objectToScale.localScale = new Vector3(
                Mathf.Lerp(this.startScale.x, this.endScale.x, currentTime / duration),
                Mathf.Lerp(this.startScale.y, this.endScale.y, currentTime / duration),
                Mathf.Lerp(this.startScale.z, this.endScale.z, currentTime / duration));*/
            yield return null;
        }

        if (this.animations != null)
        {
            this.animations.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }


    private IEnumerator AppearCoroutine()
    {
        if (this.smoke != null)
            this.smoke.Play();

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.localScale = new Vector3(0f, 1f, 0f);
        }



        float currentTime = 0;
        float duration = 0.05f;
        while (currentTime < duration)
        {
            //currentTime += Time.deltaTime;

            currentTime += Time.deltaTime;
            if (this.animations != null)
            {
                this.animations.body.localScale = new Vector3(Mathf.Lerp(0f, 1f, currentTime / duration), 1f, Mathf.Lerp(0f, 1f, currentTime / duration));
            }

            /*this.objectToScale.localScale = new Vector3(
                Mathf.Lerp(this.startScale.x, this.endScale.x, currentTime / duration),
                Mathf.Lerp(this.startScale.y, this.endScale.y, currentTime / duration),
                Mathf.Lerp(this.startScale.z, this.endScale.z, currentTime / duration));*/
            yield return null;
        }
        this.animations.body.localScale = new Vector3(1f, 1f, 1f);

        if (this.punchHitbox != null)
            this.punchHitbox.gameObject.SetActive(true);

        if (this.hitbox2 != null)
            this.hitbox2.gameObject.SetActive(true);

        if (this.animations2 != null)
        {
            this.animations2.gameObject.SetActive(true);
        }

        this.stopped = false;

        if (this.animations != null && this.animations2 != null)
            this.StartCoroutine(this.TestOraOra());
    }

    private IEnumerator DisableCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        this.Stop();
    }

    /*private void OnTriggerStay(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = true;
            //Debug.Log("test");
        }
        else
        {
            this.stay = false;

            //Debug.Log("test2");
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = true;
            Debug.Log("test");
        }
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = false;
            //Debug.Log("test");
        }
    }*/
}
