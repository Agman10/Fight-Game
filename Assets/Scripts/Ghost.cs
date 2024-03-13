using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed = 1f;

    public float activeTime = 5f;
    private float timer;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.startPos = this.transform.position;


        /*if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset += this.DisableGhost;*/
    }

    /*private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset -= this.DisableGhost;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.tempSkyboxAndStageLogic != null && GameManager.Instance.tempSkyboxAndStageLogic.currentStage == 11)
        {
            if(this.transform.position.y >= 0.1f)
            {
                float newY = Mathf.Sin(Time.time * 2f);
                this.transform.position = new Vector3(this.transform.position.x, this.startPos.y + (newY * 0.1f), this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);
                this.startPos = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);
                
            }
            

            /*this.timer += 1 * Time.deltaTime;

            if (this.timer >= activeTime)
            {
                this.gameObject.SetActive(false);
                this.timer = 0f;
            }*/
        }
        else
        {
            this.transform.position = this.transform.position + new Vector3(0f, this.speed * Time.deltaTime, 0f);

            this.timer += 1 * Time.deltaTime;

            if (this.timer >= activeTime)
            {
                this.gameObject.SetActive(false);
                this.timer = 0f;
            }
        }

        /*this.transform.position = this.transform.position + new Vector3(0f, this.speed * Time.deltaTime, 0f);

        this.timer += 1 * Time.deltaTime;

        if(this.timer >= activeTime)
        {
            this.gameObject.SetActive(false);
            this.timer = 0f;
        }*/
    }

    /*public void DisableGhost()
    {
        //Debug.Log("test");
        this.gameObject.SetActive(false);
    }*/
}
