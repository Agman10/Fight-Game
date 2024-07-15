using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDance : MonoBehaviour
{
    public TempPlayerAnimations animations;

    public int danceId;
    private void OnEnable()
    {
        if (this.danceId == 0)
        {
            this.StartCoroutine(this.TortureDance());
        }
        else if (this.danceId == 1)
        {
            this.StartCoroutine(this.DiscoDance());
        }
        else if (this.danceId == 2)
        {
            this.StartCoroutine(this.DJCoroutine());
        }
        else if (this.danceId == 3)
        {
            this.StartCoroutine(this.SpinCoroutine());
        }
        else
        {
            Debug.Log("dance id does not exist");
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator TortureDance()
    {
        if (this.animations != null)
        {
            this.animations.TortureDance(0);

            yield return new WaitForSeconds(0.3f);

            this.animations.TortureDance(1);

            yield return new WaitForSeconds(0.3f);

            this.animations.TortureDance(2);

            yield return new WaitForSeconds(0.35f);

            this.animations.TortureDance(3);

            yield return new WaitForSeconds(0.3f);

            this.StartCoroutine(this.TortureDance());
        }


    }

    private IEnumerator DiscoDance()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.225f;

            this.animations.DiscoDance(0);

            yield return new WaitForSeconds(animSpeed + 0.025f);

            this.animations.DiscoDance(1);

            yield return new WaitForSeconds(animSpeed - 0.05f);

            this.animations.DiscoDance(2);

            yield return new WaitForSeconds(animSpeed);

            this.animations.DiscoDance(1);

            yield return new WaitForSeconds(animSpeed - 0.05f);

            this.StartCoroutine(this.DiscoDance());
        }


    }

    private IEnumerator DJCoroutine()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.3f;

            if (this.animations.body != null)
                this.animations.body.localEulerAngles = new Vector3(0f, 90f, 0f);

            if (this.animations.rightArm != null)
                this.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, 50f);

            if (this.animations.leftArm != null)
                this.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, 50f);



            /*if (this.animations.rightArm != null)
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 15f, 50f);

            if (this.animations.leftArm != null)
                this.animations.leftArm.localEulerAngles = new Vector3(0f, -15f, 50f);*/


            yield return new WaitForSeconds(animSpeed);

            if (this.animations.body != null)
                this.animations.body.localEulerAngles = new Vector3(0f, 90f, -2.5f);

            if (this.animations.rightArm != null)
                this.animations.rightArm.localEulerAngles = new Vector3(20f, 0f, 52.5f);

            if (this.animations.leftArm != null)
                this.animations.leftArm.localEulerAngles = new Vector3(-20f, 0f, 52.5f);


            /*if (this.animations.rightArm != null)
                this.animations.rightArm.localEulerAngles = new Vector3(0f, 15f, 52.5f);

            if (this.animations.leftArm != null)
                this.animations.leftArm.localEulerAngles = new Vector3(0f, -15f, 52.5f);*/


            yield return new WaitForSeconds(animSpeed);

            this.StartCoroutine(this.DJCoroutine());
        }


    }

    private IEnumerator SpinCoroutine()
    {
        if (this.animations != null)
        {

            if (this.animations != null)
                this.animations.TestPose5();

            float currentTime = 0;
            float duration = 0.7f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (this.animations != null)
                    this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
                //Debug.Log(currentTime);
                yield return null;
            }

            if (this.animations != null)
                this.animations.FlameGrabHitPose();

            yield return new WaitForSeconds(0.4f);



            if (this.animations != null)
                this.animations.TestPose5();

            currentTime = 0;
            duration = 0.7f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (this.animations != null)
                    this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
                //Debug.Log(currentTime);
                yield return null;
            }

            if (this.animations != null)
                this.animations.TestPose();

            yield return new WaitForSeconds(0.4f);




            if (this.animations != null)
                this.animations.TestPose5();

            currentTime = 0;
            duration = 0.7f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (this.animations != null)
                    this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
                //Debug.Log(currentTime);
                yield return null;
            }

            if (this.animations != null)
                this.animations.TestPose3();

            yield return new WaitForSeconds(0.4f);



            /*if (this.animations != null)
                this.animations.TestPose5();

            currentTime = 0;
            duration = 0.7f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (this.animations != null)
                    this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);
                //Debug.Log(currentTime);
                yield return null;
            }

            if (this.animations != null)
                this.animations.LayingDownSassy();

            yield return new WaitForSeconds(0.4f);*/



            this.StartCoroutine(this.SpinCoroutine());
        }
    }
}
