using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearyBlink : MonoBehaviour
{

    public GameObject rightEyeOpen;
    public GameObject leftEyeOpen;

    public GameObject rightEyeClosed;
    public GameObject leftEyeClosed;

    public void ResetBlink()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.BlinkCoroutine());
    }

    public void StopBlink()
    {
        this.StopAllCoroutines();
    }

    public void CloseEyes()
    {
        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(false);
            this.leftEyeOpen.SetActive(false);

            this.rightEyeClosed.SetActive(true);
            this.leftEyeClosed.SetActive(true);
        }
    }

    public void OpenEyes()
    {
        if (this.rightEyeOpen != null && this.rightEyeClosed && this.leftEyeOpen != null && this.leftEyeClosed != null)
        {
            this.rightEyeOpen.SetActive(true);
            this.leftEyeOpen.SetActive(true);

            this.rightEyeClosed.SetActive(false);
            this.leftEyeClosed.SetActive(false);
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        this.OpenEyes();

        //yield return new WaitForSeconds(Random.Range(this.minBlinkTime, this.maxBlinkTime));
        yield return new WaitForSeconds(Random.Range(3f, 6f));

        this.CloseEyes();

        yield return new WaitForSeconds(Random.Range(0.08f, 0.12f));

        this.OpenEyes();

        yield return this.StartCoroutine(this.BlinkCoroutine());
    }
}
