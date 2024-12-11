using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboJCapStartAnimation : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public float fallDuration = 0.8f;
    //public VisualEffect fire;
    //public GameObject landingParticle;
    public GameObject propeller;
    public GameObject propellerBlade;

    [Space]
    public GameObject teleportEffect;
    public GameObject beam1, beam2, beam3, sphere1, sphere2, sphere3, sphere4, circleParticle;


    //public Attack vsDarkJCap;

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

    /*public override void OnDeath()
    {
        if (this.onGoing)
            base.OnDeath();
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            int number = Random.Range(0, 2);
            //int number = 1;
            if (number == 0)
                this.StartCoroutine(this.TemplateCoroutine());
            else
                this.StartCoroutine(this.TeleportCoroutine());


            /*if (this.vsDarkJCap != null && this.user.characterId == 0 && this.user.tempOpponent != null && this.user.tempOpponent.characterId == 1 && this.user.tempBall == null)
            {
                this.vsDarkJCap.Initiate();
            }
            else
            {
                this.StartCoroutine(this.TemplateCoroutine());
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 7f, 0f);
        //this.PlayFire(true);
        this.user.LookAtTarget();

        if (this.propeller != null)
            this.propeller.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.01f);
        /*if (this.animations != null)
            this.animations.DarkJCapStartAnimation();*/

        float currentTime = 0;
        float duration = this.fallDuration;
        //float targetVolume = 0.1f;
        float targetPosition = 0f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);

            if (this.propeller != null)
                this.propeller.transform.Rotate(0f, 1450f * Time.deltaTime, 0f);

            yield return null;
        }

        /*if (this.landingParticle != null)
        {
            GameObject landingParticlePrefab = this.landingParticle;
            landingParticlePrefab = Instantiate(landingParticlePrefab, new Vector3(this.user.transform.position.x, 0.01f, 0f), Quaternion.Euler(0, 0, 0));
        }*/


        //this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);
        /*if (this.propeller == null)
        {
            int number = Random.Range(1, 1001);
            //Debug.Log(number);
            if (number == 1)
            {
                this.user.ragdoll.transform.localScale = new Vector3(1f, 1f, 1f);
                this.user.Suicide();
            }

        }*/



        currentTime = 0;
        duration = (1f - this.fallDuration) / 2;
        //duration = 1f - this.fallDuration;
        float targetSize = 0.2f;
        start = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.propeller != null && this.propellerBlade != null)
                this.propellerBlade.transform.localScale = new Vector3(0.2f, 0.1f, Mathf.Lerp(start, targetSize, currentTime / duration));

            yield return null;
        }

        currentTime = 0;
        duration = (1f - this.fallDuration) / 2;
        //duration = 1f - this.fallDuration;
        targetPosition = 0.5f;
        start = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.propeller != null && this.propellerBlade != null)
                this.propeller.transform.localPosition = new Vector3(0, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);

            yield return null;
        }




        //yield return new WaitForSeconds(1f - this.fallDuration);

        if (this.propeller != null)
        {
            this.propeller.SetActive(false);
            this.propeller.transform.localEulerAngles = Vector3.zero;
            this.propeller.transform.localPosition = new Vector3(0f, 1f, 0f);

            if (this.propellerBlade != null)
                this.propellerBlade.transform.localScale = new Vector3(0.2f, 0.1f, 1f);
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.PlayFire(false);

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
        //this.PlayFire(false);

        if (this.teleportEffect != null)
            this.teleportEffect.SetActive(false);

        this.TeleportEffect(0);

        if (this.propeller != null)
        {
            this.propeller.SetActive(false);
            this.propeller.transform.localEulerAngles = Vector3.zero;
            this.propeller.transform.localPosition = new Vector3(0f, 1f, 0f);

            if (this.propellerBlade != null)
                this.propellerBlade.transform.localScale = new Vector3(0.2f, 0.1f, 1f);
        }
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    private IEnumerator TeleportCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        this.user.LookAtTarget();

        /*if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);*/

        this.user.transform.position = new Vector3(this.user.transform.position.x, 50f, 0f);

        if (this.teleportEffect != null)
        {
            this.teleportEffect.transform.position = new Vector3(this.transform.position.x, 0f, 0f);
            this.teleportEffect.SetActive(true);
        }
            


        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.RobotTeleportStartAnim();

        this.user.transform.position = new Vector3(this.user.transform.position.x, 0f, 0f);

        if (this.teleportEffect != null)
        {
            this.teleportEffect.transform.localPosition = new Vector3(0f, 0f, 0f);
            //this.teleportEffect.SetActive(true);
        }


        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);

        /*if (this.animations != null)
            this.animations.ViolentBalletStartAnim(0);*/

        yield return new WaitForSeconds(0.1f);

        this.TeleportEffect(1);

        yield return new WaitForSeconds(0.1f);

        this.TeleportEffect(5);


        yield return new WaitForSeconds(0.05f);

        this.TeleportEffect(2);


        yield return new WaitForSeconds(0.1f);

        this.TeleportEffect(3);

        yield return new WaitForSeconds(0.05f);

        this.TeleportEffect(2);

        yield return new WaitForSeconds(0.1f);

        this.TeleportEffect(4);

        //yield return new WaitForSeconds(0.45f);

        yield return new WaitForSeconds(0.1f);
        /*yield return new WaitForSeconds(0.05f);
        this.TeleportEffect(7);
        yield return new WaitForSeconds(0.05f);*/


        /*if (this.teleportEffect != null)
            this.teleportEffect.SetActive(false);

        this.TeleportEffect(0);*/
        this.TeleportEffect(6);


        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        if (this.teleportEffect != null)
            this.teleportEffect.SetActive(false);

        this.TeleportEffect(0);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.05f);

        //yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);


        /*if (this.animations != null)
            this.animations.ViolentBalletStartAnim(1);*/

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.05f);


        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);

        this.user.EntranceDone();
    }

    public void TeleportEffect(int id)
    {
        if(this.teleportEffect != null)
        {
            if (id == 1)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(true);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);
            }
            else if (id == 2)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(true);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);
            }
            else if (id == 3)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(true);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);
            }
            else if (id == 4)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(true);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);
            }
            else if (id == 5)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(true);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(true);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);

                if (this.circleParticle != null)
                    this.circleParticle.SetActive(true);
            }
            else if (id == 6)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(true);

                if (this.sphere4 != null)
                    this.sphere4.transform.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);
            }
            else if (id == 7)
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(false);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(true);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(true);

                if (this.sphere4 != null)
                    this.sphere4.transform.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);
            }
            else
            {
                if (this.beam1 != null)
                    this.beam1.SetActive(true);

                if (this.beam2 != null)
                    this.beam2.SetActive(false);

                if (this.beam3 != null)
                    this.beam3.SetActive(false);

                if (this.sphere1 != null)
                    this.sphere1.SetActive(false);

                if (this.sphere2 != null)
                    this.sphere2.SetActive(false);

                if (this.sphere3 != null)
                    this.sphere3.SetActive(false);

                if (this.sphere4 != null)
                    this.sphere4.SetActive(false);

                if (this.circleParticle != null)
                    this.circleParticle.SetActive(false);
            }

        }
    }


    /*public void PlayFire(bool playing)
    {
        if (this.fire != null)
        {
            if (playing)
                this.fire.Play();
            else
                this.fire.Stop();
        }
    }*/
}
