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

    public bool cameraIsLocked = false;


    public float p1ExtraWidth = 0f;
    public float p2ExtraWidth = 0f;

    private void Awake()
    {
        this.mainCamera = this.GetComponent<Camera>();
    }

    void Update()
    {
        if (!this.lockCamera && !this.cameraIsLocked)
        {
            if (this.player1 != null && this.player2 != null)
            {
                this.center = ((this.player2.position - this.player1.position) / 2.0f) + this.player1.position;

                //TRY TO FIX THE JITTERING WHEN PASSING EACHOTHER ATLEAST WHEN ITS A RAGDOLL

                float centerX = (((this.player2.position.x + this.p2ExtraWidth) - (this.player1.position.x + this.p1ExtraWidth)) / 2.0f) + this.player1.position.x /*(this.player1.position.x + this.p1ExtraWidth)*/;
                //float centerX = (((this.player1.position.x + this.p1ExtraWidth) - (this.player2.position.x + this.p2ExtraWidth)) / 2.0f) + this.player2.position.x /*(this.player1.position.x + this.p1ExtraWidth)*/;
                if (this.player1.position.x > this.player2.position.x)
                    centerX = (((this.player1.position.x + this.p1ExtraWidth) - (this.player2.position.x + this.p2ExtraWidth)) / 2.0f) + this.player2.position.x;

                //float centerX = (((this.player2.position.x + 0.15f) - (this.player1.position.x + 0.15f)) / 2.0f) + this.player1.position.x /*(this.player1.position.x + 0.15f)*/;

                //this.posX = Mathf.Clamp(this.center.x, -this.maxX, this.maxX);
                this.posX = Mathf.Clamp(centerX, -this.maxX, this.maxX);
                //if(this.posX.)

                //this.transform.position = new Vector3(this.center.x, this.transform.position.y, this.transform.position.z);
                this.transform.position = new Vector3(this.posX, this.transform.position.y, this.transform.position.z);
                //Debug.Log(this.center);
            }
        }
        
        
        //transform.LookAt(center);
    }

}
