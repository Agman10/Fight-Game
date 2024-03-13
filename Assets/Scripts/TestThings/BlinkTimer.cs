using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTimer : MonoBehaviour
{
    public float time = 3f;
    public float maxInterval = 1.2f;
    public float minInterval = 0.2f;

    //public float blinkSpeed;

    public MeshRenderer lamp;
    public Material activeMaterial;
    public Material inactiveMaterial;
    private void OnEnable()
    {
        this.StartCoroutine(this.BlinkCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator BlinkCoroutine()
    {
        //yield return new WaitForSeconds(0.1f);

        float currentTime = 0;
        float timer = this.time;
        while (timer > 0f)
        {
            float interval = this.minInterval + timer / this.time * (this.maxInterval - this.minInterval);
            timer -= Time.deltaTime;
            currentTime += Time.deltaTime;

            //Debug.Log(Time.time);
            if (timer < 0.0f) timer = 0.0f;

            if (this.lamp != null && this.activeMaterial != null && this.inactiveMaterial != null)
            {
                if (Mathf.PingPong(currentTime, interval) > (interval / 2.0f))
                    this.lamp.material = this.inactiveMaterial;
                else
                    this.lamp.material = this.activeMaterial;
            }

            

            //lamp.enabled = Mathf.PingPong(Time.time, interval) > (interval / 2.0f);
            //lamp.gameObject.SetActive(Mathf.PingPong(Time.time, interval) > (interval / 2.0f));
            yield return null;
        }


        /*float t = this.time;
        //float totalSpeedIncrease = 2f;

        float blinkSpeed = 1 + 4f * (2 / 4); // blink speeds measured in blinks per second
        while (timer > 0f)
        {

            //blinkSpeed = startSpeed + totalSpeedIncrease * timeElapsed / maxTimeElapsed; // blink speeds measured in blinks per second


            t += Time.deltaTime * blinkSpeed;
            if (t >= 1)
            {
                t = 0;
                this.lamp.material = this.inactiveMaterial;
                // switch renderer.enabled
            }
            else
            {
                this.lamp.material = this.activeMaterial;
            }
            yield return null;
        }*/
    }
}
