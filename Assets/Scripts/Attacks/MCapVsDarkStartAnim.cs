using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCapVsDarkStartAnim : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    //public GameObject punchEffect;

    public GameObject trail;
    public GameObject punchEffect;

    public AudioSource explosionSfx;
    public AudioSource swooshSfx;

    public AudioSource ragingBeastSwooshSfx;
    public AudioSource ragingBeastPunchSfx;
    public AudioSource strikeSfx;

    public GameObject strikeEffect;

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
            if (this.user.characterId == 1)
                this.StartCoroutine(this.DarkCoroutine());
            else if (this.user.characterId == 6)
                this.StartCoroutine(this.MasterCoroutine());
        }
    }

    private IEnumerator DarkCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        //this.user.LookAtTarget();

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = true;

        float startXPos = this.user.transform.position.x;

        float testFloat = 1f;
        if (this.user.playerNumber == 2)
            testFloat = -1f;

        //this.user.transform.position = new Vector3(this.user.transform.forward.z * 10f, this.user.transform.position.y, 0f);
        this.user.transform.position = new Vector3(testFloat * 10f, this.user.transform.position.y, 0f);

        //this.user.transform.position = new Vector3(-startXPos * 1.2f, 0f, 0f);


        //this.user.FlipDirection();
        //this.user.LookAtTarget();
        this.user.LookAtCenter();

        if (this.ragingBeastSwooshSfx != null)
        {
            this.ragingBeastSwooshSfx.time = 0.06f;
            this.ragingBeastSwooshSfx.Play();
        }

        yield return new WaitForSeconds(0.01f);



        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.animations != null)
            this.animations.RagingBeastDash();

        float currentTime = 0;
        float duration = 0.5f;
        float targetPosition = this.user.transform.forward.z * 5.6f;
        //float start = this.user.transform.position.x;
        //float start = this.user.transform.forward.z * -7f;
        float start = this.user.transform.forward.z * -10f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }
        /*if (this.animations != null)
            this.animations.StartAnimationRagingBeastGrab();*/

        if (this.animations != null)
            this.animations.SetGrabbingPose();

        yield return new WaitForSeconds(0.2f);
        if (this.trail != null)
            this.trail.SetActive(false);

        if (this.animations != null)
            this.animations.body.localScale = new Vector3(0f, 0f, 0f);

        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(3);


        yield return new WaitForSeconds(0.1f);

        /*float minXPos = -0.75f;
        float maxXPos = 0.75f;*/

        float minXPos = -0.75f + (this.user.transform.forward.z * -0.4f);
        float maxXPos = 0.75f + (this.user.transform.forward.z * -0.4f);

        //Vector3 randomPos = new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -2);

        int amount = 30;
        int amount2 = 0;
        /*if (this.user.playerNumber == 2)
            amount2 = 1;*/
        while (amount > 0)
        {
            /*if (amount2 == 0)
                this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), false);*/

            //this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), false);

            /*this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x - 0.75f + (this.user.transform.forward.z * -0.375f), this.RandomXY(0.5f, 2.5f), -0.5f), false);
            this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + 0.75f + (this.user.transform.forward.z * -0.375f), this.RandomXY(0.5f, 2.5f), -0.5f), false);*/

            yield return new WaitForSeconds(0.025f);
            bool playSfx = false;
            if (amount2 == 0)
                playSfx = true;
            else
                playSfx = false;

            //this.PunchEffect(randomPos, playSfx);
            this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), playSfx);
            amount -= 1;

            amount2++;
            if (amount2 > 1)
                amount2 = 0;
            yield return null;
        }

        this.user.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * -1f), this.user.transform.position.y, 0f);







        yield return new WaitForSeconds(0.1f);

        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(0);

        if (this.animations != null && this.user != null)
            this.animations.body.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);

        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);





        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.2f);

        //this.animations.McapStartAnimStrike();
        this.animations.DarkStrikeBlock();

        //yield return new WaitForSeconds(0.1f);






        currentTime = 0;
        duration = 0.2f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            //this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);

        Vector3 bodyPos = this.animations.body.transform.localPosition;
        float shakeDist = 0.025f;

        amount = 20;
        while (amount > 0)
        {
            this.animations.body.transform.localPosition = bodyPos + new Vector3(this.RandomXY(-shakeDist, shakeDist), this.RandomXY(-shakeDist, shakeDist), 0f);
            yield return new WaitForSeconds(0.025f);

            this.animations.body.transform.localPosition = bodyPos + new Vector3(this.RandomXY(-shakeDist, shakeDist), this.RandomXY(-shakeDist, shakeDist), 0f);

            amount -= 1;
            yield return null;
        }
        this.animations.body.transform.localPosition = bodyPos;

        //this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.05f);



        yield return new WaitForSeconds(0.2f);

        this.user.LookAtTarget();

        this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.05f);

        /*this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);*/

        if (this.animations != null)
            this.animations.RollAnimation();

        this.animations.body.localPosition = new Vector3(0f, 1f, 0f);

        currentTime = 0;
        duration = 0.15f;
        targetPosition = this.user.transform.forward.z * -7f;
        start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), 0f, 0f);

            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0f, 0f, /*this.user.transform.forward.z **/ 2000f * Time.deltaTime));
            yield return null;
        }

        //this.animations.SetDefaultPose();

        this.animations.SetPunchUppercutStartAnim1();
        yield return new WaitForSeconds(0.1f);
        this.animations.SetDefaultPose();

        /*yield return new WaitForSeconds(0.1f);
        this.user.LookAtTarget();*/


        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        yield return new WaitForSeconds(0.2f);

        //Debug.Log("Dark");



        this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }





    private IEnumerator MasterCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;

        float startXPos = this.user.transform.position.x;

        //this.user.LookAtTarget();
        //this.user.transform.position = new Vector3(this.user.transform.forward.z * 7f, this.user.transform.position.y, 0f);

        this.user.transform.position = new Vector3(-startXPos, 0f, 0f);

        //this.user.FlipDirection();
        //this.user.LookAtTarget();
        this.user.LookAtCenter();

        yield return new WaitForSeconds(0.01f);


        float currentTime = 0;
        float duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 0.45f)
                this.user.animations.MCapRagingBeastBlock();

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);



        yield return new WaitForSeconds(0.1f);
        Vector3 bodyPos = this.animations.body.transform.localPosition;
        float shakeDist = 0.025f;

        int amount = 30;
        while (amount > 0)
        {
            this.animations.body.transform.localPosition = bodyPos + new Vector3(this.RandomXY(-shakeDist, shakeDist), this.RandomXY(-shakeDist, shakeDist), 0f);
            yield return new WaitForSeconds(0.025f);

            this.animations.body.transform.localPosition = bodyPos + new Vector3(this.RandomXY(-shakeDist, shakeDist), this.RandomXY(-shakeDist, shakeDist), 0f);

            amount -= 1;
            yield return null;
        }
        this.animations.body.transform.localPosition = bodyPos;

        yield return new WaitForSeconds(0.1f);


        //Debug.Log("test");


        yield return new WaitForSeconds(0.1f);

        this.animations.McapStrikeStart();

        yield return new WaitForSeconds(0.2f);

        /*yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.1f);*/



        this.animations.McapStartAnimStrike();

        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.strikeSfx != null)
        {
            this.strikeSfx.time = 0.01f;
            //this.ragingBeastPunchSfx.pitch = Random.Range(0.9f, 1.1f);
            this.strikeSfx.Play();
        }

        if (this.strikeEffect != null)
        {
            GameObject startParticlePrefab = this.strikeEffect;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.tempOpponent.transform.position.x, 1.5f, -0.5f), Quaternion.Euler(0, 0, 0));
        }

        currentTime = 0;
        duration = 0.2f;
        float targetPosition = this.user.transform.forward.z * 7f;
        //float start = this.user.transform.position.x;
        //float start = this.user.transform.forward.z * -7f;
        float start = this.user.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(Mathf.Lerp(start, targetPosition, currentTime / duration), this.user.transform.position.y, 0);
            yield return null;
        }

        //yield return new WaitForSeconds(0.2f);


        /*float minXPos = -0.75f;
        float maxXPos = 0.75f;*/

        float minXPos = -0.65f + (this.user.transform.forward.z * 0.2f);
        float maxXPos = 0.65f + (this.user.transform.forward.z * 0.2f);

        //Vector3 randomPos = new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -2);

        amount = 20;
        int amount2 = 0;
        /*if (this.user.playerNumber == 2)
            amount2 = 1;*/
        while (amount > 0)
        {
            //this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), false);

            /*if (amount2 == 0)
                this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), false);*/

            /*this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x - 0.65f + (this.user.transform.forward.z * 0.2f), this.RandomXY(0.5f, 2.5f), -0.5f), false);
            this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + 0.65f + (this.user.transform.forward.z * 0.2f), this.RandomXY(0.5f, 2.5f), -0.5f), false);*/

            yield return new WaitForSeconds(0.025f);
            bool playSfx = false;
            if (amount2 == 0)
                playSfx = true;
            else
                playSfx = false;

            //this.PunchEffect(randomPos, playSfx);
            this.PunchEffect(new Vector3(this.user.tempOpponent.transform.position.x + this.RandomXY(minXPos, maxXPos), this.RandomXY(0.5f, 2.5f), -0.5f), playSfx);
            amount -= 1;

            amount2++;
            if (amount2 > 1)
                amount2 = 0;
            yield return null;
        }

        /*amount = 30;
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.025f);
            amount -= 1;
            yield return null;
        }*/
        //this.animations.body.transform.localPosition = bodyPos;

        //this.animations.SetDefaultPose();
        this.animations.McapStrikeEnd();

        if (this.trail != null)
            this.trail.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);

        this.user.LookAtTarget();
        



        yield return new WaitForSeconds(0.2f);



        yield return new WaitForSeconds(0.05f);



        currentTime = 0;
        duration = 0.15f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        this.animations.SetDefaultPose();





        yield return new WaitForSeconds(0.1f);


        yield return new WaitForSeconds(0.2f);

        //Debug.Log("Master");

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

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;

        if (GameManager.Instance != null && this.onGoing)
            GameManager.Instance.RagingBeastEffect(0);

        if (this.trail != null)
            this.trail.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }


    public void PunchEffect(Vector3 position, bool playSfx)
    {
        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            punchEffectPrefab = Instantiate(punchEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }

        if (this.ragingBeastPunchSfx != null && playSfx)
        {
            this.ragingBeastPunchSfx.time = 0.01f;
            //this.ragingBeastPunchSfx.time = 0.015f;
            //this.ragingBeastPunchSfx.pitch = Random.Range(0.9f, 1.1f);
            //this.ragingBeastPunchSfx.pitch = Random.Range(0.65f, 0.85f);

            this.ragingBeastPunchSfx.pitch = Random.Range(0.65f, 0.7f);

            if (this.user.characterId == 1)
                this.ragingBeastPunchSfx.pitch = Random.Range(0.65f, 0.7f);
            else
                this.ragingBeastPunchSfx.pitch = Random.Range(0.55f, 0.6f);

            //this.ragingBeastPunchSfx.pitch = Random.Range(0.7f, 0.9f);


            //this.punchSfx.pitch = Random.Range(0.85f, 0.95f);
            //this.punchSfx.pitch = 0.9f;
            this.ragingBeastPunchSfx.Play();
        }
    }

    private float RandomXY(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }
}
