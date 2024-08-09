using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMikeTaunts : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject electricGuitar;

    public RoseProjectile roseProjectile;

    public ParticleSystem guitarElectricity;
    public TestHitbox guitarElectricityHitbox;

    public GameObject glowingEyes;

    public ParticleSystem clubDanceElectricity;

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.user != null)
        {
            this.user.OnAttack += this.Cancel;
        }

        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.user != null)
        {
            this.user.OnAttack -= this.Cancel;
        }

        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;
    }
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
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                if (this.user.input.moveInput.y < 0f) //Down
                {
                    this.StartCoroutine(this.DownTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z > 0f) //Forward
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                    //this.StartCoroutine(this.ForwardTauntCoroutine());
                }
                else if (this.user.input.moveInput.x * this.user.transform.forward.z < 0f) //Backward
                {
                    this.StartCoroutine(this.ClubDanceCoroutine());
                }
                else //Neutral
                {
                    this.StartCoroutine(this.NeutralTauntCoroutine());
                    //this.StartCoroutine(this.ClubDanceCoroutine());
                }
            }
        }
    }

    public void Cancel()
    {
        if (!this.user.dead && this.onGoing)
        {
            //this.user.AddStun(0.1f, true);
            if (this.animations != null)
                this.animations.SetDefaultPose();
            //Debug.Log("Cancel");
            this.Stop();

        }
    }

    public override void Stop()
    {
        base.Stop();
        //this.user.rb.isKinematic = false;

        //DO SOMETINH WITH THIS LATER

        if (this.animations != null && this.animations.neutralEyes != null && this.animations.happyEyes != null)
        {
            this.animations.happyEyes.gameObject.SetActive(false);
            this.animations.neutralEyes.gameObject.SetActive(true);

            if (this.animations.angryEyes != null)
                this.animations.angryEyes.gameObject.SetActive(false);
        }

        /*if (this.animations != null)
            this.animations.SetEyes(2);*/

        if (this.guitarElectricity != null)
            this.guitarElectricity.Stop();

        if (this.guitarElectricityHitbox != null)
            this.guitarElectricityHitbox.gameObject.SetActive(false);

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.clubDanceElectricity != null)
            this.clubDanceElectricity.Stop();

        /*if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);
    }

    private IEnumerator NeutralTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.BallerWavePose(10);

        /*float currentTime = 0;
        float duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        int amount = 15;
        int waveId = 1;
        bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.05f);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (this.animations != null)
            {
                this.animations.BallerWavePose(waveId);
            }
            if (idForward)
                waveId += 1;
            else if (!idForward)
                waveId -= 1;

            if (waveId == 0)
                idForward = true;
            else if (waveId == 2)
                idForward = false;



            amount -= 1;

            if (amount <= 0 && this.user.input.taunting)
                amount = 1;

            //Debug.Log(amount);
        }

        /*if (this.animations != null)
            this.animations.BallerWavePose(1);

        currentTime = 0;
        //duration = 0.25f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.BallerWavePose(0);
        currentTime = 0;
        //duration = 0.25f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.BallerWavePose(2);
        currentTime = 0;
        //duration = 0.25f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator DownTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.ElectricGuitar(0);

        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(true);

        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }*/

        if (this.guitarElectricity != null)
            this.guitarElectricity.Play();

        if (this.guitarElectricityHitbox != null)
            this.guitarElectricityHitbox.gameObject.SetActive(true);

        int amount = 16;
        int animId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            //currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.05f);
            if (this.animations != null)
                this.animations.ElectricGuitar(animId);
            if (animId == 0)
                animId = 1;
            else
                animId = 0;

            amount -= 1;

            if (amount <= 0 && this.user.input.taunting)
                amount = 1;

            yield return null;
        }

        if (this.guitarElectricity != null)
            this.guitarElectricity.Stop();

        if (this.guitarElectricityHitbox != null)
            this.guitarElectricityHitbox.gameObject.SetActive(false);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.electricGuitar != null)
            this.electricGuitar.SetActive(false);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ForwardTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        /*if (this.animations != null)
            this.animations.TestPose3();*/

        if (this.animations != null)
            this.animations.RoseThrow();


        if (this.roseProjectile != null)
        {
            RoseProjectile rosePrefab = this.roseProjectile;

            rosePrefab.startPos = new Vector3(this.user.transform.position.x + this.transform.forward.z * 1f, this.user.transform.position.y + 2.75f, 0f);
            if (this.user.tempOpponent != null)
            {
                rosePrefab.endPos = new Vector3(this.user.tempOpponent.transform.position.x, this.user.tempOpponent.transform.position.y + 1.5f, 0f);
                //rosePrefab.endPos = new Vector3(this.user.tempOpponent.transform.position.x /*+ this.transform.forward.z * 1f*/, 0f, 0f);
            }


            rosePrefab = Instantiate(rosePrefab, rosePrefab.startPos, this.user.transform.rotation);
        }

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator ClubDanceCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        this.user.AddStun(0.2f, true);
        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        if (this.clubDanceElectricity != null)
            this.clubDanceElectricity.Play();

        float speedMultiplier = 2f;

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f);
        }
        //spin
        yield return new WaitForSeconds(0.2f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);

        /*this.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
        this.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, 25f);*/
        //this.animations.rightLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

        yield return new WaitForSeconds(0.16f / speedMultiplier);
        //this.animations.SetDefaultPose();

        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, 0f);

        /*this.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, -10f);
        this.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, 25f);*/
        //this.animations.leftLegJoint.localEulerAngles = new Vector3(0f, 0f, -15f);

        yield return new WaitForSeconds(0.16f / speedMultiplier);
        //this.animations.SetDefaultPose();

        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 180f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 225f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * -45f, 0f);
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 225f, 0f);
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * -90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * -45f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);
        yield return new WaitForSeconds(0.04f / speedMultiplier);
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.16f / speedMultiplier);


        this.animations.ClubDance(1, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(2, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(3, 1);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(4, 2);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(5, 2);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(6, 1);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(7, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(8, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);

        this.animations.ClubDance(1, 1);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(2, 2);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(3, 2);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(4, 1);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(5, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(6, 0);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(7, 1);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDance(8, 2);
        yield return new WaitForSeconds(0.08f / speedMultiplier);


        /*this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f);
        yield return new WaitForSeconds(0.12f);*/

        //sit
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.16f / speedMultiplier);


        //wave
        /*this.animations.ClubDanceWave(0, true);
        yield return new WaitForSeconds(0.04f);
        this.animations.ClubDanceWave(1, true);
        yield return new WaitForSeconds(0.1f);*/
        this.animations.ClubDanceWave(2, true);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(3, true);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(2, true);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(3, true);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        /*this.animations.ClubDanceWave(2, true);
        yield return new WaitForSeconds(0.1f);
        this.animations.ClubDanceWave(1, true);
        yield return new WaitForSeconds(0.08f);*/


        //sit again
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.16f / speedMultiplier);

        //wave again
        /*this.animations.ClubDanceWave(0, false);
        yield return new WaitForSeconds(0.04f);
        this.animations.ClubDanceWave(1, false);
        yield return new WaitForSeconds(0.1f);*/
        this.animations.ClubDanceWave(2, false);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(3, false);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(2, false);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        this.animations.ClubDanceWave(3, false);
        yield return new WaitForSeconds(0.1f / speedMultiplier);
        /*this.animations.ClubDanceWave(2, false);
        yield return new WaitForSeconds(0.1f);
        this.animations.ClubDanceWave(1, false);
        yield return new WaitForSeconds(0.08f);*/

        //sit again
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);

        //bow
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(0, true);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(1, true);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(0, true);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        /*this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.1f);*/

        //sit again
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);
        this.animations.Sit();
        yield return new WaitForSeconds(0.16f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.12f / speedMultiplier);

        //bow
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, 0f);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(0, false);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(1, false);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.ClubDanceBow(0, false);
        //this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, this.animations.body.transform.localEulerAngles.z);
        yield return new WaitForSeconds(0.08f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 135f, 0f);
        yield return new WaitForSeconds(0.08f / speedMultiplier);

        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.01f / speedMultiplier);


        if (this.user.input.taunting)
        {
            this.StopAllCoroutines();
            this.user.attackStuns.Remove(this.gameObject);
            this.StartCoroutine(this.ClubDanceCoroutine());
        }
        /*this.StopAllCoroutines();
        this.user.attackStuns.Remove(this.gameObject);
        this.StartCoroutine(this.ClubDanceCoroutine());*/
        //Repeat here

        /*this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 90f, 0f);
        yield return new WaitForSeconds(0.1f);*/

        yield return new WaitForSeconds(0.05f / speedMultiplier);
        this.animations.SetDefaultPose();
        this.animations.body.transform.localEulerAngles = new Vector3(0f, this.transform.forward.z * 45f, 0f);
        yield return new WaitForSeconds(0.05f / speedMultiplier);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.clubDanceElectricity != null)
            this.clubDanceElectricity.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator BackTauntCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.TestPose2();

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }



    private IEnumerator NeutralTaunt2Coroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        //this.user.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.HappyJumping();*/

        if (this.animations != null)
            this.animations.HappyJumping(0);

        int amount = 25;
        int animStageId = 0;
        //bool idForward = true;
        while (amount > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.1f);
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            if (this.animations != null)
            {
                this.animations.HappyJumping(animStageId);
            }


            animStageId += 1;
            amount -= 1;
            //Debug.Log(amount);
        }


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //this.user.rb.isKinematic = false;
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}