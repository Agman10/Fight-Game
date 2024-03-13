using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectAfterTime : MonoBehaviour
{
    public float duration = 0.2f;
    public GameObject objectToEnable;

    private void OnEnable()
    {
        this.StartCoroutine(this.EnableObjectCoroutine());
    }

    private IEnumerator EnableObjectCoroutine()
    {
        yield return new WaitForSeconds(this.duration);
        if (this.objectToEnable != null)
            this.objectToEnable.SetActive(true);
    }
}
