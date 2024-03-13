using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransformOnEnable : MonoBehaviour
{
    public float duration = 0.2f;
    public float startRotation = -90f;
    public float targetRotation = 0f;

    private void OnEnable()
    {
        this.StartCoroutine(this.RotatePose());
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
}
