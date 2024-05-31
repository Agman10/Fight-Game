using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransformOnEnable : MonoBehaviour
{
    public float duration = 0.2f;
    public float startRotation = -90f;
    public float targetRotation = 0f;

    [Space]
    public bool useVector3 = false;

    public Vector3 startRot;
    public Vector3 targetRot;

    private void OnEnable()
    {
        if (!this.useVector3)
            this.StartCoroutine(this.RotatePose());
        else
            this.StartCoroutine(this.RotatePoseVector3());
    }

    private IEnumerator RotatePose()
    {
        float currentTime = 0;
        //float duration = 0.2f;
        //float targetVolume = 0.1f;
        //float targetRotation = 0f;
        //float start = -90f;
        while (currentTime < this.duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(this.startRotation, this.targetRotation, currentTime / this.duration), 0);
            yield return null;
        }
    }

    private IEnumerator RotatePoseVector3()
    {
        float currentTime = 0;
        //float duration = 0.2f;
        //float targetVolume = 0.1f;
        //float targetRotation = 0f;
        //float start = -90f;
        while (currentTime < this.duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(
                Mathf.Lerp(this.startRot.x, this.targetRot.x, currentTime / this.duration),
                Mathf.Lerp(this.startRot.y, this.targetRot.y, currentTime / this.duration), 
                Mathf.Lerp(this.startRot.z, this.targetRot.z, currentTime / this.duration));
            yield return null;
        }
    }
}
