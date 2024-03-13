using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlobalZPos : MonoBehaviour
{
    public float zPos = 0f;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zPos);
    }
}
