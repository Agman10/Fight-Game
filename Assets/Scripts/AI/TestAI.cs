using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public PlayerInput playerInput;
    public TestPlayer player;
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(this.playerInput != null && this.player != null)
        {
            this.StartCoroutine(this.MoveRandomlyCoroutine());
            //this.StartCoroutine(this.DoRandomSpecials());
        }
        
    }

    private IEnumerator DoRandomSpecials()
    {

        yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        if (this.EnemyDistance() <= 1.95f)
        {
            Debug.Log(EnemyDistance());
            this.Special1(2);
        }
        else
        {
            this.Special1(RandomSpecialDirection(1, 1, 0, 1));
        }
        

        this.StartCoroutine(this.DoRandomSpecials());
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
        if (this.EnemyDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead)
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
        else if (this.EnemyDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
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
        if (this.EnemyDistance() <= 1.8f && this.EnemyYDistance() <= 2.2f && !this.player.tempOpponent.dead)
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
        else if (this.EnemyDistance() <= 1.5f && this.player.superCharge >= this.player.maxSuperCharge && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
        {
            this.Super(3);
        }
        else
        {
            if (number2 <= 20)
            {
                //this.Special1(Random.Range(0, 4));
                //this.Special1(RandomSpecialDirection(1, 1, 0, 0));

                if(Random.Range(1, 101) <= 70)
                    this.Special1(1);
                else
                    this.Special1(0);
            }
            else if (number2 > 20 && number2 <= 60)
            {
                //this.Special2(Random.Range(0, 4));

                if (this.EnemyDistance() <= 2f)
                    this.Special2(RandomSpecialDirection(1, 1, 1, 1));
                else
                    this.Special2(RandomSpecialDirection(0, 1, 1, 1));
                //this.Special2(3);
            }
            else if (number2 > 60 && number2 <= 75)
            {
                /*if (this.EnemyDistance() > 1.8f && !this.player.tempOpponent.dead && Mathf.Abs(this.player.rb.velocity.y) <= 0f)
                {
                    *//*this.Special2(3);
                    this.playerInput.moveInput = new Vector3(this.player.transform.forward.z * 1f, 0f, 0f);


                    while (this.player.attackStuns.Count > 0)
                        yield return null;

                    yield return new WaitForSeconds(0.01f);*//*



                    float currentTime = 0;
                    float duration = Random.Range(0.2f, 0.3f);
                    while (this.EnemyDistance() > 1.8f && currentTime < duration)
                    {
                        currentTime += Time.deltaTime;
                        yield return null;
                    }


                }*/
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
            /*else if (number2 > 60 && number2 <= 70)
            {
                this.Punch(Random.Range(0, 4));

                float randomPunch = Random.Range(1, 101);

                while (this.player.attackStuns.Count > 0)
                    yield return null;

                yield return new WaitForSeconds(0.01f);

                if (randomPunch <= 80)
                    this.Punch(Random.Range(0, 4));
                else
                    this.Kick(Random.Range(0, 4));
                //this.Special2(Random.Range(0, 4));
                //this.Special2(3);
            }
            else if (number2 > 70 && number2 <= 80)
            {
                this.Kick(Random.Range(0, 4));
                //this.Special2(Random.Range(0, 4));
                //this.Special2(3);
            }*/
            else if (number2 > 75 && number2 <= 100)
            {
                if (!this.player.tempOpponent.dead && this.player.superCharge >= this.player.maxSuperCharge / 2)
                {
                    //this.Super(Random.Range(0, 4));

                    if(this.player.superCharge < this.player.maxSuperCharge)
                    {
                        //this.Super(RandomSpecialDirection(0, 1, 1, 0));

                        if (this.EnemyDistance() <= 1.8f)
                            this.Super(1);
                        else
                            this.Super(2);
                    }
                    else
                    {

                        if (this.EnemyDistance() <= 1.8f)
                            this.Super(RandomSpecialDirection(1, 1, 1, 1));
                        else
                            this.Super(RandomSpecialDirection(1, 0, 1, 0));


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
                        if (this.EnemyDistance() <= 2f)
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

    [ContextMenu("TestPunch")]
    public void TestPunch()
    {
        if(this.playerInput != null)
        {
            this.playerInput.PunchInput?.Invoke(true);
        }
    }

    


    [ContextMenu("TestSpecial1")]
    public void TestSpecial1()
    {
        if (this.playerInput != null)
        {
            this.playerInput.SpecialInput?.Invoke(true);
        }
    }


    public void Punch(int id = 0)
    {
        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.PunchInput?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    public void Kick(int id = 0)
    {
        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.KickInput?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    //[ContextMenu("BackSpecial")]
    public void Special1(int id = 0)
    {
        //this.StartCoroutine(this.Special1Coroutine(id));
        //Debug.Log(id);

        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.SpecialInput?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    

    public void Special2(int id = 0)
    {
        //this.StartCoroutine(this.Special2Coroutine(id));

        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.Special2Input?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    public void Super(int id = 0)
    {
        //this.StartCoroutine(this.Special2Coroutine(id));

        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.SuperInput?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    public void Taunt(int id = 0)
    {
        //this.StartCoroutine(this.Special2Coroutine(id));

        this.playerInput.moveInput = MoveInput(id);
        this.playerInput.TauntInput?.Invoke(true);
        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    public int RandomSpecialDirection(int neutral = 1, int forward = 1, int backward = 1, int down = 1)
    {
        //int[] direcionPool;

        List<int> direcionPool = new List<int>();

        if (neutral == 1)
            direcionPool.Add(0);

        if (forward == 1)
            direcionPool.Add(1);

        if (backward == 1)
            direcionPool.Add(2);

        if (down == 1)
            direcionPool.Add(3);


        /*foreach (int id in direcionPool)
        {
            Debug.Log(id);
        }*/

        //Debug.Log(direcionPool);

        //Debug.Log(direcionPool.Count);

        if (direcionPool.Count > 0)
        {
            int randomIndex = Random.Range(0, direcionPool.Count);
            Debug.Log(direcionPool[randomIndex]);
            return direcionPool[randomIndex];
        }
        else
        {
            return 0;
        }

        /*if(neutral == 1)
            direcionPool*/
    }

    

    private IEnumerator Special1Coroutine(int id = 0)
    {
        Vector3 randomInput = new Vector3(0f, 0f, 0f);

        float forwardZ = this.player.transform.forward.z;

        if (id == 1)
            randomInput = new Vector3(forwardZ * 1f, 0f, 0f);
        else if (id == 2)
            randomInput = new Vector3(forwardZ * -1f, 0f, 0f);
        else if (id == 3)
            randomInput = new Vector3(0f, -1f, 0f);

        //this.playerInput.moveInput = new Vector3(this.player.transform.forward.z * -1f, 0f, 0f);

        //this.playerInput.moveInput = randomInput;
        this.playerInput.moveInput = MoveInput(id);

        this.playerInput.SpecialInput?.Invoke(true);
        yield return new WaitForSeconds(0.01f);

        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }

    private IEnumerator Special2Coroutine(int id = 0)
    {
        this.playerInput.moveInput = MoveInput(id);

        this.playerInput.Special2Input?.Invoke(true);
        yield return new WaitForSeconds(0.01f);

        this.playerInput.moveInput = new Vector3(0f, 0f, 0f);
    }


    public float EnemyDistance()
    {
        
        if(this.player != null && this.player.tempOpponent != null)
        {
            return Mathf.Abs(this.player.transform.position.x - this.player.tempOpponent.transform.position.x);
        }
        else
        {
            return 0f;
        }
    }

    public float EnemyYDistance()
    {

        if (this.player != null && this.player.tempOpponent != null)
        {
            return Mathf.Abs(this.player.transform.position.y - this.player.tempOpponent.transform.position.y);
        }
        else
        {
            return 0f;
        }
    }

    public Vector3 MoveInput(int id = 0)
    {
        float forwardZ = this.player.transform.forward.z;

        //Debug.Log(id);

        if (id == 1)
            return new Vector3(forwardZ * 1f, 0f, 0f);
        else if (id == 2)
            return new Vector3(forwardZ * -1f, 0f, 0f);
        else if (id == 3)
            return new Vector3(0f, -1f, 0f);
        else
            return new Vector3(0f, 0f, 0f);

    }
}
