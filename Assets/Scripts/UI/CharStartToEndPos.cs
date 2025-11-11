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
        this.StopAllCoroutines();
        if (!this.preventOnEnable)
        {
            //this.StopAllCoroutines();
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
        while (currentTime < this.duration)
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


    public void MoveReverse()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.MoveReverseCoroutine());
    }

    private IEnumerator MoveReverseCoroutine()
    {
        float currentTime = 0;
        while (currentTime < this.duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(
                Mathf.Lerp(this.endPos.x, this.startPos.x, currentTime / this.duration),
                Mathf.Lerp(this.endPos.y, this.startPos.y, currentTime / this.duration),
                Mathf.Lerp(this.endPos.z, this.startPos.z, currentTime / this.duration));
            yield return null;
        }
        this.transform.localPosition = this.startPos;
    }



    public void MoveReverseCustomTime(float speed = 0.1f)
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.MoveReverseCoroutineCustomTime(speed));
    }

    private IEnumerator MoveReverseCoroutineCustomTime(float speed = 0.1f)
    {
        float currentTime = 0;
        while (currentTime < speed)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(
                Mathf.Lerp(this.endPos.x, this.startPos.x, currentTime / speed),
                Mathf.Lerp(this.endPos.y, this.startPos.y, currentTime / speed),
                Mathf.Lerp(this.endPos.z, this.startPos.z, currentTime / speed));
            yield return null;
        }
        this.transform.localPosition = this.startPos;
    }
}
