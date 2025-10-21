using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : KickableEntity
{
    //private Rigidbody rb;
    public GameObject model;


    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.rb = this.GetComponent<Rigidbody>();

        if (this.rb != null)
            this.rb.isKinematic = false;

        if (this.model != null)
            this.model.SetActive(true);

        this.StartCoroutine(this.SmokeCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.rb != null)
            this.rb.isKinematic = false;

        if (this.model != null)
            this.model.SetActive(true);
    }

    private IEnumerator SmokeCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if(this.rb != null)
        {
            /*while (this.rb.velocity.magnitude >= 0.05f)
            {
                yield return null;
            }*/

            while (Mathf.Abs(this.rb.velocity.y) > 0f)
            {
                yield return null;
            }
            this.rb.isKinematic = true;
        }
    }

    public override void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            this.rb.AddForce(knockback);
            this.rb.AddTorque(new Vector3(1200, 1200, 1200));
        }
    }


    /*public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            //Debug.Log(knockback.magnitude);
            this.rb.AddForce(knockback);
            //this.rb.AddTorque(knockback);
            this.rb.AddTorque(new Vector3(1000, 1000, 1000));
        }
    }*/
}
