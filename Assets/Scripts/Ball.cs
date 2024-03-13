using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public MeshRenderer ballRenderer;
    public Material[] mats;
    public int currentBall;

    private Rigidbody rb;
    public Action<bool> OnGoal;

    private void Start()
    {
        float number = UnityEngine.Random.Range(1, 101);
        if (number <= 90)
            this.SetMaterial(0);
        else
            this.SetMaterial(1);
    }
    private void OnEnable()
    {
        this.rb = GetComponent<Rigidbody>();
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0, 4, 0);
    }
    private void OnDisable()
    {
        if (this.rb != null)
        {
            this.rb.velocity = Vector3.zero;
            this.rb.angularVelocity = Vector3.zero;
        }
        /*this.transform.eulerAngles = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0, 4, 0);*/
    }

    public void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            //Debug.Log(knockback.magnitude);
            this.rb.AddForce(knockback * 2f);
            //this.rb.AddTorque(knockback);
            this.rb.AddTorque(new Vector3(1000, 1000, 1000));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Goal")
        {

            /*if (this.rb != null)
            {
                this.rb.velocity = Vector3.zero;
                this.rb.angularVelocity = Vector3.zero;
            }*/
            this.gameObject.SetActive(false);
            //this.OnGoal?.Invoke(this.transform.position.x >= 0f);
            this.OnGoal?.Invoke(false);
            

            /*this.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.position = new Vector3(0, 4, 0);*/

        }
        if (collision.collider.tag == "Goal2")
        {
            this.gameObject.SetActive(false);
            this.OnGoal?.Invoke(true);

        }
    }
    public void SetMaterial(int colorId)
    {
        if (this.mats.Length >= colorId + 1 && this.mats[colorId] != null && this.ballRenderer != null)
        {
            this.ballRenderer.material = this.mats[colorId];
            this.currentBall = colorId;

        }
    }
}
