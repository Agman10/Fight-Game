using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickableEntity : MonoBehaviour
{
    public Rigidbody rb;

    public virtual void KnockBack(Vector3 knockback)
    {
        if (this.rb != null && knockback.magnitude > 0f)
        {
            this.rb.AddForce(knockback);
            this.rb.AddTorque(new Vector3(1000, 1000, 1000));
        }
    }
}
