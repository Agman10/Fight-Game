using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private TestPlayer player;
    public PlayerInput playerInput;

    public float moveSpeed = 10;
    public float jumpForce = 300;

    private float testTime = 0f;
    private float test;
    /*private float maxSpeed = 10;
    private float cappedYVelocity;*/
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.player = GetComponent<TestPlayer>();
        /*if(this.playerInput != null)
        {
            this.playerInput.JumpInput += this.Jump;
        }*/


    }

    private void Update()
    {
        
        if (this.player != null && !this.player.dead)
        {
            if (this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0 && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                if (this.playerInput != null && this.playerInput.moveInput.x != 0)
                {
                    //this.testTime += Time.deltaTime;
                    this.testTime += (this.player.transform.forward.z * this.playerInput.moveInput.x) * Time.deltaTime;

                    float newY = Mathf.Sin(this.testTime * this.moveSpeed * 1.5f);

                    if (this.player.animations != null)
                    {

                        this.player.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, newY * -40);
                        this.player.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, newY * 40);

                        this.player.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, newY * 30);
                        this.player.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, newY * -30);

                        //this.player.animations.eyes.localEulerAngles = new Vector3(0f, newY * 5, 0f);
                    }
                    this.test = 1f;
                }
                else if (this.player.animations != null)
                {
                    this.test -= Time.deltaTime;
                    if (this.test > 0.1f)
                        this.player.animations.SetDefaultPose();

                    this.testTime = 0f;

                    /*if (this.test <= 0.1f)
                    {
                        this.player.animations.SetDefaultPose();
                        this.testTime = 0f;
                    }*/



                }


            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move();

        if(this.player != null && !this.player.dead)
        {
            if (this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
            {
                this.Jump(this.playerInput.jumping);
                if (this.rb != null)
                {
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                    {
                        this.Move(this.playerInput.moveInput);
                    }
                    else
                    {
                        this.rb.velocity = new Vector3(this.rb.velocity.x / 1.01f, this.rb.velocity.y - 0.4f, 0);
                    }
                }
            }
            else
            {
                //this.Move(Vector3.zero);
                //this.rb.velocity = Vector3.zero;
                if (this.rb != null)
                {
                    //RIGHT NOW THIS SLOWS DOWN SOME ATTACKS BUT THIS SHOULD BE IMPLEMENTED SO STUNNED PLAYERS DONT GLIDE WHILE ON GROUND <-ignore this for now
                    
                    //implement a better way to do this!!
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f && this.player.attackStuns.Count <= 0)
                    {
                        this.rb.velocity = new Vector3(this.rb.velocity.x / 1.5f, this.rb.velocity.y - 0.4f, 0);
                    }
                    else
                    {
                        this.rb.velocity = new Vector3(this.rb.velocity.x / 1.01f, this.rb.velocity.y - 0.4f, 0);
                    }


                    //this.rb.velocity = new Vector3(this.rb.velocity.x / 1.01f, this.rb.velocity.y - 0.4f, 0);



                    /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                        this.rb.velocity = new Vector3(0, 0, 0);
                    else
                        this.rb.velocity = new Vector3(this.rb.velocity.x / 1.01f, this.rb.velocity.y - 0.4f, 0);*/
                }
            }

            //cappedYVelocity = Mathf.Min(Mathf.Abs(rb.velocity.y), 15) * Mathf.Sign(rb.velocity.y);
            //Debug.Log(cappedYVelocity);
            //Debug.Log(Mathf.Abs(this.rb.velocity.y));

            //Debug.Log(this.rb.velocity.magnitude);
            //Debug.Log(this.rb.velocity.normalized);
            /*if (this.rb.velocity.magnitude > this.maxSpeed)
            {
                this.rb.velocity = this.rb.velocity.normalized * this.maxSpeed;
            }*/
        }


    }
    public void Move(Vector3 moveInput)
    {
        if(this.rb != null && this.playerInput != null)
        {
            //this.rb.maxAngularVelocity = 10f;
            //this.rb.max

            //Vector3 movement = this.playerInput.moveInput * this.moveSpeed;
            Vector3 movement = moveInput * this.moveSpeed;

            //this.rb.AddForce(movement);

            //this.rb.velocity = new Vector3(movement.x, -19, 0);

            if(Mathf.Abs(movement.x) > 0)
            {
                this.rb.velocity = new Vector3(movement.x, this.rb.velocity.y, 0);
            }
            else
            {
                float velocityX = this.rb.velocity.x / 1.5f;
                if (Mathf.Abs(velocityX) <= 0.1f)
                    velocityX = 0f;
                this.rb.velocity = new Vector3(velocityX, this.rb.velocity.y, 0);
            }


            //this.rb.velocity = movement;
            //this.rb.velocity.x = movement.x;
        }
        
    }
    //[ContextMenu("Jump")]
    public void Jump(bool jumping)
    {
        if (jumping && this.rb != null && Mathf.Abs(this.rb.velocity.y) <= 0f)
        {
            this.rb.AddForce(new Vector3(0, this.jumpForce, 0));
            //this.rb.velocity = new Vector3(this.rb.velocity.x / 5, this.rb.velocity.y, 0);
            //Debug.Log("jump");

            /*if (this.player.animations != null)
            {
                *//*this.player.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, -40);
                this.player.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, 40);

                this.player.animations.rightArm.localEulerAngles = new Vector3(170f, 0f, 0f);
                this.player.animations.leftArm.localEulerAngles = new Vector3(-170f, 0f, 0f);*//*



                this.player.animations.rightArm.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.player.animations.leftArm.localEulerAngles = new Vector3(-10f, 0f, 0f);

                this.player.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10);
                this.player.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10);
            }*/

            /*if (this.player.animations != null)
            {
                this.player.animations.Jump();
            }*/
        }
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && this.player.attackStuns.Count >= 1)
        {
            if (this.rb != null)
            {
                //this.rb.velocity = new Vector3(this.rb.velocity.x / 20f, this.rb.velocity.y, 0);
                this.rb.velocity = new Vector3(0f, this.rb.velocity.y, 0f);
            }
        }
    }*/

    [ContextMenu("AddKnockback")]
    public void AddKnockback()
    {
        player.AddStun(0.2f);
        this.rb.AddForce(-400, 800, 0);
    }
}
