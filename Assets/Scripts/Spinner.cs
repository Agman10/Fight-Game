using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float xSpeed, ySpeed, zSpeed;

    public float speedMultiplier = 1;


    void Update()
    {
        this.transform.Rotate(new Vector3(this.xSpeed * Time.deltaTime, this.ySpeed * Time.deltaTime, this.zSpeed * Time.deltaTime) * this.speedMultiplier);
    }
}