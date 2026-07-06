using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyAnimations : MonoBehaviour
{
    public Transform scaler;
    public bool floating;

    public Transform rightArm, rightArmJoint, leftArm, leftArmJoint, rightLeg, rightLegJoint, leftLeg, leftLegJoint;
    public Transform body;
    public Transform eyes;
    public Transform chest;
    public Transform wingRight;
    public Transform wingLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        //this.SetIdlePose();
        
        this.StartAnimation();
    }

    public void StartAnimation()
    {
        this.SetStartPose();
        //this.chest.localEulerAngles = new Vector3(0f, 0f, -1f);
        this.StartCoroutine(this.StartAnimationCoroutine());
    }

    private IEnumerator StartAnimationCoroutine()
    {
        float currentTime = 0;
        float duration = 0.6f;
        float startScale = 0.05f;
        float startPos = -0.25f;
        float endPos = 1.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0f, 720f, currentTime / duration), 0);
            this.transform.localScale = Vector3.one * Mathf.Lerp(startScale, 1f, currentTime / duration);
            this.transform.localPosition = new Vector3(this.transform.position.x, Mathf.Lerp(startPos, endPos, currentTime / duration), 0f);
            //this.gameObject.transform.localScale = new Vector3(Mathf.Lerp(1f, startScale, currentTime / duration), Mathf.Lerp(1f, startScale, currentTime / duration), Mathf.Lerp(1f, startScale, currentTime / duration));



            yield return null;
        }
        //this.StartCoroutine(this.BounceCoroutine());

        //this.StartCoroutine(this.BounceCoroutine2(2f, 0.75f));
        currentTime = 0;
        duration = 0.3f;
        float multiplier = 2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            this.transform.localPosition = new Vector3(this.transform.position.x, Mathf.Lerp(1.0f, 1.2f, currentTime / duration), 0f);
            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);
        /*if (this.body != null)
        {
            //this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 165f, 10f);
        }*/
        this.SetStartPoseMid();
        this.chest.localEulerAngles = new Vector3(0f, 0f, -1f);
        //this.StartCoroutine(this.BounceCoroutine());
        yield return new WaitForSeconds(0.05f);

        /*currentTime = 0;
        duration = 0.05f;
        float multiplier = 3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, -1f * multiplier, currentTime / duration), Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration), 0f);
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            yield return null;
        }*/
        this.StartCoroutine(this.BounceCoroutine());
        //this.StartCoroutine(this.BounceCoroutine2(-2f));

        this.SetIdlePose();
        this.floating = true;
        this.StartCoroutine(this.FloatCoroutine());
    }

    public IEnumerator FloatCoroutine()
    {
        float currentTime = 0f;
        while (this.floating)
        {

            //float newY = Mathf.Sin(Time.time * 5f);
            //float newY2 = Mathf.Sin(Time.time * 15);
            currentTime += Time.deltaTime;
            float newY = Mathf.Sin(currentTime * 5f);
            float newY2 = Mathf.Sin(currentTime * 15);

            float wingHeight = 20f;
            //float newY = Mathf.Cos(Time.time * this.speed);

            //this.transform.position = new Vector3(this.startPos.x, this.startPos.y + (newY * this.height), this.startPos.z);
            this.scaler.localPosition = new Vector3(this.scaler.localPosition.x, newY * 0.05f, this.scaler.localPosition.z);

            this.wingRight.localEulerAngles = new Vector3(this.wingRight.localEulerAngles.x, newY2 * wingHeight, this.wingRight.localEulerAngles.z);
            this.wingLeft.localEulerAngles = new Vector3(this.wingLeft.localEulerAngles.x, newY2 * -wingHeight, this.wingLeft.localEulerAngles.z);
            yield return null;
        }
        
    }

    private IEnumerator BounceCoroutine()
    {
        float multiplier = 2f;
        float timeMultiplier = 1f;

        //this.chest.localEulerAngles = new Vector3(0f, 0f, -1f * multiplier);
        //this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        //yield return new WaitForSeconds(0.01f);
        //yield return new WaitForSeconds(0.025f);

        float currentTime = 0;
        float duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, -1f * multiplier, currentTime / duration), Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f * multiplier, -1f * multiplier, currentTime / duration));
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(-1f * multiplier, 0.5f * multiplier, currentTime / duration), Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-1f * multiplier, 0.5f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f, 0f, currentTime / duration));
            yield return null;
        }
        currentTime = 0;
        duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0.5f * multiplier, 0f, currentTime / duration), Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0.5f * multiplier, 0f, currentTime / duration));
            yield return null;
        }
        this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    private IEnumerator BounceCoroutine2(float multiplier = 2f, float timeMultiplier = 1f)
    {
        //float multiplier = 2f;
        //float timeMultiplier = 1f;

        //this.chest.localEulerAngles = new Vector3(0f, 0f, -1f * multiplier);
        //this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        //yield return new WaitForSeconds(0.01f);
        //yield return new WaitForSeconds(0.025f);

        float currentTime = 0;
        float duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, -1f * multiplier, currentTime / duration), Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f * multiplier, 1f * multiplier, currentTime / duration));
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(-1f * multiplier, 0.5f * multiplier, currentTime / duration), Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f * multiplier, -0.5f * multiplier, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(1f, 0f, currentTime / duration));
            yield return null;
        }
        currentTime = 0;
        duration = 0.1f * timeMultiplier;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(0.5f * multiplier, 0f, currentTime / duration), Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration));
            //this.chest.localEulerAngles = new Vector3(0f, Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration), 0f);

            //this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration));
            this.chest.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-0.5f * multiplier, 0f, currentTime / duration));
            yield return null;
        }
        this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
    }


    public void SetIdlePose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(25f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 20f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -18f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.chest != null)
        {
            this.chest.localEulerAngles = new Vector3(0f, 0f 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0, 165f, -3f);
            
        }
    }

    public void SetStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(35f, 0f, -40f);
            this.leftArm.localEulerAngles = new Vector3(-35f, 0f, -30f);
            this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 20f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 42f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -85f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -7f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.chest != null)
        {
            this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 165f, 22f);
        }
    }

    public void SetStartPoseMid()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, -20f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -15f);
            this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 20f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 21f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -13f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -55f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -18f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.chest != null)
        {
            this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 165f, 11f);
        }
    }

    public void SetMagicPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(25f, 25f, 65f);
            this.leftArm.localEulerAngles = new Vector3(-25f, -25f, 65f);
            this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 20f);

            this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.chest != null)
        {
            this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 175f, -32f);
        }
    }

    public void SetDefaultPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.chest != null)
        {
            this.chest.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
