using System.Collections;
using UnityEngine;

public class UfoSuperAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool controllable = false;
    public bool shipActive = false;

    public GameObject startParticle;
    public GameObject ufoModel;

    public GameObject ufoBeamHole;
    public GameObject ufoBeam;

    public GameObject explosionEffect;

    public GameObject startBeam;

    public TestHitbox hitbox;

    public bool capturing;

    public TestPlayer capturedPlayer;


    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && !this.user.knockbackInvounrability)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }

    public override void OnDeath()
    {
        if (this.onGoing)
        {
            //this.Stop();

            if(this.shipActive)
            {
                if (this.explosionEffect != null)
                {
                    GameObject explosionEffectPrefab = this.explosionEffect;
                    explosionEffectPrefab = Instantiate(explosionEffectPrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 1f, 0f), Quaternion.Euler(0, 0, 0));
                }
            }

            this.Stop();
        }
            
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.user != null)
            this.user.OnAttack += this.ActivateBeam;

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.Grab;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.user != null)
            this.user.OnAttack -= this.ActivateBeam;

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.Grab;
    }

    /*private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());*/

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
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
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.startBeam != null)
            this.startBeam.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = true;

        float currentTime = 0;
        float duration = 0.4f;
        //float targetVolume = 0.1f;
        float targetPositionY = 11f;
        float startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }

        if (this.startBeam != null)
            this.startBeam.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        this.user.knockbackInvounrability = true;

        if (this.ufoModel != null)
            this.ufoModel.SetActive(true);

        this.shipActive = true;

        if(this.animations != null)
        {
            if (this.animations.body != null)
                this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

            if(this.animations.rightLeg != null && this.animations.leftLeg != null)
            {
                this.animations.rightLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
                this.animations.leftLeg.localEulerAngles = new Vector3(0f, 0f, 90f);
            }
        }

        this.user.rb.isKinematic = false;

        currentTime = 0;
        duration = 0.4f;
        //float targetVolume = 0.1f;
        targetPositionY = 5f;
        startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }


        //this.user.rb.isKinematic = false;
        this.controllable = true;
        float time = 6f;
        Vector3 eyeRotation = this.animations.eyes.transform.eulerAngles;
        while (time > 0)
        {

            time -= Time.deltaTime;

            if (this.user.movement != null && this.user.stuns.Count <= 0)
                this.user.movement.Move(this.user.movement.playerInput.moveInput * 0.9f);

            this.user.rb.velocity = new Vector3(this.user.rb.velocity.x, 0f, 0f);
            this.user.transform.position = new Vector3(this.user.transform.position.x, 5f, 0f);


            yield return null;
        }

        this.ActivateBeam();
        yield return new WaitForSeconds(0.01f);
        Debug.Log("nooo");


        this.controllable = false;





        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 5f, 0f);

        if (this.ufoBeamHole != null)
        {
            this.ufoBeamHole.transform.localScale = new Vector3(0.1f, this.ufoBeamHole.transform.localScale.y, 0.1f);
            this.ufoBeamHole.SetActive(true);
        }
            

        currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        float targetScale = 2f;
        float startScale = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.ufoBeamHole != null)
            {
                this.ufoBeamHole.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), this.ufoBeamHole.transform.localScale.y, Mathf.Lerp(startScale, targetScale, currentTime / duration));
            }


            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }


        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(true);
        currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        targetScale = 1f;
        startScale = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.ufoBeam != null)
            {
                this.ufoBeam.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), this.ufoBeam.transform.localScale.y, Mathf.Lerp(startScale, targetScale, currentTime / duration));
            }


            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(false);

        currentTime = 0;
        duration = 0.2f;
        //float targetVolume = 0.1f;
        targetPositionY = 11f;
        startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }


        if (this.ufoBeamHole != null)
            this.ufoBeamHole.SetActive(false);


        this.user.rb.isKinematic = false;



        

        this.user.knockbackInvounrability = false;

        if (this.ufoModel != null)
            this.ufoModel.SetActive(false);

        this.shipActive = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ActivateBeam()
    {
        if (this.onGoing && this.controllable)
        {
            //Debug.Log("test");
            this.StopAllCoroutines();
            this.StartCoroutine(this.ActivateBeamCoroutine());
        }
        
    }


    

    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;
        this.user.knockbackInvounrability = false;

        this.controllable = false;
        this.shipActive = false;

        if (this.ufoModel != null)
            this.ufoModel.SetActive(false);

        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(false);

        if (this.ufoBeamHole != null)
            this.ufoBeamHole.SetActive(false);

        if (this.startBeam != null)
            this.startBeam.SetActive(false);


        if (this.capturedPlayer != null)
        {
            this.capturedPlayer.rb.isKinematic = false;
            this.capturedPlayer.knockbackInvounrability = false;

            this.capturedPlayer.attackStuns.Remove(this.gameObject);

            this.capturedPlayer.preventDeath = false;

            this.capturedPlayer.animations.SetDefaultPose();

            this.capturedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, false, true, false, true);

            this.capturedPlayer = null;
        }


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    


    private IEnumerator ActivateBeamCoroutine()
    {
        this.controllable = false;





        this.user.rb.isKinematic = true;
        this.user.transform.position = new Vector3(this.user.transform.position.x, 5f, 0f);

        if (this.ufoBeamHole != null)
        {
            this.ufoBeamHole.transform.localScale = new Vector3(0.1f, this.ufoBeamHole.transform.localScale.y, 0.1f);
            this.ufoBeamHole.SetActive(true);
        }


        float currentTime = 0;
        float duration = 0.1f;
        //float targetVolume = 0.1f;
        float targetScale = 2f;
        float startScale = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.ufoBeamHole != null)
            {
                this.ufoBeamHole.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), this.ufoBeamHole.transform.localScale.y, Mathf.Lerp(startScale, targetScale, currentTime / duration));
            }


            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }


        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(true);
        /*currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        targetScale = 1f;
        startScale = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.ufoBeam != null)
            {
                this.ufoBeam.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), this.ufoBeam.transform.localScale.y, Mathf.Lerp(startScale, targetScale, currentTime / duration));
            }


            //this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }*/

        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(2f);

        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(false);

        currentTime = 0;
        duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetPositionY = 11f;
        float startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }


        if (this.ufoBeamHole != null)
            this.ufoBeamHole.SetActive(false);


        this.user.rb.isKinematic = false;





        this.user.knockbackInvounrability = false;

        if (this.ufoModel != null)
            this.ufoModel.SetActive(false);

        this.shipActive = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }



    public void Grab(TestPlayer player)
    {
        Debug.Log("hit!!");
        if (player != null && !player.dead && !player.countering)
        {
            this.capturing = true;
            this.capturedPlayer = player;

            player.OnHit?.Invoke();

            //player.LookAtTarget();

            this.capturedPlayer.preventDeath = true;

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.33f), this.user.gameObject.transform.position.y + 1.74f, 0f);


            //player.TakeDamage(this.user.transform.position, 5f, 0f, 0f, 0f, false, true, false, true);

            /*this.user.GiveSuperCharge(3f);
            player.GiveSuperCharge(1.5f);*/

            //this.PlayBloodEffect(player.characterId);

            //player.animations.HoodGuyGrabbed();

            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

            player.attackStuns.Add(this.gameObject);

            this.StopAllCoroutines();
            this.StartCoroutine(this.CapturingCoroutine(player));
        }
    }

    IEnumerator CapturingCoroutine(TestPlayer player)
    {
        float currentTime = 0;
        float duration = 0.3f;
        //float targetVolume = 0.1f;
        float targetPositionX = this.user.transform.position.x;
        float startPositionX = player.transform.position.x;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.transform.position = new Vector3(Mathf.Lerp(startPositionX, targetPositionX, currentTime / duration), player.transform.position.y,  0);
            yield return null;
        }


        currentTime = 0;
        duration = 0.4f;
        //float targetVolume = 0.1f;
        float targetPositionY = this.user.transform.position.y;
        float startPositionY = player.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        //float targetVolume = 0.1f;
        float targetSize = 0.5f;
        float startSize = player.ragdoll.transform.localScale.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.ragdoll.transform.localScale = new Vector3(player.ragdoll.transform.localScale.x, Mathf.Lerp(startSize, targetSize, currentTime / duration), player.ragdoll.transform.localScale.z);
            yield return null;
        }

        player.ragdoll.transform.localScale = new Vector3(0.1f, 0.1f, player.transform.forward.z * 0.1f);

        yield return new WaitForSeconds(0.2f);

        if (this.ufoBeam != null)
            this.ufoBeam.SetActive(false);

        //yield return new WaitForSeconds(1.2f);
        yield return new WaitForSeconds(0.5f);

        int amount = 50;
        int amount2 = 0;
        //int amount = 180; kill the opponent
        while (amount > 0)
        {
            /*if (this.animations != null)
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);*/


            yield return new WaitForSeconds(0.025f);

            player.TakeDamage(this.user.transform.position, 1f, 0f, 0f, 0f, false, true, false, true);

            if (amount2 == 0)
                player.soundEffects.PlayHitSound();

            amount2++;
            if (amount2 > 1)
                amount2 = 0;

            /*this.user.GiveSuperCharge(0.25f);
            player.GiveSuperCharge(0.125f);*/

            amount -= 1;


            yield return null;
        }



        currentTime = 0;
        duration = 0.4f;
        //float targetVolume = 0.1f;
        targetPositionY = 12f;
        startPositionY = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(startPositionY, targetPositionY, currentTime / duration), 0);

            player.transform.position = this.user.transform.position;
            yield return null;
        }


        player.transform.position = new Vector3(this.user.transform.position.x, 10f, 0);

        player.AddStun(1f);

        this.StopGrab(player);

        //player.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, true);

        yield return new WaitForSeconds(1.2f);

        if (this.ufoBeamHole != null)
            this.ufoBeamHole.SetActive(false);


        this.user.rb.isKinematic = false;

        this.user.knockbackInvounrability = false;

        if (this.ufoModel != null)
            this.ufoModel.SetActive(false);

        this.shipActive = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void StopGrab(TestPlayer player)
    {
        if (player != null)
        {
            this.capturing = false;

            player.rb.isKinematic = false;

            //player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), this.user.gameObject.transform.position.y, 0f);


            //this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;

            player.attackStuns.Remove(this.gameObject);

            this.capturedPlayer.preventDeath = false;

            player.ragdoll.transform.localScale = new Vector3(1f, 1f, player.transform.forward.z * 1f);

            //player.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, false);

            this.capturedPlayer.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, false, true, false, true);

            if (!player.dead)
            {
                player.rb.isKinematic = false;
                /*player.TakeDamage(new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1.5f), player.transform.position.y - 0.5f, 0f), 5f, 1f, this.user.transform.forward.z * 700f, 900f);

                this.user.GiveSuperCharge(3f);
                player.GiveSuperCharge(1.5f);*/

                /*if (player.soundEffects != null)
                    player.soundEffects.PlayHitSound();*/
            }

            player.animations.SetDefaultPose();
            //player.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true, false, true);


            //this.user.rb.isKinematic = false;



            this.capturedPlayer = null;
        }
    }
}
