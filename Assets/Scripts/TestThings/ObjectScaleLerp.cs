using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaleLerp : MonoBehaviour
{
    public Vector3 startScale;
    public Vector3 endScale;

    public Transform objectToScale;

    public float duration = 0.1f;

    public bool preventOnEnable;
    private void OnEnable()
    {
        if (!this.preventOnEnable)
        {
            this.objectToScale.localScale = this.startScale;
            this.StartCoroutine(this.ScaleUpCoroutine(this.duration));
        }

    }
    private void OnDisable()
    {
        if (!this.preventOnEnable)
        {
            this.StopAllCoroutines();
            this.objectToScale.localScale = this.endScale;
        }

    }
    [ContextMenu("ScaleUp")]
    public void ScaleUp()
    {
        this.StartCoroutine(this.ScaleUpCoroutine(this.duration));
    }

    [ContextMenu("ScaleDown")]
    public void ScaleDown()
    {
        this.StartCoroutine(this.ScaleDownCoroutine(this.duration));
    }

    private IEnumerator ScaleUpCoroutine(float durration = 0.1f)
    {
        float currentTime = 0;
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < durration)
        {
            currentTime += Time.deltaTime;
            this.objectToScale.localScale = new Vector3(
                Mathf.Lerp(this.startScale.x, this.endScale.x, currentTime / this.duration),
                Mathf.Lerp(this.startScale.y, this.endScale.y, currentTime / this.duration),
                Mathf.Lerp(this.startScale.z, this.endScale.z, currentTime / this.duration));
            yield return null;
        }
        this.objectToScale.localScale = this.endScale;
    }


    private IEnumerator ScaleDownCoroutine(float durration = 0.1f)
    {
        float currentTime = 0;
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < durration)
        {
            currentTime += Time.deltaTime;
            this.objectToScale.localScale = new Vector3(
                Mathf.Lerp(this.endScale.x, this.startScale.x, currentTime / this.duration),
                Mathf.Lerp(this.endScale.y, this.startScale.y, currentTime / this.duration),
                Mathf.Lerp(this.endScale.z, this.startScale.z, currentTime / this.duration));
            yield return null;
        }
        this.objectToScale.localScale = this.startScale;
    }
}
