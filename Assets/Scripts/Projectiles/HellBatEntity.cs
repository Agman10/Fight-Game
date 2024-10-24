using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBatEntity : DamagableEntity
{
    public GameObject model;

    public Transform wingRight;
    public Transform wingLeft;

    public Collider collison;

    private float testTime;

    public GameObject eye1;
    public GameObject eye2;
    public GameObject xEye;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnEnable()
    {
        base.OnEnable();

        this.collison = GetComponent<Collider>();

        int number = Random.Range(0, 2);

        if (number == 1)
            this.SetEye(1);

        if (this.collison != null)
            this.collison.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * 10f);
        float newY2 = Mathf.Sin(Time.time * 30f);

        if (!this.dead)
        {
            if (this.model != null)
                this.model.transform.localPosition = new Vector3(this.model.transform.localPosition.x, newY * 0.07f, this.model.transform.localPosition.z);

            if (this.wingLeft != null)
                this.wingLeft.localEulerAngles = new Vector3(this.wingLeft.localEulerAngles.x, this.wingLeft.localEulerAngles.y, newY2 * 10);

            if (this.wingRight != null)
                this.wingRight.localEulerAngles = new Vector3(this.wingRight.localEulerAngles.x, this.wingRight.localEulerAngles.y, newY2 * -10);


            //this.transform.Translate(new Vector3(5f * Time.deltaTime, 0f, 0f));
        }
        else
        {
            /*this.testTime += Time.deltaTime * 1000f;

            if (this.model != null)
            {
                this.model.transform.localPosition = new Vector3(this.model.transform.localPosition.x, 0f, this.model.transform.localPosition.z);
                
                this.model.transform.localEulerAngles = new Vector3(this.testTime, this.transform.forward.z * 90f, 0f);
                //this.model.transform.localEulerAngles = new Vector3(0f, 0f, this.testTime);

                //this.model.transform.Rotate(1000f * Time.deltaTime, 0f, 1000f * Time.deltaTime);
            }
            //Debug.Log(this.transform.forward.z);
            this.transform.Translate(new Vector3(-25f * Time.deltaTime, 10f * Time.deltaTime, 0f));*/

        }

        
    }

    public override void Death()
    {
        base.Death();

        /*if (this.collison != null)
            this.collison.enabled = false;*/
    }

    private IEnumerator DieCoroutine(bool forward = false)
    {
        float testTimee = 0f;

        float time = 2f;

        float dir = this.transform.forward.z * -1f;
        if (forward)
            dir = this.transform.forward.z * 1f;

        while (time > 0)
        {
            time -= Time.deltaTime;
            testTimee += Time.deltaTime * 1000f;

            if (this.model != null)
            {
                this.model.transform.localPosition = new Vector3(this.model.transform.localPosition.x, 0f, this.model.transform.localPosition.z);

                this.model.transform.localEulerAngles = new Vector3(testTimee, this.transform.forward.z * 90f, 0f);
                //this.model.transform.localEulerAngles = new Vector3(0f, 0f, this.testTime);

                //this.model.transform.Rotate(1000f * Time.deltaTime, 0f, 1000f * Time.deltaTime);
            }

            this.transform.Translate(new Vector3(dir * 25f * Time.deltaTime, 10f * Time.deltaTime, 0f));

            yield return null;
        }

        this.gameObject.SetActive(false);
        //yield return new WaitForSeconds(1f);
    }

    public override void Die(Vector3 position)
    {
        base.Die(position);

        if (this.collison != null)
            this.collison.enabled = false;

        bool forward = false;

        this.SetEye(-1);

        if (position.x < this.transform.position.x)
        {
            forward = true;
            //if(this.transform.forward.z <)
        }
        else
        {
            forward = false;
        }
            

        Debug.Log(this.transform.forward.z);

        this.StartCoroutine(this.DieCoroutine(forward));
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        TestPlayer player = collision.collider.GetComponent<TestPlayer>();

        if (player != null && player != this.owner)
        {
            Debug.Log("test");
            player.TakeDamage(this.transform.position, 5, 0.2f, this.transform.forward.z * 300f, 300f);
        }
    }*/

    public void SetEye(int eyeId)
    {
        if(eyeId == 0)
        {
            if (this.eye1 != null)
                this.eye1.SetActive(true);

            if (this.eye2 != null)
                this.eye2.SetActive(false);

            if (this.xEye != null)
                this.xEye.SetActive(false);
        }
        else if (eyeId == 1)
        {
            if (this.eye1 != null)
                this.eye1.SetActive(false);

            if (this.eye2 != null)
                this.eye2.SetActive(true);

            if (this.xEye != null)
                this.xEye.SetActive(false);
        }
        else
        {
            if (this.eye1 != null)
                this.eye1.SetActive(false);

            if (this.eye2 != null)
                this.eye2.SetActive(false);

            if (this.xEye != null)
                this.xEye.SetActive(true);
        }
    }
}
