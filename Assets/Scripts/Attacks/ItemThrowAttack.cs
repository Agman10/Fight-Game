using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public bool isSuper = false;
    public GameObject startParticle;

    public bool cantBeCanceled = false;

    [Space]
    public Bomb bomb;
    public Bomb bigBomb;
    public Bomb flashBang;
    public SmokeGrenade smokeGrenade;
    

    public FireBall fireBall;
    public FireBall holyWater;
    public FireBall pebble;
    public FireBall snowball;
    public FireBall hammer;
    public FireBall metalPipe;
    public MolotovProjectile molotov;

    public SpikeBall spikeBall;

    public Spring spring;

    public ThrowPotion healingPotion;
    public ThrowPotion harmingPotion;

    [Space]
    //public MeteorRainSpawner meteorRainSpawner;
    public MeteorRainSpawner meteorRainSpawnerP1;
    public MeteorRainSpawner meteorRainSpawnerP2;

    [Space]
    public FoodItem donut;
    public FoodItem donut2;
    public FoodItem donut3;
    public FoodItem enegryDrink;
    public FoodItem hamburger;
    public FoodItem melon;
    public FoodItem medkit;
    public FoodItem cherry;
    public FoodItem coffee;
    public FoodItem pill;
    public FoodItem badPill;
    public FoodItem carrot;
    public FoodItem chocolate;
    public FoodItem orange;
    public FoodItem cake;
    public FoodItem pie;

    [Space]
    public AudioSource throwSfx;
    public AudioSource nothingSfx;
    public AudioSource switchPlaceSfx;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing && !this.cantBeCanceled)
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
            if (!this.isSuper)
            {
                this.user.AddStun(0.2f, true);
                //this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.ThrowItemCoroutine());
                //this.StartCoroutine(this.SuperItemThrowCoroutine());

                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {

                }
            }
            else
            {
                if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
                {
                    this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
                    this.user.AddStun(0.2f, true);
                    //this.user.AddStun(0.2f, false);
                    //this.StartCoroutine(this.ThrowItemCoroutine());
                    this.StartCoroutine(this.SuperItemThrowCoroutine());

                }
            }

            
        }
    }

    private IEnumerator ThrowItemCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        float throwSpeed = 1f;

        //MAKE SO THE RANDOM NUMBER IS HERE INSTEAD SO SOME THING CAN HAPPEN DURING THROW ON SPECIFIC NUMBER OR PLAY SFX EARLIER

        //int number = Random.Range(1, 1001);

        this.user.animations.ItemThrow(0);

        yield return new WaitForSeconds(0.2f * throwSpeed);

        this.user.animations.ItemThrow(1);

        yield return new WaitForSeconds(0.05f * throwSpeed);

        this.user.animations.ItemThrow(2);

        /*if (this.nothingSfx != null)
            this.nothingSfx.Play();*/

        if (this.throwSfx != null)
        {
            //this.throwSfx.time = 0.01f;
            this.throwSfx.Play();
        }

        yield return new WaitForSeconds(0.05f * throwSpeed);

        //MAYBE HAVE ITEMS THAT ARE THROWN FORWARD? ADD AN ANIMATION FOR IT IF ADDED

        this.user.animations.ItemThrow(3);

        //THROW RANDOM ITEM HERE

        //this.ThrowBomb();
        //this.ThrowFood();
        //this.ThrowFireBall();
        //this.ThrowHolyWater();
        //this.SwitchPlace();
        //this.ThrowFlashBang();
        //this.ThrowPebble();
        //this.ThrowSnowball();

        /*int fireBallNumber = Random.Range(1, 1001);
        if (fireBallNumber <= 500)
        {
            this.ThrowSnowball();
        }
        else
        {
            this.ThrowPebble();
        }*/

        //this.ThrowBigBomb();

        //this.ThrowSpikeBall();

        //this.ThrowHammer();
        //this.ThrowMetalPipe();

        //this.ThrowSpring();

        //this.ThrowPotion();

        //this.ThrowMolotov();

        //this.ThrowSmoke();

        //this.ThrowMeteorRain();

        //this.ThrowRandomItem();

        int number = Random.Range(1, 101);

        if (number <= 3)
            this.ThrowRandomLegendaryItem();
        else
            this.ThrowRandomItem();


        yield return new WaitForSeconds(0.3f * throwSpeed);

        this.user.animations.ItemThrow(4);

        yield return new WaitForSeconds(0.05f * throwSpeed);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f * throwSpeed);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ThrowRandomItem()
    {
        int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number <= 100)
        {
            int bombNumber = Random.Range(1, 1001);
            if (bombNumber <= 550)
            {
                this.ThrowBomb();
            }
            else
            {
                this.ThrowBigBomb();
            }
            /*else if (bombNumber > 250 && bombNumber <= 450)
            {
                this.ThrowBomb();
            }*/
            //this.ThrowBomb();
        }
        else if (number > 100 && number <= 300)
        {
            this.ThrowFood();
        }
        else if (number > 300 && number <= 600)
        {
            int fireBallNumber = Random.Range(1, 1001);
            if (fireBallNumber <= 250)
            {
                this.ThrowHolyWater();
            }
            else if (fireBallNumber > 250 && fireBallNumber <= 400)
            {
                this.ThrowPebble();
            }
            else if (fireBallNumber > 400 && fireBallNumber <= 550)
            {
                this.ThrowSnowball();
            }
            else if (fireBallNumber > 550 && fireBallNumber <= 650)
            {
                this.ThrowPotion();
            }
            else
            {
                this.ThrowFireBall();
            }
            
        }
        else if (number > 600 && number <= 715)
        {
            //this.ThrowHammer();

            int hammerNumber = Random.Range(1, 1001);
            if (hammerNumber <= 800)
                this.ThrowHammer();
            else
                this.ThrowMetalPipe();
        }
        else if (number > 715 && number <= 765)
        {
            this.ThrowSpikeBall();
        }
        else if (number > 765 && number <= 815)
        {
            this.ThrowPotion();
        }
        else if (number > 815 && number <= 865)
        {
            this.ThrowSpring();
        }
        else if (number > 865 && number <= 870)
        {
            this.ThrowFlashBang();
        }
        else if (number > 870 && number <= 910)
        {
            this.ThrowMolotov();
        }
        else if (number > 910 && number <= 950)
        {
            this.SwitchPlace();
        }
        else
        {
            this.user.animations.SetEyes(4);

            if (this.nothingSfx != null)
                this.nothingSfx.Play();
        }

        /*if (number <= 910)
        {
            if (this.throwSfx != null)
                this.throwSfx.Play();
        }
        else if (number > 910 && number <= 950)
        {
            if (this.switchPlaceSfx != null)
                this.switchPlaceSfx.Play();
        }
        else
        {
            if (this.nothingSfx != null)
                this.nothingSfx.Play();
        }*/
    }

    public void ThrowRandomLegendaryItem()
    {
        this.ThrowMeteorRain();
    }


    public void ThrowBomb()
    {
        if (this.bomb != null && this.user != null)
        {
            Bomb bombPrefab = this.bomb;
            float forward = this.user.transform.forward.z;

            bombPrefab = Instantiate(bombPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), Quaternion.Euler(0, 0, 0));
            bombPrefab.KnockBack(new Vector3(forward * 150, 500f, 0));

            if (this.user != null)
                bombPrefab.SetOwner(this.user);
        }
    }
    public void ThrowBigBomb()
    {
        if (this.bigBomb != null && this.user != null)
        {
            Bomb bombPrefab = this.bigBomb;
            float forward = this.user.transform.forward.z;

            bombPrefab = Instantiate(bombPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), Quaternion.Euler(0, 0, 0));
            bombPrefab.KnockBack(new Vector3(forward * 150 * 2, 500 * 2f, 0));

            if (this.user != null)
                bombPrefab.SetOwner(this.user);
        }
    }

    public void ThrowFlashBang()
    {
        if (this.flashBang != null && this.user != null)
        {
            Bomb flashBangPrefab = this.flashBang;
            float forward = this.user.transform.forward.z;

            flashBangPrefab = Instantiate(flashBangPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), Quaternion.Euler(0, 0, 0));
            flashBangPrefab.KnockBack(new Vector3(forward * 150, 500f, 0));

            if (this.user != null)
                flashBangPrefab.SetOwner(this.user);
        }
    }

    public void ThrowFood()
    {
        /*if(this.donut != null)
        {
            
        }*/

        FoodItem foodPrefab = null;

        int number = Random.Range(1, 10001);
        //int number = Random.Range(4001, 7001);
        //Debug.Log(number);
        if (number <= 2500)
        {
            int donutNumber = Random.Range(1, 1001);
            if (donutNumber <= 250)
                foodPrefab = this.donut2;
            else if (donutNumber > 250 && donutNumber <= 350)
                foodPrefab = this.donut3;
            else
                foodPrefab = this.donut;
        }
        else if (number > 2500 && number <= 3000)
        {
            foodPrefab = this.hamburger;

            /*int foodNumber = Random.Range(0, 4);
            Debug.Log(foodNumber);
            if (foodNumber <= 1)
                foodPrefab = this.hamburger;
            else if (foodNumber == 2)
                foodPrefab = this.cake;
            else
                foodPrefab = this.pie;*/
        }
        else if (number > 3000 && number <= 3500)
        {
            int pillNumber = Random.Range(1, 101);
            if (pillNumber <= 5)
                foodPrefab = this.badPill;
            else
                foodPrefab = this.pill;
        }
        else if(number > 3500 && number <= 7500)
        {
            //int foodNumber = Random.Range(1, 6);
            int foodNumber = Random.Range(1, 8);
            if (foodNumber <= 1)
                foodPrefab = this.melon;
            else if (foodNumber > 1 && foodNumber <= 2)
                foodPrefab = this.cherry;
            else if (foodNumber > 2 && foodNumber <= 3)
                foodPrefab = this.carrot;
            else if (foodNumber > 3 && foodNumber <= 4)
                foodPrefab = this.chocolate;
            else if (foodNumber > 4 && foodNumber <= 5)
                foodPrefab = this.orange;
            else if (foodNumber > 5 && foodNumber <= 6)
                foodPrefab = this.cake;
            else
                foodPrefab = this.pie;

            //Debug.Log(foodNumber);
        }
        /*else if (number > 3800 && number <= 5500)
        {
            foodPrefab = this.melon;
        }
        else if (number > 5500 && number <= 7500)
        {
            foodPrefab = this.cherry;
        }*/
        else if (number > 7500 && number <= 7501)
        {
            foodPrefab = this.medkit;
        }
        else
        {
            int energyNumber = Random.Range(1, 1001);
            if (energyNumber <= 500)
                foodPrefab = this.coffee;
            else
                foodPrefab = this.enegryDrink;
        }


        /*else if (number > 7501 && number <= 9501)
        {
            int energyNumber = Random.Range(1, 1001);
            if (energyNumber <= 500)
                foodPrefab = this.coffee;
            else
                foodPrefab = this.enegryDrink;
        }
        else
        {
            int foodNumber = Random.Range(1, 2);
            if (foodNumber <= 1)
                foodPrefab = this.cake;
            else
                foodPrefab = this.pie;
        }*/

        /*else if (number > 250 && number <= 500)
        {

        }
        else if (number > 500 && number <= 900)
        {
            

        }
        else
        {

        }*/

        //foodPrefab = this.pill;

        if (foodPrefab != null)
        {
            float forward = this.user.transform.forward.z;

            foodPrefab = Instantiate(foodPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            foodPrefab.model.transform.localScale = this.user.ragdoll.transform.localScale;
            foodPrefab.KnockBack(new Vector3(forward * 140, 700f, 0));
            //foodPrefab.KnockBack(new Vector3(forward * 250, 600f, 0));
        }

        
    }

    public void ThrowFireBall()
    {
        if (this.fireBall != null)
        {
            FireBall fireBallPrefab = this.fireBall;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            fireBallPrefab = Instantiate(fireBallPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                fireBallPrefab.belongsTo = this.user;

            fireBallPrefab.KnockBack(new Vector3(forward * 250f, 500f, 0));
        }
    }

    public void ThrowHolyWater()
    {
        if (this.holyWater != null)
        {
            FireBall holyWaterPrefab = this.holyWater;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            holyWaterPrefab = Instantiate(holyWaterPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                holyWaterPrefab.belongsTo = this.user;

            holyWaterPrefab.KnockBack(new Vector3(forward * 250f, 500f, 0));
        }
    }

    public void SwitchPlace()
    {
        if(this.user != null && this.user.tempOpponent != null)
        {
            Vector3 userPos = this.user.transform.position;
            Vector3 opponentPos = this.user.tempOpponent.transform.position;


            if (this.user.tempOpponent.dead)
            {
                opponentPos = new Vector3(this.user.tempOpponent.ragdoll.transform.position.x, this.user.tempOpponent.ragdoll.transform.position.y, 0f);

                this.user.transform.position = opponentPos;
                this.user.tempOpponent.ragdoll.transform.position = userPos;
                
            }
            else
            {
                this.user.transform.position = opponentPos;
                this.user.tempOpponent.transform.position = userPos;
            }
                

            

            this.user.LookAtTarget();
        }

        if (this.switchPlaceSfx != null)
            this.switchPlaceSfx.Play();

    }


    public void ThrowSpikeBall()
    {
        if (this.spikeBall != null && this.user != null)
        {
            SpikeBall spikeBallPrefab = this.spikeBall;
            float forward = this.user.transform.forward.z;

            spikeBallPrefab = Instantiate(spikeBallPrefab, new Vector3(this.user.transform.position.x + (forward * 1f), this.user.transform.position.y + 3, 0), Quaternion.Euler(0, 0, 0));
            spikeBallPrefab.KnockBack(new Vector3(forward * 20000f, 70000f, 0));

            if (this.user != null)
                spikeBallPrefab.SetOwner(this.user);
        }
    }

    public void ThrowPebble()
    {
        if (this.pebble != null)
        {
            FireBall pebblePrefab = this.pebble;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            pebblePrefab = Instantiate(pebblePrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                pebblePrefab.belongsTo = this.user;

            pebblePrefab.KnockBack(new Vector3(forward * 300f, 600f, 0));
        }
    }

    

    public void ThrowSnowball()
    {
        if (this.snowball != null)
        {
            FireBall snowballPrefab = this.snowball;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            snowballPrefab = Instantiate(snowballPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                snowballPrefab.belongsTo = this.user;

            snowballPrefab.KnockBack(new Vector3(forward * 350f, 600f, 0));
        }
    }

    public void ThrowHammer()
    {
        if (this.hammer != null)
        {
            FireBall hammerPrefab = this.hammer;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            hammerPrefab = Instantiate(hammerPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                hammerPrefab.belongsTo = this.user;

            hammerPrefab.KnockBack(new Vector3(forward * 300f, 600f, 0));
        }
    }

    public void ThrowMetalPipe()
    {
        if (this.metalPipe != null)
        {
            FireBall metalPipePrefab = this.metalPipe;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            metalPipePrefab = Instantiate(metalPipePrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                metalPipePrefab.belongsTo = this.user;

            metalPipePrefab.KnockBack(new Vector3(forward * 300f, 600f, 0));
        }
    }

    public void ThrowSpring()
    {
        if (this.spring != null)
        {
            Spring springPrefab = this.spring;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            springPrefab = Instantiate(springPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            /*if (this.user != null)
                springPrefab.belongsTo = this.user;*/

            float randomKnockback = Random.Range(200f, 350f);
            springPrefab.KnockBack(new Vector3(forward * randomKnockback, 600f, 0));
            //springPrefab.KnockBack(new Vector3(forward * 300f, 600f, 0));
        }
    }

    public void ThrowMolotov()
    {
        if (this.molotov != null)
        {
            MolotovProjectile molotovPrefab = this.molotov;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            molotovPrefab = Instantiate(molotovPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                molotovPrefab.owner = this.user;

            molotovPrefab.KnockBack(new Vector3(forward * 260f, 600f, 0));
        }
    }

    public void ThrowSmoke()
    {
        if (this.bomb != null && this.user != null)
        {
            SmokeGrenade smokePrefab = this.smokeGrenade;
            float forward = this.user.transform.forward.z;

            smokePrefab = Instantiate(smokePrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), Quaternion.Euler(0, 0, 0));
            smokePrefab.KnockBack(new Vector3(forward * 150, 500f, 0));
        }
    }

    public void ThrowMeteorRain()
    {
        MeteorRainSpawner meteorRainSpawnerPrefab = null;

        if (this.user != null)
        {
            if (this.user.playerNumber == 1 && this.meteorRainSpawnerP1 != null)
                meteorRainSpawnerPrefab = this.meteorRainSpawnerP1;
            else if (this.user.playerNumber == 2 && this.meteorRainSpawnerP2)
                meteorRainSpawnerPrefab = this.meteorRainSpawnerP2;
        }

        float forward = this.user.transform.forward.z;

        if(meteorRainSpawnerPrefab != null)
        {
            meteorRainSpawnerPrefab = Instantiate(meteorRainSpawnerPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);

            if (this.user != null)
                meteorRainSpawnerPrefab.owner = this.user;
        }
        
    }

    //MAKE IT SO IT SUPPORTS MULTIPLE POTIONS
    //IMPROVE CODE PLS
    public void ThrowPotion()
    {
        if (this.healingPotion != null)
        {
            int number = Random.Range(1, 1001);

            ThrowPotion throwPotionPrefab = this.healingPotion;
            if(number <= 500 && this.harmingPotion != null)
                throwPotionPrefab = this.harmingPotion;
            //ThrowPotion throwPotionPrefab = this.healingPotion;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            throwPotionPrefab = Instantiate(throwPotionPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            if (this.user != null)
                throwPotionPrefab.SetOwner(this.user);

            throwPotionPrefab.KnockBack(new Vector3(forward * 250f, 500f, 0));
        }
    }

    private IEnumerator SuperItemThrowCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        if (this.user.soundEffects != null)
            this.user.soundEffects.PlaySuperSfx();

        if (this.startParticle != null)
        {
            GameObject startParticlePrefab = this.startParticle;
            startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
        }

        this.user.animations.ItemThrow(0);
        yield return new WaitForSeconds(0.2f);

        this.cantBeCanceled = true;
        //this.user.knockbackInvounrability = true;

        this.user.damageMitigation = 0.75f;

        this.user.rb.isKinematic = false;

        int amount = 5;
        //float waitTime = 0.1f;
        while (amount > 0)
        {
            this.user.animations.ItemThrow(0);

            yield return new WaitForSeconds(0.025f);

            this.user.animations.ItemThrow(1);

            yield return new WaitForSeconds(0.025f);

            this.user.animations.ItemThrow(2);

            if (this.throwSfx != null)
            {
                //this.throwSfx.time = 0.01f;
                this.throwSfx.Play();
            }

            yield return new WaitForSeconds(0.025f);

            this.user.animations.ItemThrow(3);

            //this.ThrowRandomItem();
            //this.ThrowFood();
            //this.ThrowRandomItemBombAndFire();

            int number = Random.Range(1, 101);

            if (number <= 3)
                this.ThrowRandomLegendaryItem();
            else
                this.ThrowRandomItem();

            yield return new WaitForSeconds(0.025f);

            this.user.animations.ItemThrow(4);

            yield return new WaitForSeconds(0.025f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(0.025f);

            amount -= 1;
            //Debug.Log(amount);
        }

        this.user.damageMitigation = 0f;

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.cantBeCanceled = false;
        //this.user.knockbackInvounrability = false;

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public override void Stop()
    {
        base.Stop();

        if (this.isSuper)
        {
            this.user.rb.isKinematic = false;

            this.StartCoroutine(this.HitCoroutine());
        }

        this.cantBeCanceled = false;
        //this.user.knockbackInvounrability = false;


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        this.user.damageMitigation = 0f;
    }


    public void ThrowRandomItemBombAndFire()
    {
        int number = Random.Range(1, 1001);

        if (number <= 500)
        {
            this.ThrowBigBomb();
            /*int bombNumber = Random.Range(1, 1001);
            if (bombNumber <= 550)
            {
                this.ThrowBomb();
            }
            else
            {
                this.ThrowBigBomb();
            }*/
        }
        else
        {
            this.ThrowFireBall();
        }
    }
}
