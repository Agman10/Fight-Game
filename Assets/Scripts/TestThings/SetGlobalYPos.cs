using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlobalYPos : MonoBehaviour
{
    public float yPos = 0f;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.yPos, this.transform.position.z);
    }
}
