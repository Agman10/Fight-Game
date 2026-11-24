using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTestMovement : MonoBehaviour
{
    public PlayerInput input;
    float xPos;
    public GameObject characters;

    public float speed = 10f;
    public float speedMultiplier = 1f;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (this.input != null)
        {
            this.input.PunchInput += this.ResetPos;
            this.input.KickInput += this.RemoveCharacters;
            //this.input.SpecialInput += this.RemoveCharacters;
        }
    }

    private void OnDisable()
    {
        if (this.input != null)
        {
            this.input.PunchInput -= this.ResetPos;
            this.input.KickInput -= this.RemoveCharacters;
            //this.input.SpecialInput -= this.RemoveCharacters;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.input != null && this.input.moveInput.x != 0f)
        {
            //this.xPos += this.input.moveInput.x * Time.deltaTime * 10f;
            //this.xPos += this.input.moveInput.x * Time.fixedDeltaTime * 10f;

            //this.transform.position = new Vector3(this.input.moveInput.x * Time.deltaTime * 10f, 0f, 0f);


            //this.transform.Translate(this.input.moveInput.x * Time.fixedDeltaTime * 10f, 0f, 0f);


            //this.xPos += this.input.moveInput.x * Time.fixedDeltaTime * this.speed;

            this.xPos += this.input.moveInput.x * Time.fixedDeltaTime * (this.speed * this.speedMultiplier);
        }
        this.transform.position = new Vector3(this.xPos, 0f, 0f);


        /*if (this.input != null && this.rb != null)
        {
            //this.rb.velocity = new Vector3(this.input.moveInput.x * this.speed, 0f, 0f);
            this.rb.velocity = new Vector3(this.input.moveInput.x * (this.speed * this.speedMultiplier), 0f, 0f);
        }*/

        if (this.input != null)
        {
            if (this.input.special2)
                this.speedMultiplier = 2f;
            else if (this.input.special)
                this.speedMultiplier = 0.5f;
            else
                this.speedMultiplier = 1f;
        }
    }

    /*private void Update()
    {
        this.transform.position = new Vector3(this.xPos, 0f, 0f);
    }*/

    public void ResetPos(bool boolean)
    {
        if (boolean)
        {
            this.xPos = 0f;

            //this.transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    public void RemoveCharacters(bool boolean)
    {
        if (boolean)
        {
            if (this.characters != null)
                this.characters.SetActive(!this.characters.active);
        }
            
    }
}
