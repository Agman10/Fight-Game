using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationOnEnable : MonoBehaviour
{
    public Vector3 rotation1;
    public Vector3 rotation2;

    private void OnEnable()
    {
        this.transform.localEulerAngles = new Vector3(Random.Range(this.rotation1.x, rotation2.x), Random.Range(this.rotation1.y, rotation2.y), Random.Range(this.rotation1.z, rotation2.z));
    }
}
