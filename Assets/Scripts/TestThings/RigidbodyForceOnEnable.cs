using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForceOnEnable : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 rigidbodyForce;
    public Vector3 rbForce;
    public Vector3 rbTorque;
    public bool basedOnRotation = false;
    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if(this.rb != null)
        {
            //this.rb.AddForce(this.rigidbodyForce.x, this.rigidbodyForce.y, 0f);

            if (!this.basedOnRotation)
            {
                this.rb.AddForce(this.rbForce.x, this.rbForce.y, this.rbForce.z);
                this.rb.AddTorque(this.rbTorque.x, this.rbTorque.y, this.rbTorque.z);
            }
            else
            {
                this.rb.AddForce(this.transform.forward.z * this.rbForce.x, this.rbForce.y, /*this.transform.forward.z * */this.rbForce.z);
                this.rb.AddTorque(this.transform.forward.z * this.rbTorque.x, this.rbTorque.y, this.transform.forward.z * this.rbTorque.z);
            }
            
        }
    }
}
