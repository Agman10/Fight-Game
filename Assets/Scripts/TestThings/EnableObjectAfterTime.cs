using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectAfterTime : MonoBehaviour
{
    public float duration = 0.2f;
    public GameObject objectToEnable;
    public bool disableObjectOnDisable = false;

    private void OnEnable()
    {
        this.StartCoroutine(this.EnableObjectCoroutine());
    }
    private void OnDisable()
    {
        if (this.objectToEnable != null && this.disableObjectOnDisable)
        {
            this.objectToEnable.SetActive(false);
        }
    }

    private IEnumerator EnableObjectCoroutine()
    {
        yield return new WaitForSeconds(this.duration);
        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(true);
    }
}
