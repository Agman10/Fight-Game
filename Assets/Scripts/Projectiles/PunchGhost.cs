using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGhost : MonoBehaviour
{
    public TestPlayer owner;
    public Vector3 direction;
    public float speedMultiplier = 1;

    public Collider colliderCheck;

    public bool stay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.stay)
            this.transform.Translate(this.direction * Time.deltaTime * this.speedMultiplier);

        //Debug.Log(this.colliderCheck)

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(this.transform.position.x + this.transform.forward.z, this.transform.position.y + 1f, 0f), this.transform.TransformDirection(1f, 0f, 0f), out hit, 0.2f))

        {
            Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0f), this.transform.TransformDirection(1f, 0f, 0f) * hit.distance, Color.yellow);

            TestPlayer player = hit.transform.gameObject.GetComponent<TestPlayer>();

            if (player != null && !player.dead && player != this.owner)
            {
                Debug.Log("Did Hit");
                this.stay = true;
            }
            else
            {
                this.stay = false;
            }



            /*if (hit.transform.gameObject.GetComponent<TestPlayer>() != null)
            {
                Debug.Log("Did Hit");
                this.stay = true;
            }
            else
            {
                this.stay = false;
            }*/
            //Debug.Log("Did Hit");
        }
        else
        {
            this.stay = false;
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = true;
            //Debug.Log("test");
        }
        else
        {
            this.stay = false;

            //Debug.Log("test2");
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = true;
            Debug.Log("test");
        }
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && !player.dead && player != this.owner)
        {
            this.stay = false;
            //Debug.Log("test");
        }
    }*/
}
