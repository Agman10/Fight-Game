using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCapAI : CharacterAI
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void StartAI()
    {
        base.StartAI();
        this.StartCoroutine(this.MoveRandomlyCoroutine());
    }

    private IEnumerator MoveRandomlyCoroutine()
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
            //this.Special1(2);

            /*if (this.EnemyDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                this.Super(3);
            }*/

            if (this.EnemyYDistance() > 1.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                this.Special2(1);
            }

            else
            {
                this.Special1(2);

                /*if (Random.Range(1, 101) <= 30 && this.player.superCharge >= this.player.maxSuperCharge / 2 && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                    this.Super(1);
                else
                    this.Special1(2);*/
                //this.Special1(2);
            }

        }
        else if (this.EnemyXDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
        {
            this.Super(3);
        }
        else
        {
            if (Random.Range(1, 101) <= 30f)
            {
                //this.playerInput.JumpInput?.Invoke(true);
                this.playerInput.jumping = true;
                yield return new WaitForSeconds(0.01f);
                this.playerInput.jumping = false;
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

            //this.Special1(2);

            if (this.EnemyYDistance() > 1.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                this.Special2(1);
            }
            else
            {
                //this.Special1(2);

                if (Random.Range(1, 101) <= 30 && this.player.superCharge >= this.player.maxSuperCharge / 2 && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                    this.Super(1);
                else
                    this.Special1(2);
            }

        }
        else if (this.EnemyXDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
        {
            this.Super(3);
        }
        else
        {
            if (number2 <= 20)
            {
                //this.Special1(Random.Range(0, 4));
                //this.Special1(RandomSpecialDirection(1, 1, 0, 0));

                if (Random.Range(1, 101) <= 70)
                    this.Special1(1);
                else
                    this.Special1(0);
            }
            else if (number2 > 20 && number2 <= 60)
            {
                //this.Special2(Random.Range(0, 4));

                if (this.EnemyXDistance() <= 2f)
                    this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                else
                    this.Special2(RandomSpecialDirection(0, 1, 1, 1));
                //this.Special2(3);
            }
            else if (number2 > 60 && number2 <= 75)
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

            else if (number2 > 75 && number2 <= 100)
            {
                if (!this.player.tempOpponent.dead && this.player.superCharge >= this.player.maxSuperCharge / 2)
                {
                    //this.Super(Random.Range(0, 4));

                    if (this.player.superCharge < this.player.maxSuperCharge)
                    {
                        //this.Super(RandomSpecialDirection(0, 1, 1, 0));

                        /*if (this.EnemyDistance() <= 1.8f)
                            this.Super(1);
                        else
                            this.Super(2);*/


                        if (this.EnemyXDistance() <= 1.8f)
                        {
                            this.Super(1);
                        }
                        else if (Random.Range(1, 101) <= 70)
                        {
                            this.Super(2);
                        }
                        else
                        {
                            if (Random.Range(1, 101) <= 40)
                            {
                                if (Random.Range(1, 101) <= 70)
                                    this.Special1(1);
                                else
                                    this.Special1(0);
                            }
                            else
                            {
                                if (this.EnemyXDistance() <= 2f)
                                    this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                                else
                                    this.Special2(RandomSpecialDirection(0, 1, 1, 1));
                            }
                        }

                    }
                    else
                    {

                        if (this.EnemyXDistance() <= 1.8f)
                            this.Super(RandomSpecialDirection(1, 1, 1, 1));
                        else
                            this.Super(RandomSpecialDirection(4, 0, 1, 0));


                        //this.Super(RandomSpecialDirection(1, 1, 1, 1));
                    }
                }
                else if (this.player.tempOpponent.dead)
                {
                    this.Taunt(Random.Range(0, 4));
                    if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                        yield return new WaitForSeconds(Random.Range(1f, 2f));
                    /*if(this.player.attacks != null && this.player.attacks.Taunts != null)
                    {
                        while (this.player.attackStuns.Count > 0)
                        {
                            yield return null;
                        }
                    }*/
                }
                else
                {
                    if (Random.Range(1, 101) <= 40)
                    {
                        if (Random.Range(1, 101) <= 70)
                            this.Special1(1);
                        else
                            this.Special1(0);
                    }
                    else
                    {
                        if (this.EnemyXDistance() <= 2f)
                            this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                        else
                            this.Special2(RandomSpecialDirection(0, 1, 1, 1));
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

        this.StartCoroutine(this.MoveRandomlyCoroutine());
    }


    public bool MustAvoidAttacks()
    {
        if (!this.player.tempOpponent.dead &&
            this.player.tempOpponent.attackStuns.Count > 0
            )
        {
            GrandFlameAttack grandFlame = this.player.tempOpponent.attackStuns[0].GetComponent<GrandFlameAttack>();
            SummonAnvilAttack anvilSummon = this.player.tempOpponent.attackStuns[0].GetComponent<SummonAnvilAttack>();
            SuperRoadRollerAttack roadRollerAttack = this.player.tempOpponent.attackStuns[0].GetComponent<SuperRoadRollerAttack>();
            ElectricalFieldAttack electricalField = this.player.tempOpponent.attackStuns[0].GetComponent<ElectricalFieldAttack>();
            RollForwardAttack rollForward = this.player.tempOpponent.attackStuns[0].GetComponent<RollForwardAttack>();

            if (grandFlame != null)
                return true;
            else if (anvilSummon != null && anvilSummon.summoning)
                return true;
            else if (roadRollerAttack != null)
                return true;
            else if (electricalField != null && this.EnemyXDistance() < 2.5f && this.EnemyYDistance(true) < -2f && Mathf.Abs(this.player.rb.velocity.y) > 0f)
                return true;
            else if (rollForward != null && !rollForward.rollingBack && this.EnemyYDistance() <= 2f && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0 && this.player.transform.forward.z != this.player.tempOpponent.transform.forward.z)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
