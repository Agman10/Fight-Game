using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlood : MonoBehaviour
{
    private void OnEnable()
    {
        this.StartCoroutine(this.GrowCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();

        //this.gameObject.SetActive(false);
        this.transform.localScale = Vector3.one;
    }

    IEnumerator GrowCoroutine()
    {
        float startBloodScale = this.transform.localScale.x;
        float targetBloodScale = 3f;
        float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localScale = new Vector3(Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration), 1f, Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration));

            //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
            yield return null;
        }
    }
}
