using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTests : MonoBehaviour
{

    public TempPlayerAnimations animations;

    public GameObject objectoToEnable;

    public ObjectScaleLerp objectToScale;

    public GameObject startParticle;

    public int animId;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.animId == 0)
        {
            this.StartCoroutine(this.Test1());
        }
        else if (this.animId == 1)
        {
            //this.StartCoroutine(this.DiscoDance());
            this.StartCoroutine(this.Test2());
        }
        else if (this.animId == 2)
        {
            //this.StartCoroutine(this.DiscoDance());
            this.StartCoroutine(this.TestShake());
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


    private IEnumerator Test1()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.2f;

            if (this.startParticle != null)
            {
                GameObject startParticlePrefab = this.startParticle;
                startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            }

            if (this.animations != null)
                this.animations.StupidDance(1);

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.StupidDance(0);

            

            yield return new WaitForSeconds(0.4f);

            if (this.animations != null)
                this.animations.StupidDance(1);

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.StupidDance(3);

            if (this.objectToScale != null)
                this.objectToScale.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.45f);

            if (this.objectToScale != null)
                this.objectToScale.ScaleDown2(0.05f, true);

            yield return new WaitForSeconds(0.1f);
            

            if (this.animations != null)
                this.animations.StupidDance(1);

            

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(1f);

            this.StartCoroutine(this.Test1());
        }


    }

    private IEnumerator Test2()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.01f;

            int ammont = 5;

            int amount = 3;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            amount = ammont;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);
                /*yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);*/

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            amount = ammont;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);
                /*yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);*/

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            amount = ammont * 2;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            amount = ammont;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            amount = ammont;

            while (amount > 0)
            {
                this.animations.body.localScale = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);
                yield return new WaitForSeconds(animSpeed);

                this.animations.body.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(animSpeed);


                amount -= 1;
            }

            this.animations.body.localScale = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(2f);

            /*this.animations.body.localScale = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(animSpeed);

            this.animations.body.localScale = new Vector3(1f, 1f, 1f);

            yield return new WaitForSeconds(animSpeed);*/

            this.StartCoroutine(this.Test2());
        }
    }

    private IEnumerator TestShake()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.2f;

            float testTime = 0f;
            float time = 0.5f;
            float startPosY = this.animations.body.localPosition.y;
            float startPosX = this.animations.body.localPosition.x;
            while (time > 0)
            {
                time -= Time.deltaTime;
                testTime += Time.deltaTime;

                float newY = Mathf.Sin(testTime * 100f);
                //this.animations.body.localPosition = new Vector3(this.animations.body.localPosition.x, startPosY + (newY * 0.01f), this.animations.body.localPosition.z);
                this.animations.body.localPosition = new Vector3(startPosX + (newY * 0.01f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                yield return null;
            }


            this.StartCoroutine(this.TestShake());
        }


    }
}
