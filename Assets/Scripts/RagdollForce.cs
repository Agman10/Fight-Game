using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollForce : MonoBehaviour
{
    //private Vector3 position;

    public bool triggerOnCollision = true;
    public bool triggerOnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        //this.position = this.gameObject.GetComponent<Transform>().position;
        if (this.triggerOnStart)
        {
            this.TriggerExplosion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TestRagdoll ragdoll = other.GetComponent<TestRagdoll>();
        if(this.triggerOnCollision && ragdoll != null && ragdoll.rb != null)
        {
            ragdoll.rb.AddExplosionForce(200000, this.transform.position, 10);
            Debug.Log("explode");
        }
        /*if(other is TestRagdoll ragdoll && ragdoll != null)

        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddExplosionForce(10000, this.position, 10);
            Debug.Log("explode");
        }*/
            
    }
    [ContextMenu("TriggerExplosion")]
    public void TriggerExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10);
        foreach(Collider hit in colliders)
        {
            TestRagdoll ragdoll = hit.GetComponent<TestRagdoll>();

            if(ragdoll != null && ragdoll.rb != null)
            {
                ragdoll.rb.AddExplosionForce(200000, this.transform.position, 10);
                Debug.Log("explode2");
            }
        }
    }
}
