using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float speed = 5f;
    //adjust this to change how high it goes
    public float height = 0.5f;
    private Vector3 startPos;
    public bool local = false;

    /*private void Awake()
    {
        this.startPos = this.transform.position;
    }*/
    private void OnEnable()
    {
        if (this.local)
            this.startPos = this.transform.localPosition;
        else
            this.startPos = this.transform.position;
    }
    private void OnDisable()
    {
        //this.transform.position = this.startPos;
        this.transform.position = new Vector3(this.transform.position.x, this.startPos.y, this.transform.position.z);
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * this.speed);
        //float newY = Mathf.Cos(Time.time * this.speed);

        //this.transform.position = new Vector3(this.startPos.x, this.startPos.y + (newY * this.height), this.startPos.z);
        if(this.local)
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.startPos.y + (newY * this.height), this.transform.localPosition.z);
        else
            this.transform.position = new Vector3(this.transform.position.x, this.startPos.y + (newY * this.height), this.transform.position.z);
    }
}
