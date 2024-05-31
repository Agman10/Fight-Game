using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotationLerp : MonoBehaviour
{

    public float speed = 5f;

    public float height = 0.5f;

    private Vector3 startRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        this.startRotation = this.transform.localEulerAngles;
    }

    private void OnDisable()
    {
        //this.transform.position = this.startPos;
        this.transform.localEulerAngles = this.startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * this.speed);

        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.startRotation.z + (newY * this.height));
    }
}
