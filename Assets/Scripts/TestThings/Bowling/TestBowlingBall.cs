using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBowlingBall : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 torque;
    public Vector3 force;

    public bool rolling;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        if(this.rb != null)
        {
            this.rb.maxAngularVelocity = 50f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.rolling && this.rb != null)
        {
            this.rb.AddForce(this.force);
            this.rb.AddTorque(this.torque);

            //this.transform.Rotate(this.torque, Space.World);
        }
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (this.rb != null)
        {
            //this.rb.AddTorque(this.torque);
            this.rb.AddRelativeTorque(this.torque);
            this.rb.AddForce(this.force, ForceMode.Impulse);
        }
            
    }

    [ContextMenu("ResetBall")]
    public void ResetBall()
    {
        if (this.rb != null)
        {
            this.rolling = false;
            this.rb.velocity = new Vector3(0f, 0f, 0f);
            this.rb.angularVelocity = new Vector3(0f, 0f, 0f);
            this.transform.position = new Vector3(0f, 1f, 0f);
            this.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        
    }
}
