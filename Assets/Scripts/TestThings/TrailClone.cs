using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailClone : MonoBehaviour
{
    public Transform objectToTrail;

    public TempPlayerAnimations animations;
    public TempPlayerAnimations animationToCopy;
    // Start is called before the first frame update
    private void OnEnable()
    {
        this.transform.position = this.objectToTrail.position;

        this.transform.localEulerAngles = this.objectToTrail.localEulerAngles;

        this.CopyAnimations();

        this.StartCoroutine(this.TrailCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    /*private void Update()
    {
        this.CopyAnimations();

        if (this.objectToTrail != null)
            this.transform.position = new Vector3(this.objectToTrail.position.x - (this.objectToTrail.forward.z * 1.3f), this.objectToTrail.position.y, this.objectToTrail.position.z);
    }*/

    private IEnumerator TrailCoroutine()
    {
        Vector3 positionn = this.objectToTrail.position;
        Vector3 rotation = this.objectToTrail.localEulerAngles;

        //this.CopyAnimations();

        //yield return new WaitForSeconds(0.05f);

        //this.CopyAnimations();

        float currentTime = 0;
        float duration = 0.05f;
        //targetPosition = this.user.transform.forward.z * -7f;
        Vector3 start = this.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(Mathf.Lerp(start.x, positionn.x, currentTime / duration), Mathf.Lerp(start.y, positionn.y, currentTime / duration), 0f);
            yield return null;
        }

        this.transform.position = positionn;

        this.transform.localEulerAngles = rotation;

        this.CopyAnimations();

        this.StartCoroutine(this.TrailCoroutine());
    }

    public void CopyAnimations()
    {
        if(this.animations != null && this.animationToCopy != null)
        {
            this.animations.rightArm.localEulerAngles = this.animationToCopy.rightArm.localEulerAngles;

            this.animations.rightArmJoint.localEulerAngles = this.animationToCopy.rightArmJoint.localEulerAngles;

            this.animations.leftArm.localEulerAngles = this.animationToCopy.leftArm.localEulerAngles;

            this.animations.leftArmJoint.localEulerAngles = this.animationToCopy.leftArmJoint.localEulerAngles;




            this.animations.rightLeg.localEulerAngles = this.animationToCopy.rightLeg.localEulerAngles;

            this.animations.rightLegJoint.localEulerAngles = this.animationToCopy.rightLegJoint.localEulerAngles;

            this.animations.leftLeg.localEulerAngles = this.animationToCopy.leftLeg.localEulerAngles;

            this.animations.leftLegJoint.localEulerAngles = this.animationToCopy.leftLegJoint.localEulerAngles;



            this.animations.upperBody.localEulerAngles = this.animationToCopy.upperBody.localEulerAngles;
            this.animations.lowerBody.localEulerAngles = this.animationToCopy.lowerBody.localEulerAngles;


            this.animations.eyes.localEulerAngles = this.animationToCopy.eyes.localEulerAngles;
            this.animations.eyes.localPosition = this.animationToCopy.eyes.localPosition;


            this.animations.body.localEulerAngles = this.animationToCopy.body.localEulerAngles;
            this.animations.body.localPosition = this.animationToCopy.body.localPosition;

            this.animations.body.localScale = this.animationToCopy.body.localScale;

            this.transform.rotation = this.objectToTrail.rotation;
        }
    }
}
