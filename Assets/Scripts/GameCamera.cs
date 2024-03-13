using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player1;
    public Transform player2;
    public float maxX = 6.5f;
    //[Range(-8, 8)]
    public float posX;
    public Vector3 center;
    public bool lockCamera = false;

    private void Awake()
    {
        this.mainCamera = this.GetComponent<Camera>();
    }

    void Update()
    {
        if (!this.lockCamera)
        {
            if (this.player1 != null && this.player2 != null)
            {
                this.center = ((this.player2.position - this.player1.position) / 2.0f) + this.player1.position;

                float centerX = ((this.player2.position.x - (this.player1.position.x + 0.15f)) / 2.0f) + this.player1.position.x /*(this.player1.position.x + 0.15f)*/;

                //float centerX = (((this.player2.position.x + 0.15f) - (this.player1.position.x + 0.15f)) / 2.0f) + this.player1.position.x /*(this.player1.position.x + 0.15f)*/;

                this.posX = Mathf.Clamp(this.center.x, -this.maxX, this.maxX);
                //if(this.posX.)

                //this.transform.position = new Vector3(this.center.x, this.transform.position.y, this.transform.position.z);
                this.transform.position = new Vector3(this.posX, this.transform.position.y, this.transform.position.z);
                //Debug.Log(this.center);
            }
        }
        
        
        //transform.LookAt(center);
    }

}
