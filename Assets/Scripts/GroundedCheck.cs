using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public TestPlayer player;

    public bool triggerOnGround;
    //public bool collisionOnGround;

    public CapsuleCollider collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.collision != null && this.player != null && this.player.collision != null && this.player.collision is CapsuleCollider playerCollider)
        {
            this.collision.center = new Vector3(playerCollider.center.x * 0.5f, playerCollider.center.y * 0.5f, playerCollider.center.z * 0.5f);
            this.collision.radius = playerCollider.radius * 0.5f;
            this.collision.height = playerCollider.height * 0.5f;
        }

        if (this.player != null)
        {
            if (this.triggerOnGround)
                this.player.grounded = true;
            else
                this.player.grounded = false;
        }

        //this.CheckGround();
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            this.triggerOnGround = true;
        }
        else
        {
            this.triggerOnGround = false;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            this.triggerOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            this.triggerOnGround = false;
        }
    }

    public void CheckGround()
    {
        if(this.player != null)
        {
            RaycastHit hit;
            float distance = 1f;
            Vector3 dir = new Vector3(0f, -1f, 0f);

            Vector3 positionOffset = new Vector3(0f, 0f, 0f);

            if (Physics.SphereCast(this.transform.position + positionOffset, 0.1f, -this.transform.up, out hit)/*Physics.SphereCast(this.transform.position, 0.5f, -transform.up, out RaycastHit hit, 0.6f)*/)
            {
                if(!hit.collider.CompareTag("Ground")/*hit.collider.tag == "Ground"*/)
                {
                    Debug.Log("ground");
                    this.player.grounded = true;
                }
                else
                {
                    Debug.Log("not");
                    this.player.grounded = false;
                }

                //this.player.grounded = true;
            }
            else
            {
                Debug.Log("not");
                this.player.grounded = false;

                /*Debug.Log("not");
                this.player.grounded = false;*/
            }
        }

        

    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1);
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.collisionOnGround = true;
        }
    }*/

    /*private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            this.collisionOnGround = false;
        }
    }*/
}
