using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    public PlayerInput playerInput;
    public TestPlayer player;

    public virtual void OnEnable()
    {
        if (this.playerInput != null && this.player != null)
        {
            //this.StartCoroutine(this.MoveRandomlyCoroutine());
            //this.StartCoroutine(this.MoveRandomlyCoroutineGeneral());
            //this.StartCoroutine(this.DoRandomSpecials());

            this.StartAI();
        }

    }

    public virtual void StartAI()
    {

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

        /*if (neutral == 1)
            direcionPool.Add(0);

        if (forward == 1)
            direcionPool.Add(1);

        if (backward == 1)
            direcionPool.Add(2);

        if (down == 1)
            direcionPool.Add(3);*/


        for (int i = 0; i < neutral; i++)
            direcionPool.Add(0);

        for (int i = 0; i < forward; i++)
            direcionPool.Add(1);

        for (int i = 0; i < backward; i++)
            direcionPool.Add(2);

        for (int i = 0; i < down; i++)
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
            //Debug.Log(direcionPool[randomIndex]);
            return direcionPool[randomIndex];
        }
        else
        {
            //return 0;
            return Random.Range(0, 4);
        }

        /*if(neutral == 1)
            direcionPool*/
    }

    public float EnemyXDistance()
    {

        if (this.player != null && this.player.tempOpponent != null)
        {
            return Mathf.Abs(this.player.transform.position.x - this.player.tempOpponent.transform.position.x);
        }
        else
        {
            return 0f;
        }
    }

    public float EnemyYDistance(bool absolute = false)
    {

        if (this.player != null && this.player.tempOpponent != null)
        {
            if(absolute)
                return this.player.tempOpponent.transform.position.y - this.player.transform.position.y;
            else
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
