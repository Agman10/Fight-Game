using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFollowTransform : MonoBehaviour
{
    public Transform target;
    public float delay = 0.1f;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (this.target != null)
        {
            this.Execute();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this.target != null)
        {
            //this.transform.position = this.target.transform.position;
            //this.transform.Translate(this.target.position);
            //this.Execute();
            //this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, Time.deltaTime * this.speed);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, this.speed);
        }
            
    }

    void Execute()
    {

        //Vector3 movementVector = Vector3.MoveTowards(this.transform.position, target.position, Time.deltaTime);
        Vector3 movementVector = this.target.position;
        //this.transform.position = this.target.transform.position;
        StartCoroutine(MoveTorwards(movementVector, this.delay));

    }

    public IEnumerator MoveTorwards(Vector3 movementVector, float delay)
    {
        yield return new WaitForSeconds(delay);
        this.transform.position = movementVector;
        this.Execute();
    }
}
