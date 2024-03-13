using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectTransformOnEnable : MonoBehaviour
{
    public Transform objectTransform;
    public Vector3 newPosition;
    private Vector3 oldPosition;
    private void OnEnable()
    {
        if (this.objectTransform != null)
        {
            this.oldPosition = this.objectTransform.position;

            this.objectTransform.position = this.newPosition;
        }
            
    }

    private void OnDisable()
    {
        if (this.objectTransform != null)
        {
            this.objectTransform.position = this.oldPosition;
        }
    }
}
