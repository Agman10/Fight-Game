using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStartToEndPos : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public float duration = 0.1f;

    public bool preventOnEnable;

    private void OnEnable()
    {
        if (!this.preventOnEnable)
        {
            this.transform.localPosition = this.startPos;
            this.StartCoroutine(this.MoveCoroutine());
        }
        
    }
    private void OnDisable()
    {
        if (!this.preventOnEnable)
        {
            this.StopAllCoroutines();
            this.transform.localPosition = this.endPos;
        }
        
    }

    public void Move()
    {
        this.StartCoroutine(this.MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float currentTime = 0;
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(
                Mathf.Lerp(this.startPos.x, this.endPos.x, currentTime / this.duration),
                Mathf.Lerp(this.startPos.y, this.endPos.y, currentTime / this.duration),
                Mathf.Lerp(this.startPos.z, this.endPos.z, currentTime / this.duration));
            yield return null;
        }
        this.transform.localPosition = this.endPos;
    }
}
