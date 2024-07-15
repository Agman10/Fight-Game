using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerAnimation : MonoBehaviour
{
    public GameObject[] speakers;

    private void OnEnable()
    {
        foreach (GameObject speaker in this.speakers)
        {
            this.StartCoroutine(this.SpeakerAnimationCorotuine(speaker));

        }
        //this.StartCoroutine(this.SpeakerAnimationCorotuine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();

        foreach (GameObject speaker in this.speakers)
        {
            if (speaker != null)
            {
                speaker.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }


    private IEnumerator SpeakerAnimationCorotuine(GameObject speaker = null)
    {
        /*foreach (GameObject speaker in this.speakers)
        {
            
            
        }*/
        if (speaker != null)
        {
            float currentTime = 0;
            //duration = 0.15f;
            float duration = 0.1f;
            float durationBack = 0.15f;

            float durationY = 0.05f;
            float durationYBack = 0.1f;

            float startScaleX = 1f;
            float startScaleY = 1f;


            float targetScaleX = 1.3f;
            float targetScaleY = 1.1f;

            yield return new WaitForSeconds(0.1f);

            //X Scale

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                speaker.transform.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), 1f, 1f);
                yield return null;
            }

            currentTime = 0;

            while (currentTime < durationBack)
            {
                currentTime += Time.deltaTime;
                speaker.transform.localScale = new Vector3(Mathf.Lerp(targetScaleX, startScaleX, currentTime / durationBack), 1f, 1f);
                yield return null;
            }

            currentTime = 0;

            yield return new WaitForSeconds(0.2f);

            //Y Scale

            while (currentTime < durationY)
            {
                currentTime += Time.deltaTime;
                speaker.transform.localScale = new Vector3(1f, Mathf.Lerp(startScaleY, targetScaleY, currentTime / durationY), 1f);
                yield return null;
            }

            currentTime = 0;

            while (currentTime < durationYBack)
            {
                currentTime += Time.deltaTime;
                speaker.transform.localScale = new Vector3(1f, Mathf.Lerp(targetScaleY, startScaleY, currentTime / durationYBack), 1f);
                yield return null;
            }


            yield return new WaitForSeconds(0.1f);

            this.StartCoroutine(this.SpeakerAnimationCorotuine(speaker));
        }

    }
}
