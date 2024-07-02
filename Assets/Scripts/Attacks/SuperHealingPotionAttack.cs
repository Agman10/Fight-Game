using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHealingPotionAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject startParticle;

    public GameObject potion;
    public GameObject corc;
    public GameObject liquid;

    public GameObject confused;

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

    public override void OnReset()
    {
        base.OnReset();
        if (this.user != null)
            this.user.damageMitigation = 0f;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            if (this.user.superCharge >= this.user.maxSuperCharge)
            {
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    if(this.user.health < this.user.maxHealth)
                    {
                        this.user.GiveSuperCharge(-(this.user.maxSuperCharge));
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.DrinkCoroutine());
                    }
                    else
                    {
                        this.user.AddStun(0.2f, true);
                        this.StartCoroutine(this.RefuseDrinkCoroutine());
                    }
                    

                }
            }

            
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

    private IEnumerator DrinkCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;



        if (this.animations != null)
            this.animations.SetPunchPose();

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        if (this.potion != null)
            this.potion.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.BallerDrinkStart();

        yield return new WaitForSeconds(0.1f);

        if (this.corc != null)
            this.corc.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.BallerDrink();




        /*float currentTime = 0;
        float duration = 1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);

            if (this.liquid != null)
                this.liquid.transform.localScale = new Vector3(1f, Mathf.Lerp(1f, 0f, currentTime / duration), 1f);

            this.user.TakeDamage(this.transform.position, -2f);
            yield return null;
        }*/

        //this.user.damageMitigation = 0.75f;
        this.user.damageMitigation = 0.5f;

        float maxAmount = 100;
        float amount = 0;
        while (amount < maxAmount && this.user.health < this.user.maxHealth)
        {
            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);*/

            yield return new WaitForSeconds(0.025f);
            this.user.TakeDamage(this.transform.position, -0.25f);

            if (this.liquid != null)
                this.liquid.transform.localScale = new Vector3(1f, Mathf.Lerp(1f, 0f, amount / maxAmount), 1f);

            amount += 1;
            //Debug.Log(amount);
        }

        this.user.damageMitigation = 0f;


        if (this.liquid != null && amount >= maxAmount)
        {
            this.liquid.SetActive(false);
            this.liquid.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        yield return new WaitForSeconds(0.2f);

        
        if (this.animations != null)
            this.animations.BallerDrinkStart();



        yield return new WaitForSeconds(0.1f);

        if (this.corc != null)
            this.corc.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        //yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.2f);

        if (this.potion != null)
            this.potion.SetActive(false);

        if (this.corc != null)
            this.corc.SetActive(true);

        if (this.liquid != null)
        {
            this.liquid.SetActive(true);
            this.liquid.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        yield return new WaitForSeconds(0.1f);



        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    
    public override void Stop()
    {
        base.Stop();

        /*if (this.potion != null)
            this.potion.SetActive(false);

        if (this.corc != null)
            this.corc.SetActive(true);

        if(this.liquid != null)
        {
            this.liquid.SetActive(true);
            this.liquid.transform.localScale = new Vector3(1f, 1f, 1f);
        }*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        if (this.confused != null)
        {
            this.confused.SetActive(false);
            this.confused.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //this.user.damageMitigation = 0f;
        this.StartCoroutine(this.HitCoroutine());

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.potion != null)
            this.potion.SetActive(false);

        if (this.corc != null)
            this.corc.SetActive(true);

        if (this.liquid != null)
        {
            this.liquid.SetActive(true);
            this.liquid.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


    private IEnumerator RefuseDrinkCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        

        if (this.potion != null)
            this.potion.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        /*if (this.animations != null)
            this.animations.SetPunchPose();*/

        if (this.animations != null)
            this.animations.BallerDrinkStart();

        yield return new WaitForSeconds(0.2f);

        /*if (this.animations != null)
            this.animations.BallerDrinkStart();*/

        if (this.confused != null)
        {
            this.confused.SetActive(true);
            this.confused.transform.localScale = new Vector3(this.user.transform.forward.z, 1f, 1f);
        }
            

        yield return new WaitForSeconds(0.2f);

        if (this.animations != null)
            this.animations.BallerDrinkStart(true);

        yield return new WaitForSeconds(0.4f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        if (this.confused != null)
        {
            this.confused.SetActive(false);
            this.confused.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        yield return new WaitForSeconds(0.2f);

        if (this.potion != null)
            this.potion.SetActive(false);

        if (this.corc != null)
            this.corc.SetActive(true);

        if (this.liquid != null)
        {
            this.liquid.SetActive(true);
            this.liquid.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        this.user.damageMitigation = 0f;
        /*if (!this.user.dead && this.onGoing)
        {
            //this.Stop();
            this.user.damageMitigation = 0f;
        }*/
    }
}
