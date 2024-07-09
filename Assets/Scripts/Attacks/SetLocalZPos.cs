using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocalZPos : MonoBehaviour
{
    public float zPos = 0f;

    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.forward.z * this.zPos);
    }
}
