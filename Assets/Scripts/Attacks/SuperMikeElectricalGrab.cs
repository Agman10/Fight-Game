using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMikeElectricalGrab : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public GameObject startParticle;

    public GameObject glowingEyes;

    public TestHitbox hitbox;
    public TestPlayer grabbedPlayer;

    public AudioSource electricitySfx;

    public AudioSource kickSfx;

    public ParticleSystem electricStart;
    public ParticleSystem electricGrabNormal;
    public ParticleSystem electricGrabShort;
    public ParticleSystem electricMidGrabbing;

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

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.Grab;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.Grab;
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
                if (this.user.superCharge >= this.user.maxSuperCharge)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);

                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryGrabCoroutine());
                }
            }

            
        }
    }

    private IEnumerator TryGrabCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(true);

        this.SetStartPose();

        if (this.electricStart != null)
            this.electricStart.Play();

        yield return new WaitForSeconds(0.3f);

        /*if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);*/

        if (this.electricStart != null)
            this.electricStart.Stop();

        this.SetDashingPose();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.electricMidGrabbing != null)
            this.electricMidGrabbing.Play();

        float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration && this.grabbedPlayer == null)
        {
            currentTime += Time.deltaTime;

            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 16f, 0f, 0f);

            yield return null;
        }

        /*if (this.electricMidGrabbing != null)
            this.electricMidGrabbing.Stop();*/

        if (this.grabbedPlayer == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            if (this.electricMidGrabbing != null)
                this.electricMidGrabbing.Stop();

            //yield return new WaitForSeconds(0.1f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            if (this.glowingEyes != null)
                this.glowingEyes.gameObject.SetActive(false);


            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    private IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        yield return new WaitForSeconds(0.05f);
        if (this.electricMidGrabbing != null)
            this.electricMidGrabbing.Stop();

        int charId = player.characterId;
        float yPos = 2.4f;
        float xPos = 0.05f;
        if (charId == 3 || charId == 4 || charId == 7)
        {
            yPos = 1.7f;
            xPos = 0.1f;
        }

        this.SetGrabbingPose(xPos);

        this.user.transform.position = new Vector3(player.transform.position.x, yPos, 0f);

        this.PlayElectricGrabEffect(true, player.characterId);
        if (this.electricitySfx != null)
            this.electricitySfx.Play();


        int amount = 15;
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.1f);
            player.TakeDamage(this.user.transform.position, 3f);
            if (player.soundEffects != null)
                player.soundEffects.PlayHitSound();

            if (player.skelletonBody != null)
                player.skelletonBody.EnableAndDisableSkelleton();

            amount -= 1;
            yield return null;
        }

        //yield return new WaitForSeconds(1);

        this.PlayElectricGrabEffect(false, player.characterId);

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();

        charId = player.characterId;
        yPos = 1f;
        xPos = 1.3f;
        float armYRot = -10f;
        if (charId == 3 || charId == 4 || charId == 7)
        {
            yPos = 0.65f;
            xPos = 1.25f;
            armYRot = 0f;
        }

        this.SetGrabbingMidPose(armYRot);
        this.user.transform.position = new Vector3(player.transform.position.x - (this.transform.forward.z * xPos), yPos, 0f);

        yield return new WaitForSeconds(0.05f);

        this.user.transform.position = new Vector3(player.transform.position.x - (this.user.transform.forward.z * 1.5f), 0f, 0f);
        //this.user.transform.position = new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1.5f), 0f, 0f);

        //this.animations.SetDefaultPose();

        if (this.kickSfx != null)
        {
            this.kickSfx.time = 0.02f;
            this.kickSfx.Play();
        }

        this.BackflipKickAnim(0);
        yield return new WaitForSeconds(0.1f);
        this.BackflipKickAnim(1);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(2);
        this.StopGrab(player);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(3);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(4);
        yield return new WaitForSeconds(0.001f);
        this.BackflipKickAnim(5);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(6);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(7);
        yield return new WaitForSeconds(0.025f);
        this.BackflipKickAnim(8);
        yield return new WaitForSeconds(0.05f);
        this.animations.SetDefaultPose();

        //this.StopGrab(player);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead)
        {

        }

        if (!player.countering)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.grabbedPlayer = player;
            player.OnHit?.Invoke();
            player.lookAtPlayer();
            player.preventDeath = true;
            player.attackStuns.Add(this.gameObject);

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;


            /*int charId = player.characterId;
            float yPos = 2.4f;
            float xPos = 0.05f;
            if (charId == 3 || charId == 4 || charId == 7)
            {
                yPos = 1.7f;
                xPos = 0.1f;
            }

            this.SetGrabbingPose(xPos);*/

            int charId = player.characterId;
            float yPos = 1f;
            float xPos = 1.3f;
            float armYRot = -10f;
            if (charId == 3 || charId == 4 || charId == 7)
            {
                yPos = 0.65f;
                xPos = 1.25f;
                armYRot = 0f;
            }

            this.SetGrabbingMidPose(armYRot);
            player.animations.SetDefaultPose();
            player.animations.KnifePunishmentHit();

            

            this.user.transform.position = new Vector3(player.transform.position.x - (this.transform.forward.z * xPos), yPos, 0f);
            //this.user.transform.position = new Vector3(player.transform.position.x, yPos, 0f);
            player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);

            this.StartCoroutine(this.GrabbingCoroutine(player));
        }
        else
        {
            player.OnHitFromPlayer?.Invoke(this.user);
        }
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            player.preventDeath = false;

            if (!player.dead)
            {
                player.rb.isKinematic = false;

                //player.animations.SetDefaultPose();

                //player.TakeDamage(this.user.transform.position, 0f, 1f, this.user.transform.forward.z * 1000f, 1000f, true, true, false, false, true, false, true, true);
                player.TakeDamage(this.user.transform.position, 5f, 0.5f, this.user.transform.forward.z * 500f, 1000f, true, true, false, false, true, false, true, true);
                if (player.soundEffects != null)
                    player.soundEffects.PlayHitSound();

                if (player.hitEffectLogic != null)
                    player.hitEffectLogic.SpawnHitEffect(new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.5f), 1.6f, -0.375f));
            }
            this.user.rb.isKinematic = false;

            this.grabbedPlayer = null;
        }
    }





    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.knockbackInvounrability = false;
        this.user.rb.isKinematic = false;

        if (this.glowingEyes != null)
            this.glowingEyes.gameObject.SetActive(false);

        if (this.electricStart != null)
            this.electricStart.Stop();

        if (this.electricGrabNormal != null)
            this.electricGrabNormal.Stop();

        if (this.electricGrabShort != null)
            this.electricGrabShort.Stop();

        if (this.electricMidGrabbing != null)
            this.electricMidGrabbing.Stop();

        if (this.electricitySfx != null)
            this.electricitySfx.Stop();


        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.rb.isKinematic = false;
            this.grabbedPlayer.knockbackInvounrability = false;

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer.preventDeath = false;

            this.grabbedPlayer.animations.SetDefaultPose();

            if (this.grabbedPlayer.health > 0f)
                this.grabbedPlayer.animations.SetDefaultPose();

            if (this.onGoing && this.grabbedPlayer.health <= 0f)
            {
                this.grabbedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, false, false, false, true, false, true);
            }

            this.grabbedPlayer = null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlayElectricGrabEffect(bool play = true, int charId = 0)
    {
        if (play == true)
        {
            if (charId == 3 || charId == 4 || charId == 7)
            {
                if (this.electricGrabShort != null)
                    this.electricGrabShort.Play();
            }
            else
            {
                if (this.electricGrabNormal != null)
                    this.electricGrabNormal.Play();
            }
        }
        else
        {
            if (this.electricGrabNormal != null)
                this.electricGrabNormal.Stop();

            if (this.electricGrabShort != null)
                this.electricGrabShort.Stop();
        }
    }

    public void SetStartPose()
    {
        if(this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(20f, 10f, -10f),
                new Vector3(0f, 0f, 90f),
                new Vector3(-20f, -10f, -10f),
                new Vector3(0f, 0f, 90f));

            this.animations.CustomLegsPose(
                new Vector3(5f, 0f, 0f),
                new Vector3(0f, 0f, 0f),
                new Vector3(-5f, 0f, 0f),
                new Vector3(0f, 0f, 0f));
        }
    }

    public void SetDashingPose()
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(0f, 0f, 100f),
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 0f, 100f),
                new Vector3(0f, 0f, 0f));

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 30f),
                new Vector3(0f, 0f, -80f),
                new Vector3(0f, 0f, -40f),
                new Vector3(0f, 0f, -20f));

            this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 0f, -10f));
        }
    }

    public void SetGrabbingPose(float xPos = 0.05f)
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(185f, 0f, -10f), //Right Arm
                new Vector3(5f, 0f, 0f), //Right Arm Joint
                new Vector3(-185f, 0f, -10f), //Left Arm
                new Vector3(-5f, 0f, 0f)); //Left Arm Joint

            this.animations.CustomLegsPose(
                new Vector3(-2f, 0f, 10f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(2f, 0f, 10f), //Left Leg
                new Vector3(0f, 0f, 0f)); //Left Leg Joint

            this.animations.CustomBodyPose(
                new Vector3(xPos, 0f, 0f), //Position
                new Vector3(0f, -72f, -189f)); //Rotation

            //xpos = 0.1f
        }
    }

    public void SetGrabbingMidPose(float armYRot)
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(0f, armYRot, 90f),
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 0f, 90f),
                new Vector3(0f, 0f, 0f));

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 10f),
                new Vector3(0f, 0f, -40f),
                new Vector3(0f, 0f, -30f),
                new Vector3(0f, 0f, -20f));

            this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 0f, -35f));

            //xpos = 0.1f
        }
    }

    public void PoseTemplate()
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            this.animations.CustomArmsPose(
                new Vector3(0f, 0f, 0f), //Right Arm
                new Vector3(0f, 0f, 0f), //Right Arm Joint
                new Vector3(0f, 0f, 0f), //Left Arm
                new Vector3(0f, 0f, 0f)); //Left Arm Joint

            this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 0f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(0f, 0f, 0f), //Left Leg
                new Vector3(0f, 0f, 0f)); //Left Leg Joint

            this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f), //Position
                new Vector3(0f, 0f, 0f)); //Rotation
        }
    }

    public void BackflipKickAnim(int animId = 0)
    {
        if (this.animations != null)
        {
            this.animations.SetDefaultPose();

            if (animId == 0)
            {
                this.animations.CustomArmsPose(
                new Vector3(20f, 0f, 0f), //Right Arm
                new Vector3(0f, 0f, 30f), //Right Arm Joint
                new Vector3(-20f, 0f, 0f), //Left Arm
                new Vector3(0f, 0f, 30f)); //Left Arm Joint
            }
            else if (animId == 1 || animId == 2 || animId == 3 || animId == 4 || animId == 5 || animId == 6 || animId == 7)
            {
                this.animations.CustomArmsPose(
                new Vector3(60f, 0f, 0f), //Right Arm
                new Vector3(0f, 0f, 20f), //Right Arm Joint
                new Vector3(-60f, 0f, 0f), //Left Arm
                new Vector3(0f, 0f, 20f)); //Left Arm Joint
            }
            else if (animId == 8)
            {
                this.animations.CustomArmsPose(
                new Vector3(30f, 0f, 0f), //Right Arm
                new Vector3(0f, 0f, 20f), //Right Arm Joint
                new Vector3(-30f, 0f, 0f), //Left Arm
                new Vector3(0f, 0f, 20f)); //Left Arm Joint
            }





            if (animId == 0)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 17f), //Right Leg
                new Vector3(0f, 0f, -44f), //Right Leg Joint
                new Vector3(0f, 0f, 42f), //Left Leg
                new Vector3(0f, 0f, -45f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, -0.1f, 0f), //Position
                new Vector3(0f, 0f, -10f)); //Rotation
            }
            else if (animId == 1)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 0f), //Right Leg
                new Vector3(0f, 0f, -50f), //Right Leg Joint
                new Vector3(0f, 0f, -40f), //Left Leg
                new Vector3(0f, 0f, -20f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f), //Position
                new Vector3(0f, 0f, 50f)); //Rotation
            }
            else if (animId == 2)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 0f), //Right Leg
                new Vector3(0f, 0f, -5f), //Right Leg Joint
                new Vector3(0f, 0f, -60f), //Left Leg
                new Vector3(0f, 0f, -60f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0.02f, 0f), //Position
                new Vector3(0f, 0f, 90f)); //Rotation
            }
            else if (animId == 3)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 20f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(0f, 0f, -40f), //Left Leg
                new Vector3(0f, 0f, -89f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0.04f, 0f), //Position
                new Vector3(0f, 0f, 115f)); //Rotation
            }
            else if (animId == 4)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 45f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(0f, 0f, -60f), //Left Leg
                new Vector3(0f, 0f, -89f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0.06f, 0f), //Position
                new Vector3(0f, 0f, 130f)); //Rotation
            }
            else if (animId == 5)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 45f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(0f, 0f, -60f), //Left Leg
                new Vector3(0f, 0f, -91f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0.08f, 0f), //Position
                new Vector3(0f, 0f, 190f)); //Rotation
            }
            else if (animId == 6)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 45f), //Right Leg
                new Vector3(0f, 0f, -30f), //Right Leg Joint
                new Vector3(0f, 0f, -60f), //Left Leg
                new Vector3(0f, 0f, -91f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0.04f, 0f), //Position
                new Vector3(0f, 0f, 266f)); //Rotation
            }
            else if (animId == 7)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 50f), //Right Leg
                new Vector3(0f, 0f, -25f), //Right Leg Joint
                new Vector3(0f, 0f, -15f), //Left Leg
                new Vector3(0f, 0f, -40f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f), //Position
                new Vector3(0f, 0f, 320f)); //Rotation
            }
            else if (animId == 8)
            {
                this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 70f), //Right Leg
                new Vector3(0f, 0f, -60f), //Right Leg Joint
                new Vector3(0f, 0f, 25f), //Left Leg
                new Vector3(0f, 0f, -40f)); //Left Leg Joint

                this.animations.CustomBodyPose(
                new Vector3(0f, -0.2f, 0f), //Position
                new Vector3(0f, 0f, -33f)); //Rotation
            }



            /*this.animations.CustomLegsPose(
                new Vector3(0f, 0f, 0f), //Right Leg
                new Vector3(0f, 0f, 0f), //Right Leg Joint
                new Vector3(0f, 0f, 0f), //Left Leg
                new Vector3(0f, 0f, 0f)); //Left Leg Joint

            this.animations.CustomBodyPose(
                new Vector3(0f, 0f, 0f), //Position
                new Vector3(0f, 0f, 0f)); //Rotation*/

        }
    }
}
