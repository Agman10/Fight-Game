using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRagdoll : MonoBehaviour
{
    public Rigidbody rb;
    public bool canBeAttacked = false;
    private Rigidbody[] rigRigidbodies;
    private Collider[] colliders;

    public Transform rightArm, rightArmJoint, leftArm, leftArmJoint, rightLeg, rightLegJoint, leftLeg, leftLegJoint;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        this.colliders = GetComponentsInChildren<Collider>();
        this.StopRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockBack(Vector3 knockback)
    {
        if(this.rb != null && this.canBeAttacked)
        {
            this.rb.AddForce(knockback);
        }
    }

    [ContextMenu("StopRagdoll")]
    public void StopRagdoll()
    {
        if(this.rigRigidbodies.Length > 0)
        {
            foreach (Rigidbody rigidbody in this.rigRigidbodies)
            {
                rigidbody.isKinematic = true;
                /*rigidbody.ResetCenterOfMass();
                rigidbody.ResetInertiaTensor();*/
            }
        }
        
        if(this.colliders.Length > 0)
        {
            foreach (Collider collider in this.colliders)
            {
                collider.enabled = false;
            }
        }
        this.canBeAttacked = false;
        
    }

    [ContextMenu("EnableRagdoll")]
    public void EnableRagdoll()
    {
        //this.SetTPose();
        if (this.rigRigidbodies.Length > 0)
        {
            foreach (Rigidbody rigidbody in this.rigRigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        if (this.colliders.Length > 0)
        {
            foreach (Collider collider in this.colliders)
            {
                collider.enabled = true;
            }
        }
        this.StartCoroutine(this.EnablePunches());
        //this.canBeAttacked = true;
    }

    [ContextMenu("EnableRagdollWithExplosion")]
    public void EnableRagdollWithExplosion()
    {
        //this.SetTPose();
        if (this.rigRigidbodies.Length > 0)
        {
            foreach (Rigidbody rigidbody in this.rigRigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        if (this.colliders.Length > 0)
        {
            foreach (Collider collider in this.colliders)
            {
                collider.enabled = true;
            }
        }
        this.RagdollForce(new Vector3(this.transform.position.x, this.transform.position.y - 1.25f, this.transform.position.z - 1f), 200000f, 10f);
        /*if (this.rb != null)
        {
            this.rb.AddExplosionForce(200000, new Vector3(this.transform.position.x, this.transform.position.y -1.25f, this.transform.position.z -1f), 10);
        }*/

        this.StartCoroutine(this.EnablePunches());
    }
    [ContextMenu("SetTPose")]
    public void SetTPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    IEnumerator EnablePunches()
    {
        yield return new WaitForSeconds(0.3f);
        this.canBeAttacked = true;
    }

    public void RagdollForce(Vector3 position, float force = 170000, float radius = 10)
    {
        if (this.rb != null)
        {
            this.rb.AddExplosionForce(force, position, radius);
            //this.rb.AddExplosionForce(force * 0.1f, position, radius);
        }
    }
}
