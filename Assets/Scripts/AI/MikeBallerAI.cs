using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeBallerAI : CharacterAI
{
    private bool anvilElectrecute;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.player != null && this.player.tempOpponent != null)
        {
            Anvil anvil = null;
            if (this.player.tempOpponent.attackStuns.Count > 0)
                anvil = this.player.tempOpponent.attackStuns[0].GetComponent<Anvil>();

            if (anvil != null &&
                !this.player.tempOpponent.dead &&
                this.player.tempOpponent.attackStuns.Count > 0 &&
                this.player.tempOpponent.attackStuns[0] == anvil.gameObject &&
                Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
                !this.player.dead
                /*&& this.player.stuns.Count < 0*/)
            {
                this.anvilElectrecute = true;
            }
            else
            {
                this.anvilElectrecute = false;
            }

            //Debug.Log(this.player.tempOpponent.stuns.Count > 0 && Mathf.Abs(this.player.tempOpponent.rb.velocity.y) > 0 && Mathf.Abs(this.player.tempOpponent.rb.velocity.x) < 2f && this.player.tempOpponent.stuns[0] >= 1f);
        }
        else
        {
            this.anvilElectrecute = false;
        }

        //Debug.Log(this.anvilElectrecute);
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

        float waitTime = Random.Range(0.1f, 0.3f);

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
        /*if(!this.player.tempOpponent.dead && Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f && this.player.tempOpponent.attackStuns.Count > 0 && this.player.superCharge >= this.player.maxSuperCharge / 2 && !this.player.tempOpponent.knockbackInvounrability && !this.player.tempOpponent.preventDeath && this.player.attackStuns.Count <= 0)
        {
            this.Super(0);
            Debug.Log("test");
        }*/
        if (this.CanHitElectricBall())
        {
            Debug.Log("electricBall");
            this.Super(1);
        }
        else if (this.CanHitAnvil())
        {
            this.Super(0);
            Debug.Log("anvil");
            yield return this.AnvilHitCoroutine();
        }
        else if (this.MustAvoidAttacks())
        {
            yield return this.AvoidAttacksCoroutine();
        }
        else if (this.player.hits > 2)
        {
            yield return this.AvoidPunchStun();
        }
        else if (this.CanDetonateC4())
        {
            this.Special1(3);
        }
        else if (this.EnemyXDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead && !this.player.tempOpponent.knockbackInvounrability)
        {
            //Debug.Log(EnemyDistance());
            //this.Special1(2);

            if (this.EnemyYDistance() > 1.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                this.Special2(1);
            }
            else
            {
                this.Special1(2);
            }
        }
        else
        {
            if (Random.Range(1, 101) <= 30f)
            {
                //this.playerInput.JumpInput?.Invoke(true);
                this.playerInput.jumping = true;
                yield return new WaitForSeconds(0.01f);
                this.playerInput.jumping = false;

                if(Random.Range(1, 101) <= 10)
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
        /*if (!this.player.tempOpponent.dead && Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f && this.player.tempOpponent.attackStuns.Count > 0 && this.player.superCharge >= this.player.maxSuperCharge / 2 && !this.player.tempOpponent.knockbackInvounrability && !this.player.tempOpponent.preventDeath && this.player.attackStuns.Count <= 0)
        {
            this.Super(0);
            Debug.Log("test");
        }*/
        if (this.CanHitElectricBall())
        {
            Debug.Log("electricBall");
            this.Super(1);
        }
        else if (this.CanHitAnvil())
        {
            this.Super(0);
            Debug.Log("anvil");
            yield return this.AnvilHitCoroutine();
        }
        else if (this.MustAvoidAttacks())
        {
            yield return this.AvoidAttacksCoroutine();
        }
        else if (this.player.hits > 2)
        {
            yield return this.AvoidPunchStun();
        }
        else if (this.CanDetonateC4())
        {
            this.Special1(3);
        }
        else if (this.EnemyXDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead && !this.player.tempOpponent.knockbackInvounrability)
        {
            //Debug.Log(EnemyDistance());

            //this.Special1(2);

            if (this.EnemyYDistance() > 1.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            {
                this.Special2(1);
            }
            else
            {
                this.Special1(2);
            }

        }
        else
        {
            if (number2 <= 30)
            {
                yield return this.Special1Coroutine();
            }
            else if (number2 > 30 && number2 <= 65)
            {
                yield return this.Special2Coroutine();
            }
            else if (number2 > 65 && number2 <= 75)
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

                    //this.Super(RandomSpecialDirection(1, 1, 1, 1));



                    //for mike baller
                    if (this.EnemyXDistance() <= 2f && this.EnemyYDistance(true) >= -1.4f)
                    {
                        //Debug.Log(EnemyXDistance());
                        this.Super(RandomSpecialDirection(0, 0, 1, 0));
                    }
                    else
                    {
                        //this.Super(RandomSpecialDirection(2, 1, 0, 0));


                        if (!this.player.tempOpponent.preventDeath)
                            this.Super(RandomSpecialDirection(2, 1, 0, 0));
                        else
                            this.Super(RandomSpecialDirection(0, 1, 0, 0));


                    }



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
                        //this.Special1(RandomSpecialDirection(1, 1, 0, 1));
                        yield return this.Special1Coroutine();
                    }
                    else
                    {
                        //this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                        yield return this.Special2Coroutine();
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

    private IEnumerator Special1Coroutine()
    {
        //this.Special1(Random.Range(0, 4));
        //this.Special1(RandomSpecialDirection(1, 1, 0, 0));

        int randomSpecial = RandomSpecialDirection(0, 4, 0, 1);

        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            randomSpecial = RandomSpecialDirection(3, 2, 0, 1);


        if (this.player.attacks.downSpecial != null && this.player.attacks.downSpecial is CFourAttack c4 && c4.activeCFour == null)
        {
            randomSpecial = RandomSpecialDirection(0, 4, 0, 4);

            if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                randomSpecial = RandomSpecialDirection(3, 2, 0, 3);
        }



        this.Special1(randomSpecial);

        if (randomSpecial == 0)
        {
            /*if (this.EnemyXDistance() <= 3f)
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(0, 0, 1, 1));
            else
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(1, 1, 0, 0));*/

            float currentTime = 0;
            float duration = 0.15f;
            while (this.player.attackStuns.Count > 0 && currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            if (this.EnemyXDistance() <= 3f)
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(0, 0, 1, 1));
            else
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(1, 1, 0, 0));

            currentTime = 0;
            duration = 0.15f;
            while (this.player.attackStuns.Count > 0 && currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
        }

        /*if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            this.Special1(RandomSpecialDirection(2, 1, 0, 1));
        else
            this.Special1(RandomSpecialDirection(0, 2, 0, 1));*/
    }

    private IEnumerator Special2Coroutine()
    {
        //this.Special2(RandomSpecialDirection(1, 1, 1, 1));

        int randomSpecial = RandomSpecialDirection(1, 1, 3, 0);

        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            randomSpecial = RandomSpecialDirection(1, 1, 3, 1);

        this.Special2(randomSpecial);
        //Debug.Log(randomSpecial);
        if (randomSpecial == 3)
        {

            /*if (this.player.attacks.downSpecial2 != null && this.player.attacks.downSpecial2 is ElectricalFieldAttack electricalField)
            {

            }*/

            float dur = 0.2f;

            if (this.player.attacks.downSpecial2 != null && this.player.attacks.downSpecial2 is ElectricalFieldAttack electricalField)
            {
                dur = electricalField.startDelay;
            }
            float currentTime = 0;
            //float duration = 0.2f + 0.4f;
            float duration = dur + 0.4f;
            while (this.player.attackStuns.Count > 0 && currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            //yield return new WaitForSeconds(0.2f + 0.4f);
            while (this.EnemyXDistance() <= 2f && !this.player.tempOpponent.dead && this.player.attackStuns.Count > 0)
            {
                this.playerInput.special2 = true;
                yield return null;
            }
            this.playerInput.special2 = false;
        }

        //this.Special2(3);
    }

    

    public bool CanHitAnvil()
    {
        if (!this.player.tempOpponent.dead && 
            this.player.tempOpponent.attackStuns.Count > 0 && 
            this.player.superCharge >= this.player.maxSuperCharge / 2 && 
            Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
            this.player.attackStuns.Count <= 0 &&
            this.player.stuns.Count <= 0)
        {
            UppercutAttack uppercut = this.player.tempOpponent.attackStuns[0].GetComponent<UppercutAttack>();
            RollUpAttack rollUpAttack = this.player.tempOpponent.attackStuns[0].GetComponent<RollUpAttack>();
            SuperElectricBallAttack electricBallAttack = this.player.tempOpponent.attackStuns[0].GetComponent<SuperElectricBallAttack>();
            SpinAttack spinAttack = this.player.tempOpponent.attackStuns[0].GetComponent<SpinAttack>();
            ElectricalFieldAttack electricalField = this.player.tempOpponent.attackStuns[0].GetComponent<ElectricalFieldAttack>();
            //BookCounterAttack bookCounter = this.player.tempOpponent.attackStuns[0].GetComponent<BookCounterAttack>();
            ExplosiveDetonationAttack explosiveDetonation = this.player.tempOpponent.attackStuns[0].GetComponent<ExplosiveDetonationAttack>();
            DoorTeleportAttack doorTeleport = this.player.tempOpponent.attackStuns[0].GetComponent<DoorTeleportAttack>();
            SuperRoadRollerAttack roadRollerAttack = this.player.tempOpponent.attackStuns[0].GetComponent<SuperRoadRollerAttack>();



            if (uppercut != null && this.EnemyXDistance() > 2f)
                return true;
            else if (rollUpAttack != null && this.EnemyXDistance() > 2f)
                return true;
            else if (electricBallAttack != null)
                return true;
            else if (spinAttack != null && spinAttack.moveSpeed < 0.5f && this.EnemyXDistance() > 3f)
                return true;
            else if (electricalField != null && this.EnemyXDistance() > 3f)
                return true;
            else if (explosiveDetonation != null && this.EnemyXDistance() > 5f)
                return true;
            else if (doorTeleport != null && this.EnemyXDistance() > 3f && doorTeleport.comingBack)
                return true;
            else if (roadRollerAttack != null && roadRollerAttack.anvilVulnerability)
                return true;
            else
                return false;
        }
        else if (!this.player.tempOpponent.dead &&
            this.player.superCharge >= this.player.maxSuperCharge / 2 &&
            Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
            this.player.attackStuns.Count <= 0 &&
            this.player.stuns.Count <= 0)
        {
            if (this.player.tempOpponent.stuns.Count > 0 && Mathf.Abs(this.player.tempOpponent.rb.velocity.y) > 0 && Mathf.Abs(this.player.tempOpponent.rb.velocity.x) < 2f && this.player.tempOpponent.stuns[0] >= 1f)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public bool CanHitElectricBall()
    {
        if(!this.player.tempOpponent.dead && 
            this.player.superCharge >= this.player.maxSuperCharge / 2 &&
            this.player.attackStuns.Count <= 0 &&
            this.player.stuns.Count <= 0 &&
            this.EnemyXDistance() <= 0.35f &&
            this.EnemyYDistance(true) < 2.75f && this.EnemyYDistance(true) >= 0.5f)
        {
            if (this.player.transform.forward.z > 0 && this.player.transform.position.x > 12f || this.player.transform.forward.z < 0 && this.player.transform.position.x < -12f)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanDetonateC4()
    {
        if (!this.player.tempOpponent.dead &&
             this.player.attackStuns.Count <= 0 &&
             this.player.stuns.Count <= 0 &&
             this.player.attacks.downSpecial != null &&
             this.player.attacks.downSpecial is CFourAttack c4 &&
             c4.activeCFour != null)
        {
            if (Mathf.Abs(this.player.tempOpponent.transform.position.y - c4.activeCFour.transform.position.y) < 1f &&
                Mathf.Abs(this.player.tempOpponent.transform.position.x - c4.activeCFour.transform.position.x) < 1f 
                /*&& Mathf.Abs(this.player.transform.position.x - c4.activeCFour.transform.position.x) > 3.2f*/
                )
            {
                //return true;

                if (this.player.tempOpponent.health <= 20f && this.player.health > 20f)
                {
                    return true;
                }
                else return Mathf.Abs(this.player.transform.position.x - c4.activeCFour.transform.position.x) > 3.2f;
            }
            else return false;
        }
        else return false;
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

            if (grandFlame != null)
                return true;
            else if (anvilSummon != null && anvilSummon.summoning)
                return true;
            else if (roadRollerAttack != null)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator AvoidAttacksCoroutine()
    {
        if (!this.player.tempOpponent.dead /*&& this.player.tempOpponent.attackStuns.Count > 0*/)
        {
            GrandFlameAttack grandFlame = this.player.tempOpponent.attackStuns[0].GetComponent<GrandFlameAttack>();
            SummonAnvilAttack anvilSummon = this.player.tempOpponent.attackStuns[0].GetComponent<SummonAnvilAttack>();
            SuperRoadRollerAttack roadRollerAttack = this.player.tempOpponent.attackStuns[0].GetComponent<SuperRoadRollerAttack>();

            if (grandFlame != null)
                yield return this.AvoidGrandFLameCoroutine();
            else if (anvilSummon != null && anvilSummon.summoning)
                yield return this.AvoidAnvilCoroutine(anvilSummon);
            else if (roadRollerAttack != null)
                yield return this.AvoidRoadRollerCoroutine(roadRollerAttack);
        }
    }


    private IEnumerator AnvilHitCoroutine()
    {
        if (this.player.attacks.neutralSuper != null && this.player.attacks.neutralSuper is SummonAnvilAttack anvilAttack)
        {
            //Debug.Log("fdgsdfgsd");
            while (anvilAttack.onGoing)
            {
                yield return null;
            }
        }

        /*while (this.player.attackStuns.Count <= 0)
        {
            yield return null;
        }*/

        //Debug.Log("NoAttackStuns");
        //yield return new WaitForSeconds(0.01f);
        //this.Punch(0);

        float startHealth = this.player.health;

        //Debug.Log(startHealth);

        if (this.player.tempOpponent.attackStuns.Count > 0)
        {
            Anvil anvil = null;

            yield return new WaitForSeconds(0.05f);
            //Debug.Log(this.player.tempOpponent.attackStuns.Count);
            //Debug.Log(this.player.tempOpponent.attackStuns[0]);
            if (this.player.tempOpponent.attackStuns.Count > 0)
                anvil = this.player.tempOpponent.attackStuns[0].GetComponent<Anvil>();

            /*bool testBool = anvil != null &&
                !this.player.tempOpponent.dead &&
                this.player.tempOpponent.attackStuns.Count > 0 &&
                this.player.tempOpponent.attackStuns[0] == anvil.gameObject &&
                Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
                !this.player.dead &&
                this.player.health >= startHealth - 5;*/


            /*if (anvil != null &&
                !this.player.tempOpponent.dead &&
                this.player.tempOpponent.attackStuns.Count > 0 &&
                this.player.tempOpponent.attackStuns[0] == anvil.gameObject &&
                Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
                !this.player.dead &&
                this.player.health >= startHealth - 5)
            {

            }*/

            if (this.anvilElectrecute)
            {
                float currentTime = 0;
                float duration = 2f;
                while (this.EnemyXDistance() > 2f && this.anvilElectrecute)
                {
                    currentTime += Time.deltaTime;
                    this.playerInput.moveInput = this.MoveInput(1);
                    yield return null;
                }
                Debug.Log("1");
                while (this.EnemyXDistance() < 1.5f && this.anvilElectrecute)
                {
                    currentTime += Time.deltaTime;
                    if (this.player.transform.position.x < 0)
                        this.playerInput.moveInput = new Vector3(1f, 0f, 0f);
                    else
                        this.playerInput.moveInput = new Vector3(-1f, 0f, 0f);
                    //this.playerInput.moveInput = this.MoveInput(2);
                    yield return null;
                }
                Debug.Log("2");
                this.playerInput.moveInput = this.MoveInput(0);

                /*while (this.player.tempOpponent.attackStuns.Count > 0)
                {
                    yield return null;
                }*/

                //yield return new WaitForSeconds(0.4f);

                while (!anvil.growHitbox.gameObject.active && this.anvilElectrecute)
                    yield return null;

                Debug.Log("3");
                /*if (this.anvilElectrecute)
                {
                    this.Special2(3);
                    currentTime = 0;
                    duration = 0.2f + 0.4f;
                    while (this.player.attackStuns.Count > 0 && currentTime < duration)
                    {
                        currentTime += Time.deltaTime;
                        yield return null;
                    }
                    Debug.Log("4");
                }*/

                if (this.anvilElectrecute)
                {
                    this.Special2(3);
                    float dur = 0.2f;

                    if (this.player.attacks.downSpecial2 != null && this.player.attacks.downSpecial2 is ElectricalFieldAttack electricalField)
                    {
                        dur = electricalField.startDelay;
                    }

                    currentTime = 0;
                    duration = dur + 0.4f;
                    //duration = 0.2f + 0.4f;
                    while (this.player.attackStuns.Count > 0 && currentTime < duration)
                    {
                        currentTime += Time.deltaTime;
                        this.playerInput.special2 = true;
                        yield return null;
                    }
                    Debug.Log("4");
                }


                //yield return new WaitForSeconds(0.2f + 0.4f);
                while (this.EnemyXDistance() <= 2f && !this.player.tempOpponent.dead && this.player.attackStuns.Count > 0)
                {
                    this.playerInput.special2 = true;
                    yield return null;
                }
                this.playerInput.special2 = false;

                Debug.Log("tesertet");
            }

            /*while (this.player.attackStuns.Count > 0)
            {
                yield return null;
            }
            this.Taunt();*/
        }
    }

    private IEnumerator AvoidGrandFLameCoroutine()
    {
        if (this.MustAvoidAttacks() && this.EnemyXDistance() < 2f)
        {
            this.Punch(0);
        }

        while (this.MustAvoidAttacks())
        {
            if (this.EnemyXDistance() < 9f)
            {
                this.playerInput.moveInput = this.MoveInput(2);
                this.playerInput.jumping = false;
            }
            else
            {
                this.playerInput.moveInput = this.MoveInput(2);
                if (this.EnemyXDistance() > 9f)
                {
                    if (this.player.superCharge >= this.player.maxSuperCharge / 2 && this.player.attacks.neutralSuper is SummonAnvilAttack anvilAttack && anvilAttack.cooldownTimer <= 0)
                    {
                        this.Super(0);
                    }
                    else
                    {
                        this.playerInput.jumping = true;
                        yield return new WaitForSeconds(0.01f);

                        this.Special1(1);
                        this.playerInput.jumping = false;
                    }

                }

                while (this.player.attackStuns.Count > 0)
                    yield return null;
            }

            yield return null;
        }
        this.playerInput.jumping = false;
        this.playerInput.moveInput = this.MoveInput(0);
    }

    public IEnumerator AvoidAnvilCoroutine(SummonAnvilAttack anvilSummon)
    {

        if(Mathf.Abs(this.player.rb.velocity.y) > 0)
        {
            //while (this.player.stuns.Count > 0) yield return null;

            this.Special2(2);
        }

        if(anvilSummon != null && !anvilSummon.hasBeenSummoned)
        {
            if (this.EnemyXDistance() < 2f)
            {
                this.Punch(0);
            }

            /*if (this.AvoidAttacks() && this.EnemyXDistance() > 6f)
            {
                this.Special2(2);
            }*/
        }
        

        //float position = this.player.transform.position.x;

        while (this.MustAvoidAttacks() && !anvilSummon.hasBeenSummoned)
        {
            yield return null;
        }

        if (Mathf.Abs(this.player.rb.velocity.y) > 0)
        {
            //while (this.player.stuns.Count > 0) yield return null;
            this.Special2(2);
        }
        float position = this.player.transform.position.x;

        while (this.MustAvoidAttacks())
        {
            if (Mathf.Abs(position) < 12f && Mathf.Abs(position - this.player.tempOpponent.transform.position.x) < 6)
            {
                this.playerInput.moveInput = this.MoveInput(2);
            }
            else
            {
                this.playerInput.moveInput = this.MoveInput(1);
            }

            //Debug.Log(Mathf.Abs(this.EnemyXDistance()));

            /*if (this.EnemyXDistance() < 2f)
            {
                this.Punch(0);
            }*/

            yield return null;
        }
        /*if (this.EnemyXDistance() < 2f)
        {
            this.Punch(0);
        }*/
        this.playerInput.moveInput = this.MoveInput(0);
    }

    public IEnumerator AvoidRoadRollerCoroutine(SuperRoadRollerAttack roadRollerAttack)
    {
        //this.playerInput.moveInput = this.MoveInput(0);

        while (this.MustAvoidAttacks() && !roadRollerAttack.fallingDown)
            yield return null;

        float position = this.player.transform.position.x;
        while (this.MustAvoidAttacks() && roadRollerAttack.fallingDown)
        {
            if (position < 0f /*&& Mathf.Abs(position - this.player.tempOpponent.transform.position.x) < 6*/)
            {
                this.playerInput.moveInput = new Vector3(1f, 0f, 0f);
                //this.playerInput.moveInput = this.MoveInput(2);
            }
            else
            {
                this.playerInput.moveInput = new Vector3(-1f, 0f, 0f);
                //this.playerInput.moveInput = this.MoveInput(1);
            }

            //Debug.Log(Mathf.Abs(this.EnemyXDistance()));

            /*if (this.EnemyXDistance() < 2f)
            {
                this.Punch(0);
            }*/

            yield return null;
        }

        if (this.player.superCharge >= this.player.maxSuperCharge / 2)
        {
            float waitTime = 0.1f;
            if (this.player.characterId == 7)
                waitTime = 0.2f;

            yield return new WaitForSeconds(waitTime);

            //yield return new WaitForSeconds(0.1f);
            this.Super(0);
            yield return this.AnvilHitCoroutine();
        }
        else
        {
            if (this.player.characterId == 7)
                yield return new WaitForSeconds(0.2f);

            this.Special2(2);

            /*if (this.player.characterId == 7)
            {
                this.playerInput.jumping = true;
                yield return new WaitForSeconds(0.01f);
                this.playerInput.jumping = false;

                this.Special1(1);
                while (this.player.attackStuns.Count > 0)
                    yield return null;
                this.Special1(1);
                while (this.player.attackStuns.Count > 0)
                    yield return null;
                this.Special1(1);
            }
            else
            {
                this.Special2(2);
            }*/
        }

        /*if (this.EnemyXDistance() < 2f)
        {
            this.Punch(0);
        }*/
        this.playerInput.moveInput = this.MoveInput(0);
    }

    private IEnumerator AvoidPunchStun()
    {
        if (Mathf.Abs(this.transform.position.x) > 12)
        {
            while (this.player.hits > 2 && this.EnemyXDistance() < 3f && this.player.transform.position.y < 3f)
            {
                //this.playerInput.moveInput = MoveInput(2);
                //this.Kick(0);
                //this.Punch(0);
                //this.Special2(1);
                //this.playerInput.jumping = true;

                /*this.playerInput.jumping = true;
                yield return new WaitForSeconds(0.06f);
                this.playerInput.jumping = false;*/
                //this.Kick(0);

                this.playerInput.jumping = true;
                this.Kick(0);
                while (this.player.attackStuns.Count > 0)
                    yield return null;
                //this.Special2(2);
                //this.Special2(1);
                //this.Special2(0);
                //this.Kick(0);
                //this.Special2(3);
                /*this.Punch(0);
                while (this.player.attackStuns.Count > 0)
                    yield return null;*/

                //this.playerInput.jumping = true;

                //this.Special1(2);
                yield return null;
            }
            this.playerInput.jumping = false;
            this.Special2(2);

            //old Mike Baller
            /*if (this.player.hits > 2)
            {

                while (this.player.hits > 2 && this.EnemyXDistance() < 3f)
                {
                    //this.playerInput.moveInput = MoveInput(2);
                    this.Punch(0);
                    yield return null;
                }
                while (this.player.attackStuns.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(0.01f);
                this.Special2(2);
            }*/

        }
        else
        {
            while (this.player.hits > 2 && Mathf.Abs(this.transform.position.x) < 12 && this.EnemyXDistance() < 3f || this.EnemyXDistance() < 3f && Mathf.Abs(this.transform.position.x) < 12)
            {
                this.playerInput.moveInput = MoveInput(2);
                //this.Kick(0);
                //this.Punch(0);
                //this.Special2(1);
                //this.playerInput.jumping = true;

                //this.playerInput.jumping = true;
                /*yield return new WaitForSeconds(0.06f);
                this.playerInput.jumping = false;*/
                //this.Kick(0);
                //this.Punch(0);
                //this.Special2(2);
                //this.Special2(1);
                //this.Kick(0);
                yield return null;
            }
            //this.playerInput.jumping = false;
            this.playerInput.moveInput = MoveInput(0);
            //this.Special2(0);

            if(Mathf.Abs(this.transform.position.x) < 12)
            {
                if (this.player.characterId != 7)
                {
                    this.Special2(0);
                    while (this.player.attackStuns.Count > 0)
                        yield return null;
                    this.Special2(2);
                }
                else
                {
                    if (this.CanDetonateC4())
                    {
                        this.Special1(3);
                    }
                    else
                    {
                        this.Special1(1);
                        while (this.player.attackStuns.Count > 0)
                            yield return null;
                        this.Special2(2);
                    }
                        

                    
                }
            }

            
                
        }
    }

    private IEnumerator AttackTests()
    {
        //yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
        yield return new WaitForSeconds(0.01f);

        //yield return this.Special2Coroutine();

        //yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        /*if (Random.Range(1, 101) <= 30f)
        {
            //this.playerInput.JumpInput?.Invoke(true);
            this.playerInput.jumping = true;
            yield return new WaitForSeconds(0.01f);
            this.playerInput.jumping = false;


        }*/

        //this.playerInput.moveInput = MoveInput(Random.Range(0, 4));

        /*if (this.MustAvoidAttacks())
        {
            yield return AvoidAttacksCoroutine();
        }*/

        if (this.player.attacks.downSpecial != null && this.player.attacks.downSpecial is CFourAttack c4 && c4.activeCFour == null)
        {
            this.Special1(3);
        }

        if (this.player.hits > 2)
        {
            yield return this.AvoidPunchStun();

            /*while (this.player.hits > 2 && this.EnemyXDistance() < 3f)
            {
                this.playerInput.moveInput = MoveInput(2);
                //this.Kick(0);
                //this.Punch(0);
                //this.Special2(1);
                //this.playerInput.jumping = true;

                this.playerInput.jumping = true;
                *//*yield return new WaitForSeconds(0.06f);
                this.playerInput.jumping = false;*//*
                //this.Kick(0);
                //this.Punch(0);
                //this.Special2(2);
                this.Special2(1);
                //this.Kick(0);
                yield return null;
            }
            this.playerInput.jumping = false;*/
            /*while (this.player.attackStuns.Count > 0)
            {
                yield return null;
            }*/
            //this.Special2(2);

            //this.playerInput.moveInput = MoveInput(0);

            /*while (this.player.hits > 2 && this.EnemyXDistance() < 3f)
            {
                this.playerInput.moveInput = MoveInput(2);
                //this.Punch(0);
                yield return null;
            }
            while (this.player.attackStuns.Count > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.01f);
            this.Special2(2);*/
        }


        //this.playerInput.moveInput = MoveInput(Random.Range(0, 4));

        /*if (!this.player.tempOpponent.dead &&
             this.player.attackStuns.Count <= 0 &&
             this.player.stuns.Count <= 0)
        {
            if (this.player.attacks.downSpecial != null && this.player.attacks.downSpecial is CFourAttack c4)
            {
                if (c4.activeCFour == null)
                {
                    this.Special1(3);

                }
                else if (
                    Mathf.Abs(this.player.tempOpponent.transform.position.y - c4.activeCFour.transform.position.y) < 1f &&
                    Mathf.Abs(this.player.tempOpponent.transform.position.x - c4.activeCFour.transform.position.x) < 1f &&
                    Mathf.Abs(this.player.transform.position.x - c4.activeCFour.transform.position.x) > 3.2f
                    )
                {
                    this.Special1(3);
                }
            }
        }*/




        //Debug.Log(this.EnemyYDistance(true));
        /*if (this.CanHitElectricBall())
        {
            Debug.Log(this.transform.forward.z);
            Debug.Log(this.transform.position.x);
            this.Super(1);
        }*/

        /*if (this.CanHitAnvil())
        {
            //Debug.Log("an");
            this.Super(0);
            yield return this.AnvilHitCoroutine();

            yield return new WaitForSeconds(2f);
        }*/






        /*if (!this.player.tempOpponent.dead && Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f && Mathf.Abs(this.player.rb.velocity.y) <= 0f && this.player.tempOpponent.attackStuns.Count > 0 && this.player.superCharge >= this.player.maxSuperCharge / 2 && !this.player.tempOpponent.knockbackInvounrability && !this.player.tempOpponent.preventDeath && this.player.attackStuns.Count <= 0)
        {
            this.Super(0);
            Debug.Log("test");
        }*/

        /*if (!this.player.tempOpponent.dead && this.player.tempOpponent.attackStuns.Count > 0 && this.player.superCharge >= this.player.maxSuperCharge / 2)
        {
            UppercutAttack uppercut = this.player.tempOpponent.attackStuns[0].GetComponent<UppercutAttack>();
            if (uppercut != null)
                this.Super(0);
        }*/
        /*if (CanHitAnvil())
        {
            this.Super(0);

            *//*SummonAnvilAttack anvilAttack = null;

            if (this.player.attackStuns.Count > 0)
                anvilAttack = this.player.attackStuns[0].GetComponent<SummonAnvilAttack>();*//*

            if (this.player.attacks.neutralSuper != null && this.player.attacks.neutralSuper is SummonAnvilAttack anvilAttack)
            {
                //Debug.Log("fdgsdfgsd");
                while (anvilAttack.onGoing)
                {
                    yield return null;
                }
            }

            *//*while (this.player.attackStuns.Count <= 0)
            {
                yield return null;
            }*//*

            //Debug.Log("NoAttackStuns");
            //yield return new WaitForSeconds(0.01f);
            //this.Punch(0);

            float startHealth = this.player.health;

            //Debug.Log(startHealth);

            if (this.player.tempOpponent.attackStuns.Count > 0)
            {
                Anvil anvil = null;

                yield return new WaitForSeconds(0.05f);
                //Debug.Log(this.player.tempOpponent.attackStuns.Count);
                //Debug.Log(this.player.tempOpponent.attackStuns[0]);
                if (this.player.tempOpponent.attackStuns.Count > 0)
                    anvil = this.player.tempOpponent.attackStuns[0].GetComponent<Anvil>();

                *//*bool testBool = anvil != null &&
                    !this.player.tempOpponent.dead &&
                    this.player.tempOpponent.attackStuns.Count > 0 &&
                    this.player.tempOpponent.attackStuns[0] == anvil.gameObject &&
                    Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
                    !this.player.dead &&
                    this.player.health >= startHealth - 5;*/


        /*if (anvil != null &&
            !this.player.tempOpponent.dead &&
            this.player.tempOpponent.attackStuns.Count > 0 &&
            this.player.tempOpponent.attackStuns[0] == anvil.gameObject &&
            Mathf.Abs(this.player.rb.velocity.y) <= 0 &&
            !this.player.dead &&
            this.player.health >= startHealth - 5)
        {

        }*//*

        if (this.anvilElectrecute)
        {
            float currentTime = 0;
            float duration = 2f;
            while (this.EnemyXDistance() > 2f && this.anvilElectrecute)
            {
                currentTime += Time.deltaTime;
                this.playerInput.moveInput = this.MoveInput(1);
                yield return null;
            }
            Debug.Log("1");
            while (this.EnemyXDistance() < 1.5f && this.anvilElectrecute)
            {
                currentTime += Time.deltaTime;
                if (this.player.transform.position.x < 0)
                    this.playerInput.moveInput = new Vector3(1f, 0f, 0f);
                else
                    this.playerInput.moveInput = new Vector3(-1f, 0f, 0f);
                //this.playerInput.moveInput = this.MoveInput(2);
                yield return null;
            }
            Debug.Log("2");
            this.playerInput.moveInput = this.MoveInput(0);

            *//*while (this.player.tempOpponent.attackStuns.Count > 0)
            {
                yield return null;
            }*//*

            //yield return new WaitForSeconds(0.4f);

            while (!anvil.growHitbox.gameObject.active && this.anvilElectrecute)
                yield return null;

            Debug.Log("3");
            if (this.anvilElectrecute)
            {
                this.Special2(3);
                float dur = 0.2f;

                if (this.player.attacks.downSpecial2 != null && this.player.attacks.downSpecial2 is ElectricalFieldAttack electricalField)
                {
                    dur = electricalField.startDelay;
                }

                currentTime = 0;
                //duration = dur + 0.1f;
                duration = 0.2f + 0.4f;
                while (this.player.attackStuns.Count > 0 && currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    this.playerInput.special2 = true;
                    yield return null;
                }
                Debug.Log("4");
            }

            Debug.Log(this.EnemyXDistance());
            //yield return new WaitForSeconds(0.2f + 0.4f);
            while (this.EnemyXDistance() <= 2f && !this.player.tempOpponent.dead && this.player.attackStuns.Count > 0)
            {
                this.playerInput.special2 = true;
                yield return null;
            }
            this.playerInput.special2 = false;

            Debug.Log("tesertet");
        }

        *//*while (this.player.attackStuns.Count > 0)
        {
            yield return null;
        }
        this.Taunt();*//*
    }

    Debug.Log("end");
    //this.Taunt();

}*/


        /*if (!this.player.tempOpponent.dead && 
            Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f && 
            Mathf.Abs(this.player.rb.velocity.y) <= 0f && 
            this.player.tempOpponent.attackStuns.Count > 0 && 
            this.player.superCharge >= this.player.maxSuperCharge / 2 && 
            !this.player.tempOpponent.knockbackInvounrability && 
            !this.player.tempOpponent.preventDeath && 
            this.player.attackStuns.Count <= 0)
        {
            yield return new WaitForSeconds(0.2f);
            *//*if(Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f)
            {
                this.Super(0);
                Debug.Log("test");
            }*//*

            while (Mathf.Abs(this.player.tempOpponent.rb.velocity.x) <= 2.5f)
            {
                //currentTime += Time.deltaTime;
                yield return null;
            }
            if (this.player.tempOpponent.attackStuns.Count > 0)
            {
                this.Super(0);
                Debug.Log("test");
            }
            //this.Super(0);

        }*/

        /*if (Random.Range(1, 101) <= 30f)
        {
            //this.playerInput.JumpInput?.Invoke(true);
            this.playerInput.jumping = true;
            yield return new WaitForSeconds(0.01f);
            this.playerInput.jumping = false;
        }

        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));


        if (!this.player.tempOpponent.dead && this.player.superCharge >= this.player.maxSuperCharge / 2)
        {
            //for mike baller
            if (this.EnemyXDistance() <= 2f && this.EnemyYDistance(true) >= -1.4f)
            {
                //Debug.Log(EnemyXDistance());
                //Debug.Log(this.EnemyYDistance(true));
                this.Super(RandomSpecialDirection(0, 0, 1, 0));
            }
            else
            {
                Debug.Log(EnemyXDistance());
                Debug.Log(this.EnemyYDistance(true));
                //this.Super(RandomSpecialDirection(1, 1, 0, 0));
            }



        }*/


        /*int randomSpecial = this.RandomSpecialDirection(0, 2, 0, 1);

        if (Mathf.Abs(this.player.rb.velocity.y) <= 0f)
            randomSpecial = this.RandomSpecialDirection(1, 0, 0, 0);

        this.Special1(randomSpecial);

        if (randomSpecial == 0)
        {
            if (this.EnemyXDistance() <= 3f)
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(0, 0, 1, 1));
            else
                this.playerInput.moveInput = MoveInput(RandomSpecialDirection(1, 1, 0, 0));

            float currentTime = 0;
            float duration = 0.25f;
            while (this.player.attackStuns.Count > 0 && currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

        }

        this.playerInput.moveInput = MoveInput(Random.Range(0, 4));*/

        this.StartCoroutine(this.AttackTests());
    }


    private IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));



        //this.StartCoroutine(Special2Coroutine());
        //yield return Special2Coroutine();
        yield return TestCoroutine2();

        Debug.Log("done1");
        //this.playerInput.moveInput = MoveInput(0);
        this.StartCoroutine(this.TestCoroutine());
    }

    private IEnumerator TestCoroutine2()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));

        yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));

        Debug.Log("done2");
    }


}
