using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagingBeast : Attack
{
    //public TestPlayer user;
    public TestHitbox hitbox;
    public TempPlayerAnimations animations;

    public TestPlayer victim;

    public Camera mainCamera;
    public GameObject skull;
    public GameObject backSkull;
    public LayerMask midAttackLayers;
    public LayerMask normalLayers;
    public LayerMask deathLayers;
    public GameObject trail;
    public GameObject punchEffect;
    public GameObject startParticle;

    private float dashSpeed = 17f;
    private float dashDuration = 1.2f;


    public bool moving;
    public bool onGoing;

    public override void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
        }

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.DoRagingBeast;

    }
    public override void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
        }

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.DoRagingBeast;

    }
    public override void OnHit()
    {
        if (this.moving && !this.onGoing)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.HitCoroutine());

            //Debug.Log("test");
            /*this.Stop();
            if (this.animations != null && !this.user.dead)
                this.animations.SetDefaultPose();*/
        }
        
    }
    public override void OnDeath()
    {
        if (this.onGoing || this.moving)
            this.Stop();
    }

    void Update()
    {
        if (this.moving && this.user != null && this.user.rb != null)
            this.user.rb.velocity = new Vector3(this.user.transform.forward.z * this.dashSpeed, 0f, 0f);
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        if(this.user != null && this.user.stuns.Count <= 0 && this.user.attackStuns.Count <= 0)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.TryRagingBeastCoroutine());
                }
            }
        }
    }

    IEnumerator TryRagingBeastCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.RagingBeastStartPose();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.RagingBeastDash();
        this.moving = true;
        if (this.trail != null)
            this.trail.SetActive(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(this.dashDuration);

        if(this.victim == null)
        {
            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);
            this.moving = false;

            if (this.user != null && this.user.rb != null)
                this.user.rb.velocity = Vector3.zero;

            if (this.animations != null)
                this.animations.SetDefaultPose();

            if (this.trail != null)
                this.trail.SetActive(false);

            this.user.attackStuns.Remove(this.gameObject);
        }
        
    }

    public void DoRagingBeast(TestPlayer player)
    {
        player.attackStuns.Add(this.gameObject);
        player.OnHit.Invoke();
        //player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);
        this.user.transform.position = new Vector3(player.transform.position.x - (this.user.transform.forward.z * 1.1f), 0f, 0f);
        player.preventDeath = true;
        this.user.preventDeath = true;
        this.user.knockbackInvounrability = true;
        player.knockbackInvounrability = true;
        if (this.trail != null)
            this.trail.SetActive(false);

        this.victim = player;
        this.moving = false;
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.animations != null)
            this.animations.SetGrabbingPose();

        this.user.rb.isKinematic = true;
        this.user.rb.constraints = RigidbodyConstraints.FreezeAll;
        player.rb.constraints = RigidbodyConstraints.FreezeAll;

        if (player.animations != null)
            player.animations.body.localPosition = new Vector3(0f, 1.95f, 0f);

        this.StartCoroutine(this.RagingBeastCoroutine(player));
    }

    IEnumerator RagingBeastCoroutine(TestPlayer player)
    {
        this.onGoing = true;

        float currentTime = 0;
        float duration = 0.025f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        /*if (this.mainCamera != null)
        {
            this.mainCamera.cullingMask = this.midAttackLayers;
            this.mainCamera.clearFlags = CameraClearFlags.SolidColor;
            this.mainCamera.backgroundColor = Color.black;
        }*/

        if (GameManager.Instance != null)
            GameManager.Instance.RagingBeastEffect(1);

        bool ghost = true;

        /*float damage = 2f;

        float minXPos = -0.5f;
        float maxXPos = 0.5f;
        float minYPos = 1f;
        float maxYPos = 2f;*/

        float damage = 1f;

        float minXPos = -0.75f;
        float maxXPos = 0.75f;
        float minYPos = 0.5f;
        float maxYPos = 2.5f;
        yield return new WaitForSeconds(0.1f);

        this.user.transform.position = new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1), 0f, 0f);
        //this.user.transform.position = new Vector3(player.transform.position.x - (this.user.transform.forward.z * 2.1f), 0f, 0f);

        /*if (this.user.transform.position.x <= 0f)
            this.user.transform.position = new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1), 0f, 0f);
        else
            player.transform.position = new Vector3(player.transform.position.x + (this.user.transform.forward.z * 1), 0f, 0f);*/

        //this.PunchEffect(new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 2f, 0));
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.3f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);

        //int amount = 18;
        int amount = 43;
        while (amount > 0)
        {
            /*float waitTime = 0.05f;
            if (amount <= 30f && amount > 15f)
                waitTime = 0.025f;
            else if (amount <= 15f)
                waitTime = 0.01f;
            yield return new WaitForSeconds(waitTime);*/

            /*float waitTime = 0.025f;
            if (amount <= 20f)
                waitTime = 0.01f;
            yield return new WaitForSeconds(waitTime);*/

            //yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(0.025f);

            this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
            player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
            amount -= 1;
            yield return null;
        }

        /*yield return new WaitForSeconds(0.3f);
        amount = 25;
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.01f);
            this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
            player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
            amount -= 1;
            yield return null;
        }*/


        /*amount = 40;
        damage = 1f;
        yield return new WaitForSeconds(0.3f);
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.025f);
            this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
            player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
            amount -= 1;
            yield return null;
        }*/


        //this.user.transform.position = new Vector3(this.user.transform.position.x - (this.user.transform.forward.z * 1), 0f, 0f);


        /*this.user.transform.position = new Vector3(this.transform.position.x - (this.transform.forward.z * 0.5f), 0f, 0f);
        player.transform.position = new Vector3(player.transform.position.x - (player.transform.forward.z * 0.5f), 0f, 0f);*/

        player.animations.LayingDownPose();

        //player.ragdoll.transform.localPosition = new Vector3(0f, 0.5f, 0f);

        yield return new WaitForSeconds(0.1f);
        if (player.health > 0f)
        {
            //yield return new WaitForSeconds(0.1f);
            /*if (this.mainCamera != null)
            {
                this.mainCamera.cullingMask = this.normalLayers;
                this.mainCamera.clearFlags = CameraClearFlags.Skybox;

            }*/

            if (GameManager.Instance != null)
                GameManager.Instance.RagingBeastEffect(0);

            this.user.animations.RagingBeastPose();

            //yield return new WaitForSeconds(0.1f);
            if (this.backSkull != null)
                this.backSkull.SetActive(true);

            yield return new WaitForSeconds(0.75f);

            /*if (this.backSkull != null)
                this.backSkull.SetActive(false);
            yield return new WaitForSeconds(0.1f);*/

            player.preventDeath = false;
            this.user.preventDeath = false;
            //player.TakeDamage(this.user.transform.position, 5f, 0f, 0f, 0f, false, ghost);
            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;
            this.StopRagingBeast(player);
        }
        else
        {
            //yield return new WaitForSeconds(0.1f);
            this.user.animations.RagingBeastPose();
            /*if (this.mainCamera != null)
            {
                this.mainCamera.cullingMask = this.deathLayers;
                //this.mainCamera.clearFlags = CameraClearFlags.Skybox;
                this.mainCamera.backgroundColor = Color.black;
                if (this.skull != null)
                    this.skull.SetActive(true);

            }*/

            if (GameManager.Instance != null)
                GameManager.Instance.RagingBeastEffect(2);
            //yield return new WaitForSeconds(0.1f);
            if (this.backSkull != null)
                this.backSkull.SetActive(true);

            yield return new WaitForSeconds(2f);
            /*if (this.mainCamera != null)
            {
                this.mainCamera.cullingMask = this.normalLayers;
                this.mainCamera.clearFlags = CameraClearFlags.Skybox;

            }
            if (this.skull != null)
                this.skull.SetActive(false)*/;

            if (GameManager.Instance != null)
                GameManager.Instance.RagingBeastEffect(0);

            yield return new WaitForSeconds(0.3f);

            /*if (this.backSkull != null)
                this.backSkull.SetActive(false);
            yield return new WaitForSeconds(0.1f);*/


            this.user.preventDeath = false;
            player.preventDeath = false;
            player.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, ghost);

            this.user.knockbackInvounrability = false;
            player.knockbackInvounrability = false;
            //yield return new WaitForSeconds(1f);
            this.StopRagingBeast(player);
        }
        
    }

    public void StopRagingBeast(TestPlayer player)
    {
        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.backSkull != null)
            this.backSkull.SetActive(false);

        if (!player.dead)
        {
            player.animations.SetDefaultPose();
            //player.ragdoll.transform.localPosition = new Vector3(0f, 1.95f, 0f);
        }

        this.user.rb.isKinematic = false;
        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        this.user.attackStuns.Remove(this.gameObject);
        player.attackStuns.Remove(this.gameObject);
        this.onGoing = false;
        this.victim = null;
        //this.user.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true);
    }

    public override void Stop()
    {
        this.StopAllCoroutines();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.trail != null)
            this.trail.SetActive(false);

        this.moving = false;
        this.user.preventDeath = false;
        this.user.knockbackInvounrability = false;

        this.user.rb.isKinematic = false;

        /*if (this.mainCamera != null && this.onGoing)
        {
            this.mainCamera.cullingMask = this.normalLayers;
            this.mainCamera.clearFlags = CameraClearFlags.Skybox;

        }
        if (this.skull != null && this.onGoing)
            this.skull.SetActive(false);*/

        if (GameManager.Instance != null /*&& this.onGoing*/)
            GameManager.Instance.RagingBeastEffect(0);

        if (this.backSkull != null)
            this.backSkull.SetActive(false);
        
        if (this.victim != null)
        {
            this.victim.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            this.victim.attackStuns.Remove(this.gameObject);
            this.victim.preventDeath = false;
            this.victim.knockbackInvounrability = false;
            if(this.onGoing && this.victim.health <= 0f)
            {
                this.victim.TakeDamage(this.user.transform.position, 0f, 0f, 0f, 0f, false, true);
            }
            else
            {
                this.victim.animations.SetDefaultPose();
                //this.victim.ragdoll.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            }
            this.victim = null;
        }
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PunchEffect(Vector3 position)
    {
        if (this.punchEffect != null)
        {
            GameObject punchEffectPrefab = this.punchEffect;
            punchEffectPrefab = Instantiate(punchEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }
    }
    private float RandomX(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }

    private float RandomY(float min, float max)
    {
        //Debug.Log(Random.Range(min, max));
        return Random.Range(min, max);
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);

        if (this.moving && !this.onGoing)
        {
            //Debug.Log("test");
            this.Stop();
            if (this.animations != null && !this.user.dead)
                this.animations.SetDefaultPose();
        }

        /*if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }*/
    }

    /*IEnumerator backupCoroutine(TestPlayer player)
    {
        bool ghost = false;

        float damage = 5f;

        float minXPos = -0.5f;
        float maxXPos = 0.5f;
        float minYPos = 1f;
        float maxYPos = 2f;

        yield return new WaitForSeconds(0.1f);
        //this.PunchEffect(new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 2f, 0));
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.1f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        //player.preventDeath = false;
        this.user.transform.position = new Vector3(this.transform.position.x - (this.transform.forward.z * 1), 0f, 0f);
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);






        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        //player.preventDeath = false;
        //this.user.transform.position = new Vector3(this.transform.position.x - (this.transform.forward.z * 1), 0f, 0f);
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);


        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);


        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
        yield return new WaitForSeconds(0.05f);
        this.PunchEffect(new Vector3(player.transform.position.x + this.RandomX(minXPos, maxXPos), this.RandomY(minYPos, maxYPos), 0));
        player.TakeDamage(this.user.transform.position, damage, 0f, 0f, 0f, false, ghost);
    }*/
}
