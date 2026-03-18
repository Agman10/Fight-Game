using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBowlingBall : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 torque;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (this.rb != null)
        {
            this.rb.AddTorque(this.torque);
            this.rb.AddForce(this.force);
        }
            
    }
}
