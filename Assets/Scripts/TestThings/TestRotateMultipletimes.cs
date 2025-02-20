using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotateMultipletimes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("test")]
    public void Rotate()
    {
        this.StartCoroutine(this.RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 2f, currentTime / duration));
            yield return null;
        }


        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(2f, -2f, currentTime / duration));
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-2f, 2f, currentTime / duration));
            yield return null;
        }



        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(2f, -1f, currentTime / duration));
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-1f, 1f, currentTime / duration));
            yield return null;
        }




        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f, 0f, currentTime / duration));
            yield return null;
        }
    }
}
