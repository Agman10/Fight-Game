using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNukeCall : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool inputting;

    public GameObject startParticle;
    public GameObject wristPad;
    public GameObject nukeBeaconModel;

    public GameObject inputUI;
    public GameObject failCross;

    private int inputStage;

    public SuperNuke nuke;
    public NukeBeacon nukeBeacon;

    public AudioSource clickSound;
    public AudioSource failSound;
    public AudioSource throwSfx;

    [Space]
    public NukeInputArrow arrowUp;
    public NukeInputArrow arrowRight;
    public NukeInputArrow arrowDown1;
    public NukeInputArrow arrowDown2;
    public NukeInputArrow arrowDown3;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.user != null && this.user.input != null)
        {
            this.user.input.MoveInput += OnMoveInput;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.user != null && this.user.input != null)
        {
            this.user.input.MoveInput -= OnMoveInput;
        }
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            

            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    //this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryCallNuke());
                }
            }
        }
    }

    private IEnumerator TryCallNuke()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.inputting = true;

        if (this.animations != null)
            this.animations.HoodGuyWristPad();

        if (this.inputUI != null)
            this.inputUI.SetActive(true);

        if (this.wristPad != null)
            this.wristPad.SetActive(true);

        if (this.nukeBeaconModel != null)
            this.nukeBeaconModel.SetActive(true);

        float time = 6f;
        while (time > 0 && this.inputting)
        {
            time -= Time.deltaTime;

            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);

        this.inputting = false;

        if (this.wristPad != null)
            this.wristPad.SetActive(false);

        if (this.inputUI != null)
            this.inputUI.SetActive(false);

        if (this.nukeBeaconModel != null)
            this.nukeBeaconModel.SetActive(false);

        //this.ResetUI();
        this.SetStage(0);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void SuccessfullyCallNuke()
    {
        this.StopAllCoroutines();
        this.inputting = false;
        this.StartCoroutine(this.SuccessfullyCallNukeCoroutine());
    }

    private IEnumerator SuccessfullyCallNukeCoroutine()
    {
        this.user.GiveSuperCharge(-this.user.maxSuperCharge);

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        /*if (this.nuke != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            SuperNuke nukePrefab = this.nuke;
            nukePrefab = Instantiate(nukePrefab, new Vector3(summonPos, 11f, 0), Quaternion.Euler(0, 0, 0));
            nukePrefab.SetOwner(this.user);
            //bigSpherePrefab.belongsTo = this.user;
        }*/

        if (this.nukeBeacon != null && this.user.tempOpponent != null)
        {
            float summonPos = this.user.tempOpponent.transform.position.x;
            if (this.user.tempOpponent.dead && this.user.tempOpponent.ragdoll != null)
                summonPos = this.user.tempOpponent.ragdoll.transform.position.x;

            NukeBeacon nukeBeaconPrefab = this.nukeBeacon;
            nukeBeaconPrefab = Instantiate(nukeBeaconPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1), 2.5f, 0), Quaternion.Euler(0, 0, 0));
            nukeBeaconPrefab.SetOwner(this.user);
            nukeBeaconPrefab.ThrowBeacon(summonPos, this.user.transform.forward.z);
            //bigSpherePrefab.belongsTo = this.user;
        }

        this.user.animations.NukeBeaconThrow();

        if (this.throwSfx != null)
        {
            //this.throwSfx.time = 0.01f;
            this.throwSfx.Play();
        }

        if (this.nukeBeaconModel != null)
            this.nukeBeaconModel.SetActive(false);

        if (this.wristPad != null)
            this.wristPad.SetActive(false);

        yield return new WaitForSeconds(0.2f);


        /*if (this.wristPad != null)
            this.wristPad.SetActive(false);*/

        if (this.inputUI != null)
            this.inputUI.SetActive(false);

        this.SetStage(0);

        /*yield return new WaitForSeconds(0.2f);

        this.animations.HoodGuySalute();
        this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 70f, this.animations.body.localEulerAngles.z);

        if (this.wristPad != null)
            this.wristPad.SetActive(false);*/

        yield return new WaitForSeconds(0.5f);

        if (this.wristPad != null)
            this.wristPad.SetActive(false);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void FailCallNuke()
    {
        this.StopAllCoroutines();
        this.inputting = false;
        if (this.failSound != null)
            this.failSound.Play();
        this.StartCoroutine(this.FailCallNukeCoroutine());
    }

    private IEnumerator FailCallNukeCoroutine()
    {
        this.inputting = false;

        this.animations.SetEyes(4);

        this.StartCoroutine(this.FailAnimationCoroutine());

        if (this.failCross != null)
            this.failCross.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(false);

        /*yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.failCross != null)
            this.failCross.SetActive(false);

        yield return new WaitForSeconds(0.2f);*/



        if (this.wristPad != null)
            this.wristPad.SetActive(false);

        if (this.inputUI != null)
            this.inputUI.SetActive(false);

        if (this.nukeBeaconModel != null)
            this.nukeBeaconModel.SetActive(false);

        //this.ResetUI();
        this.SetStage(0);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator FailAnimationCoroutine()
    {
        float eyesXRotation = this.animations.eyes.localEulerAngles.x;
        float eyesZRotation = this.animations.eyes.localEulerAngles.z;

        this.animations.eyes.localEulerAngles = new Vector3(eyesXRotation, -4, eyesZRotation);

        yield return new WaitForSeconds(0.1f);

        this.animations.eyes.localEulerAngles = new Vector3(eyesXRotation, 4, eyesZRotation);
        yield return new WaitForSeconds(0.1f);

        this.animations.eyes.localEulerAngles = new Vector3(eyesXRotation, -2, eyesZRotation);

        yield return new WaitForSeconds(0.1f);

        this.animations.eyes.localEulerAngles = new Vector3(eyesXRotation, 2, eyesZRotation);

        yield return new WaitForSeconds(0.1f);

        this.animations.eyes.localEulerAngles = new Vector3(eyesXRotation, 0, eyesZRotation);
    }

    public override void Stop()
    {
        base.Stop();

        if (this.inputUI != null)
            this.inputUI.SetActive(false);

        if (this.failCross != null)
            this.failCross.SetActive(false);

        if (this.wristPad != null)
            this.wristPad.SetActive(false);

        if (this.nukeBeaconModel != null)
            this.nukeBeaconModel.SetActive(false);

        //this.ResetUI();
        this.SetStage(0);

        this.inputting = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void OnMoveInput(Vector3 movement)
    {
        if (GameManager.Instance != null && !GameManager.Instance.gameIsPaused)
        {
            if (this.onGoing && this.inputting && movement != Vector3.zero)
            {
                //Debug.Log(movement);

                if (this.inputStage == 0)
                {
                    if (movement == new Vector3(0f, 1f, 0f)) //Up
                        this.SetStage(1);
                    else
                        this.FailCallNuke();
                }
                else if (this.inputStage == 1)
                {
                    if (movement == new Vector3(1f, 0f, 0f) /*|| movement == new Vector3(1f, 1f, 0f)*/) //Right
                        this.SetStage(2);
                    else
                        this.FailCallNuke();
                }
                else if (this.inputStage == 2)
                {
                    if (movement == new Vector3(0f, -1f, 0f) || movement == new Vector3(1f, -1f, 0f)) //Down 1
                        this.SetStage(3);
                    else
                        this.FailCallNuke();
                }
                else if (this.inputStage == 3)
                {
                    if (movement == new Vector3(0f, -1f, 0f)) //Down 2
                        this.SetStage(4);
                    else
                        this.FailCallNuke();
                }
                else if (this.inputStage == 4)
                {
                    if (movement == new Vector3(0f, -1f, 0f))
                    {
                        this.SetStage(5);
                        this.SuccessfullyCallNuke();
                    }
                    else
                    {
                        this.FailCallNuke();
                    }
                }
            }
        }
        
    }

    public void SetStage(int stage)
    {
        this.inputStage = stage;
        if (stage == 0) //Up
        {
            this.ResetUI();
        }
        else if (stage == 1) //Right
        {
            if (this.arrowUp != null)
                this.arrowUp.SetArrow(2);

            if (this.arrowRight != null)
                this.arrowRight.SetArrow(0);
        }
        else if (stage == 2) //Down 1
        {
            if (this.arrowRight != null)
                this.arrowRight.SetArrow(2);

            if (this.arrowDown1 != null)
                this.arrowDown1.SetArrow(0);
        }
        else if (stage == 3) //Down 2
        {
            if (this.arrowDown1 != null)
                this.arrowDown1.SetArrow(2);

            if (this.arrowDown2 != null)
                this.arrowDown2.SetArrow(0);
        }
        else if (stage == 4) //Down 3
        {
            if (this.arrowDown2 != null)
                this.arrowDown2.SetArrow(2);

            if (this.arrowDown3 != null)
                this.arrowDown3.SetArrow(0);
        }
        else
        {
            if (this.arrowDown3 != null)
                this.arrowDown3.SetArrow(2);
        }

        if (stage != 0) 
        {
            if (this.clickSound != null)
                this.clickSound.Play();

            this.StartCoroutine(this.ClickAnimationCoroutine());
        }
    }

    private IEnumerator ClickAnimationCoroutine()
    {
        this.animations.rightArm.localEulerAngles = new Vector3(0f, -5f, 53f);
        this.animations.rightArmJoint.localEulerAngles = new Vector3(-75f, 23f, 0f);
        yield return new WaitForSeconds(0.02f);
        if (this.animations != null)
            this.animations.HoodGuyWristPad();
    }

    public void ResetUI()
    {
        if (this.arrowUp != null)
            this.arrowUp.SetArrow(0);

        if (this.arrowRight != null)
            this.arrowRight.SetArrow(1);

        if (this.arrowDown1 != null)
            this.arrowDown1.SetArrow(1);

        if (this.arrowDown2 != null)
            this.arrowDown2.SetArrow(1);

        if (this.arrowDown3 != null)
            this.arrowDown3.SetArrow(1);
    }
}
