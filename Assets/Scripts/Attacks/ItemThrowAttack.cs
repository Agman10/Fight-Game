using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public Bomb bomb;
    public Bomb bigBomb;
    public Bomb flashBang;

    public FoodItem donut;
    public FoodItem donut2;
    public FoodItem enegryDrink;
    public FoodItem hamburger;

    public FireBall fireBall;
    public FireBall holyWater;
    public FireBall pebble;
    public FireBall snowball;

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
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.animations.ItemThrow(0);

        yield return new WaitForSeconds(0.2f);

        this.user.animations.ItemThrow(1);

        yield return new WaitForSeconds(0.05f);

        this.user.animations.ItemThrow(2);

        yield return new WaitForSeconds(0.05f);

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

        this.ThrowRandomItem();

        yield return new WaitForSeconds(0.3f);

        this.user.animations.ItemThrow(4);

        yield return new WaitForSeconds(0.05f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        yield return new WaitForSeconds(0.3f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void ThrowRandomItem()
    {
        int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number <= 200)
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
        else if (number > 200 && number <= 400)
        {
            this.ThrowFood();
        }
        else if (number > 400 && number <= 800)
        {
            int fireBallNumber = Random.Range(1, 1001);
            if (fireBallNumber <= 250)
            {
                this.ThrowHolyWater();
            }
            else if (fireBallNumber > 250 && fireBallNumber <= 450)
            {
                this.ThrowPebble();
            }
            else if (fireBallNumber > 450 && fireBallNumber <= 600)
            {
                this.ThrowSnowball();
            }
            else
            {
                this.ThrowFireBall();
            }
            
        }
        else if (number > 800 && number <= 900)
        {
            this.SwitchPlace();
        }

        else if (number > 900 && number <= 910)
        {
            this.ThrowFlashBang();
        }
        else
        {
            this.user.animations.SetEyes(4);
        }
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
            bombPrefab.KnockBack(new Vector3(forward * 150, 500f, 0));

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

        int number = Random.Range(1, 1001);
        //Debug.Log(number);
        if (number <= 400)
        {
            int donutNumber = Random.Range(1, 1001);
            if (donutNumber <= 250)
                foodPrefab = this.donut2;
            else
                foodPrefab = this.donut;
        }
        else if (number > 400 && number <= 650)
        {
            foodPrefab = this.hamburger;
        }
        else
        {
            foodPrefab = this.enegryDrink;
        }

        /*else if (number > 250 && number <= 500)
        {

        }
        else if (number > 500 && number <= 900)
        {
            

        }
        else
        {

        }*/

        if(foodPrefab != null)
        {
            float forward = this.user.transform.forward.z;

            foodPrefab = Instantiate(foodPrefab, new Vector3(this.user.transform.position.x + (forward * 0.8f), this.user.transform.position.y + 3, 0), this.user.transform.rotation);
            foodPrefab.model.transform.localScale = this.user.ragdoll.transform.localScale;
            foodPrefab.KnockBack(new Vector3(forward * 250, 600f, 0));
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

    public override void Stop()
    {
        base.Stop();
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
