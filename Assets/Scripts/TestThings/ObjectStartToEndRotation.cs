using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStartToEndRotation : MonoBehaviour
{
    public Transform objectToRotate;
    public Vector3 startRotation;
    public Vector3 endRotation;

    public float duration = 0.1f;

    public bool preventOnEnable;

    private void OnEnable()
    {
        if (!this.preventOnEnable)
        {
            if (this.objectToRotate != null)
                this.objectToRotate.localEulerAngles = this.startRotation;
            this.StartCoroutine(this.RotateCoroutine());
        }

    }
    private void OnDisable()
    {
        if (!this.preventOnEnable)
        {
            this.StopAllCoroutines();
            if (this.objectToRotate != null)
                this.objectToRotate.localEulerAngles = this.endRotation;
        }

    }

    public void Rotate()
    {
        this.StartCoroutine(this.RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        float currentTime = 0;
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if(this.objectToRotate != null)
            {
                this.objectToRotate.localEulerAngles = new Vector3(
                Mathf.Lerp(this.startRotation.x, this.endRotation.x, currentTime / this.duration),
                Mathf.Lerp(this.startRotation.y, this.endRotation.y, currentTime / this.duration),
                Mathf.Lerp(this.startRotation.z, this.endRotation.z, currentTime / this.duration));
            }

            
            yield return null;
        }
        if (this.objectToRotate != null)
            this.objectToRotate.localEulerAngles = this.endRotation;
    }
}
