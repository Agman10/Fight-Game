using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCounterAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool countering;

    public GameObject book;
    public GameObject fallenBook;

    public TestPlayer grabbedPlayer;

    public AudioSource teleportSfx;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && !this.countering)
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

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
        {
            this.user.OnHitFromPlayer += this.HitFromPlayer;
            /*this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
            this.user.OnReset += OnReset;*/
        }
        

        
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
        {
            this.user.OnHitFromPlayer -= this.HitFromPlayer;
            /*this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
            this.user.OnReset -= OnReset;*/
        }
        

        
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
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
            }
            
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.1f);


        this.user.damageMitigation = -0.5f;

        if (this.animations != null)
            this.animations.BookStart(true);

        yield return new WaitForSeconds(0.1f);

        this.user.FlipDirection();
        if (this.animations != null)
            this.animations.BookStart(true);

        yield return new WaitForSeconds(0.1f);




        if (this.book != null)
            this.book.SetActive(true);

        if (this.animations != null)
            this.animations.Book(0);

        yield return new WaitForSeconds(0.025f);



        //this.countering = true;
        this.SetCountering(true);

        if (this.book != null)
            this.book.SetActive(true);

        if (this.animations != null)
            this.animations.Book();

        //yield return new WaitForSeconds(0.4f);



        yield return new WaitForSeconds(0.35f);

        if (this.animations != null)
            this.animations.Book(0);




        //this.countering = false;
        this.SetCountering(false);

        this.user.damageMitigation = -0.5f;

        yield return new WaitForSeconds(0.025f);


        if (this.book != null)
            this.book.SetActive(false);


        if (this.animations != null)
            this.animations.BookStart(true);

        yield return new WaitForSeconds(0.05f);

        this.user.FlipDirection();
        if (this.animations != null)
            this.animations.BookStart(false);

        yield return new WaitForSeconds(0.05f);


        this.user.damageMitigation = 0f;


        if (this.animations != null)
            this.animations.SetDefaultPose();

        //yield return new WaitForSeconds(0.1f);
        //this.user.FlipDirection();
        yield return new WaitForSeconds(0.3f);


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator CounterCoroutine(TestPlayer player)
    {
        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;*/

        //if(this.fallenBook != null)

        


        player.attackStuns.Add(this.gameObject);

        this.grabbedPlayer = player;

        //player.AddStun(0.2f, true);

        player.OnHit?.Invoke();
        //player.AddStun(1f, true);
        player.rb.isKinematic = true;

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = true;
        }*/

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.05f);
        if (this.fallenBook != null)
        {
            GameObject fallenBookPrefab = this.fallenBook;
            fallenBookPrefab = Instantiate(fallenBookPrefab, new Vector3(this.user.transform.position.x + (this.user.transform.forward.z * 0.45f), 0f, 0f), Quaternion.Euler(0, 0, 0));
            fallenBookPrefab.transform.localScale = new Vector3(this.transform.forward.z, 1f, 1f);
        }

        if (this.teleportSfx != null)
        {
            //this.teleportSfx.time = 0.01f;
            this.teleportSfx.Play();
        }

        //player.rb.isKinematic = false;
        this.SetCountering(false);
        this.user.rb.isKinematic = true;

        float maxXPos = 12.5f;

        if (GameManager.Instance != null && GameManager.Instance.gameMode == 1)
        {
            maxXPos = 9.5f;
        }


        if (player.transform.position.x > maxXPos && player.transform.forward.z <= -1f)
            player.transform.position = new Vector3(maxXPos, player.transform.position.y, 0f);
        else if (player.transform.position.x < -maxXPos && player.transform.forward.z > -1f)
            player.transform.position = new Vector3(-maxXPos, player.transform.position.y, 0f);

        this.user.transform.position = new Vector3(player.transform.position.x + (player.transform.forward.z * -1.5f), player.transform.position.y, 0f);
        this.user.LookAtTarget();

        if (this.animations != null)
            this.animations.CounterPointing();

        if (this.book != null)
            this.book.SetActive(false);

        /*yield return new WaitForSeconds(0.01f);
        this.user.LookAtTarget();

        if (this.animations != null)
            this.animations.FlameGrabHitPose();*/

        /*yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.CounterPointing();*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        player.rb.isKinematic = false;


        /*if (player.animations != null)
            player.animations.SetDefaultPose();*/


        /*yield return new WaitForSeconds(0.3f);
        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }

        yield return new WaitForSeconds(0.1f);*/


        this.user.GiveSuperCharge(2f);
        player.GiveSuperCharge(1f);

        yield return new WaitForSeconds(0.4f);

        //player.rb.isKinematic = false;

        player.AddStun(0.4f, true);
        player.attackStuns.Remove(this.gameObject);

        this.user.rb.isKinematic = false;
        
        this.grabbedPlayer = null;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public void HitFromPlayer(TestPlayer player)
    {
        
        if(player != null && player != this.user)
        {
            //Debug.Log("test" + player.name);

            if (this.countering && this.grabbedPlayer == null)
            {
                //Debug.Log(player.name + " " + player.playerNumber);
                this.StopAllCoroutines();
                //this.SetCountering(false);
                this.StartCoroutine(this.CounterCoroutine(player));
                /*if (this.user.soundEffects != null)
                {
                    this.user.soundEffects.StopHitSound();
                }*/
                //player.OnHit?.Invoke();
            }
                
        }
    }

    public override void Stop()
    {
        base.Stop();

        if (this.book != null)
            this.book.SetActive(false);

        if (this.user != null)
            this.user.LookAtTarget();

        //this.countering = false;


        //this.SetCountering(false);

        this.countering = false;
        this.user.knockbackInvounrability = false;
        this.user.rb.isKinematic = false;

        //this.Test();
        

        this.StartCoroutine(this.HitCoroutine());

        if (this.grabbedPlayer != null)
        {
            this.grabbedPlayer.rb.isKinematic = false;

            this.grabbedPlayer.attackStuns.Remove(this.gameObject);

            this.grabbedPlayer = null;
        }

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void SetCountering(bool counter)
    {
        if (counter)
        {
            this.countering = true;
            if (this.user != null)
            {
                this.user.knockbackInvounrability = true;
                this.user.damageMitigation = 1f;
                this.user.rb.isKinematic = true;
            }
        }
        else
        {
            this.countering = false;
            if (this.user != null)
            {
                this.user.knockbackInvounrability = false;
                this.user.damageMitigation = 0f;
                this.user.rb.isKinematic = false;
            }
        }
    }

    public void Test()
    {
        this.countering = false;
        if (this.user != null)
        {
            this.user.knockbackInvounrability = false;
            this.user.damageMitigation = 0f;
            this.user.rb.isKinematic = false;

        }
        /*this.countering = false;
        this.user.knockbackInvounrability = false;
        this.user.rb.isKinematic = false;*/
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        this.user.damageMitigation = 0f;

        //this.SetCountering(false);
        /*if (!this.user.dead && this.onGoing)
        {
            //this.Stop();
            this.user.damageMitigation = 0f;
        }*/
    }


}
