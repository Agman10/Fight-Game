using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerAnimations : MonoBehaviour
{
    public Transform rightArm, rightArmJoint, leftArm, leftArmJoint, rightLeg, rightLegJoint, leftLeg, leftLegJoint;

    [Space]
    public Transform upperBody;
    public Transform lowerBody;
    public Transform eyes;
    
    //public Transform eyeHeight;

    [Space]
    public Transform body;

    [Space]
    public int characterId;
    public float defaultYPos = 1.95f;
    public float armsDefaultYPos = 0.3f;
    public float defaultEyeYHeight = 0f;

    [Space]
    public GameObject neutralEyes;
    public GameObject happyEyes;
    public GameObject frowningEyes;
    public GameObject angryEyes;
    public GameObject boredEyes;
    public GameObject sleepEyes;

    [Space]
    public GameObject bloodPuddle;

    [Space]
    public Transform dress;
    public Transform eyesPosTransform;

    [Space]
    public Transform wingRight;
    public Transform wingLeft;

    // Start is called before the first frame update
    void Start()
    {
        //this.SetDefaultPose();

        //this.SetKickUpercutAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnEnable()
    {
        if (this.rightArm != null)
            this.armsHeight = this.rightArm.localPosition.y;
    }*/

    public void SetTPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if(this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = Vector3.zero;
            //this.body.localPosition = new Vector3(0f, 1.95f, 0f);
        }
    }

    public void SetDefaultPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;


            this.rightArm.localScale = new Vector3(1f, 1f, 1f);
            this.rightArmJoint.localScale = new Vector3(1f, 1f, 1f);

            this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            this.leftLeg.localScale = new Vector3(1f, 1f, 1f);

            this.rightLegJoint.localScale = new Vector3(1f, 1f, 1f);

            this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos, this.rightArm.localPosition.z);
            this.leftArm.localPosition = new Vector3(this.leftArm.localPosition.x, this.armsDefaultYPos, this.leftArm.localPosition.z);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = Vector3.zero;
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
        }
            

        if (this.body != null)
        {
            this.body.localEulerAngles = Vector3.zero;
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);

        if (this.eyesPosTransform != null)
        {
            this.eyesPosTransform.localPosition = new Vector3(0f, 0f, 0f);
            this.eyesPosTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.dress != null)
        {
            this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.dress.localPosition = new Vector3(0f, 0f, 0f);
        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.wingRight.localScale = new Vector3(1f, 1f, 1f);
            this.wingLeft.localScale = new Vector3(1f, 1f, 1f);
        }


        /*if (this.neutralEyes != null)
            this.neutralEyes.gameObject.SetActive(true);

        if (this.happyEyes != null)
            this.happyEyes.gameObject.SetActive(false);*/

        //Debug.Log("Test");
    }

    public void SetEyes(int id)
    {
        if (id == 1) //Happy Eyes
        {
            if (this.neutralEyes != null && this.happyEyes != null)
            {
                this.happyEyes.gameObject.SetActive(true);
                this.neutralEyes.gameObject.SetActive(false);
            }

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
        else if (id == 2) //Frowning Eyes
        {
            if (this.neutralEyes != null && this.frowningEyes != null)
            {
                this.frowningEyes.gameObject.SetActive(true);
                this.neutralEyes.gameObject.SetActive(false);
            }

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
        else if (id == 3) //Angry Eyes
        {
            if (this.neutralEyes != null && this.angryEyes != null)
            {
                this.angryEyes.gameObject.SetActive(true);
                this.neutralEyes.gameObject.SetActive(false);
            }

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
        else if (id == 4) //Bored
        {
            if (this.neutralEyes != null && this.boredEyes != null)
            {
                this.boredEyes.gameObject.SetActive(true);
                this.neutralEyes.gameObject.SetActive(false);
            }

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
        else if (id == 5) //Sleep
        {
            if (this.neutralEyes != null && this.sleepEyes != null)
            {
                this.sleepEyes.gameObject.SetActive(true);
                this.neutralEyes.gameObject.SetActive(false);
            }

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);
        }
        else if (id == -1) //No Eyes
        {
            if (this.neutralEyes != null)
                this.neutralEyes.gameObject.SetActive(false);

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
        else //Neutral Eyes
        {
            if (this.neutralEyes != null)
                this.neutralEyes.gameObject.SetActive(true);

            if (this.happyEyes != null)
                this.happyEyes.gameObject.SetActive(false);

            if (this.frowningEyes != null)
                this.frowningEyes.gameObject.SetActive(false);

            if (this.angryEyes != null)
                this.angryEyes.gameObject.SetActive(false);

            if (this.boredEyes != null)
                this.boredEyes.gameObject.SetActive(false);

            if (this.sleepEyes != null)
                this.sleepEyes.gameObject.SetActive(false);
        }
    }
    [ContextMenu("InversePose")]
    public void InversePose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            Vector3 armRight = this.rightArm.localEulerAngles;
            Vector3 armLeft = this.leftArm.localEulerAngles;
            Vector3 armRightJoint = this.rightArmJoint.localEulerAngles;
            Vector3 armLeftJoint = this.leftArmJoint.localEulerAngles;

            Vector3 legRight = this.rightLeg.localEulerAngles;
            Vector3 legLeft = this.leftLeg.localEulerAngles;
            Vector3 legRightJoint = this.rightLegJoint.localEulerAngles;
            Vector3 legLeftJoint = this.leftLegJoint.localEulerAngles;

            this.rightArm.localEulerAngles = new Vector3(-armLeft.x, -armLeft.y, armLeft.z);
            this.leftArm.localEulerAngles = new Vector3(-armRight.x, -armRight.y, armRight.z);
            this.rightArmJoint.localEulerAngles = new Vector3(-armLeftJoint.x, -armLeftJoint.y, armLeftJoint.z);
            this.leftArmJoint.localEulerAngles = new Vector3(-armRightJoint.x, -armRightJoint.y, armRightJoint.z);

            this.rightLeg.localEulerAngles = new Vector3(-legLeft.x, -legLeft.y, legLeft.z);
            this.leftLeg.localEulerAngles = new Vector3(-legRight.x, -legRight.y, legRight.z);
            this.rightLegJoint.localEulerAngles = new Vector3(-legLeftJoint.x, -legLeftJoint.y, legLeftJoint.z);
            this.leftLegJoint.localEulerAngles = new Vector3(-legRightJoint.x, -legRightJoint.y, legRightJoint.z);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }
    }

    public void EnableBloodPuddle(bool enable = true)
    {
        if (this.bloodPuddle != null)
            this.bloodPuddle.SetActive(enable);
    }

    public void ResetRigPos()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(this.characterId == 3)
            {
                this.rightArm.localPosition = new Vector3(0f, 0.15f, -0.6f);
                this.leftArm.localPosition = new Vector3(0f, 0.15f, 0.6f);
                this.rightArmJoint.localPosition = new Vector3(0f, -0.6f, 0f);
                this.leftArmJoint.localPosition = new Vector3(0f, -0.6f, 0f);

                this.rightLeg.localPosition = new Vector3(0f, -0.4f, -0.375f);
                this.leftLeg.localPosition = new Vector3(0f, -0.4f, 0.375f);
                this.rightLegJoint.localPosition = new Vector3(0f, -0.6f, 0f);
                this.leftLegJoint.localPosition = new Vector3(0f, -0.6f, 0f);
            }
            else
            {
                this.rightArm.localPosition = new Vector3(0f, 0.3f, -0.5f);
                this.leftArm.localPosition = new Vector3(0f, 0.3f, 0.5f);
                this.rightArmJoint.localPosition = new Vector3(0f, -0.6f, 0f);
                this.leftArmJoint.localPosition = new Vector3(0f, -0.6f, 0f);

                this.rightLeg.localPosition = new Vector3(0f, -0.75f, -0.375f);
                this.leftLeg.localPosition = new Vector3(0f, -0.75f, 0.375f);
                this.rightLegJoint.localPosition = new Vector3(0f, -0.6f, 0f);
                this.leftLegJoint.localPosition = new Vector3(0f, -0.6f, 0f);
            }

            
        }

        /*if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }*/
        //Debug.Log("Test");
    }

    [ContextMenu("Jump")]
    public void Jump()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(0f, 15f, 80f);
            this.leftArm.localEulerAngles = new Vector3(0f, -15f, 80f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);*/

            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void SetPunchPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 90);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }


        /*if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, -5f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/
    }
    public void SetStartPunchPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, -50, -90);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(120, 0, 0);
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        /*if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 6f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/
    }
    public void SetStartPunchPose0()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10, 0, -35);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        /*if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 12f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }*/
    }
    public void SetStartPunchPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, 45);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 70);
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            /*this.rightArm.localEulerAngles = new Vector3(0, -130, -90);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(80, 0, 0);
            this.leftArmJoint.localEulerAngles = Vector3.zero;*/

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void SetPunchEndPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(45, 0, 45);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(0, -35, 90);
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    public void SetKickPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 90);
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void SetStartKickPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 30);
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = new Vector3(0, 0, -50);
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void SetStartKickPose0()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30);
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void SetStartKickPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 50);
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = new Vector3(0, 0, -20);
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    public void SetStartThrowFirePose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(-20, 0, -120);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void SetStartThrowFirePose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -10f, 90f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, 90f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 0f);

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    public void SetStartThrowBombPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, -20);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    public void SetGrabbingPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 90f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;
    }

    public void SetSuperSpinGrabbingPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 90f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;
    }

    public void SetGrabbingStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 100);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 100);

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetSpinGrabEndPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(115f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-115f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(115f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-115f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -60f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -80f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 75f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.65f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.3f, 0f);
        }
    }

    public void SetSpinGrabEndPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 110f);
            this.leftArm.localEulerAngles = new Vector3(-125f, 0f, 100f);
            this.rightArmJoint.localEulerAngles = new Vector3(95f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-80f, 0f, 50f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -60f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -80f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 75f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.65f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.3f, 0f);
        }
    }

    public void SetSpinKickStartAnimPose1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(50, 0, 84);
            this.leftArm.localEulerAngles = new Vector3(31, 0, 135);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(33f, 0, 53);
            this.leftLeg.localEulerAngles = new Vector3(-55f, 0, -33);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(180f, 0f, 50f);
            //this.body.localPosition = new Vector3(0f, 1.25f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.7f, 0f);
        }
    }
    public void SetSpinKickStartAnimPose()
    {
        /*if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(50f, 0, 0);
            this.leftLeg.localEulerAngles = new Vector3(-50f, 0, 0);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(180f, 0f, 50f);
            this.body.localPosition = new Vector3(0f, 1.95f, 0f);
        }*/
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(180, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-180, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(50f, 0, 0);
            this.leftLeg.localEulerAngles = new Vector3(-50f, 0, 0);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(180f, 0f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.5f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.45f, 0f);
        }
    }
    
    public void SetSpinKickAnimPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(90, 0, 0);
            this.leftLeg.localEulerAngles = new Vector3(-90, 0, 0);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(180f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("SetSpinKickAnimPoseStart0")]
    public void SetSpinKickAnimPoseStart0()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(180, 0, 0);
            this.leftArm.localEulerAngles = new Vector3(-180, 0, 0);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 0);

            /*this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);*/

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(180f, 0f, -90f);
            this.body.localEulerAngles = new Vector3(0, 0f, -115f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.45f, 0f);
        }

    }
    //[ContextMenu("SetSpinKickAnimPoseEnd1")]
    public void SetSpinKickAnimPoseEnd1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(180f, 0f, -90f);
            this.body.localEulerAngles = new Vector3(0, 0f, 180f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
        
    }
    //[ContextMenu("SetSpinKickAnimPoseEnd2")]
    public void SetSpinKickAnimPoseEnd2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(180f, 0f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("SetSpinKickAnimPoseEnd3")]
    public void SetSpinKickAnimPoseEnd3()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 69);
            this.leftArm.localEulerAngles = new Vector3(0, 0, 88);
            this.rightArmJoint.localEulerAngles = new Vector3(-70, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(70, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(180f, 0f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 45f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetSpinKickAnimPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10, 0, 20);
            this.leftArm.localEulerAngles = new Vector3(-17, 0, -25);
            this.rightArmJoint.localEulerAngles = new Vector3(0, -40, 100);
            this.leftArmJoint.localEulerAngles = new Vector3(24, 0, 90);

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 90);
            this.leftLeg.localEulerAngles = new Vector3(0, 0, 40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -80);

            this.rightLeg.localScale = new Vector3(1f, 1.4f, 1f);
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    public void SetSpinKickAnimPose2End()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10, 0, 20);
            this.leftArm.localEulerAngles = new Vector3(-17, 0, -25);
            this.rightArmJoint.localEulerAngles = new Vector3(0, -40, 100);
            this.leftArmJoint.localEulerAngles = new Vector3(24, 0, 90);

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 130);
            this.leftLeg.localEulerAngles = new Vector3(0, 0, 40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -80);

            this.rightLeg.localScale = new Vector3(1f, 1.4f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    public void SetSpinKick2AnimPoseStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(40, 0, 14);
            this.leftArm.localEulerAngles = new Vector3(-38, 0, -25);
            this.rightArmJoint.localEulerAngles = new Vector3(0, -40, 100);
            this.leftArmJoint.localEulerAngles = new Vector3(24, 0, 90);

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 90);
            this.leftLeg.localEulerAngles = new Vector3(0, 0, -30);
            this.rightLegJoint.localEulerAngles = new Vector3(0, 0, -100);
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -80);

            //this.rightLeg.localScale = new Vector3(1f, 1.4f, 1f);
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -135f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.7f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, 0f);
        }
    }
    public void SetSpinKickAnimPose2End2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10, 0, 20);
            this.leftArm.localEulerAngles = new Vector3(-17, 0, -25);
            this.rightArmJoint.localEulerAngles = new Vector3(0, -40, 100);
            this.leftArmJoint.localEulerAngles = new Vector3(24, 0, 90);

            this.rightLeg.localEulerAngles = new Vector3(0, 0, 60);
            this.leftLeg.localEulerAngles = new Vector3(0, 0, 40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -80);

            this.rightLeg.localScale = new Vector3(1f, 1.4f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetPunchUpercutAnim()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(169f, 0f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-3f, 0f, -40f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, -39f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 39f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 70f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -17f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -98f);
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -35f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetPunchUppercutStartAnim1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -15f, 90f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 15f, 90f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 67f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -113f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -10f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.48f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.47f, 0f);
        }
    }

    public void SetPunchUppercutStartAnim2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10f, 0f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -15f, 90f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 15f, 90f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -15f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    public void SetPunchUppercutStartAnim3()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10f, 0f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -15f, 90f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 15f, 90f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -15f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.8f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.15f, 0f);
        }
    }

    public void SetKickUppercutStartAnim()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(18f, 0f, -28f);
            this.leftArm.localEulerAngles = new Vector3(-17f, 0f, 24f);
            this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

            this.rightLeg.localEulerAngles = new Vector3(-7f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.rightLegJoint.localEulerAngles = new Vector3(7f, 0f, -25f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);


            this.rightLeg.localScale = new Vector3(1, 1f, 1);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        /*if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -150f, -12f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetKickUppercutStartAnim2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -42f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);

            this.rightLeg.localEulerAngles = new Vector3(-25f, 0f, 20f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -38f);


            this.rightLeg.localScale = new Vector3(1, 1f, 1);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, -30f, 0f);
        }

        /*if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 112f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetKickUpercutAnim0()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 54);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 84);

            this.rightLeg.localEulerAngles = new Vector3(0, -9, 94);
            this.leftLeg.localEulerAngles = new Vector3(-13.7f, 38.85f, -39.69f);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -48);


            this.rightLeg.localScale = new Vector3(1, 1.4f, 1);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = new Vector3(0f, -39f, 0f);
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0, this.transform.forward.z * 90, 50);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetKickUpercutAnim()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 54);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 84);

            this.rightLeg.localEulerAngles = new Vector3(0, -9, 94);
            this.leftLeg.localEulerAngles = new Vector3(-14, 0, -40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -48);


            this.rightLeg.localScale = new Vector3(1, 1.4f, 1);
        }
        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0, this.transform.forward.z * 90, 50);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetKickUpercutAnim2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 54);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 84);

            this.rightLeg.localEulerAngles = new Vector3(0, 45, 94);
            this.leftLeg.localEulerAngles = new Vector3(-14, 0, -40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -48);


            this.rightLeg.localScale = new Vector3(1, 1.4f, 1);
        }
        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0, this.transform.forward.z * 90, 50);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SetKickUpercutAnim3()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 34);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 84);

            this.rightLeg.localEulerAngles = new Vector3(0, 100, 45);
            this.leftLeg.localEulerAngles = new Vector3(-14, 0, -40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -48);


            this.rightLeg.localScale = new Vector3(1, 1f, 1);
        }
        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0, this.transform.forward.z * 90, 30);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("SetKickUpercutEndAnim")]
    public void SetKickUpercutEndAnim()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(65, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-65, 0, -2);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 44);

            /*this.rightArm.localEulerAngles = new Vector3(90, 0, -18);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 34);
            this.rightArmJoint.localEulerAngles = new Vector3(0, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(0, 0, 84);*/

            this.rightLeg.localEulerAngles = new Vector3(14f, 0f, -20f);
            this.leftLeg.localEulerAngles = new Vector3(-14f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -48f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -48f);


            this.rightLeg.localScale = new Vector3(1, 1f, 1);
        }
        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0, this.transform.forward.z * 80, 25);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    /*public void SetKickUpercutAnim4()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(85, 0, 30);
            this.leftArm.localEulerAngles = new Vector3(-85, 0, 30);
            this.rightArmJoint.localEulerAngles = new Vector3(75, 0, 0);
            this.leftArmJoint.localEulerAngles = new Vector3(-75, 0, 0);

            this.rightLeg.localEulerAngles = new Vector3(0, 100, 45);
            this.leftLeg.localEulerAngles = new Vector3(-14, 0, -40);
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = new Vector3(0, 0, -48);
        }
    }*/

    public void SuperRazorKickEnd()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("SuperRazorKickStart")]
    public void SuperRazorKickStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(28.28f, -21.3f, 23.5f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 45f, -35.8f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -29.6f, 81.95f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 53.11f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 37.59f, 117.28f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 12f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -118.1f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -17f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 76f, 7f);
            //this.body.localPosition = new Vector3(0f, 1.9f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
        }
    }

    public void SetFreezePose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(15f, -75f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-15f, 75f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(115f, -53f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-115f, 53f, 0f);

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void PunchBarage1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -50f, -90f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = new Vector3(120f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    public void PunchBarage2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftArm.localEulerAngles = new Vector3(0f, 50f, -90f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);


            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 50f, -90f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);*/

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }

    public void SetPunchLeftPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }
    }
    [ContextMenu("LayDown")]
    public void LayingDownPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(21.465f, -0.892f, 40.568f);
            this.leftArm.localEulerAngles = new Vector3(-16.627f, 36.738f, -12.007f);
            this.rightArmJoint.localEulerAngles = new Vector3(-17.519f, -40.927f, 25.77f);
            this.leftArmJoint.localEulerAngles = new Vector3(-12.297f, -36.043f, -13.808f);

            this.rightLeg.localEulerAngles = new Vector3(-9.554f, 15.118f, 34.909f);
            this.leftLeg.localEulerAngles = new Vector3(18.315f, 0.805f, 33.402f);
            this.rightLegJoint.localEulerAngles = new Vector3(-63.67f, -58.299f, 21.523f);
            this.leftLegJoint.localEulerAngles = new Vector3(-59.733f, 4.011f, -39.567f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 28f, /*this.transform.forward.z **/ -180f, -90f);
            this.body.localPosition = new Vector3(0f, 0.5f, 0f);
        }


        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, -20f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 20f, 0f);
            this.wingRight.localScale = new Vector3(1f, 1f, 1f);
            this.wingLeft.localScale = new Vector3(1f, 1f, 1f);
        }


        this.SetEyes(2);

        //body rotation : x: 29.418, y: 7.114 z: -90
        //body rotation : x: -28, y: -180 z: -90
        //Body pos y: 0.5
    }

    [ContextMenu("RagingBeastDash")]
    public void RagingBeastDash()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(19.6f, 0f, 48.4f);
            this.leftArm.localEulerAngles = new Vector3(-37.7f, 0f, 18.2f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 86.8f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 25.9f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 70.6f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -102.22f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 5f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = Vector3.zero;
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("RagingBeastPose")]
    public void RagingBeastPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-35f, 15f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(35f, -15f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(4f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-4f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-4f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(4f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 36.3f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void RagingBeastStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-34f, 0f, 0f);
            //this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 97f);
            this.rightArmJoint.localEulerAngles = new Vector3(35f, 0f, 110f);
            this.leftArmJoint.localEulerAngles = new Vector3(-54f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(72f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-72f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-72f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(72f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -29.4f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);;
            //this.body.localPosition = new Vector3(0f, 1.51f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.44f, 0f);
        }

        this.SetEyes(0);
    }
    //[ContextMenu("RagingBeastStartMidPose")]
    public void RagingBeastStartMidPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(24f, 0f, 92f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 71f);
            //this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 97f);
            this.rightArmJoint.localEulerAngles = new Vector3(-27f, 0f, 50.5f);
            this.leftArmJoint.localEulerAngles = new Vector3(93f, 0f, 39f);

            this.rightLeg.localEulerAngles = new Vector3(48f, 0f, 45f);
            this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f); ;
            //this.body.localPosition = new Vector3(0f, 1.51f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.44f, 0f);*/

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 10f, this.transform.forward.z * 20f, 0f);
            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 10f, this.transform.forward.z * 0f, 0f);
            this.body.localPosition = new Vector3(-0.08f, this.defaultYPos + 0.05f, this.transform.forward.z * 0.15f);
        }

        this.SetEyes(0);
    }

    public void FlameGrabDash()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90, 0, 90);
            this.leftArm.localEulerAngles = new Vector3(-90, 0, 90);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -55.8f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -49.1f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void FlameGrabDash2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 90f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -55f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -55f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -35f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -35f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("FlameGrabHitPose")]
    public void FlameGrabHitPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(47.33f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-15.9f, 0f, 73.6f);
            this.rightArmJoint.localEulerAngles = new Vector3(-61.3f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 23.36f, 98.9f);

            this.rightLeg.localEulerAngles = new Vector3(6f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-4f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 4f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
            


    }
    [ContextMenu("FlameGrabStartPose")]
    public void FlameGrabStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(69.2f, -35.564f, 47.54f);
            this.leftArm.localEulerAngles = new Vector3(-3.566f, 12.748f, -43.864f);
            this.rightArmJoint.localEulerAngles = new Vector3(9.887f, -2.261f, 84.081f);
            this.leftArmJoint.localEulerAngles = new Vector3(1.869f, 6.131f, 27.654f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -14f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 96f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 9f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -128f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 18f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 4f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("Zero")]
    public void MasterJcapFlameGrab()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 12.5f, 120f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 5f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 23f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -30f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void MasterJcapFlameGrabbed()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 5f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 5f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 180f, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    public void SetGrabbingHeadbutPose1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(-75f, 90f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-75f, 90f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.body.localPosition = new Vector3(-0.1f, this.defaultYPos, 0f);
        }
    }
    public void SetGrabbingHeadbutPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(-105f, 90f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-105f, 90f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.body.localPosition = new Vector3(0.1f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("PhsychoFlamerPose")]
    public void SetPshychoFlamerPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(185f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-185f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(35f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 5f);
            this.leftLeg.localEulerAngles = new Vector3(15f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -90f);
            //this.body.localPosition = new Vector3(0f, 1.67f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.28f, 0f);
        }
    }
    [ContextMenu("PhsychoFlamerStartPose")]
    public void SetPshychoFlamerStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -3f, 45f);
            this.leftArm.localEulerAngles = new Vector3(0f, 18f, 35f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -39f, 90f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 46f, 83f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 67f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -113f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);


            /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -15f, 90f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 15f, 90f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 67f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -113f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 20f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.48f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.47f, 0f);
        }
    }
    [ContextMenu("PhsychoFlamerStartPose2")]
    public void SetPshychoFlamerStartPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

            this.rightLeg.localEulerAngles = new Vector3(-12f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(6f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -7f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 14f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("PhsychoFlamerEndPose")]
    public void PshychoFlamerEndPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 45f, 60f);
            this.leftArm.localEulerAngles = new Vector3(0f, -45f, 60f);
            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(9f, 0f, 45f);
            this.leftLeg.localEulerAngles = new Vector3(-9f, 0f, 45f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("SuperFireBallShoot")]
    public void SuperFireBallShoot(bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(74.6f, -97.1f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-99.33f, 98.2f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(14.13f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (inAir)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 70f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 16.1f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            }

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (inAir)
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            else
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);

            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.39f, 0f);
        }
    }
    //[ContextMenu("SuperFireBallCharge")]
    public void SuperFireBallCharge(bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 65f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 65f);
            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 15f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, -35f, 0f);

            if (inAir)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 70f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 16.1f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 64.19f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -23.33f, 0f);

        if (this.body != null)
        {
            if(inAir)
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            else
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);

            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.56f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.39f, 0f);
        }
    }
    [ContextMenu("GrandFlame")]
    public void GrandFlame()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(35f, 45f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-35f, -45f, 20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 14f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("GrandFlameStart")]
    public void GrandFlameStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 14f, 145f);
            this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -25f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 15f, 60f);

            this.rightLeg.localEulerAngles = new Vector3(25f, 0f, -20f);
            this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(-4f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 4f, this.transform.forward.z * 90f, 20f);
            //this.body.localPosition = new Vector3(0f, 1.85f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.1f, 0f);
        }
    }
    //[ContextMenu("GrandFlameMid")]
    public void GrandFlameMid()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

            this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 80f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -90f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 10f, -90f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 10f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -20f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("SpinAttack")]
    public void SpinAttack()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(5f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = Vector3.zero;
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("SpinAttackStart")]
    public void SpinAttackStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 75f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -45f);
            this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 30f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);*/

            this.rightArm.localEulerAngles = new Vector3(20f, 20f, 75f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 20f, -75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-40f, 0f, 30f);
            this.leftArmJoint.localEulerAngles = new Vector3(40f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("NinjaTeleport")]
    public void NinjaTeleport()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(0f, -10f, 70f);
            this.leftArm.localEulerAngles = new Vector3(0f, 10f, 70f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -46f, 86f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 46f, 86f);*/

            this.rightArm.localEulerAngles = new Vector3(0f, -10f, 60f);
            this.leftArm.localEulerAngles = new Vector3(0f, 10f, 60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -44f, 86f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 44f, 86f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("ShootPose")]
    public void ShootPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 80f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, 80f);
            this.rightArmJoint.localEulerAngles = new Vector3(5f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(45f, -15f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 20f, 30f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
            this.leftLegJoint.localEulerAngles = new Vector3(5f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("ShootPose2")]
    public void ShootPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 100f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, 100f);
            this.rightArmJoint.localEulerAngles = new Vector3(5f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(45f, -15f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 20f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
            this.leftLegJoint.localEulerAngles = new Vector3(5f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("ShootPoseCrouch")]
    public void ShootPoseCrouch(bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 80f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, 80f);
            this.rightArmJoint.localEulerAngles = new Vector3(5f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(45f, -15f, 0f);

            if (inAir)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 70f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 16.1f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            }


            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (inAir)
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            else
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);


            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.39f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.56f, 0f);
        }
    }

    //[ContextMenu("ShootPoseCrouch2")]
    public void ShootPoseCrouch2(bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 100f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, 100f);
            this.rightArmJoint.localEulerAngles = new Vector3(5f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(45f, -15f, 0f);

            if (inAir)
            {
                //this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
                //this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -75f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 16.1f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -70f);
            }


            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (inAir)
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
            else
                this.body.localEulerAngles = new Vector3(0f, 0f, 15f);


            //this.body.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.39f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.56f, 0f);
        }
    }

    public void GunFiredPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0, 0, 100);
            this.leftArm.localEulerAngles = new Vector3(-20, 0, 0);
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("GunReload")]
    public void GunReload()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -16f, 87f);
            this.leftArm.localEulerAngles = new Vector3(-1f, 0f, 65f);
            this.rightArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(52, 0, 0f);

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("SuperFlameSpin1")]
    public void SuperFlameSpin1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(125f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-125f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("SuperFlameSpin2")]
    public void SuperFlameSpin2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 75f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void DarkJCapStartAnimation()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

            this.rightLeg.localEulerAngles = new Vector3(-12f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(6f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -7f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    
    public void StartAnimationRagingBeastBlock()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 54f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 25.5f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 31f);

            this.rightLeg.localEulerAngles = new Vector3(15f, 0f, -15f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(15f, 0f, -15f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 9f, -2f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    public void StartAnimationRagingBeastBlock1()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 58f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, 80f);
            this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 67f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, -76f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    
    public void StartAnimationRagingBeastBlock2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 55.5f, 75f);
            this.leftArm.localEulerAngles = new Vector3(0f, -38f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-87f, 0f, 67.5f);
            this.leftArmJoint.localEulerAngles = new Vector3(87f, 0f, 37.5f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-13f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 9f, 15f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    public void StartAnimationRagingBeastGrab()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(this.transform.forward.z > 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0, 0, 95);
                this.leftArm.localEulerAngles = new Vector3(0, 0, 85);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0, 0, 85);
                this.leftArm.localEulerAngles = new Vector3(0, 0, 95);
            }
            
            this.rightArmJoint.localEulerAngles = Vector3.zero;
            this.leftArmJoint.localEulerAngles = Vector3.zero;

            this.rightLeg.localEulerAngles = Vector3.zero;
            this.leftLeg.localEulerAngles = Vector3.zero;
            this.rightLegJoint.localEulerAngles = Vector3.zero;
            this.leftLegJoint.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;
    }


    public void MCapRagingBeastBlock()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 54f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-40f, 0f, 25.5f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 80f);

            this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 95f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -90f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 9f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("ATrail")]
    public void McapStartAnimStrike()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 40f, 130f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -40f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 105f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 16f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -55f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * -30f, -50f);
            this.body.localPosition = new Vector3(0.55f, this.defaultYPos - 0.5f, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    public void DarkStrikeBlock()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -15f, 54f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 25.5f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 82f);

            this.rightLeg.localEulerAngles = new Vector3(15f, 0f, -15f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(15f, 0f, -15f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 70f, -2f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }


    public void McapStrikeStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(145f, 0f, 10f);
            this.leftArm.localEulerAngles = new Vector3(-33f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(70f, 0f, 25f);
            this.leftArmJoint.localEulerAngles = new Vector3(-33f, 0f, 32f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 100f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -1210f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 20f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -30f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 44f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    public void McapStrikeEnd()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 40f, 35f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -20f);
            this.rightArmJoint.localEulerAngles = new Vector3(-40f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 75f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -14f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -55f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * -30f, -20f);
            this.body.localPosition = new Vector3(0.22f, this.defaultYPos - 0.3f, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    //[ContextMenu("J-Cap vs Dark J-Cap")]
    public void JcapVsDarkStartAnimation()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (this.transform.forward.z > 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -10f, 85f);
                this.leftArm.localEulerAngles = new Vector3(-46f, 0f, -48f);
                this.rightArmJoint.localEulerAngles = new Vector3(-65f, 0f, 34f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 38f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(46f, 0f, -48f);
                this.leftArm.localEulerAngles = new Vector3(0f, 8f, 100f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 38f);
                this.leftArmJoint.localEulerAngles = new Vector3(64f, 0f, 0f);
            }

            

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (this.transform.forward.z > 0)
                this.body.localEulerAngles = new Vector3(0f, 0f, -19f);
            else
                this.body.localEulerAngles = new Vector3(0f, -11.5f, -19f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    //[ContextMenu("J-Cap vs Dark J-Cap 0")]
    public void JcapVsDarkStartAnimation0()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (this.transform.forward.z > 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 138f, 85f);
                this.leftArm.localEulerAngles = new Vector3(-46f, 0f, -48f);
                this.rightArmJoint.localEulerAngles = new Vector3(-105f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 38f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(46f, 0f, -48f);
                this.leftArm.localEulerAngles = new Vector3(0f, -138f, 85f);
                //this.leftArm.localEulerAngles = new Vector3(0f, -174f, 85f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 38f);
                this.leftArmJoint.localEulerAngles = new Vector3(105f, 0f, 0f);
                //this.leftArmJoint.localEulerAngles = new Vector3(45f, 0f, 0f);
            }



            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (this.transform.forward.z > 0)
                this.body.localEulerAngles = new Vector3(0f, 0f, -19f);
            else
                this.body.localEulerAngles = new Vector3(0f, -11.5f, -19f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("JCapLandingStartAnimation")]
    public void JCapLandingStartAnimation()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 40f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, -75f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(85f, -43f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-89f, 35f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-78f, -47f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightArm.localScale = new Vector3(1f, 1.1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -36.5f);
            //this.body.localPosition = new Vector3(0f, 1.1f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.85f, 0f);
        }
    }

    [ContextMenu("JCapLandingStartAnimation2")]
    public void JCapLandingStartAnimation2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(0f, 15f, -75f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(67f, -43f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-68f, 35f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-78f, -47f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightArm.localScale = new Vector3(1f, 1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -15f);
            //this.body.localPosition = new Vector3(0f, 1.4f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.55f, 0f);
        }
    }

    //[ContextMenu("Laughing")]
    public void Laughing(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (this.characterId == 3)
            {
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -5f);
                this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
            }
            else
            {
                this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -5f);
                this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
            }

            if (stage == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 90f);
                //this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                //this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
                //this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 85f);
                //this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                //this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if(stage == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.06f, this.defaultYPos, 0f);
            }
            
        }

        if (this.neutralEyes != null && this.happyEyes != null)
        {
            this.happyEyes.gameObject.SetActive(true);
            this.neutralEyes.gameObject.SetActive(false);
        }
    }

    //[ContextMenu("Laughing2")]
    public void Laughing2(int stage = 0)
    {
        //int stage = 0;
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stage == 1)
            {
                /*this.rightArm.localEulerAngles = new Vector3(0f, -9f, 50f);
                this.leftArm.localEulerAngles = new Vector3(0f, 9f, 50f);*/

                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, 0f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(-70, 0f, 12f);
                this.leftArmJoint.localEulerAngles = new Vector3(66f, 0f, -3f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos + 0.05f, this.rightArm.localPosition.z);
                this.leftArm.localPosition = new Vector3(this.leftArm.localPosition.x, this.armsDefaultYPos + 0.05f, this.leftArm.localPosition.z);
            }
            else
            {
                /*this.rightArm.localEulerAngles = new Vector3(0f, -9f, 45f);
                this.leftArm.localEulerAngles = new Vector3(0f, 9f, 45f);*/

                this.rightArm.localEulerAngles = new Vector3(15f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, -5f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(-70, 0f, 12f);
                this.leftArmJoint.localEulerAngles = new Vector3(66f, 0f, -3f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos, this.rightArm.localPosition.z);
                this.leftArm.localPosition = new Vector3(this.leftArm.localPosition.x, this.armsDefaultYPos, this.leftArm.localPosition.z);
            }

            /*this.rightArmJoint.localEulerAngles = new Vector3(-70, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 12f);*/

            this.rightArmJoint.localEulerAngles = new Vector3(-15, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, 12f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stage == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.06f, this.defaultYPos, 0f);
            }

        }

        if (this.neutralEyes != null && this.happyEyes != null)
        {
            this.happyEyes.gameObject.SetActive(true);
            this.neutralEyes.gameObject.SetActive(false);
        }
    }

    public void Laughing3(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            
            if (this.characterId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);

                /*this.rightArm.localEulerAngles = new Vector3(40f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(-95f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(95f, 0f, 0f);*/
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(40f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
            }
            

            if (stage == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos + 0.05f, this.rightArm.localPosition.z);
                this.leftArm.localPosition = new Vector3(this.leftArm.localPosition.x, this.armsDefaultYPos + 0.05f, this.leftArm.localPosition.z);*/
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos, this.rightArm.localPosition.z);
                this.leftArm.localPosition = new Vector3(this.leftArm.localPosition.x, this.armsDefaultYPos, this.leftArm.localPosition.z);*/
            }

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stage == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.06f, this.defaultYPos, 0f);
            }

        }

        if (this.neutralEyes != null && this.happyEyes != null)
        {
            this.happyEyes.gameObject.SetActive(true);
            this.neutralEyes.gameObject.SetActive(false);
        }
    }


    //[ContextMenu("Zero")]
    public void HappyJumping(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stage == 1 || stage == 2 || stage == 5 || stage == 6)
            {
                this.rightArm.localEulerAngles = new Vector3(140f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-140f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
            }
            else if (stage >= 18)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -10f, 45f);
                this.leftArm.localEulerAngles = new Vector3(-155f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-85f, 0f, 45f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -10f, 45f);
                this.leftArm.localEulerAngles = new Vector3(0f, 10f, 45f);
                this.rightArmJoint.localEulerAngles = new Vector3(-85f, 0f, 45f);
                this.leftArmJoint.localEulerAngles = new Vector3(85f, 0f, 45f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stage == 7 || stage == 11 || stage == 15)
                this.upperBody.localEulerAngles = new Vector3(0f, -5f, 0f);
            else if (stage == 9 || stage == 13 || stage == 17)
                this.upperBody.localEulerAngles = new Vector3(0f, 5f, 0f);
            else
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);

            //this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            if (stage == 7 || stage == 11 || stage == 15)
                this.eyes.localEulerAngles = new Vector3(0f, -10f, 0f);
            else if (stage == 9 || stage == 13 || stage == 17)
                this.eyes.localEulerAngles = new Vector3(0f, 10f, 0f);
            else
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        if (this.body != null)
        {
            if (stage == 1 || stage == 2 || stage == 5 || stage == 6)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos + 0.55f, 0f);
            }
            else if (stage >= 18)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -5f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            
        }

        if (this.neutralEyes != null && this.happyEyes != null)
        {
            this.happyEyes.gameObject.SetActive(true);
            this.neutralEyes.gameObject.SetActive(false);
        }
    }


    [ContextMenu("Roll")]
    public void RollAnimation()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(60f, -100f, -20f);
            this.leftArm.localEulerAngles = new Vector3(-60f, 100f, -20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 10f, 85f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, -10f, 85f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -138f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -138f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void WalkPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -50f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = Vector3.zero;
            this.lowerBody.localEulerAngles = Vector3.zero;
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = Vector3.zero;

        if (this.body != null)
        {
            this.body.localEulerAngles = Vector3.zero;
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        //Debug.Log("Test");
    }
    //[ContextMenu("LDance")]
    public void LDance(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(16f, 0f, 140f);
            this.leftArm.localEulerAngles = new Vector3(13f, 0f, 42f);
            this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 15f);
            this.leftArmJoint.localEulerAngles = new Vector3(25f, 0f, 0f);

            if(stage == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-75f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(75f, 0f, 0f);
            }
            else if (stage == 2)
            {
                this.rightLeg.localEulerAngles = new Vector3(80f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-80f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            if(stage == 1)
                this.body.localPosition = new Vector3(0f, this.defaultYPos + 0.25f, 0f);
            else
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void AnvilHit()
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

            this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            this.rightArm.localScale = new Vector3(1f, 1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    [ContextMenu("Wry")]
    public void Wry()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(50f, 0f, -25f);
            this.leftArm.localEulerAngles = new Vector3(-50f, 0f, -25f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);

            this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 54f, 47f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 30f, 47f);
            //this.body.localPosition = new Vector3(0f, 1.47f, 0.1f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.48f, 0.1f);
        }

        if (this.dress != null)
            this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    [ContextMenu("Wry2")]
    public void Wry2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, -25f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -25f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);

            this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 20f);
            this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 20f);
            this.rightLegJoint.localEulerAngles = new Vector3(-15f, 0f, -30f);
            this.leftLegJoint.localEulerAngles = new Vector3(15f, 0f, -30f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 54f, 47f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 30f, 10f);
            //this.body.localPosition = new Vector3(0f, 1.47f, 0.1f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.1f, 0.1f);
        }
    }

    [ContextMenu("TestPose")]
    public void TestPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(170f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-170f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 9f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-20f, 0f, -9f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -16f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
            
    }

    [ContextMenu("TestPose2")]
    public void TestPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(3f, 0f, 43f);
            this.leftArm.localEulerAngles = new Vector3(-118f, 20f, 48f);
            this.rightArmJoint.localEulerAngles = new Vector3(-110f, 0f, 32);
            this.leftArmJoint.localEulerAngles = new Vector3(1f, -34f, 97f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 9f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(20f, 0f, -9f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 3f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

    }
    [ContextMenu("TestPose3")]
    public void TestPose3()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            //this.rightArm.localEulerAngles = new Vector3(3f, 0f, 43f);
            this.rightArm.localEulerAngles = new Vector3(3f, 5f, 43f);
            this.leftArm.localEulerAngles = new Vector3(-153f, 0f, 34f);
            //this.leftArm.localEulerAngles = new Vector3(-153f, 0f, 38f);
            this.rightArmJoint.localEulerAngles = new Vector3(-110f, 0f, 32);
            this.leftArmJoint.localEulerAngles = new Vector3(1f, -34f, 97f);

            this.rightLeg.localEulerAngles = new Vector3(32f, 0f, 9f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-20f, 0f, -9f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -18f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

    }
    [ContextMenu("TestPose4")]
    public void TestPose4()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 42f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -35f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-39f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 62f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 15f, this.transform.forward.z * 90f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.82f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.13f, 0f);
        }
    }
    [ContextMenu("TestPose5")]
    public void TestPose5()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(this.characterId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(195f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-195f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(35f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(185f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-185f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(38.5f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-38.5f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }
    [ContextMenu("TestPose6")]
    public void TestPose6()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(195f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-195f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(35f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void TestPose7()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 22f, 62f);
            this.leftArm.localEulerAngles = new Vector3(-1f, 0f, 42f);
            this.rightArmJoint.localEulerAngles = new Vector3(-116f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(55f, 0f, -13f);

            this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, 0f);
            //this.body.localPosition = new Vector3(0f, 1.82f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.06f, 0f);
        }
    }

    [ContextMenu("LayDown")]
    public void LayDown()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, -15f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -15f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, -15f);
            this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 90f);
            if (this.characterId == 3)
                this.body.localPosition = new Vector3(0f, this.defaultYPos -1.35f, 0f);
            else
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 1.7f, 0f);
        }

        this.SetEyes(2);
    }

    [ContextMenu("SpinEnd")]
    public void SpinEnd()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(150f, 0f, -30f);
            this.leftArm.localEulerAngles = new Vector3(-150f, 0f, -30f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(-35f, -15f, -65f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 7.5f, -25f);
            this.body.localPosition = new Vector3(0.22f, this.defaultYPos, 0f);
        }
    }


    
    //[ContextMenu("sentai")]
    public void Sentai(int animId = 2)
    {
        
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (animId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -10f, 80f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 80f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
                this.leftArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (animId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 13f, 92f);
                //this.leftArm.localEulerAngles = new Vector3(0f, -13f, 82f);
                this.leftArm.localEulerAngles = new Vector3(0f, -13f, 72f);
                this.rightArmJoint.localEulerAngles = new Vector3(-88f, 0f, 47f);
                //this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, -15f);
                this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(51f, -18f, 8f);
                this.leftLeg.localEulerAngles = new Vector3(-42f, -16f, 7f);
                this.rightLegJoint.localEulerAngles = new Vector3(-37f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(12f, 0f, 0f);
            }
            else if (animId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 38f, 64f);
                this.leftArm.localEulerAngles = new Vector3(0f, -28f, 64f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(84f, 0f, 4f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(20f, 0f, 0f);
            }
            else if (animId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -33f);
                this.leftArm.localEulerAngles = new Vector3(0f, -8.5f, 76f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 120f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 23.5f);

                this.rightLeg.localEulerAngles = new Vector3(45f, -52f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-37.5f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-45f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (animId == 5)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 40f, 88f);
                this.leftArm.localEulerAngles = new Vector3(0f, -81f, 84f);
                this.rightArmJoint.localEulerAngles = new Vector3(-88f, 50f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 23f);

                /*this.rightArm.localEulerAngles = new Vector3(0f, 40f, 109f);
                this.leftArm.localEulerAngles = new Vector3(0f, -81f, 58f);
                this.rightArmJoint.localEulerAngles = new Vector3(-53f, 74f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(52f, 0f, 53f);*/

                this.rightLeg.localEulerAngles = new Vector3(51f, -18f, 8f);
                this.leftLeg.localEulerAngles = new Vector3(-42f, -16f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-37f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(12f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 22f, 48f);
                this.leftArm.localEulerAngles = new Vector3(0f, -44f, 109f);
                this.rightArmJoint.localEulerAngles = new Vector3(-88f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(51f, -18f, 8f);
                this.leftLeg.localEulerAngles = new Vector3(-42f, -16f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-37f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(12f, 0f, 0f);
            }
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            if (animId == 1)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (animId == 2)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (animId == 3)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (animId == 4)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (animId == 5)
                this.lowerBody.localEulerAngles = new Vector3(0f, -10f, 0f);
            else
                this.lowerBody.localEulerAngles = new Vector3(0f, 10f, 0f);
        }

        if (this.eyes != null)
        {
            if (animId == 1)
                this.eyes.localEulerAngles = new Vector3(0f, -17f, 0f);
            /*else if (animId == 2)
                this.eyes.localEulerAngles = new Vector3(0f, -18f, 3f);*/
            else if (animId == 2)
                this.eyes.localEulerAngles = new Vector3(0f, -18f, 5f);
            else if (animId == 3)
                this.eyes.localEulerAngles = new Vector3(0f, -8f, 0f);
            else if (animId == 4)
                this.eyes.localEulerAngles = new Vector3(0f, -16f, 0f);
            else if (animId == 5)
                this.eyes.localEulerAngles = new Vector3(0f, -17f, 0f);
            /*else if (animId == 5)
                this.eyes.localEulerAngles = new Vector3(0f, -6f, 0f);*/
            else
                this.eyes.localEulerAngles = new Vector3(0f, -17f, 0f);
        }
            

        if (this.body != null)
        {
            if (animId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 95f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
            }
            else if (animId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 108f, -7f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, 0f);
            }
            else if (animId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.15f, 0f);
            }
            else if (animId == 4)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 117f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, 0f);
            }
            else if (animId == 5)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 108f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, 0f);
            }
            
        }
    }



    //Mike Baller Animations
    //[ContextMenu("SummonLightningCloud")]
    public void SummonLightningCloud(float stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 13f, 45f);
            //this.leftArm.localEulerAngles = new Vector3(0f, -15f, 115f);

            if (stage == 0)
            {
                this.leftArm.localEulerAngles = new Vector3(0f, -15f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 25f);
            }
            else if (stage == 2)
            {
                this.leftArm.localEulerAngles = new Vector3(0f, -15f, 130f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 75f);
            }
            else
            {
                this.leftArm.localEulerAngles = new Vector3(0f, -15f, 115f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 55f);
            }

            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
            //this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 55f);

            this.rightLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

            //this.leftArm.localEulerAngles = new Vector3(0f, -15f, 95f);
            //this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 70f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 13f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("ElectricAttackPose")]
    public void ElectricAttackPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 25f, 50f);
            this.leftArm.localEulerAngles = new Vector3(0f, -25f, 50f);
            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 35f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 35f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 35f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, -35f, 60f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -100f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -100f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.45f, 0f);
        }
    }
    [ContextMenu("ElectricAttackPose2")]
    public void ElectricAttackPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 85f, 50f);
            this.leftArm.localEulerAngles = new Vector3(0f, -85f, 50f);
            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 35f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 35f);

            this.rightLeg.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.55f, 0f);
        }
    }

    //[ContextMenu("Zero")]
    public void BallerWavePose(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stage == 1)
                this.rightArm.localEulerAngles = new Vector3(180f, 0f, 0f);
            else if (stage == 2)
                this.rightArm.localEulerAngles = new Vector3(200f, 0f, 0f);
            else
                this.rightArm.localEulerAngles = new Vector3(160f, 0f, 0f);

            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
        this.SetEyes(1);

        /*if(this.neutralEyes != null && this.happyEyes != null)
        {
            this.happyEyes.gameObject.SetActive(true);
            this.neutralEyes.gameObject.SetActive(false);
        }*/
    }

    //[ContextMenu("BallerDrinkStart")]
    public void BallerDrinkStart(bool confused = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(10f, 0f, 50f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 20f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            if (!confused)
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            else
                this.eyes.localEulerAngles = new Vector3(0f, 35f, 0f);
        }
            

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("BallerDrink")]
    public void BallerDrink()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(35f, -48f, 21f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, -6f, 70f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if(this.characterId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(65f, -48f, 21f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, -6f, 85f);
            }


            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.body.localPosition = new Vector3(-0.12f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("BallerPullRope")]
    public void BallerPullRope(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, -35f);
            this.rightArmJoint.localEulerAngles = new Vector3(-85f, 0f, -30f);*/

            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);

            if (stageId == 1)
            {
                this.leftArm.localEulerAngles = new Vector3(-60f, 0f, -43f);
                this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 70f);
            }
            else
            {
                /*this.leftArm.localEulerAngles = new Vector3(-70f, 0f, -45f);
                this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 60f);*/

                this.leftArm.localEulerAngles = new Vector3(-80f, 0f, -45f);
                this.leftArmJoint.localEulerAngles = new Vector3(-130f, 0f, 70f);
            }

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("ViolentBalletStartAnim")]
    public void ViolentBalletStartAnim(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(0f, 20f, 95f);
            this.leftArm.localEulerAngles = new Vector3(0f, -20f, 95f);
            this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(35f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-35f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-35f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(35f, 0f, -15f);*/

            this.rightArm.localEulerAngles = new Vector3(0f, 20f, 70f);
            this.leftArm.localEulerAngles = new Vector3(0f, -20f, 70f);
            this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(32f, 0f, 45f);
            this.leftLeg.localEulerAngles = new Vector3(-32f, 0f, 45f);
            this.rightLegJoint.localEulerAngles = new Vector3(-35f, 0f, -63f);
            this.leftLegJoint.localEulerAngles = new Vector3(35f, 0f, -63f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            if (stage == 1)
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 10f);
            else
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, -55f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.8f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.6f, 0f);
        }
    }


    //[ContextMenu("ElectricGuitar")]
    public void ElectricGuitar(int stage = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stage == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(3f, 0f, 69f);
                this.leftArm.localEulerAngles = new Vector3(-53f, 0f, 32f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, -10f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, -25f, 81f);

                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, -15f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(39f, 0f, 69f);
                this.leftArm.localEulerAngles = new Vector3(-53f, 0f, 32f);
                this.rightArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 81f);

                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, -10f);
            }

            

            //this.rightLeg.localEulerAngles = new Vector3(10f, 0f, -10f);
            this.leftLeg.localEulerAngles = new Vector3(-11f, 0f, 8f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -13f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stage == 1)
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -5f, this.transform.forward.z * 90f, 10f);
            else
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -5f, this.transform.forward.z * 90f, 5f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(3);
    }

    [ContextMenu("Sax")]
    public void Sax()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 10f, 75f);
            this.leftArm.localEulerAngles = new Vector3(0f, -10f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 15f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 5f, 5f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("RobotTeleportStartAnim")]
    public void RobotTeleportStartAnim()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, -30f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, -30f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 6f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 25f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -60f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -65f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -30f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.45f, 0f);
        }
    }

    [ContextMenu("Sit")]
    public void Sit()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(40f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-40f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 75f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 75f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.95f, 0f);
        }
    }
    //[ContextMenu("ClubDanceBow")]
    public void ClubDanceBow(int stageId = 0, bool right = true)
    {
        
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if(stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 25f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stageId == 1)
            {
                /*this.body.localEulerAngles = new Vector3(0f, 0f, -50f);
                //this.body.localPosition = new Vector3(0.55f, this.defaultYPos - 0.2f, 0f);
                this.body.localPosition = new Vector3(0, this.defaultYPos - 0.2f, 0f);*/
                if (right)
                {
                    this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, -50f);
                    this.body.localPosition = new Vector3(0.39f, this.defaultYPos - 0.2f, this.transform.forward.z * -0.39f);

                    /*this.body.localEulerAngles = new Vector3(0f, 0f, -50f);
                    this.body.localPosition = new Vector3(0.55f, this.defaultYPos - 0.2f, 0f);*/

                    //this.body.localPosition = new Vector3(0, this.defaultYPos - 0.2f, 0f);
                }
                else
                {
                    this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135, -50f);
                    this.body.localPosition = new Vector3(-0.39f, this.defaultYPos - 0.2f, this.transform.forward.z * -0.39f);

                    /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180, -50f);
                    this.body.localPosition = new Vector3(-0.55f, this.defaultYPos - 0.2f, 0f);*/
                }
            }
            else
            {
                /*this.body.localEulerAngles = new Vector3(0f, 0f, -25f);
                //this.body.localPosition = new Vector3(0.169f, this.defaultYPos - 0.04f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.04f, 0f);*/

                if (right)
                {
                    this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, -25f);
                    this.body.localPosition = new Vector3(0.13f, this.defaultYPos - 0.04f, this.transform.forward.z * -0.13f);

                    /*this.body.localEulerAngles = new Vector3(0f, 0f, -25f);
                    this.body.localPosition = new Vector3(0.169f, this.defaultYPos - 0.04f, 0f);*/
                }
                else
                {
                    this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, -25f);
                    this.body.localPosition = new Vector3(-0.13f, this.defaultYPos - 0.04f, this.transform.forward.z * -0.13f);

                    /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180, -25f);
                    this.body.localPosition = new Vector3(-0.169f, this.defaultYPos - 0.04f, 0f);*/
                }
            }
        }
    }


    //[ContextMenu("ClubDance")]
    public void ClubDance(int stageId = 0, int armStageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(armStageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(-10f, 0f, 40f);
                this.leftArm.localEulerAngles = new Vector3(10f, 0f, 40f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, 0f);
            }
            else if (armStageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(-10f, 0f, 30f);
                this.leftArm.localEulerAngles = new Vector3(10f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(-10f, 0f, 50f);
                this.leftArm.localEulerAngles = new Vector3(10f, 0f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, 0f);
            }


            if (stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 10f);
            }
            else if (stageId == 2)
            {
                this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(10f, 0f, -10f);
            }
            else if (stageId == 4)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 5)
            {
                this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, -10f);
            }
            else if (stageId == 6)
            {
                this.rightLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
            }
            else if (stageId == 7)
            {
                this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 10f);
            }
            else if (stageId == 8)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);*/

            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*if (stageId == 1)
                this.lowerBody.localEulerAngles = new Vector3(10f, 0f, 10f);
            else if (stageId == 2)
                this.lowerBody.localEulerAngles = new Vector3(15f, 0f, 0f);
            else if (stageId == 3)
                this.lowerBody.localEulerAngles = new Vector3(10f, 0f, -10f);
            else if (stageId == 4)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, -15f);
            else if (stageId == 5)
                this.lowerBody.localEulerAngles = new Vector3(-10f, 0f, -10f);
            else if (stageId == 6)
                this.lowerBody.localEulerAngles = new Vector3(-15f, 0f, 0f);
            else if (stageId == 7)
                this.lowerBody.localEulerAngles = new Vector3(-10f, 0f, 10f);
            else if (stageId == 8)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 15f);
            else
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);*/


        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -10f, this.transform.forward.z * 90f, -10f);
                this.body.localPosition = new Vector3(-0.065f, this.defaultYPos - 0.05f, this.transform.forward.z * -0.08f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -15f, this.transform.forward.z * 90f, 0f);
                //this.body.localPosition = new Vector3(-0.065f, this.defaultYPos - 0.05f, 0f);
                this.body.localPosition = new Vector3(-0.0975f, this.defaultYPos - 0.05f, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -10f, this.transform.forward.z * 90f, 10f);
                this.body.localPosition = new Vector3(-0.065f, this.defaultYPos - 0.05f, this.transform.forward.z * 0.08f);
            }
            else if (stageId == 4)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 15f);
                //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, this.transform.forward.z * 0.08f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, this.transform.forward.z * 0.12f);
            }
            else if (stageId == 5)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 10f, this.transform.forward.z * 90f, 10f);
                this.body.localPosition = new Vector3(0.065f, this.defaultYPos - 0.05f, this.transform.forward.z * 0.08f);
            }
            else if (stageId == 6)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 15f, this.transform.forward.z * 90f, 0f);
                //this.body.localPosition = new Vector3(0.065f, this.defaultYPos - 0.05f, 0f);
                this.body.localPosition = new Vector3(0.0975f, this.defaultYPos - 0.05f, 0f);
            }
            else if (stageId == 7)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 10f, this.transform.forward.z * 90f, -10f);
                this.body.localPosition = new Vector3(0.065f, this.defaultYPos - 0.05f, this.transform.forward.z * -0.08f);
            }
            else if (stageId == 8)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -15f);
                //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, this.transform.forward.z * -0.08f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, this.transform.forward.z * -0.12f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }

        }
    }

    public void ClubDanceWave(int stageId = 0, bool leftArm = true)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (leftArm)
            {
                if (stageId == 1)
                {
                    this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else if (stageId == 2)
                {
                    this.leftArm.localEulerAngles = new Vector3(-140f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else if (stageId == 3)
                {
                    this.leftArm.localEulerAngles = new Vector3(-185f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }

                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(3f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(3f, 0f, 0f);
            }
            else
            {
                if (stageId == 1)
                {
                    this.rightArm.localEulerAngles = new Vector3(90f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else if (stageId == 2)
                {
                    this.rightArm.localEulerAngles = new Vector3(140f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else if (stageId == 3)
                {
                    this.rightArm.localEulerAngles = new Vector3(185f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }


                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-3f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-3f, 0f, 0f);
            }
            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/


            /*if (leftArm)
            {
                this.rightLeg.localEulerAngles = new Vector3(3f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(3f, 0f, 0f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(-3f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-3f, 0f, 0f);
            }*/
            
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if(leftArm)
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -3f, this.transform.forward.z * 90f, 0f);
            else
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 3f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }


    //[ContextMenu("AlienGunShoot")]
    public void AlienGunShoot(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
                this.rightArm.localEulerAngles = new Vector3(0f, -15f, 80f);
            else
                this.rightArm.localEulerAngles = new Vector3(0f, -15f, 75f);

            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("C4Detonate")]
    public void C4Detonate(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("RoseThrow")]
    public void RoseThrow()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, -120f);
            this.leftArm.localEulerAngles = new Vector3(-17.5f, 0f, 32f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, -37f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 28f, 85f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    //[ContextMenu("Shrug")]
    public void Shrug(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, -10f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 10f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(50f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-50f, 0f, 0f);

                
            }
            else if (stageId == 2)
            {
                /*this.rightArm.localEulerAngles = new Vector3(30f, -20f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 20f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(125f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-125f, 0f, 0f);*/


                this.rightArm.localEulerAngles = new Vector3(20f, -20f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 20f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(135f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-135f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -7f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 7f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 7f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -7f);*/


            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -7f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -7f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if(stageId == 1)
                this.upperBody.localEulerAngles = new Vector3(0f, 55f, 0f);
            else if (stageId == 2)
                this.upperBody.localEulerAngles = new Vector3(0f, 55f, 0f);
            else
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 15f, 0f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 20f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    [ContextMenu("Dizzy")]
    public void Dizzy()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 95f);
            this.rightArmJoint.localEulerAngles = new Vector3(-25f, 0f, 40f);
            this.leftArmJoint.localEulerAngles = new Vector3(-70f, 0f, 99f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(2);
    }

    //[ContextMenu("SpinGrab")]
    public void SpinGrab()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -10f, 90f);
            this.leftArm.localEulerAngles = new Vector3(0f, 10f, 90f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);*/

            this.rightLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.1f, 0f);
        }
    }

    //[ContextMenu("SpinGrabbed")]
    public void SpinGrabbed()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(135f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-135f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(180f, 180f, 90f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos + 0.15f, 0f);
        }

        this.SetEyes(2);
    }

    //[ContextMenu("SpinGrab")]
    public void SpinGrabEnd(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stageId == 1)
            {
                
                this.rightArm.localEulerAngles = new Vector3(0f, 20f, 130f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 130f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 10f, 70f);
                this.leftArm.localEulerAngles = new Vector3(0f, -10f, 70f);
            }
            
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }
    }

    public void SpinGrabStart(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(90f, 0f, -15f);
                this.leftArm.localEulerAngles = new Vector3(-90f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(90f, 0f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 90f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 80f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 80f);
                this.rightArmJoint.localEulerAngles = new Vector3(-35f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(35f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -45f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(100f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(-100f, 0f, -40f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.body.localPosition = new Vector3(0.065f, this.defaultYPos, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.body.localPosition = new Vector3(0.24f, this.defaultYPos - 0.03f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.035f, this.defaultYPos, 0f);
            }
        }

        if (stageId == 1)
            this.SetEyes(0);
        else if (stageId == 2)
            this.SetEyes(0);
        else if (stageId == 3)
            this.SetEyes(2);
        else
            this.SetEyes(0);
    }

    public void ElectricSphereThrow(int stageId = 0, bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                if (inAir)
                {
                    this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -25f);
                    this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                    this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
                }
                else
                {
                    this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }
                
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(180f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-180f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);

                if (inAir)
                {
                    this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                    this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                    this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
                }
                else
                {
                    this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                    this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                }

                
            }

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.035f, this.defaultYPos, 0f);
            }
            
        }

        this.SetEyes(0);


        /*if (stageId == 1)
        {

        }
        else if (stageId == 2)
        {

        }
        else
        {

        }*/
    }


    [ContextMenu("TestPose77")]
    public void TestPose77()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 18f, 27f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-55f, 0f, -5f);
            this.leftArmJoint.localEulerAngles = new Vector3(115f, 0f, 40f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("Test222")]
    public void Test222()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 38f, 148f);
            this.leftArm.localEulerAngles = new Vector3(0f, -136f, 67f);
            this.rightArmJoint.localEulerAngles = new Vector3(-92f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(100f, 0f, 4f);

            this.rightLeg.localEulerAngles = new Vector3(30f, 55f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-30f, 55f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(20f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -8f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 40f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.15f, 0f);
        }

        this.SetEyes(0);
    }


    //[ContextMenu("Book")]
    public void Book(int stageId = 0)
    {
        int animId = 1;
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 13.5f, 45f);
            //this.leftArm.localEulerAngles = new Vector3(0f, -48.5f, 94f);


            //this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 16f);


            //this.leftArmJoint.localEulerAngles = new Vector3(99f, 0f, 0f);


            if(stageId == 1)
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 11f);
            else
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 16f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -47f);
            //this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);

            if (animId == 1)
            {
                //this.leftArm.localEulerAngles = new Vector3(0f, -48.5f, 62.5f);
                this.leftArm.localEulerAngles = new Vector3(0f, -48.5f, 80f);
                this.leftArmJoint.localEulerAngles = new Vector3(99f, 0f, 10f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -65f);
            }
            else
            {
                this.leftArm.localEulerAngles = new Vector3(0f, -48.5f, 94f);
                this.leftArmJoint.localEulerAngles = new Vector3(99f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
            }
            //this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 10f);
            //this.dress.localEulerAngles = new Vector3(0f, 0f, -9f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 4f, 0f);

            if (animId == 1)
            {
                this.eyes.localPosition = new Vector3(0.03f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 4f, -4f);
                //this.eyes.localEulerAngles = new Vector3(0f, 4f, -5f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 4f, 0f);
            }
        }
            

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -16f, -5f);
            this.body.localPosition = new Vector3(-0.12f, this.defaultYPos - 0.08f, 0f);*/


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -16f, -10f);




            /*this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.body.localPosition = new Vector3(-0.28f, this.defaultYPos - 0.08f, 0f);*/

            if (animId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, 5f);
                this.body.localPosition = new Vector3(-0.68f, this.defaultYPos - 0.25f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.body.localPosition = new Vector3(-0.28f, this.defaultYPos - 0.08f, 0f);
            }

            
        }

        this.SetEyes(-1);

        if (this.dress != null)
            this.dress.localEulerAngles = new Vector3(0f, 0f, -9f);
    }

    //[ContextMenu("BookStart")]
    public void BookStart(bool noEyes = true)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 13.5f, 45f);
            this.leftArm.localEulerAngles = new Vector3(0f, -50f, 32f);
            this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 16f);
            this.leftArmJoint.localEulerAngles = new Vector3(35f, 0f, 10f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -47f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -65f);




        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
        }
        if (noEyes)
            this.SetEyes(-1);
        else
            this.SetEyes(0);

        if (this.dress != null)
            this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    [ContextMenu("CounterPointing")]
    public void CounterPointing()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            //this.rightArm.localEulerAngles = new Vector3(8f, 0f, 44f);
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 46f);
            this.leftArm.localEulerAngles = new Vector3(-12f, 0f, -59.5f);
            this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 23f);
            //this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 23f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 50.5f);
            //this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 25f);

            this.rightLeg.localEulerAngles = new Vector3(21f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 24.5f);
            this.rightLegJoint.localEulerAngles = new Vector3(19.5f, 0f, -21f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -50.5f);

            this.rightArmJoint.localScale = new Vector3(1f, 1.15f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 7.5f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 0f, -4f);
            //this.eyes.localEulerAngles = new Vector3(0f, -5f, -4f);
            
            //this.eyes.localEulerAngles = new Vector3(0f, -2.5f, -4f);
            this.eyes.localEulerAngles = new Vector3(0f, -2.5f, -6f);
            this.eyes.localPosition = new Vector3(0.04f, this.defaultEyeYHeight, 0f);
        }
            

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 27.5f);
            this.body.localPosition = new Vector3(-0.84f, this.defaultYPos - 0.31f, 0f);*/


            //this.body.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.body.localPosition = new Vector3(-0.84f, this.defaultYPos - 0.31f, 0f);

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 5f, 20f);

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, 20f);
            
        }

        this.SetEyes(-1);

        if (this.dress != null)
            this.dress.localEulerAngles = new Vector3(0f, 0f, -7.5f);
    }


    //[ContextMenu("CowardStart")]
    public void CowardStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(this.characterId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, -105f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 105f, 0f);
                //this.rightArmJoint.localEulerAngles = new Vector3(86f, -58f, -22f);
                this.rightArmJoint.localEulerAngles = new Vector3(80f, -70f, -45f);
                //this.leftArmJoint.localEulerAngles = new Vector3(-86f, 58f, -22f);
                this.leftArmJoint.localEulerAngles = new Vector3(-80f, 70f, -45f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(40f, -105f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-40f, 105f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(95f, -70f, -45f);
                this.leftArmJoint.localEulerAngles = new Vector3(-95f, 70f, -45f);
            }

            

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(2);
    }


    //[ContextMenu("Juggle")]
    public void Juggle(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            

            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(19f, 0f, 23f);
                this.leftArm.localEulerAngles = new Vector3(-27f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-5f, -21f, 79f);
                this.leftArmJoint.localEulerAngles = new Vector3(35f, 0f, 55f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(19f, 0f, 23f);
                this.leftArm.localEulerAngles = new Vector3(-32f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-5f, -21f, 79f);
                this.leftArmJoint.localEulerAngles = new Vector3(35f, -7f, 75f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(27f, 0f, 23f);
                this.leftArm.localEulerAngles = new Vector3(-32f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 79f);
                this.leftArmJoint.localEulerAngles = new Vector3(35f, -7f, 75f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(27f, 0f, 23f);
                this.leftArm.localEulerAngles = new Vector3(-27f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 79f);
                this.leftArmJoint.localEulerAngles = new Vector3(35f, 0f, 55f);
            }

            

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    

    [ContextMenu("TestRoadRollerPunch")]
    public void TestRoadRollerPunch()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 12.5f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, -35f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);

            this.rightLeg.localEulerAngles = new Vector3(37f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -62.5f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -38f);

            this.rightArm.localScale = new Vector3(1f, 1.1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, -20f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 100f, -37f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(-0.17f, this.defaultYPos - 0.9f, 0f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("TestRoadRollerPunch2")]
    public void RoadRollerPunch(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, -35f);
            this.leftArm.localEulerAngles = new Vector3(0f, -12.5f, 60f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if(stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(90f, 0f, -35f);
                this.leftArm.localEulerAngles = new Vector3(0f, -12.5f, 60f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightArm.localScale = new Vector3(1f, 1f, 1f);
                this.leftArm.localScale = new Vector3(1f, 1.1f, 1f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 12.5f, 60f);
                this.leftArm.localEulerAngles = new Vector3(-90f, 0f, -35f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 115f);

                this.rightArm.localScale = new Vector3(1f, 1.1f, 1f);
                this.leftArm.localScale = new Vector3(1f, 1f, 1f);

            }


            this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-37f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -38f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -62.5f);


            //this.rightArm.localScale = new Vector3(1f, 1.1f, 1f);
            //this.leftArm.localScale = new Vector3(1f, 1.1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 1)
                this.upperBody.localEulerAngles = new Vector3(0f, 20f, 0f);
            else
                this.upperBody.localEulerAngles = new Vector3(0f, -20f, 0f);

            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 80f, -37f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.body.localPosition = new Vector3(0.17f, this.defaultYPos - 0.9f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.9f, 0f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("Wry")]
    public void RoadRollerWry()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(50f, 0f, -25f);
            this.leftArm.localEulerAngles = new Vector3(-50f, 0f, -25f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);

            this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 54f, 47f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 80f, 47f);
            //this.body.localPosition = new Vector3(0f, 1.47f, 0.1f);
            this.body.localPosition = new Vector3(-0.18f, this.defaultYPos - 0.48f, 0.1f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("Zero")]
    public void KnifeThrowStart(bool inAir = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 21f, 65f);
            //this.rightArm.localEulerAngles = new Vector3(25f, 21f, 50f);


            this.leftArm.localEulerAngles = new Vector3(-45f, -25f, 0f);

            //this.rightArmJoint.localEulerAngles = new Vector3(-68f, 0f, 100f);


            this.rightArmJoint.localEulerAngles = new Vector3(-68f, 0f, 0f);
            //this.rightArmJoint.localEulerAngles = new Vector3(-40f, 0f, 0f);

            this.leftArmJoint.localEulerAngles = new Vector3(90f, 0f, 0f);



            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);*/

            if (inAir)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -90f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);

                /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);*/
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void RoadRollerFall()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(180f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-180f, 0f, 20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -160f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.50f, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("SpinTaunt")]
    public void SpinTaunt(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, -15f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);

                this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(60f, -100f, -20f);
                this.leftArm.localEulerAngles = new Vector3(-60f, 100f, -20f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 10f, 85f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, -10f, 85f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 10f, 80f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -10f, 80f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -138f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -138f);
            }
            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            if (stageId == 1)
            {
                this.eyes.localPosition = new Vector3(0f,this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
                
            else
            {
                //this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localPosition = new Vector3(0.02f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, -4f);
            }
                
        }
            

        if (this.body != null)
        {
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 30f, 20f);
                //this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos + 0.15f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 30f, -20f);
                //this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.2f, 0f);
            }
            
        }

        if (this.dress != null)
        {
            

            if (stageId == 1)
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.dress.localPosition = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.dress.localPosition = new Vector3(-0.1f, 0f, 0f);
            }
        }

        /*if (stageId == 1)
            this.SetEyes(3);
        else
            this.SetEyes(0);*/

        this.SetEyes(0);
    }


    //[ContextMenu("HoodGuyGrab")]
    public void HoodGuyGrab()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 25f, 100f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60.5f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);

            this.rightLeg.localEulerAngles = new Vector3(0f, -30f, -57f);
            this.leftLeg.localEulerAngles = new Vector3(-19f, 0f, 16.4f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -6f);
            this.leftLegJoint.localEulerAngles = new Vector3(30f, 0f, -32.5f);

            this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, -20f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 20f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0.02f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, -4f);
        }
            

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 20f);
            this.body.localPosition = new Vector3(-0.2f, this.defaultYPos - 0.25f, 0f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("HoodGuyGrabMid")]
    public void HoodGuyGrabMid()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 30f, 60f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -60.5f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 25f, 75f);

            this.rightLeg.localEulerAngles = new Vector3(0f, -30f, -37f);
            this.leftLeg.localEulerAngles = new Vector3(-19f, 0f, 36.4f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -6f);
            this.leftLegJoint.localEulerAngles = new Vector3(30f, 0f, -32.5f);

            this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 20f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0.02f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, -4f);
        }

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.15f, 0f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("HoodGuyGrabStart")]
    public void HoodGuyGrabStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(114.5f, 0f, -37f);
            this.leftArm.localEulerAngles = new Vector3(0f, -21f, 58f);
            this.rightArmJoint.localEulerAngles = new Vector3(23.5f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(48.5f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(32f, 0f, 54f);
            this.leftLeg.localEulerAngles = new Vector3(-39.5f, 0f, 35f);
            this.rightLegJoint.localEulerAngles = new Vector3(-15.5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(-12f, 0f, -9f);

            this.leftLeg.localScale = new Vector3(1f, 1.1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -16f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 66.5f, -45f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.28f, this.transform.forward.z * -0.65f);
        }

        this.SetEyes(3);
    }

    //[ContextMenu("Zero")]
    public void HoodGuyGrabbed()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 75f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 75f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 80f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, -90f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);

            if(this.characterId == 3)
            {
                this.body.localPosition = new Vector3(0f, 1.6f, this.transform.forward.z * -0.4f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, 1.55f, this.transform.forward.z * -0.4f);
            }
        }

        this.SetEyes(2);
    }

    //[ContextMenu("HoodGuyThrow")]
    public void HoodGuyThrow()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 60f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 50f);*/

            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 180f, -30f);
            this.body.localPosition = new Vector3(0.18f, this.defaultYPos - 0.2f, 0f);


            /*this.body.localEulerAngles = new Vector3(0f, 180f, -25f);
            this.body.localPosition = new Vector3(0.15f, this.defaultYPos - 0.15f, 0f);*/
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void ExplosiveBox(int stageId = 0, bool frowning = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(-8f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(8f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 35f, 75f);
                this.leftArm.localEulerAngles = new Vector3(0f, -35f, 75f);
                this.rightArmJoint.localEulerAngles = new Vector3(-85f, 0f, -9f);
                this.leftArmJoint.localEulerAngles = new Vector3(85f, 0f, -9f);
            }
            

            this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(30f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -15f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.09f, this.transform.forward.z * -0.19f);
        }

        if (frowning)
            this.SetEyes(2);
        else
            this.SetEyes(0);
    }


    //[ContextMenu("ScyteSwing")]
    public void ScyteSwing()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 65f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 65f);
            this.rightArmJoint.localEulerAngles = new Vector3(-20f, 90f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(20f, -90f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    //[ContextMenu("ScyteSpin")]
    public void ScyteSpin()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, -90f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, -90f);
            this.rightArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(55f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-55f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-100f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(100f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.44f, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("Zero")]
    public void Zero()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 180f, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("SexKick")]
    public void SexKick()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, -20f);

            this.leftArm.localEulerAngles = new Vector3(-40f, 0f, 0f);
            //this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
            //this.leftArm.localEulerAngles = new Vector3(-30f, 0f, -10f);

            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 130f);

            //this.leftArmJoint.localEulerAngles = new Vector3(-45f, 0f, 75f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, -10f, 80f);
            //this.leftArmJoint.localEulerAngles = new Vector3(0f, -25f, 105f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 45f);
            this.leftLeg.localEulerAngles = new Vector3(0f, -60f, 50f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 35f, -95f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 15f);

            //this.leftLeg.localEulerAngles = new Vector3(0f, -60f, 72f);
            //this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -15f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * -20f, this.transform.forward.z * 60f, 0f);
            //this.body.localEulerAngles = new Vector3(-20f, 60f, 0f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.43f, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("SexKickStart")]
    public void SexKickStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, -20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 70f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 50f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(0f, -30f, 20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 15f, -80f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * -10f, this.transform.forward.z * 40f, 0f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * -5f, this.transform.forward.z * 20f, -10f);


            //this.body.localEulerAngles = new Vector3(-20f, 60f, 0f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.43f, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("SexKick2")]
    public void SexKick2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, -20f);
            this.leftArm.localEulerAngles = new Vector3(-37f, 0f, 10f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 130f);
            this.leftArmJoint.localEulerAngles = new Vector3(-90f, 0f, 15f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -36f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 5f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -60f);

            this.rightLeg.localScale = new Vector3(1f, 1.1f, 1f);
            this.rightLegJoint.localScale = new Vector3(1f, 1.1f, 1f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 48f, 0f);
            //this.lowerBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 30f, 25f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 25f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("AirPunch")]
    public void AirPunch(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 160f, 90f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 70f);
                this.rightArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                //this.rightArm.localEulerAngles = new Vector3(0f, 30f, 70f);

                this.rightArm.localEulerAngles = new Vector3(0f, 30f, 80f);

                this.leftArm.localEulerAngles = new Vector3(0f, 20f, -70f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 80f);
            }
            else
            {

            }


            /*this.rightArm.localEulerAngles = new Vector3(0f, 30f, 70f);
            this.leftArm.localEulerAngles = new Vector3(0f, 20f, -70f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 80f);*/

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -112f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 5f, 0f);
            }
            else if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            }
            else
            {

            }

            //this.upperBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
            }
            else if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -30f);
            }
            else
            {

            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, -30f);

            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    //[ContextMenu("Zero")]
    public void TestHoodGuyPunch(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, -75f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 80f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightArm.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(-5f, 0f, 120f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                //this.rightArm.localScale = new Vector3(1f, 1.1f, 1f);
                this.rightArm.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 20f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, -80f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 133.5f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 74f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -95f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightArm.localScale = new Vector3(1f, 1f, 1f);
            }


            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (stageId == 1)
                this.eyes.localEulerAngles = new Vector3(0f, 5f, 0f);
            else if (stageId == 2)
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            else
                this.eyes.localEulerAngles = new Vector3(0f, 10f, 0f);

        }
            

        if (this.body != null)
        {
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.body.localPosition = new Vector3(0.25f, this.defaultYPos - 0.04f, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.body.localPosition = new Vector3(0.37f, this.defaultYPos - 0.09f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.body.localPosition = new Vector3(0.13f, this.defaultYPos, 0f);
            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    public void NewPunch(int stageId = 0, bool leftArm = false, bool stabilizeEyes = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            

            if (stageId == 0)
            {
                //this.rightArm.localEulerAngles = new Vector3(0f, -50f, -90f);
                this.rightArm.localEulerAngles = new Vector3(0f, 140f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-75f, 0f, 0f);
                //this.rightArmJoint.localEulerAngles = new Vector3(120f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 120f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, 50f, -90f);
                    this.rightArm.localEulerAngles = new Vector3(75f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 120f);
                }
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 70f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, 115f, -90f);
                    this.rightArm.localEulerAngles = new Vector3(25f, 0f, 0f);
                    this.leftArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                }
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 40f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -30f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, -40f, 90f);
                    this.rightArm.localEulerAngles = new Vector3(20f, 0f, -30f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                }
            }
            else if (stageId == 3)
            {
                //this.rightArm.localEulerAngles = new Vector3(0f, 20f, 40f);
                this.rightArm.localEulerAngles = new Vector3(0f, -10f, 30f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -10f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, 10f, 30f);
                    this.rightArm.localEulerAngles = new Vector3(20f, 0f, -10f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                }
            }
            else if (stageId == 4)
            {
                //this.rightArm.localEulerAngles = new Vector3(0f, 20f, 40f);
                this.rightArm.localEulerAngles = new Vector3(0f, 40f, 90f);
                this.leftArm.localEulerAngles = new Vector3(0f, 50f, -90f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, -40f, 90f);
                    this.rightArm.localEulerAngles = new Vector3(0f, 140f, 90f);
                    this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(-120f, 0f, 0f);
                }
            }
            else if (stageId == 5)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 70f, 90f);
                this.leftArm.localEulerAngles = new Vector3(-25f, 0f, -30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 90f);

                if (leftArm)
                {
                    this.leftArm.localEulerAngles = new Vector3(0f, 115f, -90f);
                    this.rightArm.localEulerAngles = new Vector3(25f, 0f, -30f);
                    this.leftArmJoint.localEulerAngles = new Vector3(-70f, 0f, 0f);
                    this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 90f);
                }
            }
            else
            {
                /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);*/
            }



            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -25f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -52f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 25f, 0f);

                if (leftArm)
                    this.upperBody.localEulerAngles = new Vector3(0f, -25f, 0f);
            }
            else if (stageId == 1 || stageId == 5)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2 || stageId == 4)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -40f, 0f);

                if (leftArm)
                    this.upperBody.localEulerAngles = new Vector3(0f, 40f, 0f);
            }
            else if (stageId == 3)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -10f, 0f);

                if (leftArm)
                    this.upperBody.localEulerAngles = new Vector3(0f, 10f, 0f);

            }
            //this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.eyes != null && stabilizeEyes)
        {
            float angle = this.upperBody.localEulerAngles.y;
            angle = (angle > 180) ? angle - 360 : angle;

            this.eyes.localEulerAngles = new Vector3(0f, -(angle * 0.5f), 0f);
            //Debug.Log(angle);

            //this.eyes.localEulerAngles = new Vector3(0f, -(this.upperBody.localEulerAngles.y * 0.5f), 0f);
        }

        /*if (this.eyes != null)
        {

            if (stageId == 0)
            {
                this.eyes.localEulerAngles = new Vector3(0f, -12.5f, 0f);

                if (leftArm)
                    this.eyes.localEulerAngles = new Vector3(0f, 12.5f, 0f);
            }
            else if (stageId == 1 || stageId == 5)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2 || stageId == 4)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);

                if (leftArm)
                    this.eyes.localEulerAngles = new Vector3(0f, -20f, 0f);
            }
            else if (stageId == 3)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 5f, 0f);

                if (leftArm)
                    this.eyes.localEulerAngles = new Vector3(0f, -5f, 0f);

            }
        }*/


        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            /*float testMinus = 0f;
            if (this.characterId == 3)
                testMinus = 0.03f;*/

            /*if (stageId == 0)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.body.localPosition = new Vector3(0.065f - testMinus, this.defaultYPos, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else
            {

            }*/
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void Kick(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 3)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                //this.rightLeg.localScale = new Vector3(1f, 1.2f, 1f);
                this.rightLeg.localScale = new Vector3(1f, 1.1f, 1f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }

            

            //this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);

            /*if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 2.5f);
                this.body.localPosition = new Vector3(-0.035f, this.defaultYPos, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.065f, this.defaultYPos, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -2.5f);
                this.body.localPosition = new Vector3(0.035f, this.defaultYPos, 0f);
            }*/
        }

        this.SetEyes(0);
    }

    public void NewKick(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);*/
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 5f);

                /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 2.5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 2.5f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);

                /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, -2.5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -2.5f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -5f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -5f);

                /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, -7.5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -7.5f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 85f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                //this.rightLeg.localScale = new Vector3(1f, 1.2f, 1f);
                this.rightLeg.localScale = new Vector3(1f, 1.1f, 1f);
            }
            else
            {
                /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);*/
            }



            //this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
            float testMinus = 0f;
            if (this.characterId == 3)
                testMinus = 0.03f;

            if (stageId == 0)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.body.localPosition = new Vector3(0.065f - testMinus, this.defaultYPos, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.065f + testMinus, this.defaultYPos, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.065f + testMinus, this.defaultYPos, 0f);
            }
            else
            {
                /*this.body.localEulerAngles = new Vector3(0f, 0f, -2.5f);
                this.body.localPosition = new Vector3(0.035f, this.defaultYPos, 0f);*/
            }
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Kick2")]
    public void HoodGuyKick(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -10f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -10f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);*/

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -15f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);*/

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 50f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -20f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -20f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);*/

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 70f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 70f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1.2f, 1f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);*/

                /*this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -40f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }


            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, -20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 55f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.body.localPosition = new Vector3(-0.44f, this.defaultYPos - 0.13f, 0f);*/


            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.body.localPosition = new Vector3(-0.2f, this.defaultYPos - 0.03f, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.body.localPosition = new Vector3(-0.32f, this.defaultYPos - 0.07f, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 35f);
                this.body.localPosition = new Vector3(-0.44f, this.defaultYPos - 0.13f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.body.localPosition = new Vector3(-0.07f, this.defaultYPos, 0f);
            }
        }

        this.SetEyes(0);
    }


    public void HoodGuyBackKick(int stageId = 0, int eyeId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 80f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 80f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -120f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 35f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 35f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

                /*this.rightArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);*/

                /*this.rightArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -90f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);

                this.rightLeg.localScale = new Vector3(1f, 1.2f, 1f);
            }
            else
            {

            }

            /*this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                //this.upperBody.localEulerAngles = new Vector3(0f, -10f, 0f);
                this.upperBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            }
            else if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -30f, 0f);
            }
            else if (stageId == 2)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -40f, 0f);
            }
            else
            {

            }

            //this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, -20f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, -30f);
                this.body.localPosition = new Vector3(-0.255f, this.defaultYPos - 0.12f, 0f);*/

                /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 100f, -30f);
                this.body.localPosition = new Vector3(-0.42f, this.defaultYPos - 0.12f, -0.56f);*/

                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 120f, -30f);
                this.body.localPosition = new Vector3(-0.46f, this.defaultYPos - 0.12f, this.transform.forward.z * - 0.42f);
            }
            else if (stageId == 1)
            {
                /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, -20f);
                this.body.localPosition = new Vector3(-0.255f, this.defaultYPos - 0.04f, 0f);*/

                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 140f, -20f);
                this.body.localPosition = new Vector3(-0.44f, this.defaultYPos - 0.04f, this.transform.forward.z * -0.25f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, -40f);
                this.body.localPosition = new Vector3(-0.38f, this.defaultYPos - 0.18f, 0f);
            }
            else
            {

            }
            /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/

            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        this.SetEyes(eyeId);

        /*if (this.scyte != null)
        {
            this.scyte.localPosition = new Vector3(0f, -0.56f, 0.8f);
            this.scyte.localEulerAngles = new Vector3(0f, -90f, -90f);
        }*/
    }

    //[ContextMenu("Zero")]
    public void DoorWalk(int StageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if(StageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 20f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, -20f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            }
            else if (StageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, -20f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, 20f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (StageId == 3)
            {
                if(this.transform.forward.z >= 0)
                {
                    this.rightArm.localEulerAngles = new Vector3(0f, 40f, 85f);
                    this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                }
                else
                {
                    this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                    this.leftArm.localEulerAngles = new Vector3(0f, -40f, 85f);
                }
                

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            

            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void StupidDance(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, 10f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, 10f);

                /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, -10f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -10f);*/

                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 25f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            }
            else if (stageId == 2)
            {
                /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, 15f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, 15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);*/

                this.rightArm.localEulerAngles = new Vector3(10f, 0f, -5f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -5f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 110f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 110f);

                /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -40f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);*/


                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);

                /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -35f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -35f);*/

                /*this.rightArm.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -40f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);


                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -35f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2 || stageId == 3)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, -5f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            //this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {

            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.body.localPosition = new Vector3(0.11f, this.defaultYPos, 0f);
            }
            else if (stageId == 2)
            {
                //this.body.localPosition = new Vector3(-0.1f, this.defaultYPos - 0.12f, 0f);


                this.body.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.body.localPosition = new Vector3(-0.05f, this.defaultYPos - 0.12f, 0f);
                //this.body.localPosition = new Vector3(0.15f, this.defaultYPos - 0.12f, 0f);

                /*this.body.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.body.localPosition = new Vector3(0.18f, this.defaultYPos - 0.2f, 0f);*/
            }
            else if (stageId == 3)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.body.localPosition = new Vector3(0.05f, this.defaultYPos - 0.03f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.body.localPosition = new Vector3(-0.18f, this.defaultYPos - 0.2f, 0f);
            }

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }


    //[ContextMenu("TestAnimated")]
    public void TestAnimated(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {

            }
            else if (stageId == 2)
            {

            }
            else
            {

            }

            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }



    public void CaramelDance(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(140f, 0f, 60f);
                this.leftArm.localEulerAngles = new Vector3(-140f, 0f, 60f);
                this.rightArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(140f, 0f, 60f);
                this.leftArm.localEulerAngles = new Vector3(-140f, 0f, 60f);
                this.rightArmJoint.localEulerAngles = new Vector3(80f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);
            }
            else
            {
                /*this.rightArm.localEulerAngles = new Vector3(135f, 0f, 55f);
                this.leftArm.localEulerAngles = new Vector3(-135f, 0f, 55f);*/

                this.rightArm.localEulerAngles = new Vector3(135f, -10f, 45f);
                this.leftArm.localEulerAngles = new Vector3(-135f, 10f, 45f);

                this.rightArmJoint.localEulerAngles = new Vector3(65f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(-65f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            }

            /*this.rightArm.localEulerAngles = new Vector3(135f, 0f, 55f);
            this.leftArm.localEulerAngles = new Vector3(-135f, 0f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(65f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-65f, 0f, 0f);*/

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(5f, 0f, 0f);

            }
            else if (stageId == 2)
            {
                this.upperBody.localEulerAngles = new Vector3(-5f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(-5f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            //this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -15f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.12f, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 15f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.12f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        if (this.dress != null)
        {
            if (stageId == 1)
            {
                this.dress.localEulerAngles = new Vector3(-3f, 0f, 0f);
                this.dress.localPosition = new Vector3(0f, 0.05f, 0f);
            }
            else if (stageId == 2)
            {
                this.dress.localEulerAngles = new Vector3(3f, 0f, 0f);
                this.dress.localPosition = new Vector3(0f, 0.05f, 0f);

                /*this.dress.localEulerAngles = new Vector3(1.5f, 0f, 0f);
                this.dress.localPosition = new Vector3(0f, 0f, 0f);*/
            }
            else
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.dress.localPosition = new Vector3(0f, 0f, 0f);
            }

            /*this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.dress.localPosition = new Vector3(0f, 0f, 0f);*/
        }

        this.SetEyes(1);
    }


    public void TestStupidWalk(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 1)
            {
                /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, -20f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -20f);*/

                this.rightArm.localEulerAngles = new Vector3(15f, -25f, -20f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 25f, -20f);

                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -55f);
            }
            else if (stageId == 2)
            {
                /*this.rightArm.localEulerAngles = new Vector3(10f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -40f);*/

                this.rightArm.localEulerAngles = new Vector3(20f, -25f, -40f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 25f, -40f);

                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 90f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, 20f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, 20f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            }

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 1)
            {
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 2.5f);
            }
            else if (stageId == 2)
            {
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 5f);
            }

            this.upperBody.localEulerAngles = new Vector3(0f, 5f, 0f);
            //this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 15f, 6f);

            this.eyes.localEulerAngles = new Vector3(0f, 0f, 6f);
            //this.eyes.localPosition = new Vector3(0.05f, this.defaultEyeYHeight, 0f);
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);


            /*this.eyes.localEulerAngles = new Vector3(0f, 0f, 3f);
            this.eyes.localPosition = new Vector3(0.02f, 0f, 0f);*/


            /*this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.eyes.localPosition = new Vector3(0f, 0f, 0f);*/
        }

        if (this.body != null)
        {
            if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.09f, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.06f, 0f);
            }
            else
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.19f, 0f);
            }

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        if (this.dress != null)
        {
            if (stageId == 1)
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, -2.5f);
            }
            else if (stageId == 2)
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.dress.localEulerAngles = new Vector3(0f, 0f, -5f);
            }


            //this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        this.SetEyes(0);
    }

    public void Pointt()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -10f, 80f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(65f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void Pointt2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            //this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.rightArm.localEulerAngles = new Vector3(40f, 0f, 0f);

            this.leftArm.localEulerAngles = new Vector3(-75f, 0f, 35f);

            //this.rightArmJoint.localEulerAngles = new Vector3(-65f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);

            this.leftArmJoint.localEulerAngles = new Vector3(-15f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 60f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(1);
    }


    public void Crawl(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(145f, 0f, 25f);
                this.leftArm.localEulerAngles = new Vector3(-52f, 0f, 45f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(50f, 0f, 15f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 65f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(130f, 0f, 55f);
                this.leftArm.localEulerAngles = new Vector3(-130f, 0f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 15f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 65f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 65f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(52f, 0f, 45f);
                this.leftArm.localEulerAngles = new Vector3(-145f, 0f, 25f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, -15f);
                this.leftLeg.localEulerAngles = new Vector3(-50f, 0f, 15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 65f);
            }
            else
            {

            }

            /*this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.eyes.localEulerAngles = new Vector3(0f, 180f, 0f);

            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, -80f);
            }
            else if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 0f, -80f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -10f, -80f);
            }
            else
            {

            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.98f, 0f);
        }

        this.SetEyes(0);
    }


    public void TortureDance(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*if (stageId == 1)
            {

            }
            else if (stageId == 2)
            {

            }
            else
            {

            }*/


            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(35f, 35f, 65f);
                this.leftArm.localEulerAngles = new Vector3(-10f, -10.5f, 51.5f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50f, 0f, 27f);
                this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 43f);

                this.rightLeg.localEulerAngles = new Vector3(9f, 26f, 34f);
                this.leftLeg.localEulerAngles = new Vector3(-11f, 0f, 23f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -38f);
                this.leftLegJoint.localEulerAngles = new Vector3(-7f, 20f, -45f);

                this.rightArm.localPosition = new Vector3(0f, 0.3f, -0.5f);
                this.leftArm.localPosition = new Vector3(0f, 0.3f, 0.5f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -11f, 51f);
                this.leftArm.localEulerAngles = new Vector3(0f, 5.5f, 81f);
                this.rightArmJoint.localEulerAngles = new Vector3(-75f, 0f, 15f);
                this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(7f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 38f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
                this.leftLegJoint.localEulerAngles = new Vector3(16f, 0f, -22f);

                this.rightArm.localPosition = new Vector3(0f, 0.3f, -0.5f);
                this.leftArm.localPosition = new Vector3(0f, 0.3f, 0.5f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(135f, 0f, 30f);
                this.leftArm.localEulerAngles = new Vector3(-135f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 105f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 105f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 86f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -115f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);

                this.rightArm.localPosition = new Vector3(0f, 0.3f, -0.5f);
                this.leftArm.localPosition = new Vector3(0f, 0.3f, 0.5f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(25f, 0f, 30f);
                this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-45f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(45f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, 60f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);

                this.rightArm.localPosition = new Vector3(0f, 0.2f, -0.5f);
                this.leftArm.localPosition = new Vector3(0f, 0.2f, 0.5f);
            }
            else
            {

            }

            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);


            this.rightArm.localPosition = new Vector3(0f, 0.3f, -0.5f);
            this.leftArm.localPosition = new Vector3(0f, 0.3f, 0.5f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 5f);
            }
            else
            {

            }


            /*this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.eyes.localEulerAngles = new Vector3(0f, -20f, 0f);
            }
            else if (stageId == 2)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {

            }
            //this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            /*if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.24f, this.transform.forward.z * 0.55f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 12f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(-0.09f, this.defaultYPos - 0.1f, this.transform.forward.z * 0.1f);
                this.body.localEulerAngles = new Vector3(-7f, this.transform.forward.z * 105f, -7f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0.36f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 10f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.35f, this.transform.forward.z * 0.35f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, -5f);
            }
            else
            {

            }*/


            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.24f, 0.55f);
                this.body.localEulerAngles = new Vector3(0f, 90f, 12f);
                //this.body.localEulerAngles = new Vector3(0f, 0f, 12f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(-0.09f, this.defaultYPos - 0.1f, 0.1f);
                this.body.localEulerAngles = new Vector3(-7f, 105f, -7f);
                //this.body.localEulerAngles = new Vector3(-7f, 15f, -7f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0.36f);
                this.body.localEulerAngles = new Vector3(0f, 90f, 10f);
                //this.body.localEulerAngles = new Vector3(0f, 0f, 10f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.35f, 0.35f);
                this.body.localEulerAngles = new Vector3(0f, 90f, -5f);
                //this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
            }
            else
            {

            }


            /*this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0f);*/
        }
        //Debug.Log(this.transform.forward.z);
        this.SetEyes(0);
    }


    public void DiscoDance(int stageId)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(140f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                /*this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(5f, 0f, 0f);*/

                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(40f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 35f);
                //this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 35f);

                /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);*/

                this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 38f);
                this.rightArmJoint.localEulerAngles = new Vector3(-55f, 0f, 0f);

                /*this.rightLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);*/

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
            }
            else
            {

            }

            //this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 15f);
            //this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(55f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);*/

            //this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localEulerAngles = new Vector3(-5f, 90f, 0f);
                this.body.localPosition = new Vector3(-0.07f, this.defaultYPos - 0.03f, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localEulerAngles = new Vector3(0f, 90f, 0f);
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localEulerAngles = new Vector3(5f, 90f, 0f);
                this.body.localPosition = new Vector3(0.07f, this.defaultYPos - 0.03f, 0f);
            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    public void HypeDance(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(70f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(110f, 0f, -10f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(70f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(110f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -50f);
            }

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, -5f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 25f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 25f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
            }

        }

        this.SetEyes(0);
    }

    public void RussianDance(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 45f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 62f);
            this.rightArmJoint.localEulerAngles = new Vector3(-70f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(75f, 0f, 12f);

            if (stageId == 0)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 10f, 50f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -10f, 50f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -100f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -100f);
            }
            else if (stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 35f, 90f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -105f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -35f, 90f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -105f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


            


        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            /*if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }*/

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            /*if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);
            }*/


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.35f, 0f);
                //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.42f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.42f, 0f);
            }

            this.body.localEulerAngles = new Vector3(0f, 90f, 0f);

        }

        this.SetEyes(0);
    }



    //[ContextMenu("Shippu")]
    public void ForwardKickShippu(int stageId = 0/*, bool upwards = false*/, int direction = 0)
    {
        //direction 0 = normal, 1 = upper, 2 = lower 

        /*int stageId = 1;
        bool upwards = false;*/
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 10f, 75f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -42f);
                this.rightArmJoint.localEulerAngles = new Vector3(-96f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 92f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -45f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(-38f, 0f, -45f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 75f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(96f, 0f, 18f);

                this.rightLeg.localEulerAngles = new Vector3(83f, 0f, 6.5f);
                this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 20f);
                //this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -83.5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 15f, -83.5f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);

                /*if (upwards)
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 35f, -83.5f);*/

                if (direction == 1)
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 35f, -83.5f);
                else if (direction == 2)
                    this.rightLegJoint.localEulerAngles = new Vector3(0f, 5f, -83.5f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, -60f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 75f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(96f, 0f, 18f);

                //this.rightLeg.localEulerAngles = new Vector3(90f, 0f, 16f);

                //this.rightLeg.localEulerAngles = new Vector3(100f, 0f, 30f);
                this.rightLeg.localEulerAngles = new Vector3(90f, 0f, 30f);


                this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1.2f, 1f);

                /*if (upwards)
                    this.rightLeg.localEulerAngles = new Vector3(110f, 0f, 16f);*/

                /*if (upwards)
                    this.rightLeg.localEulerAngles = new Vector3(115f, 0f, 30f);*/

                if (direction == 1)
                    this.rightLeg.localEulerAngles = new Vector3(115f, 0f, 30f);
                else if (direction == 2)
                    this.rightLeg.localEulerAngles = new Vector3(80f, 0f, 30f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 75f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(96f, 0f, 18f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 71f);

                this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);


                /*if (upwards)
                    this.rightLeg.localEulerAngles = new Vector3(40f, 0f, 71f);*/

                if (direction == 1)
                    this.rightLeg.localEulerAngles = new Vector3(40f, 0f, 71f);
                /*else if (direction == 2)
                    this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 71f);*/
            }
            else if (stageId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, -40f);
                this.leftArm.localEulerAngles = new Vector3(0f, -25f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(45f, 0f, 18f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 68.5f);
                this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 5)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 26f);
                this.leftArm.localEulerAngles = new Vector3(0f, -55f, 41f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 62f);
                this.leftArmJoint.localEulerAngles = new Vector3(45f, 0f, 18f);

                this.rightLeg.localEulerAngles = new Vector3(11.5f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(-4f, 0f, 26f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 6)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 26f);
                this.leftArm.localEulerAngles = new Vector3(0f, -55f, 25f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(17.5f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(11.5f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(-4f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }

            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (stageId == 1)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 2)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 3)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 4)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 4)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 5)
                this.upperBody.localEulerAngles = new Vector3(0f, 17.5f, 0f);
            else if (stageId == 6)
                this.upperBody.localEulerAngles = new Vector3(0f, 10f, 0f);

            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            if (stageId == 5)
                this.eyes.localEulerAngles = new Vector3(0f, -20f, 0f);
            if (stageId == 6)
                this.eyes.localEulerAngles = new Vector3(0f, -15f, 0f);
            else
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0.02f, this.defaultYPos + 0.05f, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0.25f, this.defaultYPos, this.transform.forward.z * 0.35f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 20f, this.transform.forward.z * -20f, -20f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0.2f, this.defaultYPos, this.transform.forward.z * 0.5f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 20f, this.transform.forward.z * -55f, -20f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0.1f, this.defaultYPos, this.transform.forward.z * 0.6f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 20f, this.transform.forward.z * -90f, -20f);
            }
            else if (stageId == 4)
            {
                this.body.localPosition = new Vector3(0.06f, this.defaultYPos, this.transform.forward.z * 0.71f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 10f, this.transform.forward.z * -122.5f, -20f);
            }
            else if (stageId == 5)
            {
                this.body.localPosition = new Vector3(-0.28f, this.defaultYPos, this.transform.forward.z * 0.46f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 5f, this.transform.forward.z * 100f, -10f);
            }
            else if (stageId == 6)
            {
                this.body.localPosition = new Vector3(-0.28f, this.defaultYPos, this.transform.forward.z * 0.3f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 5f, this.transform.forward.z * 60f, -5f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);


        if (this.wingRight != null)
        {
            if (stageId == 1 || stageId == 2)
                this.wingRight.localEulerAngles = new Vector3(0f, 60f, 0f);
            else if (stageId == 3)
                this.wingRight.localEulerAngles = new Vector3(0f, 15f, 0f);
            else if (stageId == 4)
                this.wingRight.localEulerAngles = new Vector3(0f, 3f, 0f);
            else
                this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);

        }
    }


    public void HammerAttack(int stageId = 0)
    {
        //direction 0 = normal, 1 = upper, 2 = lower 

        /*int stageId = 1;
        bool upwards = false;*/
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 20f, -110f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, -110f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);

                //Hammer Pos: x = -1.2, y = 0.205, z = 0
                //Hammer Rot: x = 0, y = 0, z = 246
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -15f, 150f);
                this.leftArm.localEulerAngles = new Vector3(0f, 15f, 150f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, -10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);

                //Hammer Pos: x = -0.15, y = 1.42, z = 0
                //Hammer Rot: x = 0, y = 0, z = 162
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -20f, 55f);
                this.leftArm.localEulerAngles = new Vector3(0f, 20f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(40f, 0f, -20f);
                this.leftLeg.localEulerAngles = new Vector3(-40f, 0f, -20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);

                //Hammer Pos: x = 1.445, y = -0.125, z = 0
                //Hammer Rot: x = 0, y = 0, z = 20
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                //this.rightLeg.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            

            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            
            if (stageId == 0)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (stageId == 1)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (stageId == 2)
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, -5f);

            //this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, 15f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, -5f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.2f, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, -40f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
                
            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        //this.SetEyes(0);

        if (stageId == 0)
            this.SetEyes(0 /*2*/);
        else if (stageId == 1)
            this.SetEyes(3);
        else if (stageId == 2)
            this.SetEyes(3);
        else
            this.SetEyes(0);
    }



    public void ItemThrow(int stageId = 0)
    {
        /*int stageId = 1;
        bool upwards = false;*/
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.rightArmJoint.localEulerAngles = new Vector3(-80f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, -10f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 5f);
                this.leftArmJoint.localEulerAngles = new Vector3(50f, 0f, -10f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 60f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 115f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, -35f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else if (stageId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, 30f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 25f, -10f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.05f, 0f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 20f, -10f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, -5f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, 5f);
            }
            else if (stageId == 4)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
                this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);

            }


            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }


    [ContextMenu("ShootSelf")]
    public void ShootSelf()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(85f, 0f, -15f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(120f, -90f, -5f);
            this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(5f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("MikeBallerAngry")]
    public void MikeBallerAngry(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 16f, 60f);
            this.leftArm.localEulerAngles = new Vector3(0f, 10f, -57f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 50f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -3f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -35f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -19f);

            if(stageId == 0)
            {
                this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos, this.rightArm.localPosition.z);
            }
            else
            {
                this.rightArm.localPosition = new Vector3(this.rightArm.localPosition.x, this.armsDefaultYPos + 0.02f, this.rightArm.localPosition.z);
            }
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * -30, -15f);
            this.body.localPosition = new Vector3(this.transform.forward.z  * -0.33f, this.defaultYPos - 0.12f, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void MikeBallerVsViolentStartPose()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-40f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(95f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("Zero")]
    public void MikeBallerVsViolentStartPose2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(15f, 0f, -15f);
            this.leftArm.localEulerAngles = new Vector3(-15f, 0f, -15f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 60f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 25f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("ShockwavePunchStart")]
    public void ShockwavePunchStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 14f, 145f);
            this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);
            this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 15f);

            this.rightLeg.localEulerAngles = new Vector3(10f, 0f, -10f);
            this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, -10f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 10f);
        }
            

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 10f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0.125f);
        }

        this.SetEyes(0);
    }

    [ContextMenu("ShockwavePunch")]
    public void ShockwavePunch()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, 50f);
            this.leftArm.localEulerAngles = new Vector3(-90f, -44f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(70f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 4f);
            this.leftLeg.localEulerAngles = new Vector3(0f, -35f, 115f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -40f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, -25f, -76f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 32f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * -12f, this.transform.forward.z * 35f, -51f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.85f, 0f);
        }

        this.SetEyes(0);
    }

    public void ShockwavePunchMid(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(68f, 90f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-96f, 70f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 87f);

                this.rightLeg.localEulerAngles = new Vector3(24f, 0f, 30f);
                this.leftLeg.localEulerAngles = new Vector3(-43f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-32f, 0f, -25f);
                this.leftLegJoint.localEulerAngles = new Vector3(-4f, 0f, 0f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, 20f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
                //this.leftArm.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 7f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -10f, 40f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -21f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -16f);
            }

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 70f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            
        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localEulerAngles = new Vector3(0f, -33f, 0f);
            }
            else
            {
                this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);
            }

            
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.18f, 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 5f, this.transform.forward.z * 90f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0.2f, this.defaultYPos - 0.15f, this.transform.forward.z * -0.2f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -5f, this.transform.forward.z * 20f, -25f);
            }
            
        }

        this.SetEyes(0);
    }

    public void HoodGuyButtAttack(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {

            }
            else if (stageId == 1)
            {

            }
            else
            {

            }

            this.rightArm.localEulerAngles = new Vector3(40f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 20f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 88f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 60f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 83f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 83f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -70f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, -16f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 180f, -50f);
            }
            else
            {

            }

        }

        if(this.eyesPosTransform != null)
        {
            this.eyesPosTransform.localPosition = new Vector3(0f, -0.04f, 0.04f);
            this.eyesPosTransform.localEulerAngles = new Vector3(40f, 0f, 0f);
        }

        if (this.dress != null)
        {
            this.dress.localPosition = new Vector3(-0.05f, 0.1f, 0f);
            this.dress.localEulerAngles = new Vector3(0f, 0f, 5f);
        }

        this.SetEyes(0);
    }


    public void PunchGhostSummoning()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 30f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, -30f, 85f);
            this.rightArmJoint.localEulerAngles = new Vector3(-45f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 5f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -25f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 30f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -22f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            this.body.localPosition = new Vector3(-0.25f, this.defaultYPos - 0.05f, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    public void PunchGhostSummoning2()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 40f, 0f);
            this.leftArm.localEulerAngles = new Vector3(0f, -30f, 83f);
            this.rightArmJoint.localEulerAngles = new Vector3(-44f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 5f);

            this.rightLeg.localEulerAngles = new Vector3(3f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-2f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 30f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 60f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -22f, -1f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 3f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(0);
    }

    [ContextMenu("LayingDownSassy")]
    public void LayingDownSassy()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(150f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-14f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(110f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
            //this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 25f);

            this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            //this.leftLeg.localEulerAngles = new Vector3(32f, 0f, 18f);
            this.leftLeg.localEulerAngles = new Vector3(32f, 0f, -13f);
            this.rightLegJoint.localEulerAngles = new Vector3(-14f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(25f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * -80f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 1.15f, 0f);
        }

        this.SetEyes(0);
    }

    //[ContextMenu("HoldingTwoFlames")]
    public void HoldingTwoFlames()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(28f, 0f, 10f);
            this.leftArm.localEulerAngles = new Vector3(-28f, 0f, 10f);
            this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 100f);

            this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void HoldingOneFlame()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(28f, 0f, 10f);
            this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(45f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }


    public void Smoking(int stageId = 0, bool changeLegRotation = false)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(25f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-65f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 14.4f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50.57f, 0f, 47.1f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 0f, 50f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50.57f, 0f, 47.1f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, -5f, 50f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 8.1f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(-50.57f, 0f, 47.1f);
                this.leftArmJoint.localEulerAngles = new Vector3(73.6f, 0f, 51.63f);
            }
            else if (stageId == 5)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, -5f, 50f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, 10f);
                this.rightArmJoint.localEulerAngles = new Vector3(-85f, 0f, 42.7f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 16f);
            }
            else if (stageId == 6)
            {
                //this.rightArm.localEulerAngles = new Vector3(25f, 0f, 0f);
                this.rightArm.localEulerAngles = new Vector3(25f, 10f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-15f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-65f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);


                /*this.rightArm.localEulerAngles = new Vector3(15f, 0f, 0f);
                //this.leftArm.localEulerAngles = new Vector3(-25f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-25f, -10f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(65f, 0f, 0f);*/
            }
            else
            {

            }

            if (/*stageId == 6 */!changeLegRotation)
            {
                this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            /*this.rightLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
        }

        this.SetEyes(0);
    }

    public void Parry()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            //this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
            this.rightArm.localEulerAngles = new Vector3(30f, 0f, -15f);

            //this.leftArm.localEulerAngles = new Vector3(-34f, 0f, 0f);
            //this.leftArm.localEulerAngles = new Vector3(-79f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-65f, 0f, 0f);

            this.rightArmJoint.localEulerAngles = new Vector3(35f, 0f, 110f);

            //this.leftArmJoint.localEulerAngles = new Vector3(-54f, 0f, 0f);
            //this.leftArmJoint.localEulerAngles = new Vector3(-44f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(-90f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(72f, 0f, 0f);

            //this.leftLeg.localEulerAngles = new Vector3(-72f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(-60f, 0f, 0f);

            this.rightLegJoint.localEulerAngles = new Vector3(-72f, 0f, 0f);

            //this.leftLegJoint.localEulerAngles = new Vector3(72f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(24f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, -29.4f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f); ;
            //this.body.localPosition = new Vector3(0f, 1.51f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.44f, 0f);
        }

        this.SetEyes(0);
    }

    public void ScissorKick(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, 15f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 45f);

                this.rightLeg.localEulerAngles = new Vector3(30f, 0f, 65f);
                this.leftLeg.localEulerAngles = new Vector3(-30f, 0f, 65f);
                this.rightLegJoint.localEulerAngles = new Vector3(-30f, 0f, -30f);
                this.leftLegJoint.localEulerAngles = new Vector3(30f, 0f, -30f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(90f, 0f, 82f);
                this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 82f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 75f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 75f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -32f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, -15f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -15f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 75f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(10f, 0f, -30f);
                this.leftArm.localEulerAngles = new Vector3(-10f, 0f, -30f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 75f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 75f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 75f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 6f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -5f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(40f, 0f, -10f);
                this.leftArm.localEulerAngles = new Vector3(-40f, 0f, -10f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 30f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -2f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (stageId == 5)
            {
                this.rightArm.localEulerAngles = new Vector3(40f, 30f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 23f, 10f);
                this.rightArmJoint.localEulerAngles = new Vector3(-22f, 0f, 24f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 35f);

                this.rightLeg.localEulerAngles = new Vector3(-20f, 0f, 40f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 44f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -110f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.leftLeg.localScale = new Vector3(1f, 1.2f, 1f);
            }
            else
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.leftLeg.localScale = new Vector3(1f, 1f, 1f);
            }

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (stageId == 5)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 5f);
            }

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.23f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -30f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -80f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -165f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 86f);
            }
            else if (stageId == 4)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 20f);
            }
            else if (stageId == 5)
            {
                this.body.localPosition = new Vector3(-0.2f, this.defaultYPos - 0.51f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 15f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(0);
    }


    public void FootDive(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
                this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
                this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

                this.rightLeg.localEulerAngles = new Vector3(-5f, -10f, 110f);
                this.leftLeg.localEulerAngles = new Vector3(5f, 10f, 110f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -120f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -120f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
                this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(-82f, 0f, 12f);
                this.leftArmJoint.localEulerAngles = new Vector3(82f, 0f, -5f);

                this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
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

            
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 25f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 35f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

            
        }
    }

    public void DemonCradle(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {

            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 125f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 125f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 47f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -83f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -85f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(150f, 0f, 55f);
                this.leftArm.localEulerAngles = new Vector3(-150f, 0f, 55f);
                this.rightArmJoint.localEulerAngles = new Vector3(-90f, -195f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(90f, -195f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(-8f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(12f, 0f, 10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -13f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -20f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(60f, 0f, 50f);
                this.leftArm.localEulerAngles = new Vector3(-60f, 0f, 50f);
                this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 45f);
                this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 45f);

                this.rightLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(15f, 0f, 40f);
                this.rightLegJoint.localEulerAngles = new Vector3(-5f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
            }
            else if (stageId == 4)
            {
                this.rightArm.localEulerAngles = new Vector3(40f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-45f, 0f, -10f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 55f);
                this.leftArmJoint.localEulerAngles = new Vector3(60f, 0f, 45f);

                this.rightLeg.localEulerAngles = new Vector3(15f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 95f);
                this.rightLegJoint.localEulerAngles = new Vector3(-40f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, -7f, -110f);
            }
            else if (stageId == 5)
            {
                /*this.rightArm.localEulerAngles = new Vector3(0f, 20f, 130f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 130f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 40f);
                this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 40f);*/

                /*this.rightArm.localEulerAngles = new Vector3(0f, 10f, 80f);
                this.leftArm.localEulerAngles = new Vector3(0f, -20f, 130f);
                this.rightArmJoint.localEulerAngles = new Vector3(-30f, 0f, 90f);
                this.leftArmJoint.localEulerAngles = new Vector3(30f, 0f, 40f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                //this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
                //this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);*/


                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 10f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 20f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 20f);
                this.leftArmJoint.localEulerAngles = new Vector3(5f, 0f, 10f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 40f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -30f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -75f);
            }
            else
            {

            }

            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            /*this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);*/
            if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, -80f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, -14f, 0f);
            }
            else if (stageId == 3)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 4)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, -17f, 0f);
            }
            else if (stageId == 5)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


        }

        if (this.body != null)
        {
            /*this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);*/

            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.25f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * -10f, 0f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * -90f, 0f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * -180f, -5f);
            }
            else if (stageId == 4)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * -7f, this.transform.forward.z * 90f, 0f);
            }
            else if (stageId == 5)
            {

                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -3f);
                /*this.body.localPosition = new Vector3(-0.12f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 10f, 10f);*/
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        if (this.wingRight != null && this.wingLeft != null)
        {
            if (stageId == 2)
            {
                this.wingRight.localEulerAngles = new Vector3(0f, -20f, 0f);
                this.wingLeft.localEulerAngles = new Vector3(0f, 20f, 0f);
                this.wingRight.localScale = new Vector3(1f, 1.2f, 1.5f);
                this.wingLeft.localScale = new Vector3(1f, 1.2f, 1.5f);
            }
            else
            {
                this.wingRight.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.wingLeft.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.wingRight.localScale = new Vector3(1f, 1f, 1f);
                this.wingLeft.localScale = new Vector3(1f, 1f, 1f);
            }

        }

        this.SetEyes(0);
    }

    public void Armageddon(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(30f, 0f, 20f);
                this.leftArm.localEulerAngles = new Vector3(-30f, 0f, 20f);
                this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(5f, 9f, 120f);
                this.leftArm.localEulerAngles = new Vector3(-5f, -5f, 120f);
                this.rightArmJoint.localEulerAngles = new Vector3(-90f, 0f, -10f);
                this.leftArmJoint.localEulerAngles = new Vector3(90f, 0f, -10f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 5f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightArm.localEulerAngles = new Vector3(130f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-130f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.rightArm.localEulerAngles = new Vector3(15f, 5f, 80f);
                this.leftArm.localEulerAngles = new Vector3(-15f, -5f, 80f);
                this.rightArmJoint.localEulerAngles = new Vector3(-90f, 0f, 50f);
                this.leftArmJoint.localEulerAngles = new Vector3(90f, 0f, 50f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
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

            

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            /*if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }*/

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            /*if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }*/


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * -0.12f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, -10f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * -0.06f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, -5f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0.12f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, 10f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 25f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(0);
    }


    //[ContextMenu("GrandFlameStart")]
    public void Exorcism()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 14f, 145f);
            this.leftArm.localEulerAngles = new Vector3(0f, -14f, 145f);
            this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 25f);
            this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 25f);

            this.rightLeg.localEulerAngles = new Vector3(25f, 0f, -20f);
            this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, 20f);
            //this.body.localPosition = new Vector3(0f, 1.85f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.1f, 0f);
        }
    }



    [ContextMenu("TestFly")]
    public void TestFly()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(90f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-90f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -20f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -5f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -5f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 12f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 180f, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            this.body.localEulerAngles = new Vector3(0f, 0f, -48f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void KnifePunishmentHit()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 15f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 5f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 5f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 42f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -42f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 42f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -42f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 40f, -5f);
            this.body.localPosition = new Vector3(-0.1f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            /*this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);*/
        }

        this.SetEyes(2);
    }

    public void KnifePunishmentStart()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, -9f, 63f);
            this.leftArm.localEulerAngles = new Vector3(0f, 9f, 55f);
            this.rightArmJoint.localEulerAngles = new Vector3(-53f, 0f, 12f);
            this.leftArmJoint.localEulerAngles = new Vector3(67f, 0f, -20f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -47f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -80f);


        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 10f);
            //this.dress.localEulerAngles = new Vector3(0f, 0f, -9f);
        }

        if (this.eyes != null)
        {
            //this.eyes.localEulerAngles = new Vector3(0f, 4f, 0f);
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 4f, 0f);
        }


        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 10f, 5f);
            this.body.localPosition = new Vector3(-0.68f, this.defaultYPos - 0.25f, 0f);


        }

        this.SetEyes(-1);

        if (this.dress != null)
            this.dress.localEulerAngles = new Vector3(0f, 0f, -9f);
    }

    public void KnifePunishmentFalling(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 10f, 80f);
                this.leftArm.localEulerAngles = new Vector3(0f, -10f, 80f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 18f, 65f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 10f, 80f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, 26.1f);
                this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 21.5f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 37.1f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 18f, 65f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 10f, 80f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {

            }

            /*this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.4f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 20f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 1.05f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 18.5f);
            }
            else
            {

            }

        }
        if (stageId == 0)
        {
            this.SetEyes(2);
        }
        else if (stageId == 1)
        {
            this.SetEyes(0);
        }
        else
        {

        }
        //this.SetEyes(0);
    }

    public void KnifePunishmentWryPoseAnim(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(0f, 18f, 116f);
                this.leftArm.localEulerAngles = new Vector3(0f, -18f, 116f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, -110f, 88f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 110f, 88f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
                this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 15f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -37f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -39.5f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(50f, 0f, -25f);
                this.leftArm.localEulerAngles = new Vector3(-50f, 0f, -25f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);
                this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);

                this.rightLeg.localEulerAngles = new Vector3(25f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-25f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -58f);
            }
            else
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

            

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 10f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 10f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(-0.68f, this.defaultYPos - 0.21f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 10f, 10f);
            }
            else
            {
                this.body.localPosition = new Vector3(-0.64f, this.defaultYPos - 0.48f, this.transform.forward.z * 0.3f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 30f, 47f);
            }

        }

        if (this.dress != null)
        {
            if (stageId == 0)
                this.dress.localEulerAngles = new Vector3(0f, 0f, -9f);
            else
                this.dress.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
            

        this.SetEyes(0);
    }


    public void WitchBroomSit()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(25f, -30f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-25f, 30f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 10f);

            this.rightLeg.localEulerAngles = new Vector3(0f, -10f, 50f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 10f, 50f);
            this.rightLegJoint.localEulerAngles = new Vector3(10f, 0f, -50f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -65f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, 0f);
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);

            //this.body.localEulerAngles = new Vector3(0f, 0f, 0f);
            //this.body.localPosition = new Vector3(0f, this.defaultYPos, 0f);
        }

        this.SetEyes(0);
    }

    public void HoodGuyPipe(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightArm.localEulerAngles = new Vector3(24f, 0f, 0f);
                this.leftArm.localEulerAngles = new Vector3(-24f, 0f, 0f);
                this.rightArmJoint.localEulerAngles = new Vector3(-20f, 0f, 0f);
                this.leftArmJoint.localEulerAngles = new Vector3(20f, 0f, 0f);

                this.rightLeg.localEulerAngles = new Vector3(10f, 0f, 0f);
                this.leftLeg.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(10f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightArm.localEulerAngles = new Vector3(20f, 0f, -25f);
                this.leftArm.localEulerAngles = new Vector3(-155f, 0f, 30f);
                this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 85f);
                this.leftArmJoint.localEulerAngles = new Vector3(-30f, 0f, -30f);

                this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 35f);
                this.leftLeg.localEulerAngles = new Vector3(5f, 0f, 0f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -55f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
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

            

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 90f, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 65f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(0);
    }

    //[ContextMenu("MikeSleep")]
    public void MikeSleeping()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 22f, 87f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 95f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 15f, 90f);
            this.leftLeg.localEulerAngles = new Vector3(0f, -15f, 90f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -2f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -2f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, -12f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 5f, 0f);

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0f, this.defaultYPos - 1.06f, this.transform.forward.z * 0f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
        }

        this.SetEyes(5);
    }

    //[ContextMenu("JcapSleep")]
    public void JCapSleeping(int stageId = 0)
    {
        //int stageId = 0;
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 5f, 102f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -5f, 102f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 5f, 101f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -5f, 101f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 2)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 5f, 100f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -5f, 100f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 3)
            {
                this.rightLeg.localEulerAngles = new Vector3(0f, 5f, 99f);
                this.leftLeg.localEulerAngles = new Vector3(0f, -5f, 99f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {

            }

            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 25f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 25f);
            this.rightArmJoint.localEulerAngles = new Vector3(-15f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(15f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 5f, 0f);


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0.2f, this.defaultYPos - 1.05f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -15f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0.1875f, this.defaultYPos - 1.0475f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -14f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(0.175f, this.defaultYPos - 1.045f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -13f);
            }
            else if (stageId == 3)
            {
                this.body.localPosition = new Vector3(0.1625f, this.defaultYPos - 1.0425f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -12f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(5);
    }


    public void RoboJCapSleeping()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            /*this.rightArm.localEulerAngles = new Vector3(0f, 20f, 25f);
            this.leftArm.localEulerAngles = new Vector3(0f, -20f, 25f);*/

            this.rightArm.localEulerAngles = new Vector3(10f, 0f, 25f);
            this.leftArm.localEulerAngles = new Vector3(-10f, 0f, 25f);
            this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 20f, 0f);

        if (this.body != null)
        {
            this.body.localPosition = new Vector3(0.317f, this.defaultYPos - 0.07f, this.transform.forward.z * 0f);
            this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, -25f);
            //this.body.localPosition = new Vector3(0.26f, this.defaultYPos - 0.06f, this.transform.forward.z * -0.22f);
            //this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 36f, -25f);
            
        }

        this.SetEyes(5);
    }


    public void DemonSleeping(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                this.rightLeg.localEulerAngles = new Vector3(20f, 0f, 60f);
                this.leftLeg.localEulerAngles = new Vector3(-20f, 0f, 60f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -126f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -126f);
            }
            else if (stageId == 1)
            {
                this.rightLeg.localEulerAngles = new Vector3(19.6f, 0f, 59f);
                this.leftLeg.localEulerAngles = new Vector3(-19.6f, 0f, 59f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, -0.5f, -126f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 0.5f, -126f);
            }
            else if (stageId == 2)
            {
                this.rightLeg.localEulerAngles = new Vector3(19.2f, 0f, 58f);
                this.leftLeg.localEulerAngles = new Vector3(-19.2f, 0f, 58f);
                this.rightLegJoint.localEulerAngles = new Vector3(0f, -1f, -126f);
                this.leftLegJoint.localEulerAngles = new Vector3(0f, 1f, -126f);
            }
            else
            {

            }

            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 20f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 20f);
            this.rightArmJoint.localEulerAngles = new Vector3(-10f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(10f, 0f, 0f);

            /*this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);*/

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        if (this.eyes != null)
        {
            this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0.655f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 10f, -20f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(-0.011f, this.defaultYPos - 0.652f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 10f, -19f);
            }
            else if (stageId == 2)
            {
                this.body.localPosition = new Vector3(-0.022f, this.defaultYPos - 0.649f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 10f, -18f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(5);

        if (this.wingRight != null && this.wingLeft != null)
        {
            this.wingRight.localEulerAngles = new Vector3(0f, -15f, 0f);
            this.wingLeft.localEulerAngles = new Vector3(0f, 15f, 0f);
            this.wingRight.localScale = new Vector3(1f, 1f, 1f);
            this.wingLeft.localScale = new Vector3(1f, 1f, 1f);
        }
    }






    public void AnimationTemplate(int stageId = 0)
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            if (stageId == 0)
            {
                
            }
            else if (stageId == 1)
            {

            }
            else
            {

            }

            this.rightArm.localEulerAngles = new Vector3(20f, 0f, 0f);
            this.leftArm.localEulerAngles = new Vector3(-20f, 0f, 0f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            if (stageId == 0)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
                this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

        if (this.eyes != null)
        {
            if (stageId == 0)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                this.eyes.localPosition = new Vector3(0f, this.defaultEyeYHeight, 0f);
                this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);
            }


        }

        if (this.body != null)
        {
            if (stageId == 0)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }
            else if (stageId == 1)
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }
            else
            {
                this.body.localPosition = new Vector3(0f, this.defaultYPos - 0f, this.transform.forward.z * 0f);
                this.body.localEulerAngles = new Vector3(this.transform.forward.z * 0f, this.transform.forward.z * 0f, 0f);
            }

        }

        this.SetEyes(0);
    }

    /*[ContextMenu("MikeBallerAngryOld")]
    public void MikeBallerAngryOld()
    {
        if (this.rightArm != null && this.rightArmJoint != null && this.leftArm != null && this.leftArmJoint != null && this.rightLeg != null && this.rightLegJoint != null && this.leftLeg != null && this.leftLegJoint != null)
        {
            this.rightArm.localEulerAngles = new Vector3(0f, 3f, 40f);
            this.leftArm.localEulerAngles = new Vector3(0f, 10f, -110f);
            this.rightArmJoint.localEulerAngles = new Vector3(0f, 0f, 100f);
            this.leftArmJoint.localEulerAngles = new Vector3(0f, 0f, 30f);

            this.rightLeg.localEulerAngles = new Vector3(0f, 0f, 60f);
            this.leftLeg.localEulerAngles = new Vector3(0f, 0f, -3f);
            this.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -45f);
            this.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -37f);
        }

        if (this.upperBody != null && this.lowerBody != null)
        {
            this.upperBody.localEulerAngles = new Vector3(0f, 0f, 0f);
            this.lowerBody.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (this.eyes != null)
            this.eyes.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (this.body != null)
        {

            //this.body.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
            this.body.localEulerAngles = new Vector3(0f, 0f, -15f);
            this.body.localPosition = new Vector3(-0.32f, this.defaultYPos - 0.2f, 0f);
        }

        this.SetEyes(0);
    }*/
}
