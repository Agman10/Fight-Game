using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetConstantZScale : MonoBehaviour
{
    public float zScale = 1f;
    public Transform originScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.originScale != null)
        {
            //this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, -this.originScale.localScale.z * -this.zScale);
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.originScale.transform.forward.z * this.zScale);
        }
            

        //this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, -this.transform.localScale.z * -this.zScale);
    }
}
