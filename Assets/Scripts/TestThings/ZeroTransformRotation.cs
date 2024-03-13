using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroTransformRotation : MonoBehaviour
{
    public Transform transformObject;

    public bool notLocal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(this.transformObject != null)
        {
            this.transform.eulerAngles = new Vector3(this.transformObject.localRotation.x, 0f, this.transformObject.localRotation.x);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, 0f, this.transform.rotation.z);
        }*/

        
        //this.transform.eulerAngles = new Vector3(this.transform.rotation.x, 0f, this.transform.rotation.z);
        if(this.notLocal)
            this.transform.eulerAngles = new Vector3(0f, this.transform.rotation.y, 0f);
        else
            this.transform.localEulerAngles = new Vector3(0f, this.transform.rotation.y, 0f);

        //this.transform.localEulerAngles = new Vector3(0f, this.transformObject.rotation.y, 0f);
        //this.transform.localEulerAngles = new Vector3(0f, this.transformObject.localRotation.y, 0f);
    }
}
