using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWaterDeathLogic : MonoBehaviour
{
    public TestPlayer user;
    //public TempPlayerAnimations animations;
    public GameObject iceCube;
    public ParticleSystem waterSplash;
    public SoundEffect splashSfx;
    //public bool onGoing;
    private void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnReset += this.OnReset;
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnReset -= this.OnReset;
        }
    }

    public void OnReset()
    {
        this.Stop();
    }

    public void FreezeToDeath(float yPos = -2.45f)
    {
        if(this.user != null && this.user.animations != null && this.iceCube != null)
        {
            //this.iceCube.SetActive(true);
            this.user.animations.SetDefaultPose();
            this.user.animations.IceCubeFreezePose();
            this.PlayWaterSplash();
            
            this.StartCoroutine(this.IceCubeCoroutine(yPos));
        }
    }

    private IEnumerator IceCubeCoroutine(float yPos = -2.45f)
    {
        yield return new WaitForSeconds(0.5f);
        this.iceCube.SetActive(true);

        float currentTime = 0;
        float duration = 0.5f;
        //float targetVolume = 0.1f;
        float targetPosition = yPos;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        //currentTime = 0;
        //duration = 10f;
        //float targetVolume = 0.1f;
        float testTime = 0f;
        while (this.user.dead)
        {
            //currentTime += Time.deltaTime;
            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 2f);

            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            this.user.transform.position = new Vector3(this.user.transform.position.x, yPos + (newY * 0.1f), 0);
            yield return null;
        }
    }

    public void PlayWaterSplash()
    {
        if (this.waterSplash != null)
            this.waterSplash.Play();

        this.PlaySpashSfx();
    }

    public void PlaySpashSfx()
    {
        this.splashSfx.PlaySound();
    }

    public void Stop()
    {
        this.StopAllCoroutines();
        //this.user.rb.isKinematic = false;
        if (this.iceCube != null)
            this.iceCube.SetActive(false);

        //this.onGoing = false;
        //this.user.attackStuns.Remove(this.gameObject);
    }
}
