using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public Renderer[] eyes;
    public Material openEye, closedEye;
    public float minBlinkTime = 3f, maxBlinkTime = 6f, minClosedEyeDuration = 0.08f, maxClosedEyeDuration = 0.12f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        this.StartCoroutine(this.Blink());

        /*if (this.eyes.Length >= 1 && this.eyes[0] != null && this.eyes[0].material != null)
        {
            this.openEye = this.eyes[0].material;
            this.closedEye = new Material(this.eyes[0].material);
            Color baseColor = this.openEye.GetColor("_BaseColor");
            this.closedEye.SetColor("_BaseColor", new Color(baseColor.r * closedEyeStrenght, baseColor.g * closedEyeStrenght, baseColor.b * closedEyeStrenght, 1));
            //this.closedEye.SetColor("_BaseColor", this.openEye.GetColor("_BaseColor") * 0.2f);
        }

        for (int i = 0; i <= this.eyes.Length - 1; i++)
        {
            if (this.eyes[i] != null && this.openEye != null)
                this.eyes[i].material = this.openEye;
        }*/
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        for (int i = 0; i <= this.eyes.Length - 1; i++)
        {
            if (this.eyes[i] != null && this.openEye != null)
                this.eyes[i].material = this.openEye;
        }
    }

    private IEnumerator Blink()
    {
        yield return StartCoroutine(this.Close());
        yield return StartCoroutine(this.Open());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(Random.Range(this.minBlinkTime, this.maxBlinkTime));

        for (int i = 0; i <= this.eyes.Length - 1; i++)
        {
            if (this.eyes[i] != null && this.closedEye != null)
                this.eyes[i].material = this.closedEye;
        }
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(Random.Range(this.minClosedEyeDuration, this.maxClosedEyeDuration));

        for (int i = 0; i <= this.eyes.Length - 1; i++)
        {
            if (this.eyes[i] != null && this.openEye != null)
                this.eyes[i].material = this.openEye;
        }
        yield return StartCoroutine(this.Blink());
    }
}
