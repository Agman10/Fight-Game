using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject hammer;
    public TestHitbox hitbox;
    public GameObject impactEffect;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
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
            //this.user.AddStun(0.2f, true);

            this.user.AddStun(0.2f, false);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.hitbox != null)
        {
            this.hitbox.damage = 15f;
            this.hitbox.superChargeAmount = 10f;
        }


        if (this.animations != null)
            this.animations.HammerAttack(0);

        if (this.hammer != null)
        {
            this.hammer.gameObject.SetActive(true);
            this.hammer.gameObject.transform.localPosition = new Vector3(-1.2f, 0.205f, 0f);
            this.hammer.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 246f);
        }

        /*float currentTime = 0;
        float duration = 0.3f;
        float targetPosition = 0f;
        float start = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }*/

        float waitTime = 0.5f;
        while (/*this.user.transform.position.y != 0f */Mathf.Abs(this.user.rb.velocity.y) > 0f)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if (waitTime < 0f)
            waitTime = 0f;

        this.user.rb.isKinematic = true;

        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(waitTime);

        float testTime = 0f;
        float startPosY = this.hammer.transform.localPosition.y;
        float startRightArmZRot = this.animations.rightArm.localEulerAngles.z;
        float startLeftArmZRot = this.animations.leftArm.localEulerAngles.z;

        float extraDamage = 0f;
        float extraCharge = 0f;

        while (this.user.input.special2 && testTime < 3f)
        {

            if (this.hitbox != null)
            {
                //this.hitbox.damage += Time.deltaTime * 2f;
                extraDamage += Time.deltaTime * 5f;
                extraCharge += Time.deltaTime * 2f;
            }

            testTime += Time.deltaTime;

            float newY = Mathf.Sin(testTime * 100f);
            
            this.hammer.transform.localPosition = new Vector3(this.hammer.transform.localPosition.x, startPosY + (newY * 0.01f), this.hammer.transform.localPosition.z);

            float newZ = Mathf.Sin(testTime * 300f);
            this.animations.rightArm.localEulerAngles = new Vector3(this.animations.rightArm.localEulerAngles.x, this.animations.rightArm.localEulerAngles.y, startRightArmZRot + (newZ * 0.5f));
            this.animations.leftArm.localEulerAngles = new Vector3(this.animations.leftArm.localEulerAngles.x, this.animations.leftArm.localEulerAngles.y, startLeftArmZRot + (newZ * 0.5f));
            yield return null;
        }
        //Debug.Log(extraDamage);
        //Debug.Log(extraCharge);

        extraDamage = Mathf.Round(extraDamage);
        extraCharge = Mathf.Round(extraCharge);

        //Debug.Log(extraDamage);
        //Debug.Log(extraCharge);

        if (this.animations != null)
            this.animations.HammerAttack(1);

        if (this.hammer != null)
        {
            this.hammer.gameObject.SetActive(true);
            this.hammer.gameObject.transform.localPosition = new Vector3(-0.15f, 1.42f, 0f);
            this.hammer.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 162f);
        }

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.HammerAttack(2);

        if (this.hammer != null)
        {
            this.hammer.gameObject.SetActive(true);
            this.hammer.gameObject.transform.localPosition = new Vector3(1.445f, -0.125f, 0f);
            this.hammer.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 20f);
        }

        if (this.impactEffect != null)
        {
            GameObject impactEffectPrefab = this.impactEffect;
            impactEffectPrefab = Instantiate(impactEffectPrefab, new Vector3(this.user.transform.position.x+ (this.transform.forward.z * 1.45f), 0.01f, 0f), Quaternion.Euler(0, 0, 0));
        }

        if (this.hitbox != null)
        {
            this.hitbox.damage = 15 + extraDamage;
            this.hitbox.superChargeAmount = 10 + extraCharge;
            //extraDamage += Time.deltaTime * 2f;
        }

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.hitbox != null)
        {
            this.hitbox.gameObject.SetActive(false);
            this.hitbox.damage = 15f;
            this.hitbox.superChargeAmount = 10f;
        }

        yield return new WaitForSeconds(0.4f);

        

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.hammer != null)
        {
            this.hammer.gameObject.SetActive(false);
            this.hammer.gameObject.transform.localPosition = new Vector3(-1.2f, 0.205f, 0f);
            this.hammer.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 246f);
        }

        this.user.rb.isKinematic = false;

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
        {
            this.hitbox.damage = 15f;
            this.hitbox.superChargeAmount = 10f;
            this.hitbox.gameObject.SetActive(false);
        }

        if (this.hammer != null)
        {
            this.hammer.gameObject.SetActive(false);
            this.hammer.gameObject.transform.localPosition = new Vector3(-1.2f, 0.205f, 0f);
            this.hammer.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 246f);
        }

        this.user.rb.isKinematic = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
