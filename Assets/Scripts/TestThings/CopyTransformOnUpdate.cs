using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransformOnUpdate : MonoBehaviour
{
    public Transform transformToCopy;
    void Update()
    {
        if(this.transformToCopy != null)
        {
            this.transform.localEulerAngles = new Vector3(this.transformToCopy.localEulerAngles.x, this.transformToCopy.localEulerAngles.y, this.transformToCopy.localEulerAngles.z);
            this.transform.localPosition = new Vector3(this.transformToCopy.localPosition.x, this.transformToCopy.localPosition.y, this.transformToCopy.localPosition.z);
            this.transform.localScale = new Vector3(this.transformToCopy.localScale.x, this.transformToCopy.localScale.y, this.transformToCopy.localScale.z);
        }
            
    }
}
