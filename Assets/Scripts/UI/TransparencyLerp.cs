using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransparencyLerp : MonoBehaviour
{
    public float duration = 0.1f;
    public Image image;
    public TextMeshProUGUI textMesh;


    private void OnEnable()
    {
        this.StartCoroutine(this.TransparencyLerpCoroutine());
    }

    private IEnumerator TransparencyLerpCoroutine()
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.image != null)
                this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, Mathf.Lerp(0, 1, currentTime / this.duration));

            if (this.textMesh != null)
                this.textMesh.color = new Color(this.textMesh.color.r, this.textMesh.color.g, this.textMesh.color.b, Mathf.Lerp(0, 1, currentTime / this.duration));

            yield return null;
        }
        if (this.image != null)
            this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1);

        if (this.textMesh != null)
            this.textMesh.color = new Color(this.textMesh.color.r, this.textMesh.color.g, this.textMesh.color.b, 1);
    }
}
