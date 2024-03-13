using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisableObject : MonoBehaviour
{
    public GameObject objectToDisable;

    public float enableTime = 1f;
    public float disableTime = 1f;
    private void OnEnable()
    {
        if (this.objectToDisable != null)
            this.objectToDisable.SetActive(false);

        this.StartCoroutine(this.EnableAndDisableCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator EnableAndDisableCoroutine()
    {
        yield return new WaitForSeconds(this.enableTime);

        if (this.objectToDisable != null)
            this.objectToDisable.SetActive(true);

        yield return new WaitForSeconds(this.disableTime);

        if (this.objectToDisable != null)
            this.objectToDisable.SetActive(false);
    }
}
