using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoseLookAtCamera : MonoBehaviour
{
    public Transform camera;

    public float baseRotation = 90f;
    public float rotationMultiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.camera != null)
        {
            float distance = this.transform.position.x - this.camera.position.x;
            Debug.Log(distance);

            //this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, 90f + (distance / 1.7f), this.transform.localEulerAngles.z);

            //this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, 90f + (distance * 5), this.transform.localEulerAngles.z);
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.baseRotation + (distance * this.rotationMultiplier), this.transform.localEulerAngles.z);

            //this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, 60f + (distance * 4.29f), this.transform.localEulerAngles.z);

            //this.transform.position = new Vector3(distance * 0.1f, this.transform.position.y, this.transform.position.z);
        }
    }
}
