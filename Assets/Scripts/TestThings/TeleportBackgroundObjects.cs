using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBackgroundObjects : MonoBehaviour
{
    public Transform xTransform;
    public Transform yTransform;
    public Transform zTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MoveToDirection moveToDirection = other.GetComponent<MoveToDirection>();

        if (moveToDirection != null)
        {
            if (this.xTransform != null)
                moveToDirection.transform.position = new Vector3(this.xTransform.position.x, moveToDirection.transform.position.y, moveToDirection.transform.position.z);
        }
    }

}
