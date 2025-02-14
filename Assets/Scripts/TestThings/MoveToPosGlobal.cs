using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosGlobal : MonoBehaviour
{
    //public Vector3 startPos;
    public Vector3 endPos;

    public float duration = 0.1f;

    public bool preventOnEnable;

    public Vector3 localPos;
    public Vector3 globalPos;

    private void OnEnable()
    {
        this.localPos = this.transform.localPosition;
        this.globalPos = this.transform.position;

        //this.transform.localPosition = this.startPos;
        this.StartCoroutine(this.MoveCoroutine());

    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.transform.localPosition = this.localPos;

    }

    // Update is called once per frame
    void Update()
    {
        
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

        Debug.Log(this.endPos.x * this.transform.forward.z);

        Vector3 endPosition = new Vector3(
            (this.globalPos.x + this.endPos.x) - this.localPos.x,
            (this.globalPos.y + this.endPos.y) - this.localPos.y,
            (this.globalPos.z + this.endPos.z) - this.localPos.z);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(
                Mathf.Lerp(this.globalPos.x, endPosition.x, currentTime / this.duration),
                Mathf.Lerp(this.globalPos.y, endPosition.y, currentTime / this.duration),
                Mathf.Lerp(this.globalPos.z, endPosition.z, currentTime / this.duration));
            yield return null;
        }
        //this.transform.localPosition = this.endPos;
        this.transform.position = endPosition;
    }
}
