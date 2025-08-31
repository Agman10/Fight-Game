using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectTransformOnEnable : MonoBehaviour
{
    public Transform objectTransform;
    public Vector3 newPosition;
    private Vector3 oldPosition;

    public bool changeRotation;
    public Vector3 newRotation;
    private Vector3 oldRotation;
    private void OnEnable()
    {
        if (this.objectTransform != null)
        {
            this.oldPosition = this.objectTransform.position;

            this.objectTransform.position = this.newPosition;

            if (this.changeRotation)
            {
                this.oldRotation = this.objectTransform.localEulerAngles;

                this.objectTransform.localEulerAngles = this.newRotation;
            }
        }
            
    }

    private void OnDisable()
    {
        if (this.objectTransform != null)
        {
            this.objectTransform.position = this.oldPosition;

            if (this.changeRotation)
            {
                this.objectTransform.localEulerAngles = this.oldRotation;
            }
        }
    }
}
