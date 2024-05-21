using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRigidBodyVelocity : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 rbVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rb != null)
        {
            this.rb.velocity = new Vector3(this.rb.velocity.x + this.rbVelocity.x, this.rb.velocity.y + this.rbVelocity.y, this.rb.velocity.z + this.rbVelocity.z);
        }
    }
}
