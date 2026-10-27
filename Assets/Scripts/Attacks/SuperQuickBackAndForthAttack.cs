using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperQuickBackAndForthAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;
    public TestHitbox hitboxForward;
    public TestHitbox hitboxBack;
    public TestHitbox hitboxFinal;
    public AudioSource sfx;

    public TempPlayerAnimations trailCopy;

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

        if (this.hitboxForward != null)
            this.hitboxForward.OnPlayerCollision += this.SpawnHitEffect;

        if (this.hitboxBack != null)
            this.hitboxBack.OnPlayerCollision += this.SpawnHitEffect;

        /*if (this.hitboxFinal != null)
            this.hitboxFinal.OnPlayerCollision += this.SpawnHitEffect;*/
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitboxForward != null)
            this.hitboxForward.OnPlayerCollision -= this.SpawnHitEffect;

        if (this.hitboxBack != null)
            this.hitboxBack.OnPlayerCollision -= this.SpawnHitEffect;
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
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);

                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TemplateCoroutine());


                }
            }
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1.45f, -0.7f), Quaternion.Euler(0, 0, 0));
        }

        this.SetStartPose();
        yield return new WaitForSeconds(0.3f);

        int amount3 = 3;
        for (int i = 0; i < amount3; i++)
        {
            this.animations.body.localScale = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(0.0001f);
            this.animations.body.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);
            yield return new WaitForSeconds(0.0001f);
        }

        this.SetDashingPose();

        this.user.rb.isKinematic = true;
        this.user.EnableCollision(false);



        /*this.animations.body.localPosition = new Vector3(1.75f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
        yield return new WaitForSeconds(0f);
        this.animations.body.localPosition = new Vector3(3.5f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);*/




        if (this.hitboxForward != null)
            this.hitboxForward.gameObject.SetActive(true);

        if (this.trailCopy != null)
            this.trailCopy.gameObject.SetActive(true);

        if (this.sfx != null)
            this.sfx.Play();

        int amount2 = 5;
        for (int j = 0; j < amount2; j++)
        {
            int amount = 8;
            for (int i = 0; i < amount; i++)
            {

                this.BackAndForth(i, j);


                yield return new WaitForSeconds(0.0001f);
            }
        }
        /*if (this.sfx != null)
            this.sfx.Stop();*/

        if (this.sfx != null)
            this.sfx.loop = false;

        if (this.trailCopy != null)
            this.trailCopy.gameObject.SetActive(false);

        if (this.hitboxForward != null)
            this.hitboxForward.gameObject.SetActive(false);

        if (this.hitboxBack != null)
            this.hitboxBack.gameObject.SetActive(false);

        if (this.hitboxFinal != null)
            this.hitboxFinal.gameObject.SetActive(false);

        this.user.rb.isKinematic = false;
        this.user.EnableCollision(true);

        this.SetEndPose();

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.5f);

        if (this.sfx != null)
        {
            this.sfx.Stop();
            this.sfx.loop = true;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;
        this.user.EnableCollision(true);

        if (this.hitboxForward != null)
            this.hitboxForward.gameObject.SetActive(false);

        if (this.hitboxBack != null)
            this.hitboxBack.gameObject.SetActive(false);

        if (this.hitboxFinal != null)
            this.hitboxFinal.gameObject.SetActive(false);

        this.animations.body.localScale = new Vector3(1f, 1f, this.user.transform.forward.z);

        if (this.trailCopy != null)
            this.trailCopy.gameObject.SetActive(false);

        if (this.sfx != null)
        {
            this.sfx.Stop();
            this.sfx.loop = true;
            
        }
            

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void SpawnHitEffect(TestPlayer player)
    {
        if (player != null && player.hitEffectLogic != null)
        {
            int charId = player.characterId;


            float xPos = player.transform.position.x + Random.Range(-0.5f, 0.5f);
            float yPos = player.transform.position.y + Random.Range(1.1f, 2.75f);
            //float yPos = player.transform.position.y + Random.Range(1.3f, 1.8f);
            //float yPos = player.transform.position.y + Random.Range(0.2f, 1.95f);

            if (charId == 3 || charId == 4 || charId == 7)
            {
                yPos = player.transform.position.y + Random.Range(1.2f, 2.1f);
            }


            if (yPos > 2.75f)
                yPos = 2.75f;

            player.hitEffectLogic.SpawnHitEffect(new Vector3(xPos, yPos, -0.5f));
        }
    }

    public void BackAndForth(int animId = 0, int amountId = 0)
    {
        float distance = 3.5f;

        if (animId == 0)
        {
            this.animations.body.localPosition = new Vector3(distance * 0.5f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
        }
        else if (animId == 1)
        {
            this.animations.body.localPosition = new Vector3(distance, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 90f, this.animations.body.localEulerAngles.z);
                
        }
        else if (animId == 2)
        {

            if (amountId == 4)
            {
                if (this.hitboxForward != null)
                    this.hitboxForward.gameObject.SetActive(false);

                if (this.hitboxBack != null)
                    this.hitboxBack.gameObject.SetActive(false);

                if (this.hitboxFinal != null)
                    this.hitboxFinal.gameObject.SetActive(true);
            }
            else
            {
                if (this.hitboxForward != null)
                    this.hitboxForward.gameObject.SetActive(false);

                if (this.hitboxBack != null)
                    this.hitboxBack.gameObject.SetActive(true);
            }

            /*if (this.hitboxForward != null)
                this.hitboxForward.gameObject.SetActive(false);

            if (this.hitboxBack != null)
                this.hitboxBack.gameObject.SetActive(true);*/

            this.animations.body.localPosition = new Vector3(distance * 0.5f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 180f, this.animations.body.localEulerAngles.z);
        }
        else if (animId == 3)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
        }
        else if (animId == 4)
        {
            

            this.animations.body.localPosition = new Vector3(-(distance * 0.5f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
        }
        else if (animId == 5)
        {
            this.animations.body.localPosition = new Vector3(-(distance), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 90f, this.animations.body.localEulerAngles.z);

            if (amountId == 4 && this.sfx != null)
                this.sfx.loop = false;
        }
        else if (animId == 6)
        {
            if (amountId == 4)
            {
                if (this.hitboxForward != null)
                    this.hitboxForward.gameObject.SetActive(false);

                if (this.hitboxBack != null)
                    this.hitboxBack.gameObject.SetActive(false);

                /*if (this.hitboxFinal != null)
                    this.hitboxFinal.gameObject.SetActive(true);*/

                /*if (this.sfx != null)
                    this.sfx.loop = false;*/
            }
            else
            {
                if (this.hitboxForward != null)
                    this.hitboxForward.gameObject.SetActive(true);

                if (this.hitboxBack != null)
                    this.hitboxBack.gameObject.SetActive(false);
            }

            /*if (this.hitboxForward != null)
                this.hitboxForward.gameObject.SetActive(true);

            if (this.hitboxBack != null)
                this.hitboxBack.gameObject.SetActive(false);*/


            this.animations.body.localPosition = new Vector3(-(distance * 0.5f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            this.animations.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, 0f, this.animations.body.localEulerAngles.z);
        }
        else if (animId == 7)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
        }


        if(this.trailCopy != null)
        {
            if (animId == 0)
            {
                this.trailCopy.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            }
            else if (animId == 1)
            {
                this.trailCopy.body.localPosition = new Vector3(distance * 0.5f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                this.trailCopy.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, 0f, this.animations.body.localEulerAngles.z);
            }
            else if (animId == 2)
            {
                this.trailCopy.body.localPosition = new Vector3(distance, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                this.trailCopy.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 90f, this.animations.body.localEulerAngles.z);
            }
            else if (animId == 3)
            {
                this.trailCopy.body.localPosition = new Vector3(distance * 0.5f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                this.trailCopy.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 180f, this.animations.body.localEulerAngles.z);
            }
            else if (animId == 4)
            {
                this.trailCopy.body.localPosition = new Vector3(0f, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            }
            else if (animId == 5)
            {
                this.trailCopy.body.localPosition = new Vector3(-(distance * 0.5f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
            }
            else if (animId == 6)
            {
                this.trailCopy.body.localPosition = new Vector3(-distance, this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                this.trailCopy.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, this.transform.forward.z * 90f, this.animations.body.localEulerAngles.z);
            }
            else if (animId == 7)
            {
                this.trailCopy.body.localPosition = new Vector3(-(distance * 0.5f), this.animations.body.localPosition.y, this.animations.body.localPosition.z);
                this.trailCopy.body.localEulerAngles = new Vector3(this.animations.body.localEulerAngles.x, 0f, this.animations.body.localEulerAngles.z);
            }

            this.trailCopy.body.localScale = this.animations.body.localScale;
        }
            
    }

    public void SetDashingPose()
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(0f, -30f, -60f),
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 30f, -60f),
                new Vector3(0f, 0f, 0f));

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 50f),
                new Vector3(0f, 0f, -85f),
                new Vector3(0f, 0f, -30f),
                new Vector3(0f, 0f, -15f));

            this.animations.CustomBodyPose(
                new Vector3(0f, -0.25f, 0f),
                new Vector3(0f, 0f, -30f));

            this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 10f);
        }
    }

    public void SetStartPose()
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(0f, 15f, 65f), //Right Arm
                new Vector3(0f, 0f, 0f), //Right Arm Joint
                new Vector3(0f, 30f, -40f), //Left Arm
                new Vector3(0f, 0f, 0f)); //Left Arm Joint

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 75f), //Right Leg
                new Vector3(0f, 0f, -100f), //Right Leg Joint
                new Vector3(0f, 0f, 25f), //Left Leg
                new Vector3(0f, 0f, -42f)); //Left Leg Joint

            this.animations.CustomBodyPose(
                new Vector3(0f, -0.85f, 0f), //Position
                new Vector3(0f, 0f, -60f)); //Rotation


            this.animations.upperBody.localEulerAngles = new Vector3(0f, -10f, 0f);

            this.animations.eyes.localEulerAngles = new Vector3(0f, 10f, 15f);

            if (this.animations.eyesPosTransform != null)
                this.animations.eyesPosTransform.localPosition = new Vector3(0f, 0.03f, 0f);
        }
    }

    public void SetEndPose()
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(20f, 0f, -20f),
                new Vector3(0f, 0f, 0f),
                new Vector3(-20f, 0f, -20f),
                new Vector3(0f, 0f, 0f));

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 30f),
                new Vector3(0f, 0f, -65f),
                new Vector3(0f, 0f, -15f),
                new Vector3(0f, 0f, -15f));

            this.animations.CustomBodyPose(
                new Vector3(0f, -0.15f, 0f),
                new Vector3(0f, 0f, -15f));

            this.animations.eyes.localEulerAngles = new Vector3(0f, 0f, 5f);
        }
    }
}
