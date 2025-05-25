using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownEntityAI : CharacterAI
{
    // Start is called before the first frame update
    void Start()
    {
        //this.StartAI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartAI()
    {
        base.StartAI();
        this.StartCoroutine(this.MoveRandomlyCoroutineGeneral());
        //this.StartCoroutine(this.AttackTests());
        //this.StartCoroutine(TestCoroutine());
    }


    private IEnumerator MoveRandomlyCoroutineGeneral()
    {
        float number = Random.Range(1, 101);
        //float number = Random.Range(1, 21);

        float xMovement = 0f;

        float waitTime = Random.Range(0.1f, 0.5f);

        if (number <= 60)
        {
            xMovement = this.player.transform.forward.z * 1f;
        }
        else if (number > 60 && number <= 80)
        {
            xMovement = this.player.transform.forward.z * -1f;

            waitTime = Random.Range(0.1f, 0.2f);
        }
        else
        {
            xMovement = 0f;

            waitTime = Random.Range(0.05f, 0.2f);
        }

        this.playerInput.moveInput = new Vector3(xMovement, 0f, 0f);



        //yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        yield return new WaitForSeconds(waitTime);


        /*if (this.EnemyDistance() <= 1.5f && this.EnemyYDistance() >= 2.2f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
        {
            this.Super(3);
        }*/
        if (this.EnemyXDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead)
        {
            //Debug.Log(EnemyDistance());
            this.Special1(2);
        }
        else
        {
            if (Random.Range(1, 101) <= 30f)
            {
                //this.playerInput.JumpInput?.Invoke(true);
                this.playerInput.jumping = true;
                yield return new WaitForSeconds(0.01f);
                this.playerInput.jumping = false;

                if (Random.Range(1, 101) <= 10)
                {
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
                    if (Random.Range(1, 101) <= 50)
                        this.Punch(Random.Range(0, 4));
                    else
                        this.Kick(Random.Range(0, 4));
                }
            }
        }

        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));

        float number2 = Random.Range(1, 101);
        //float number2 = Random.Range(1, 20);

        //this.Special2(3);
        //Debug.Log(EnemyDistance());

        //this.RandomSpecial1(1, 1, 0, 1);

        if (this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            number2 = Random.Range(1, 201);

        /*if (this.EnemyDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
        {
            this.Super(3);
        }*/
        if (this.EnemyXDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead)
        {
            //Debug.Log(EnemyDistance());

            this.Special1(2);

        }
        else
        {
            if (number2 <= 30)
            {
                //this.Special1(Random.Range(0, 4));
                //this.Special1(RandomSpecialDirection(1, 1, 0, 0));

                this.Special1(RandomSpecialDirection(1, 1, 0, 1));

                /*if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                    this.Special1(RandomSpecialDirection(1, 1, 0, 1));
                else
                    this.Special1(RandomSpecialDirection(1, 1, 0, 1));*/
            }
            else if (number2 > 30 && number2 <= 60)
            {
                //this.Special2(Random.Range(0, 4));

                //this.Special2(RandomSpecialDirection(1, 1, 1, 1));

                if (Mathf.Abs(this.player.rb.velocity.y) <= 0f) //on ground
                    this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                else //in air
                    this.Special2(RandomSpecialDirection(2, 2, 3, 0));
                //this.Special2(3);
            }
            else if (number2 > 60 && number2 <= 75)
            {
                yield return this.PunchCoroutine();


            }
            else if (number2 > 75 && number2 <= 100)
            {
                if (!this.player.tempOpponent.dead && this.player.superCharge >= this.player.maxSuperCharge / 2)
                {
                    //this.Super(Random.Range(0, 4));

                    //this.Super(RandomSpecialDirection(1, 1, 1, 1));

                    int neutralChance = 0;
                    if (this.CanDoNeutralSuper())
                        neutralChance = 3;

                    Debug.Log(this.CanDoNeutralSuper());

                    if (this.player.superCharge < this.player.maxSuperCharge)
                    {
                        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                            this.Super(RandomSpecialDirection(neutralChance, 3, 1, 0));
                        else
                            this.Super(RandomSpecialDirection(neutralChance, 0, 1, 0));
                    }
                    else
                    {
                        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                            this.Super(RandomSpecialDirection(neutralChance, 1, 1, 3));
                        else
                            this.Super(RandomSpecialDirection(neutralChance, 0, 1, 0));
                    }


                    

                    //for mike baller
                    /*if (this.EnemyXDistance() <= 2f)
                    {
                        Debug.Log(EnemyXDistance());
                        this.Super(RandomSpecialDirection(0, 0, 1, 0));
                    }
                    else
                    {
                        this.Super(RandomSpecialDirection(1, 1, 0, 0));
                    }*/



                }
                else if (this.player.tempOpponent.dead)
                {
                    this.Taunt(Random.Range(0, 4));
                    if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                        yield return new WaitForSeconds(Random.Range(1f, 2f));
                }
                else
                {
                    if (Random.Range(1, 101) <= 50)
                    {
                        this.Special1(RandomSpecialDirection(1, 1, 0, 1));
                    }
                    else
                    {
                        //this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f) //on ground
                            this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                        else //in air
                            this.Special2(RandomSpecialDirection(1, 1, 1, 0));
                    }
                }

                //this.Special2(Random.Range(0, 4));
                //this.Special2(3);
            }
            else
            {
                if (this.player.tempOpponent.dead)
                {
                    this.Taunt(Random.Range(0, 4));
                    if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                        yield return new WaitForSeconds(Random.Range(1f, 2f));
                }
            }
        }



        this.playerInput.moveInput = MoveInput(Random.Range(0, 4));

        this.StartCoroutine(this.MoveRandomlyCoroutineGeneral());
    }


    public IEnumerator PunchCoroutine()
    {
        float randomPunch = Random.Range(1, 101);
        if (randomPunch <= 50)
            this.Punch(Random.Range(0, 4));
        else
            this.Kick(Random.Range(0, 4));

        if (Random.Range(1, 101) <= 70)
        {
            while (this.player.attackStuns.Count > 0)
                yield return null;

            yield return new WaitForSeconds(0.01f);

            randomPunch = Random.Range(1, 101);

            if (randomPunch <= 80)
                this.Punch(Random.Range(0, 4));
            else
                this.Kick(Random.Range(0, 4));
        }
    }

    public bool CanDoNeutralSuper()
    {
        int opponentId = this.player.tempOpponent.characterId;

        float superCost = this.player.maxSuperCharge;
        
        if (opponentId == 1 || opponentId == 3)
            superCost = this.player.maxSuperCharge / 2;

        bool canDoInAir = false;

        if (opponentId == 4)
            canDoInAir = true;

        bool hasEnough = this.player.superCharge >= superCost;

        bool isOnGround = Mathf.Abs(this.player.rb.velocity.y) <= 0f;

        if (hasEnough)
        {
            if (!canDoInAir)
            {
                return isOnGround;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        
    }
}
