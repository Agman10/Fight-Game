using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCapVsDarkJCapStartAnim : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject punchEffect;

    public override void OnHit()
    {
        base.OnHit();
        /*if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }*/
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -10f, this.user.transform.position.y, 0f);

        yield return new WaitForSeconds(0.01f);

        if (this.animations != null)
            this.animations.JcapVsDarkStartAnimation0();

        float currentTime = 0;
        float duration = 0.3f;
        float targetPosition = this.user.transform.forward.z * -0.9f;
        //float start = this.user.transform.position.x;
        //float start = this.user.transform.forward.z * -7f;
        float start = this.user.transform.forward.z * -10f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);

            

            yield return null;
        }

        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            //punchEffectPrefab = Instantiate(punchEffectPrefab, new Vector3(0f, 2.15f, 0f), Quaternion.Euler(0, 0, 0));
            punchEffectPrefab = Instantiate(punchEffectPrefab, new Vector3(0f, 1.5f, 0f), Quaternion.Euler(0, 0, 0));
        }


        if (this.animations != null)
            this.animations.JcapVsDarkStartAnimation();
        
        if (GameManager.Instance != null && GameManager.Instance.randomNumber == 1)
        {
            yield return new WaitForSeconds(0.1f);
            this.user.Die(Vector3.zero, true, false, true);
        }
        else
        {
            currentTime = 0;
            duration = 1.5f;
            float rightArmZRotation = this.animations.rightArm.transform.localEulerAngles.z;
            float leftArmZRotation = this.animations.leftArm.transform.localEulerAngles.z;
            float rotationHeight = 0.2f;
            float rotationSpeed = 50f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                if (this.animations != null)
                {
                    float newY = Mathf.Sin(Time.time * rotationSpeed);
                    if (this.user.transform.forward.z > 0f)
                    {
                        this.animations.rightArm.transform.localEulerAngles = new Vector3(
                        this.animations.rightArm.transform.localEulerAngles.x,
                        this.animations.rightArm.transform.localEulerAngles.y,
                        rightArmZRotation + (newY * rotationHeight));
                    }
                    else
                    {
                        this.animations.leftArm.transform.localEulerAngles = new Vector3(
                        this.animations.leftArm.transform.localEulerAngles.x,
                        this.animations.leftArm.transform.localEulerAngles.y,
                        leftArmZRotation + (newY * rotationHeight));
                    }
                }
                yield return null;
            }





            //yield return new WaitForSeconds(1.5f);

            if (this.animations != null)
                this.animations.RollAnimation();

            currentTime = 0;
            duration = 0.15f;
            targetPosition = this.user.transform.forward.z * -3.95f;
            start = this.user.transform.position.x;
            float startY = this.user.transform.position.y;
            float targetPositionY = 4;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0);

                if (this.animations != null)
                    this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));
                yield return null;
            }

            /*if (this.animations != null)
                this.animations.StartAnimationRagingBeastBlock2();*/

            currentTime = 0;
            duration = 0.15f;
            targetPosition = this.user.transform.forward.z * -7f;
            start = this.user.transform.position.x;
            startY = this.user.transform.position.y;
            targetPositionY = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), Mathf.Lerp(startY, targetPositionY, currentTime / duration), 0);

                if (this.animations != null)
                    this.animations.body.transform.Rotate(new Vector3(0f, 0f, 2000f * Time.deltaTime));
                yield return null;
            }

            if (this.animations != null)
                this.animations.SetDefaultPose();
            yield return new WaitForSeconds(0.2f);
        }
        


        



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
