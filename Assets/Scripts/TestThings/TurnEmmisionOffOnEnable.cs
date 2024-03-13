using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEmmisionOffOnEnable : MonoBehaviour
{
    public Renderer emissionRenderer;
    private void OnEnable()
    {
        this.emissionRenderer = this.GetComponent<Renderer>();
        if (this.emissionRenderer != null && this.emissionRenderer.materials.Length >= 1 && this.emissionRenderer.materials[0] != null)
        {
            this.emissionRenderer.material.SetColor("_EmissionColor", Color.black);
        }
        this.StartCoroutine(this.TurnOffEmissionCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        /*this.emissionRenderer = this.GetComponent<Renderer>();
        if (this.emissionRenderer != null && this.emissionRenderer.materials.Length >= 1 && this.emissionRenderer.materials[0] != null)
        {
            this.emissionRenderer.material.SetColor("_EmissionColor", Color.black);
        }*/
    }

    private IEnumerator TurnOffEmissionCoroutine()
    {
        yield return new WaitForSeconds(0.01f);

        if (this.emissionRenderer != null && this.emissionRenderer.materials.Length >= 1 && this.emissionRenderer.materials[0] != null)
        {
            this.emissionRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
