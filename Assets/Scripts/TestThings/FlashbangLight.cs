using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbangLight : MonoBehaviour
{

    private void OnEnable()
    {
        this.StartCoroutine(this.ScaleUp());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator ScaleUp()
    {
        float currentTime = 0f;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetScale = 60f;
        float start = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localScale = new Vector3(Mathf.Lerp(start, targetScale, currentTime / duration), Mathf.Lerp(start, targetScale, currentTime / duration), 1f);
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, Mathf.Lerp(0.1f, 16f, currentTime / duration));
            //this.transform.localScale = new Vector3(Mathf.Lerp(start, targetScale, currentTime / duration), Mathf.Lerp(start, targetScale, currentTime / duration), 1f);
            yield return null;
        }

        /*yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);*/
    }
}
