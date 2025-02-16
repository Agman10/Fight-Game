using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.forward.z);
    }
}
