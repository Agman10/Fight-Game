using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCapVsDarkJCapStartAnim : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject punchEffect;

    public AudioSource explosionSfx;
    public AudioSource swooshSfx;

    public AnimationCurve jumpCurve;

    [Space]
    public GameObject jCapSeriousEyes;
    public GameObject darkHandFlameP1;
    public GameObject darkHandFlameP2;

    public AnimationCurve endMovementCurve;

    public EnableArrayObjects flameTrailsPrefabP1;
    public EnableArrayObjects flameTrailsPrefabP2;

    public EnableArrayObjects currentFlameTrailsPrefab;

    public AudioSource flameSwooshSfx;
    public AudioSource flameSwooshSfx2;

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
            //this.StartCoroutine(this.TemplateCoroutine());

            if (GameManager.Instance.randomNumber >= 500)
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }
            else
            {
                if (this.user.characterId == 0)
                {
                    this.StartCoroutine(this.Vs2JCapCoroutine());
                }
                else if (this.user.characterId == 1)
                {
                    this.StartCoroutine(this.Vs2DarkCoroutine());
                }
            }

            
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -10f, this.user.transform.position.y, 0f);

        if (this.swooshSfx != null)
        {
            this.swooshSfx.time = 0.06f;
            this.swooshSfx.Play();
        }

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

        if (this.explosionSfx != null)
        {
            this.explosionSfx.time = 0.08f;
            this.explosionSfx.Play();
        }


        if (this.animations != null)
            this.animations.JcapVsDarkStartAnimation();
        
        if (GameManager.Instance != null && GameManager.Instance.randomNumber == 800)
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

            /*currentTime = 0;
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
            }*/



            currentTime = 0;
            duration = 0.35f;
            targetPosition = this.user.transform.forward.z * -7f;
            start = this.user.transform.position.x;
            /*startY = this.user.transform.position.y;
            targetPositionY = 0;*/
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.jumpCurve.Evaluate(currentTime / duration), 0f);

                this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 720f, currentTime / duration));
                yield return null;
            }

            this.user.transform.position = new Vector3(this.user.transform.forward.z * -7f, 0f, 0f);

            this.animations.RoadRollerEndLand();
            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.SetDefaultPose();
            yield return new WaitForSeconds(0.2f);
        }
        


        



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator Vs2JCapCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -0.6f, this.user.transform.position.y, 0f);

        this.animations.JCapVsDark2StartJCap();

        if (this.jCapSeriousEyes != null)
            this.jCapSeriousEyes.SetActive(true);

        this.animations.SetEyes(-1);

        yield return new WaitForSeconds(0.01f);

        this.animations.JCapVsDark2StartJCap();

        yield return new WaitForSeconds(0.7f);

        this.InstantiateFlameTrails();

        yield return new WaitForSeconds(0.3f);

        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(1);

        this.animations.SetEyes(0);
        if (this.jCapSeriousEyes != null)
            this.jCapSeriousEyes.SetActive(false);


        yield return new WaitForSeconds(0.05f);

        if (this.flameSwooshSfx != null)
            this.flameSwooshSfx.Play();

        this.EnableFlameTrails(0);

        yield return new WaitForSeconds(0.4f);
        if (this.flameSwooshSfx != null)
            this.flameSwooshSfx.Play();

        this.EnableFlameTrails(1);

        yield return new WaitForSeconds(0.4f);
        if (this.flameSwooshSfx != null)
            this.flameSwooshSfx.Play();

        this.EnableFlameTrails(2);

        yield return new WaitForSeconds(0.5f);

        this.user.FlipDirection();

        /*if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(0);*/

        if (GameManager.Instance != null)
            GameManager.Instance.TurnScreenWhite();

        yield return new WaitForSeconds(0.05f);
        this.EnableFlameTrails(3);
        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        if (this.flameSwooshSfx2 != null)
            this.flameSwooshSfx2.Play();


        if (GameManager.Instance != null)
            GameManager.Instance.TurnScreenNormal();

        this.animations.JCapVsDark2EndJCap(0);
        this.user.transform.position = new Vector3(this.user.transform.forward.z * 1f, 0f, 0f);

        //this.EnableFlameTrails(3);

        float currentTime = 0;
        float duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.forward.z * (this.endMovementCurve.Evaluate(currentTime / duration)), 0f, 0f);
            yield return null;
        }

        this.user.transform.position = new Vector3(this.user.transform.forward.z * 4f, 0f, 0f);

        yield return new WaitForSeconds(0.2f);
        this.animations.JCapVsDark2EndJCap(1);

        yield return new WaitForSeconds(0.05f);

        this.user.FlipDirection();

        this.animations.RollAnimation();

        this.animations.body.localPosition = new Vector3(0f, 1f, 0f);

        currentTime = 0;
        duration = 0.2f;
        float targetPositionX = this.user.transform.forward.z * -7f;
        float startX = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), 0f, 0f);
            this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 360, currentTime / duration));
            yield return null;
        }

        this.user.transform.position = new Vector3(this.user.transform.forward.z * -7f, 0f, 0f);

        /*this.animations.RoadRollerEndLand();
        yield return new WaitForSeconds(0.1f);*/

        this.animations.SetDefaultPose();

        this.RemoveFlameTrails();

        yield return new WaitForSeconds(0.2f);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator Vs2DarkCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.LookAtTarget();
        this.user.transform.position = new Vector3(this.user.transform.forward.z * -0.6f, this.user.transform.position.y, 0f);

        this.animations.JCapVsDark2StartDark();

        yield return new WaitForSeconds(0.01f);

        this.animations.JCapVsDark2StartDark();

        yield return new WaitForSeconds(0.7f);

        this.EnableDarkHandFlame(true);

        yield return new WaitForSeconds(0.3f);

        this.EnableDarkHandFlame(false);

        yield return new WaitForSeconds(0.05f);

        yield return new WaitForSeconds(0.4f);

        yield return new WaitForSeconds(0.4f);

        yield return new WaitForSeconds(0.5f);


        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.1f);

        this.user.FlipDirection();

        this.animations.JCapVsDark2EndDark(0);
        this.user.transform.position = new Vector3(this.user.transform.forward.z * 1f, 0f, 0f);

        float currentTime = 0;
        float duration = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.forward.z * (this.endMovementCurve.Evaluate(currentTime / duration)), 0f, 0f);
            yield return null;
        }

        this.user.transform.position = new Vector3(this.user.transform.forward.z * 4f, 0f, 0f);

        yield return new WaitForSeconds(0.2f);
        this.animations.JCapVsDark2EndDark(1);

        yield return new WaitForSeconds(0.05f);

        this.user.FlipDirection();

        this.animations.RollAnimation();

        this.animations.body.localPosition = new Vector3(0f, 1f, 0f);

        currentTime = 0;
        duration = 0.2f;
        float targetPositionX = this.user.transform.forward.z * -7f;
        float startX = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(startX, targetPositionX, currentTime / duration), 0f, 0f);
            this.animations.body.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 360, currentTime / duration));
            yield return null;
        }

        this.user.transform.position = new Vector3(this.user.transform.forward.z * -7f, 0f, 0f);

        /*this.animations.RoadRollerEndLand();
        yield return new WaitForSeconds(0.1f);*/

        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }


    public override void Stop()
    {
        base.Stop();
        if (!this.user.dead)
            this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        if (this.darkHandFlameP2 != null)
            this.darkHandFlameP2.SetActive(false);

        if (this.darkHandFlameP1 != null)
            this.darkHandFlameP1.SetActive(false);

        if (this.jCapSeriousEyes != null)
            this.jCapSeriousEyes.SetActive(false);

        /*if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(0);*/

        if (GameManager.Instance != null)
            GameManager.Instance.TurnScreenNormal();

        this.RemoveFlameTrails();

        this.user.EntranceDone();
    }

    public void EnableDarkHandFlame(bool boolean)
    {
        
        if (boolean == true)
        {
            if (this.user != null)
            {
                if(this.user.playerNumber == 1)
                {
                    if (this.darkHandFlameP1 != null)
                        this.darkHandFlameP1.SetActive(true);
                }
                else
                {
                    if (this.darkHandFlameP2 != null)
                        this.darkHandFlameP2.SetActive(true);
                }
            }
        }
        else
        {
            if (this.darkHandFlameP2 != null)
                this.darkHandFlameP2.SetActive(false);

            if (this.darkHandFlameP1 != null)
                this.darkHandFlameP1.SetActive(false);
        }
    }

    public void InstantiateFlameTrails()
    {
        if(this.user != null)
        {
            EnableArrayObjects flameTrails = null;

            if(this.user.playerNumber == 1 && this.flameTrailsPrefabP1 != null)
            {
                flameTrails = this.flameTrailsPrefabP1;
            }
            else if (this.user.playerNumber == 2 && this.flameTrailsPrefabP2 != null)
            {
                flameTrails = this.flameTrailsPrefabP2;
            }

            if (flameTrails != null)
            {
                flameTrails = Instantiate(flameTrails, Vector3.zero, Quaternion.identity);
                this.currentFlameTrailsPrefab = flameTrails;
            }
        }
        
    }

    public void EnableFlameTrails(int trailsId)
    {
        if (this.currentFlameTrailsPrefab != null)
        {
            this.currentFlameTrailsPrefab.EnableObject(trailsId);
        }
    }

    public void RemoveFlameTrails()
    {
        if (this.currentFlameTrailsPrefab != null)
        {
            this.currentFlameTrailsPrefab.gameObject.SetActive(false);
            this.currentFlameTrailsPrefab = null;
        }
    }
}
