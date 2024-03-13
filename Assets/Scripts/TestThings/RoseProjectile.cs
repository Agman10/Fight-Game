using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseProjectile : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private void OnEnable()
    {
        this.StartCoroutine(this.Move());
    }
    private void OnDisable()
    {
        
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.1f);
        float halfDistance = (this.endPos.x - this.startPos.x) * 0.5f;
        float currentTime = 0;

        //float duration = 0.5f;
        float duration = halfDistance * 0.1f;
        if (duration < 0.3f)
            duration = 0.3f;
        //else if (halfDistance > )

        //float duration = halfDistance * 0.1f;


        //float targetPosition = (this.endPos.x - this.startPos.x) * 0.5f;
        float targetPosition = this.startPos.x + ((this.endPos.x - this.startPos.x) * 0.5f);
        Debug.Log((this.endPos.x - this.startPos.x) * 0.5f);
        float start = this.startPos.x;
        float startY = this.startPos.y;
        float targetPositionY = this.startPos.y + 4;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0);

            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));*/
            yield return null;
        }

        currentTime = 0;
        //duration = 0.5f;
        //duration = halfDistance * 0.1f;

        targetPosition = this.endPos.x;

        start = this.transform.position.x;
        startY = this.transform.position.y;
        targetPositionY = this.endPos.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0);

            /*if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));*/
            yield return null;
        }
    }
}
