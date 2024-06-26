using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperSpinGrab : Attack
{
    //public TestPlayer user;
    public TestHitbox hitbox;
    public TempPlayerAnimations animations;
    

    public bool grabbing;
    public bool tryGrabbing;
    public GameObject impactEffect;
    public TestPlayer grabbedPlayer;

    public bool turnUpsideDown;
    public float damage = 20f;
    public float upwardForce = 1000f;
    public float downwardForce = 500f;
    public float grabStunLength = 0.6f;
    public float grabSpeed = 0.2f;

    private float grabbedPlayerYPos;

    public VisualEffect legFire1;
    public VisualEffect legFire2;
    public bool playFire;

    public int endPoseId = 0;

    //temporary way to give grab animation
    public GameObject rightArm;
    public GameObject leftArm;

    public AudioSource explosionImpactSfx;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnEnable()
    {
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += Grab;

        if (this.user != null)
        {
            this.user.OnDeath += this.OnDeath;
            this.user.OnHit += this.OnHit;
        }
            
    }
    public override void OnDisable()
    {
        this.StopAllCoroutines();
        this.grabbing = false;
        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= Grab;

        if (this.user != null)
        {
            this.user.OnDeath -= this.OnDeath;
            this.user.OnHit -= this.OnHit;
        }
            
    }
    public override void OnHit()
    {
        if(this.animations != null && !this.user.dead && this.tryGrabbing)
        {
            this.Stop();
            this.animations.SetDefaultPose();
        }
    }
    public override void OnDeath()
    {
        this.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (this.grabbing)
        {
            this.user.ragdoll.transform.Rotate(new Vector3(0, 1700 * Time.deltaTime, 0));

            if (this.grabbedPlayer != null && this.grabbedPlayer.animations != null && this.grabbedPlayer.animations.body != null)
            {
                this.grabbedPlayer.animations.body.localPosition = new Vector3(1.2f, 0f, 0f);
                this.grabbedPlayer.animations.body.localEulerAngles = new Vector3(0, -180f, 0f);
            }

            if (this.animations != null)
                this.animations.SetGrabbingPose();
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate(/*bool stopMomentum = true*/)
    {
        if (this.user != null && !this.grabbing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
                this.user.rb.AddForce(0, 300, 0);

            this.tryGrabbing = true;
            this.user.AddStun(this.grabStunLength, true /*stopMomentum*/);
            this.StartCoroutine(this.TryGrabbingCorutine(this.grabSpeed));
        }
    }

    public void Grab(TestPlayer player)
    {
        if(player != null && !player.dead)
        {
            this.tryGrabbing = false;
            /*if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);*/

            //Debug.Log("Grabbing: " + player.name);

            this.grabbedPlayer = player;

            this.user.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            player.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            this.user.AddStun(1.2f, true);
            player.AddStun(1.5f, true);
            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;
            //Debug.Log(player.ragdoll.transform.localPosition.y);
            //float ragdollPosY = player.ragdoll.transform.localPosition.y;
            this.grabbedPlayerYPos = player.ragdoll.transform.localPosition.y;
            //Debug.Log(ragdollPosY);

            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), player.gameObject.transform.position.y, player.gameObject.transform.position.z);
            player.ragdoll.gameObject.transform.parent = this.user.ragdoll.transform;
            player.ragdoll.transform.localPosition = new Vector3(1.2f, 0, 0);
            //Debug.Log(ragdollPosY);

            this.grabbing = true;

            //make it look like player is grabbing
            /*if(this.rightArm != null && this.leftArm != null)
            {
                this.rightArm.transform.localEulerAngles = new Vector3(90f, 0f, 90f);
                this.leftArm.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
            }*/
            if (this.playFire)
                this.PlayFire(true);

            if (this.animations != null)
                this.animations.SetGrabbingPose();

            this.user.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));
            player.rb.AddForce(new Vector3(0f, this.upwardForce, 0f));
            this.StartCoroutine(this.GrabbingCoroutine(player, 1));
        }
        
    }

    public void StopGrab(TestPlayer player)
    {
        //Debug.Log("stop Grabbing: " + player.name);

        //stop making it look like player is grabbing
        /*if (this.rightArm != null && this.leftArm != null)
        {
            this.rightArm.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            this.leftArm.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
        }*/
        if (this.explosionImpactSfx != null)
        {
            //this.explosionImpactSfx.PlaySound();
            this.explosionImpactSfx.time = 0.08f;
            this.explosionImpactSfx.Play();
        }

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.grabbing = false;
        this.user.knockbackInvounrability = false;
        player.knockbackInvounrability = false;
        float ragdollPosY = player.ragdoll.transform.localPosition.y;
        //player.gameObject.transform.position = new Vector3(player.transform.position.x, 0f, 0f);

        this.user.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);
        
        player.ragdoll.gameObject.transform.parent = player.transform;
        //player.ragdoll.transform.localPosition = new Vector3(0f, player.ragdoll.transform.localPosition.y, 0f);
        player.ragdoll.transform.localPosition = new Vector3(0f, this.grabbedPlayerYPos, 0f);
        //player.AddKnockback(this.belongsTo.transform.forward.z * 1000, 1000);
        //player.TakeDamage(this.belongsTo.transform.position, 20);
        player.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);
        player.TakeDamage(this.user.transform.position, this.damage, 1.1f, this.user.transform.forward.z * 1000f, 1000f);
        this.user.GiveSuperCharge(10f);
        player.GiveSuperCharge(5f);

        if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 0.1f, 0), Quaternion.Euler(0, 0, 0));
        }

        this.grabbedPlayer = null;
        this.PlayFire(false);
    }

    IEnumerator GrabbingCoroutine(TestPlayer player, float time)
    {
        yield return new WaitForSeconds(0.5f);
        this.MidGrab(player);
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(this.PlayerOnGround);
        this.StopGrab(player);

        if(this.animations != null)
        {
            if (this.endPoseId == 1)
                this.animations.Test222();
            else if (this.endPoseId == 2)
                this.animations.SetSpinGrabEndPose2();
            else
                this.animations.SetSpinGrabEndPose();
        }
        //this.animations.SetSpinGrabEndPose();
        //this.animations.SetSpinGrabEndPose2();
        //this.animations.Wry();

        yield return new WaitForSeconds(0.4f);
        this.user.attackStuns.Remove(this.gameObject);
        this.animations.SetDefaultPose();
        /*yield return new WaitForSeconds(time);
        this.StopGrab(player);*/
        //this.ThrowFireBall();
    }
    public void MidGrab(TestPlayer player)
    {
        if(this.turnUpsideDown)
            this.user.ragdoll.transform.localEulerAngles = new Vector3(180, this.user.ragdoll.transform.localEulerAngles.y, 0);

        this.user.rb.AddForce(new Vector3(0f, -this.downwardForce, 0f));
        player.rb.AddForce(new Vector3(0f, -this.downwardForce, 0f));
    }

    bool PlayerOnGround()
    {
        if (this.user != null)
            return this.user.transform.position.y <= 0f;
        else return false;
    }

    IEnumerator TryGrabbingCorutine(float time)
    {
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.SetGrabbingStartPose();
        yield return new WaitForSeconds(time);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        /*if (this.animations != null)
            this.animations.SetGrabbingPose();*/

        if (this.animations != null)
            this.animations.SetSuperSpinGrabbingPose();

        yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (!this.grabbing && this.animations != null)
        {
            yield return new WaitForSeconds(0.1f);
            this.animations.SetGrabbingStartPose();
            yield return new WaitForSeconds(0.1f);
            this.animations.SetDefaultPose();
            this.tryGrabbing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
            
        //this.();
        //this.ThrowFireBall();
    }

    public override void Stop()
    {
        this.StopAllCoroutines();
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        this.grabbing = false;
        this.user.knockbackInvounrability = false;
        this.tryGrabbing = false;
        this.PlayFire(false);

        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.knockbackInvounrability = false;
            this.grabbedPlayer.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            this.grabbedPlayer.ragdoll.gameObject.transform.parent = this.grabbedPlayer.transform;
            //player.ragdoll.transform.localPosition = new Vector3(0f, player.ragdoll.transform.localPosition.y, 0f);
            this.grabbedPlayer.ragdoll.transform.localPosition = new Vector3(0f, this.grabbedPlayerYPos, 0f);
            //player.AddKnockback(this.belongsTo.transform.forward.z * 1000, 1000);
            //player.TakeDamage(this.belongsTo.transform.position, 20);
            this.grabbedPlayer.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.grabbedPlayer = null;

        }
        this.user.attackStuns.Remove(this.gameObject);
        /*this.PlayFire(false, 2);
        this.spinning = false;*/
        //this.user.attackStuns.Remove(this.gameObject);
        //this.ongoing = false;
    }

    public void PlayFire(bool playing)
    {
        if (playing)
        {
            if (this.legFire1 != null)
                this.legFire1.Play();

            if (this.legFire2 != null)
                this.legFire2.Play();
        }
        else
        {
            if (this.legFire1 != null)
                this.legFire1.Stop();

            if (this.legFire2 != null)
                this.legFire2.Stop();
        }
    }
}
