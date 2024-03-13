using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForceOnEnable : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 rigidbodyForce;
    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if(this.rb != null)
        {
            this.rb.AddForce(this.rigidbodyForce.x, this.rigidbodyForce.y, 0f);
        }
    }
}
