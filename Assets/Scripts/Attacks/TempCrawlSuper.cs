using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCrawlSuper : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public TestHitbox hitbox;
    public TestPlayer victim;

    public GameObject startParticle;
    public GameObject dissapearEffect;
    public GameObject cosmoPortal;

    private bool moving = false;

    private float camDistance;
    private bool testBool; //this bool have to do with camera and makes sure it doesnt stop while crawling rename it to a better name

    private bool victimGone;

    public override void OnEnable()
    {
        base.OnEnable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision += this.OnHitSuper;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (this.hitbox != null)
            this.hitbox.OnPlayerCollision -= this.OnHitSuper;
    }

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
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
            if (this.moving)
            {
                //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 20f, this.user.rb.velocity.y, 0f);
                this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 22f, this.user.rb.velocity.y, 0f);
                //this.user.rb.velocity = new Vector3(this.user.transform.forward.z * 25f, this.user.rb.velocity.y, 0f);
            }

        }

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null && this.testBool == true)
        {
            float testFloat = 0.01f;
            if (this.user.transform.position.x < 0f)
                testFloat = -0.01f;
            else
                testFloat = 0.01f;

            if(GameManager.Instance.gameMode == 1)
                this.camDistance = 11 + testFloat;
            else
                this.camDistance = Mathf.Abs(GameManager.Instance.gameCamera.transform.position.x) + 7.5f + testFloat;
            

            //this.camDistance = Mathf.Abs(GameManager.Instance.gameCamera.transform.position.x) + 7.5f;

            //Debug.Log(testFloat);
        }
        else
        {
            this.camDistance = 1000f;
        }
        //Debug.Log(this.camDistance);

        /*GameCamera camera = null;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            camera = GameManager.Instance.gameCamera;*/
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null && !this.onGoing)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge);
                    this.user.AddStun(0.2f, true);
                    this.StartCoroutine(this.AnimationTestCoroutine());
                }
                
            }
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    


    private IEnumerator AnimationTestCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;



        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            this.animations.TestStupidWalk(0);
            //this.animations.StupidDance(0);
        }

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }


        //int amountt = 15;
        int amountt = 10;
        int animationIdd = 1;
        while (amountt > 0)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            yield return new WaitForSeconds(0.025f);

            if (animationIdd == 0)
            {
                this.animations.TestStupidWalk(0);
                //this.animations.StupidDance(0);

                animationIdd += 1;
            }
            else if (animationIdd == 1)
            {
                this.animations.TestStupidWalk(1);
                //this.animations.StupidDance(1);

                animationIdd += 1;
            }
            else if (animationIdd == 2)
            {
                this.animations.TestStupidWalk(2);
                //this.animations.StupidDance(2);

                animationIdd += 1;
            }
            else if (animationIdd == 3)
            {
                this.animations.TestStupidWalk(1);
                //this.animations.StupidDance(1);

                animationIdd = 0;
            }
            amountt -= 1;
            if (amountt <= 0 && this.user.input.super)
                amountt = 1;
        }







        /*if (this.animations != null)
            this.animations.TestStupidWalk(0);

        this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(0.3f);*/

        this.user.rb.isKinematic = false;

        this.ChangeCollision(true);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);


        if (Mathf.Abs(this.user.transform.position.x) >= 14f)
        {
            //Debug.Log(-(this.transform.forward.z * 13.99f));

            //this.user.transform.position = new Vector3(this.user.transform.position.x + this.user.transform.forward.z * 0.01f, this.user.transform.position.y, 0f);
            this.user.transform.position = new Vector3(-(this.transform.forward.z * 13.99f), this.user.transform.position.y, 0f);
        }


        if (this.animations != null)
            this.animations.Crawl(0);

        int amount = 60;
        int animationId = 1;

        float longWaitTime = 0.25f;
        float shortWaitTime = 0.05f;

        float waitTime = shortWaitTime;



        this.moving = true;

        //NOTE FOR WHEN MAKING IT FOR A SUPER IS MAYBE TRY TO MAKE IT BASED ON THE CAMERA INSTEAD (camera x pos * 6.5)

        /*GameCamera camera = null;

        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            camera = GameManager.Instance.gameCamera;*/


        while (amount > 0 && Mathf.Abs(this.user.transform.position.x) < 14f && Mathf.Abs(this.user.transform.position.x) < this.camDistance && this.moving/*|| camera != null && Mathf.Abs(this.user.transform.position.x) < Mathf.Abs(camera.transform.position.x) + 7.5f*/ /*&& this.user.transform.position.x < this.user.transform.forward.z * 13.9f*/)
        {
            //Debug.Log(Mathf.Abs(camera.transform.position.x) + 7.5f);
            //Debug.Log(Mathf.Abs(this.user.transform.forward.z * this.user.transform.position.x));
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            yield return new WaitForSeconds(0.025f);

            this.testBool = true;

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(this.transform.forward.z * 20f, this.user.rb.velocity.y, 0f);*/

            /*if (this.animations != null)
            {
                this.animations.CaramelDance(animationId);
                Debug.Log(animationId);
            }*/

            if (animationId == 0)
            {
                this.animations.Crawl(0);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 1)
            {
                this.animations.Crawl(1);
                waitTime = longWaitTime;
                animationId += 1;
            }
            else if (animationId == 2)
            {
                this.animations.Crawl(2);
                waitTime = shortWaitTime;
                animationId += 1;
            }
            else if (animationId == 3)
            {
                this.animations.Crawl(1);
                waitTime = longWaitTime;
                animationId = 0;
            }

            amount -= 1;
            /*if (amount <= 0 && this.user.input.taunting)
                amount = 1;*/
            //Debug.Log(amount);
        }
        this.moving = false;
        this.ChangeCollision(false);

        if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);


        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.testBool = false;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        //End

        if (this.victim == null)
        {
            //Debug.Log("test");

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.1f);

            this.onGoing = false;
            this.user.attackStuns.Remove(this.gameObject);
        }
    }

    public void OnHitSuper(TestPlayer player)
    {
        //this.Stop();

        //player.rb.velocity = Vector3.zero;

        this.moving = false;

        this.victim = player;

        player.attackStuns.Add(this.gameObject);
        player.OnHit.Invoke();


        this.user.rb.isKinematic = true;
        player.rb.isKinematic = true;

        player.gameObject.transform.position = new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 1.2f), player.transform.position.y, 0f);


        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.animations != null)
            this.animations.SetGrabbingPose();


        this.StartCoroutine(this.HitSuperCoroutine(player));
    }

    public void StopSuper(TestPlayer player)
    {

        player.attackStuns.Remove(this.gameObject);

        /*this.user.rb.isKinematic = false;
        player.rb.isKinematic = false;*/

        this.user.attackStuns.Remove(this.gameObject);

        this.onGoing = false;
        this.victim = null;
    }

    IEnumerator HitSuperCoroutine(TestPlayer player)
    {
        yield return new WaitForSeconds(0.001f);
        if (this.animations != null)
            this.animations.SetGrabbingPose();

        float currentTime = 0;
        float duration = 0.025f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);


            if (player.animations != null)
            {
                player.animations.SetDefaultPose();
                player.animations.body.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            }
                

            if (this.animations != null)
                this.animations.SetGrabbingPose();
            yield return null;
        }

        if (this.animations != null)
            this.animations.SetGrabbingPose();

        yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.Pointt2();

        this.victimGone = true;

        player.preventDeath = true;

        if (this.dissapearEffect != null)
        {
            GameObject dissapearParticlePrefab = this.dissapearEffect;
            //dissapearParticlePrefab = Instantiate(dissapearParticlePrefab, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, 0f), Quaternion.Euler(0, 0, 0));

            //dissapearParticlePrefab = Instantiate(dissapearParticlePrefab, new Vector3(player.transform.position.x, player.transform.position.y + 1.95f, 0f), Quaternion.Euler(0, 0, 0));

            dissapearParticlePrefab = Instantiate(dissapearParticlePrefab, new Vector3(player.transform.position.x, player.transform.position.y + 1.95f, 0f), Quaternion.Euler(0, 0, 0));
        }
        //yield return new WaitForSeconds(0.05f);




        currentTime = 0;
        duration = 0.1f;
        float targetScale = 0f;
        float startScale = 1f;
        float startScaleX = this.animations.body.transform.localScale.x;
        float startScaleZ = this.animations.body.transform.localScale.z;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            player.animations.body.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration));
            //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
            yield return null;
        }




        if (player.animations != null)
            player.animations.body.gameObject.SetActive(false);

        if (player.collision != null)
            player.collision.enabled = false;

        if (player.animations != null)
        {
            player.animations.SetDefaultPose();
            //player.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);
            //player.animations.body.transform.localPosition = new Vector3(0f, 1.95f, 0f);

            player.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);
        }


        /*if (this.dissapearEffect != null)
        {
            GameObject dissapearParticlePrefab = this.dissapearEffect;
            dissapearParticlePrefab = Instantiate(dissapearParticlePrefab, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, 0f), Quaternion.Euler(0, 0, 0));
        }*/

        player.transform.position = new Vector3(player.transform.position.x, 9f, 0f);

        /*yield return new WaitForSeconds(0.25f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.25f);*/

        yield return new WaitForSeconds(0.5f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.attackStuns.Remove(this.gameObject);
        this.user.rb.isKinematic = false;



        int amount = 20;
        while (amount > 0)
        {
            /*if (this.animations != null)
                this.animations.body.transform.Rotate(0f, -1500 * Time.deltaTime, 0f);*/
            yield return new WaitForSeconds(0.1f);
            player.TakeDamage(this.user.transform.position, 2.5f);
            amount -= 1;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        float cosmoYPos = 8f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
            cosmoYPos = 10f;

        if (this.cosmoPortal != null)
        {
            GameObject cosmoPortalPrefab = this.cosmoPortal;
            cosmoPortalPrefab = Instantiate(cosmoPortalPrefab, new Vector3(player.transform.position.x, cosmoYPos, 0f), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.5f);

        

        if (player.animations != null)
            player.animations.body.gameObject.SetActive(true);

        if (player.collision != null)
            player.collision.enabled = true;

        player.preventDeath = false;

        /*if (player.health <= 0f)
        {
            player.animations.SetEyes(2);
            //player.ragdoll.rb.AddTorque(new Vector3(2100, 2100, 2100));
            //player.animations.body.transform.localScale = new Vector3(1f, 1f, -1f);
        }*/
            

        player.TakeDamage(this.user.transform.position, 0f, 0.35f, 0f, 0f, false, false, false, false);

        if (player.dead)
        {
            player.animations.SetEyes(2);
            player.ragdoll.rb.AddTorque(new Vector3(2000, -2000, 2000));
            //player.animations.body.transform.localScale = new Vector3(1f, 1f, -1f);
        }

        this.victimGone = false;

        player.rb.isKinematic = false;

        //this.user.rb.isKinematic = false;

        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.41f);

        this.StopSuper(player);
    }

    public override void Stop()
    {
        base.Stop();

        this.testBool = false;

        this.moving = false;
        this.user.rb.isKinematic = false;
        this.ChangeCollision(false);

        

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.victim != null)
        {
            this.victim.attackStuns.Remove(this.gameObject);
            this.victim.rb.isKinematic = false;

            if (this.victim.animations != null)
                this.victim.animations.body.gameObject.SetActive(true);

            if (this.victim.collision != null)
                this.victim.collision.enabled = true;

            this.victim.preventDeath = false;

            if (this.victimGone && this.cosmoPortal != null)
            {
                GameObject cosmoPortalPrefab = this.cosmoPortal;
                cosmoPortalPrefab = Instantiate(cosmoPortalPrefab, new Vector3(this.victim.transform.position.x, 8f, 0f), Quaternion.Euler(0, 0, 0));
                this.victim.transform.position = new Vector3(this.victim.transform.position.x, 9f, 0f);
            }

            if (this.victim.animations != null)
            {
                this.victim.animations.SetDefaultPose();
                //this.victim.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);
                //player.animations.body.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            }

            if (this.victim.health <= 0f)
            {
                this.victim.animations.SetEyes(2);
                this.victim.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);
            }
                


            

            this.victim.TakeDamage(this.user.transform.position, 0f, 0.35f, 0f, 0f, false, false, false, false);

            if (this.victim.dead)
                this.victim.ragdoll.rb.AddTorque(new Vector3(2000, -2000, 2000));

            //this.victim.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);

            this.victim = null;
        }

        this.victimGone = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public void ChangeCollision(bool crawl = false)
    {
        if (this.user.collision != null && this.user.collision is CapsuleCollider capsuleCollider)
        {
            if (crawl)
            {
                capsuleCollider.radius = 0.5f;
                capsuleCollider.height = 2f;

                capsuleCollider.center = new Vector3(0f, 1f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }
            else
            {
                capsuleCollider.radius = 0.5f;
                capsuleCollider.height = 3f;

                capsuleCollider.center = new Vector3(0f, 1.5f, 0f);
                //this.user.transform.position = new Vector3(this.user.transform.position.x, this.user.transform.position.y - 0.5f, 0f);
            }

        }
    }
}
