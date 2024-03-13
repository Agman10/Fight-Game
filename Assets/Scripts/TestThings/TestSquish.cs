using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSquish : MonoBehaviour
{
    public Transform objectToSquish;
    public Transform blood;
    //public Transform objectToSquish;


    [ContextMenu("Squish")]
    public void Squish()
    {
        this.StartCoroutine(this.SquishCoroutine());
    }

    [ContextMenu("UnSquish")]
    public void UnSquish()
    {
        this.StartCoroutine(this.UnSquishCoroutine());
    }

    [ContextMenu("Squish2")]
    public void Squish2()
    {
        this.StartCoroutine(this.Squish2Coroutine());
    }

    [ContextMenu("UnSquish2")]
    public void UnSquish2()
    {
        this.StartCoroutine(this.UnSquish2Coroutine());
    }

    IEnumerator SquishCoroutine()
    {
        //yield return new WaitForSeconds(1f);

        float currentTime = 0;
        float duration = 0.15f;
        //float targetVolume = 0.1f;
        float targetScaleXZ = 1.5f;
        //float targetScaleY = 0.01f;
        float startScaleXZ = 1f;
        //float startScaleY = 1f;


        float targetScaleY = 0.01f;
        float targetScaleX = 1.25f;
        float targetScaleZ = 1.25f;

        float startScaleY = this.transform.localScale.y;
        float startScaleX = this.transform.localScale.x;
        float startScaleZ = this.transform.localScale.z;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
            
            //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
            yield return null;
        }

        if(this.blood != null)
        {
            this.blood.gameObject.SetActive(true);

            float startBloodScale = this.blood.localScale.x;
            float targetBloodScale = 3f;
            currentTime = 0;
            duration = 0.3f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.blood.localScale = new Vector3(Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration), 1f, Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration));

                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
            
        }

    }

    IEnumerator UnSquishCoroutine()
    {
        //yield return new WaitForSeconds(1f);

        float currentTime = 0;
        float duration = 0.3f;
        //float targetVolume = 0.1f;
        float targetScaleXZ = 1f;
        //float targetScaleY = 1f;
        float startScaleXZ = 1.5f;
        //float startScaleY = 0.01f;

        float targetScaleY = 1f;
        float targetScaleX = 1f;
        float targetScaleZ = 1f;


        float startScaleY = this.transform.localScale.y;
        float startScaleX = this.transform.localScale.x;
        float startScaleZ = this.transform.localScale.z;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
            //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
            yield return null;
        }

        if (this.blood != null)
        {
            this.blood.gameObject.SetActive(false);
            this.blood.localScale = Vector3.one;

        }
    }


    IEnumerator Squish2Coroutine()
    {
        if(this.objectToSquish != null)
        {
            //yield return new WaitForSeconds(1f);

            float currentTime = 0;
            float duration = 0.15f;
            //float targetVolume = 0.1f;
            float targetScaleXZ = 1.5f;
            //float targetScaleY = 0.01f;
            float startScaleXZ = 1f;
            //float startScaleY = 1f;


            float startPosY = this.objectToSquish.localPosition.y;
            float targetPosY = this.objectToSquish.localPosition.y * 0.01f;

            float startScaleY = this.objectToSquish.localScale.y;
            float startScaleX = this.objectToSquish.localScale.x;
            float startScaleZ = this.objectToSquish.localScale.z;

            float targetScaleY = 0.01f;
            float targetScaleX = 1.25f;
            float targetScaleZ = this.objectToSquish.localScale.z * 1.25f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.objectToSquish.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                this.objectToSquish.localPosition = new Vector3(this.objectToSquish.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), this.objectToSquish.localPosition.z);
                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
        }
        
    }

    IEnumerator UnSquish2Coroutine()
    {
        if (this.objectToSquish != null)
        {
            //yield return new WaitForSeconds(1f);

            float currentTime = 0;
            float duration = 0.3f;
            //float targetVolume = 0.1f;
            float targetScaleXZ = 1.5f;
            //float targetScaleY = 0.01f;
            float startScaleXZ = 1f;
            //float startScaleY = 1f;


            float startPosY = this.objectToSquish.localPosition.y;
            float targetPosY = this.objectToSquish.localPosition.y * 100f;

            float startScaleY = this.objectToSquish.localScale.y;
            float startScaleX = this.objectToSquish.localScale.x;
            float startScaleZ = this.objectToSquish.localScale.z;

            float targetScaleY = 1f;
            float targetScaleX = 1f;
            float targetScaleZ = 1f;

            if(this.objectToSquish.localScale.z < 0f)
                targetScaleZ = -1f;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.objectToSquish.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                this.objectToSquish.localPosition = new Vector3(this.objectToSquish.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), this.objectToSquish.localPosition.z);
                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
        }

    }
}
