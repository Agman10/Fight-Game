using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepNoseBubble : MonoBehaviour
{

    public Transform bubble;
    public Transform face;

    private void OnEnable()
    {
        if (this.bubble != null)
            this.StartCoroutine(this.BubbleCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator BubbleCoroutine()
    {
        float currentTime = 0;
        float duration = 0.5f;
        float startScale = 0.5f;
        float endScale = 1f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.bubble.localScale = new Vector3(
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration));


            if(this.face != null)
            {
                this.face.localEulerAngles = new Vector3(this.face.localEulerAngles.x, this.face.localEulerAngles.y, Mathf.Lerp(3f, 0f, currentTime / duration));
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);

        currentTime = 0;
        duration = 0.5f;
        startScale = 1f;
        endScale = 0.5f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.bubble.localScale = new Vector3(
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration),
                Mathf.Lerp(startScale, endScale, currentTime / duration));

            if (this.face != null)
            {
                this.face.localEulerAngles = new Vector3(this.face.localEulerAngles.x, this.face.localEulerAngles.y, Mathf.Lerp(0f, 3f, currentTime / duration));
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        this.StartCoroutine(this.BubbleCoroutine());
    }
}
