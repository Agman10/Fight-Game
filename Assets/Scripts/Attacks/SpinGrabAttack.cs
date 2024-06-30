using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGrabAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;

    public bool grabbing;
    public bool tryGrabbing;
    public TestPlayer grabbedPlayer;
    public Ball grabbedBall;

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.hitbox != null)
        {
            this.hitbox.OnPlayerCollision += this.Grab;
            this.hitbox.OnBallCollision += this.GrabBall;
        }
            
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
        {
            this.hitbox.OnPlayerCollision -= this.Grab;
            this.hitbox.OnBallCollision -= this.GrabBall;
        }
            
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
        /*if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }*/

        if (this.onGoing && this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.user.rb.velocity.x / 1.5f, this.user.rb.velocity.y - 0.4f, 0);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) > 0f)
                this.user.rb.AddForce(0, 300, 0);

            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TryGrabbingCorutine(0.2f));
        }
    }

    IEnumerator TryGrabbingCorutine(float time = 0.2f)
    {
        this.onGoing = true;
        this.user.attackStuns.Add(this.gameObject);
        if (this.animations != null)
            this.animations.SpinGrabStart(0);
        //yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(0.1f);
        this.animations.SpinGrabStart(1);
        yield return new WaitForSeconds(0.1f);


        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        if (this.animations != null)
            this.animations.SpinGrabStart(2);
        yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (!this.grabbing && this.animations != null)
        {
            yield return new WaitForSeconds(0.1f);
            //this.animations.SetGrabbingStartPose();
            this.animations.SpinGrabStart(3);
            yield return new WaitForSeconds(0.2f);
            this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.4f);
            this.tryGrabbing = false;
            this.user.attackStuns.Remove(this.gameObject);
            this.onGoing = false;
        }

        //this.();
        //this.ThrowFireBall();
    }

    IEnumerator GrabbingCoroutine(TestPlayer player)
    {
        //yield return new WaitForSeconds(0.5f);
        /*this.MidGrab(player);
        yield return new WaitForSeconds(0.3f);*/
        //yield return new WaitUntil(this.PlayerOnGround);
        float currentTime = 0;
        float duration = 0.2f;
        float targetPosition = 0f;
        float start = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, 1500f * Time.deltaTime, 0));

            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);



            player.animations.SpinGrabbed();
            if (player.characterId == 3 || player.characterId == 4)
                player.ragdoll.transform.localPosition = new Vector3(2.7f, 0.15f, 0);
            else
                player.ragdoll.transform.localPosition = new Vector3(3.05f, 0.15f, 0);


            yield return null;
        }

        float time = 1f;
        while (time > 0f)
        {
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, 1500f * Time.deltaTime, 0));

            time -= Time.deltaTime;

            yield return null;
        }
        //Debug.Log(this.user.transform.forward.z + this.user.movement.playerInput.moveInput.x);

        /*if (this.user.movement.playerInput.moveInput.x > 0 && this.user.transform.forward.z < 0)
        {
            this.StopGrab(player, false);

        }*/

        float maxXPos = 12.5f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
        {
            maxXPos = 9.5f;
        }

        if (this.user.movement.playerInput.moveInput.x + this.user.transform.forward.z == 0)
        {
            if (this.user.transform.position.x > maxXPos && this.user.transform.forward.z < 0 || this.user.transform.position.x < -maxXPos && this.user.transform.forward.z > 0)
                this.StopGrab(player, false);
            else
                this.StopGrab(player, true);

        }
        else
        {
            this.StopGrab(player, false);
        }

        //this.StopGrab(player, false);

        this.animations.SpinGrabEnd(0);
        yield return new WaitForSeconds(0.05f);
        this.animations.SpinGrabEnd(1);
        yield return new WaitForSeconds(0.1f);

        /*this.animations.SpinGrabEnd();
        yield return new WaitForSeconds(0.1f);*/
        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.25f);
        this.user.attackStuns.Remove(this.gameObject);
        this.animations.SetDefaultPose();
        this.onGoing = false;
        /*yield return new WaitForSeconds(time);
        this.StopGrab(player);*/
        //this.ThrowFireBall();
    }


    public void Grab(TestPlayer player)
    {
        if (player != null && !player.dead && !player.countering)
        {
            this.tryGrabbing = false;
            player.OnHit?.Invoke();
            /*if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);*/

            //Debug.Log("Grabbing: " + player.name);

            player.attackStuns.Add(this.gameObject);

            this.grabbedPlayer = player;

            this.user.rb.isKinematic = true;
            player.rb.isKinematic = true;

            /*this.user.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            player.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;*/

            /*this.user.AddStun(1.2f, true);
            player.AddStun(1.5f, true);*/

            if (player.collision != null)
                player.collision.enabled = false;


            this.user.knockbackInvounrability = true;
            player.knockbackInvounrability = true;
            //Debug.Log(player.ragdoll.transform.localPosition.y);
            //float ragdollPosY = player.ragdoll.transform.localPosition.y;
            //this.grabbedPlayerYPos = player.ragdoll.transform.localPosition.y;
            //Debug.Log(ragdollPosY);

            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), player.gameObject.transform.position.y, player.gameObject.transform.position.z);
            player.ragdoll.gameObject.transform.parent = this.user.ragdoll.transform;

            player.animations.SpinGrabbed();
            if (player.characterId == 3 || player.characterId == 4)
                player.ragdoll.transform.localPosition = new Vector3(2.7f, 0.15f, 0);
            else
                player.ragdoll.transform.localPosition = new Vector3(3.05f, 0.15f, 0);
            //Debug.Log(ragdollPosY);

            this.grabbing = true;

            //make it look like player is grabbing
            /*if(this.rightArm != null && this.leftArm != null)
            {
                this.rightArm.transform.localEulerAngles = new Vector3(90f, 0f, 90f);
                this.leftArm.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
            }*/
            

            if (this.animations != null)
                this.animations.SpinGrab();

            

            /*this.user.rb.AddForce(new Vector3(0f, 1000f, 0f));
            player.rb.AddForce(new Vector3(0f, 1000f, 0f));*/
            this.StartCoroutine(this.GrabbingCoroutine(player));
        }
        else
        {
            if (this.animations != null)
                this.animations.SpinGrabStart(3);
        }
    }

    public void StopGrab(TestPlayer player, bool back = false)
    {
        //bool back = true;

        this.user.rb.isKinematic = false;
        player.rb.isKinematic = false;

        /*this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        player.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;*/



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

        //player.ragdoll.transform.localPosition = new Vector3(0f, this.grabbedPlayerYPos, 0f);
        player.animations.SetDefaultPose();

        //player.AddKnockback(this.belongsTo.transform.forward.z * 1000, 1000);
        //player.TakeDamage(this.belongsTo.transform.position, 20);
        player.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);
        //player.TakeDamage(this.user.transform.position, 10f, 1.1f, this.user.transform.forward.z * 1200f, 1200f);
        this.user.GiveSuperCharge(10f);
        player.GiveSuperCharge(5f);

        if (player.collision != null)
            player.collision.enabled = true;


        if (back)
        {
            player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * -1.2f), player.gameObject.transform.position.y, player.gameObject.transform.position.z);

            //player.TakeDamage(this.user.transform.position, 10f, 1.3f, this.user.transform.forward.z * -1200f, 1200f);
            //this.user.LookAtTarget();

            //player.TakeDamage(this.user.transform.position - new Vector3(this.transform.forward.z * 3, 0, 0), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
            //player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * -1), player.transform.position.y - 0.5f, 0f), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
            /*this.user.LookAtTarget();
            player.LookAtTarget();*/

            this.user.lookAtPlayer();
            player.lookAtPlayer();

            //player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * 4), player.transform.position.y - 1f, 0f), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
            //player.TakeDamage(new Vector3(this.user.transform.position.x, player.transform.position.y - 0.5f, 0f), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
            player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * 6), player.transform.position.y - 0.5f, 0f), 20f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
        }
        else
        {
            //player.TakeDamage(this.user.transform.position, 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);

            //player.TakeDamage(this.user.transform.position - new Vector3(this.transform.forward.z * 3, 0, 0), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);

            //player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * 1), player.transform.position.y - 0.5f, 0f), 10f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);

            player.TakeDamage(new Vector3(this.user.transform.position.x, player.transform.position.y - 0.5f, 0f), 20f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
        }

        /*if (this.impactEffect != null)
        {
            GameObject impactPrefab = this.impactEffect;
            impactPrefab = Instantiate(impactPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), 0.1f, 0), Quaternion.Euler(0, 0, 0));
        }*/

        player.attackStuns.Remove(this.gameObject);

        this.grabbedPlayer = null;
        this.onGoing = false;
    }

    public override void Stop()
    {
        base.Stop();

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.user.rb.isKinematic = false;
        //this.user.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        this.grabbing = false;
        this.user.knockbackInvounrability = false;
        this.tryGrabbing = false;


        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.knockbackInvounrability = false;

            this.grabbedPlayer.rb.isKinematic = false;
            //this.grabbedPlayer.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            this.grabbedPlayer.ragdoll.gameObject.transform.parent = this.grabbedPlayer.transform;
            //player.ragdoll.transform.localPosition = new Vector3(0f, player.ragdoll.transform.localPosition.y, 0f);
            //this.grabbedPlayer.ragdoll.transform.localPosition = new Vector3(0f, this.grabbedPlayerYPos, 0f);
            //player.AddKnockback(this.belongsTo.transform.forward.z * 1000, 1000);
            //player.TakeDamage(this.belongsTo.transform.position, 20);
            this.grabbedPlayer.animations.SetDefaultPose();
            this.grabbedPlayer.ragdoll.transform.localEulerAngles = new Vector3(0, 0, 0);

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            if (this.grabbedPlayer.collision != null)
                this.grabbedPlayer.collision.enabled = true;

            this.grabbedPlayer = null;

            

        }

        if (this.grabbedBall != null)
        {
            this.grabbedBall.rb.isKinematic = false;
            this.grabbedBall.collision.enabled = true;

            if (GameManager.Instance != null)
                this.grabbedBall.transform.parent = GameManager.Instance.gameObject.transform.parent;

            this.grabbedBall.transform.position = new Vector3(this.grabbedBall.transform.position.x, this.grabbedBall.transform.position.y, 0f);

            this.grabbedBall = null;
        }


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator GrabbingBallCoroutine(Ball ball)
    {
        float currentTime = 0;
        float duration = 0.2f;
        float targetPosition = 0f;
        float start = this.user.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, 1500f * Time.deltaTime, 0));

            this.user.transform.position = new Vector3(this.user.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
            //player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);

            ball.rb.transform.localPosition = new Vector3(1.5f, 0.15f, 0);

            //player.animations.SpinGrabbed();
            /*if (player.characterId == 3 || player.characterId == 4)
                player.ragdoll.transform.localPosition = new Vector3(2.7f, 0.15f, 0);
            else
                player.ragdoll.transform.localPosition = new Vector3(3.05f, 0.15f, 0);*/


            yield return null;
        }

        float time = 1f;
        while (time > 0f)
        {
            if (this.animations != null)
                this.animations.body.transform.Rotate(new Vector3(0, 1500f * Time.deltaTime, 0));

            time -= Time.deltaTime;

            yield return null;
        }
        //Debug.Log(this.user.transform.forward.z + this.user.movement.playerInput.moveInput.x);

        /*if (this.user.movement.playerInput.moveInput.x > 0 && this.user.transform.forward.z < 0)
        {
            this.StopGrab(player, false);

        }*/

        float maxXPos = 12.5f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
        {
            maxXPos = 9.5f;
        }

        if (this.user.movement.playerInput.moveInput.x + this.user.transform.forward.z == 0)
        {
            /*if (this.user.transform.position.x > maxXPos && this.user.transform.forward.z < 0 || this.user.transform.position.x < -maxXPos && this.user.transform.forward.z > 0)
                this.StopGrabBall(ball, false);
            else
                this.StopGrabBall(ball, true);*/


            this.StopGrabBall(ball, true);
        }
        else
        {
            this.StopGrabBall(ball, false);
        }

        //this.StopGrab(player, false);

        this.animations.SpinGrabEnd(0);
        yield return new WaitForSeconds(0.05f);
        this.animations.SpinGrabEnd(1);
        yield return new WaitForSeconds(0.1f);

        /*this.animations.SpinGrabEnd();
        yield return new WaitForSeconds(0.1f);*/
        this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.25f);
        this.user.attackStuns.Remove(this.gameObject);
        this.animations.SetDefaultPose();
        this.onGoing = false;
    }

    public void GrabBall(Ball ball)
    {
        if (ball != null && this.grabbedPlayer == null && !this.grabbing)
        {
            this.tryGrabbing = false;
            this.grabbedBall = ball;

            this.user.rb.isKinematic = true;

            if (ball.collision != null)
                ball.collision.enabled = false;

            ball.rb.isKinematic = true;

            /*if (ball.collision != null)
                ball.collision.enabled = false;*/


            this.user.knockbackInvounrability = true;

            //ball.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), player.gameObject.transform.position.y, player.gameObject.transform.position.z);
            ball.gameObject.transform.parent = this.user.ragdoll.transform;
            ball.transform.localPosition = new Vector3(1.4f, 0.15f, 0);
            //Debug.Log(ragdollPosY);

            this.grabbing = true;


            if (this.animations != null)
                this.animations.SpinGrab();

            this.StartCoroutine(this.GrabbingBallCoroutine(ball));
        }
    }

    public void GrabBallDelay(Ball ball)
    {
        this.StartCoroutine(this.GrabBallDelayCoroutine(ball));
    }

    private IEnumerator GrabBallDelayCoroutine(Ball ball)
    {
        yield return new WaitForSeconds(0.01f);
        this.GrabBall(ball);
    }

    public void StopGrabBall(Ball ball, bool back = false)
    {
        this.user.rb.isKinematic = false;

        /*if (GameManager.Instance != null)
            ball.transform.parent = GameManager.Instance.gameObject.transform.parent;*/

        ball.rb.isKinematic = false;

        if (ball.collision != null)
            ball.collision.enabled = true;

        if (GameManager.Instance != null)
            ball.transform.parent = GameManager.Instance.gameObject.transform.parent;

        if (this.animations != null)
            this.animations.SetDefaultPose();


        this.grabbing = false;
        this.user.knockbackInvounrability = false;

        if (back)
        {
            ball.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * -1.5f), ball.gameObject.transform.position.y, 0f);

            this.user.LookAtTarget();

            //player.TakeDamage(new Vector3(player.transform.position.x + (player.transform.forward.z * 6), player.transform.position.y - 0.5f, 0f), 20f, 1.3f, this.user.transform.forward.z * 1200f, 1200f);
            ball.KnockBack(new Vector3(this.user.transform.forward.z * 600f, 400f, 0f));
        }
        else
        {
            ball.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.5f), ball.gameObject.transform.position.y, 0f);

            ball.KnockBack(new Vector3(this.user.transform.forward.z * 600f, 400f, 0f));
        }


        this.grabbedBall = null;
        this.onGoing = false;
    }
}
