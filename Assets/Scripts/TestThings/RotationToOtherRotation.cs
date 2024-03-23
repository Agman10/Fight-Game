using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationToOtherRotation : MonoBehaviour
{
    //public float multiplier = 1f;
    //float currentTime = 0;
    // Start is called before the first frame update
    public float speed = 5f;
    //adjust this to change how high it goes
    public float height = 0.5f;
    private Vector3 startPos;
    public bool yRotation = false;
    void Start()
    {
        
    }
    /*private void OnEnable()
    {
        //this.StartCoroutine(this.RotateWings());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }*/

    // Update is called once per frame
    void Update()
    {
        /*this.transform.Rotate(new Vector3(0f, 0f, (100f * this.multiplier) * Time.deltaTime));
        if (this.transform.localEulerAngles.z >= 25f)
            this.multiplier = -this.multiplier;*/


        /*//float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetRotation = 25f * this.multiplier;
        float start = 0f;
        currentTime += Time.deltaTime;
        this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(start, targetRotation, currentTime / duration));*/

        float newY = Mathf.Sin(Time.time * this.speed);
        //this.transform.localEulerAngles = new Vector3(this.startPos.x, this.startPos.y + (newY * this.height), this.startPos.z);

        /*if (!this.yRotation)
            this.transform.localEulerAngles = new Vector3(0f, 0f, this.startPos.z + (newY * this.height));
        else
            this.transform.localEulerAngles = new Vector3(0f, this.startPos.z + (newY * this.height), 0f);*/

        if (!this.yRotation)
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.startPos.z + (newY * this.height));
        else
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.startPos.z + (newY * this.height), this.transform.localEulerAngles.z);
    }

    /*IEnumerator RotateWings()
    {
        float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetRotation = 25f * this.multiplier;
        float start = this.transform.localEulerAngles.z;
        Debug.Log(targetRotation);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(start, targetRotation, currentTime / duration));

            //this.transform.Rotate(new Vector3(0f, 0f, (100f * this.multiplier) * Time.deltaTime));
            yield return null;
        }
        //this.multiplier = -this.multiplier;
        this.StartCoroutine(this.RotateWings2());
    }
    IEnumerator RotateWings2()
    {
        float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetRotation = -25f * this.multiplier;
        float start = this.transform.localEulerAngles.z;
        Debug.Log(targetRotation);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(start, targetRotation, currentTime / duration));
            //this.transform.Rotate(new Vector3(0f, 0f, (100f * this.multiplier) * Time.deltaTime));
            yield return null;
        }

        *//*while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.Rotate(new Vector3(0f, 0f, (100f * this.multiplier) * Time.deltaTime));
            //this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(start, targetRotation, currentTime / duration));
            yield return null;
        }*//*
        //this.multiplier = -this.multiplier;
        this.StartCoroutine(this.RotateWings());
    }*/
}
