using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeEverySecond : MonoBehaviour
{
    public Vector3 startSize;
    public Vector3 otherSize;
    public float duration = 1f;
    private void OnEnable()
    {
        this.transform.localScale = this.startSize;
        this.StartCoroutine(this.ChangeSize());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator ChangeSize()
    {
        yield return new WaitForSeconds(this.duration);
        this.transform.localScale = this.otherSize;
        yield return new WaitForSeconds(this.duration);
        this.transform.localScale = this.startSize;
        yield return StartCoroutine(this.ChangeSize());
    }
}
