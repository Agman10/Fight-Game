using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationHandler : MonoBehaviour
{

    public TestPlayer player1;
    public TestPlayer player2;

    public bool P1Ready;
    public bool P2Ready;

    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
            GameManager.Instance.gameCamera.cameraIsLocked = false;*/
    }

    public void AddPlayers(TestPlayer p1, TestPlayer p2)
    {
        if(p1 != null && p2 != null)
        {
            this.player1 = p1;
            this.player2 = p2;
        }

        if(this.player1 != null && this.player2 != null)
        {
            this.player1.attackStuns.Add(this.gameObject);
            this.player2.attackStuns.Add(this.gameObject);

            /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
                GameManager.Instance.gameCamera.cameraIsLocked = true;*/

            this.player1.OnDeath += this.RemovePlayers;
            this.player2.OnDeath += this.RemovePlayers;

            this.player1.OnEntranceDone += this.EntranceDone;
            this.player2.OnEntranceDone += this.EntranceDone;
        }

        
    }

    public void EntranceDone(int playerNumber)
    {
        //Debug.Log("EntranceDone " + playerNumber);

        if (playerNumber == 1 && !this.P1Ready)
        {
            this.P1Ready = true;
        }

        if (playerNumber == 2 && !this.P2Ready)
        {
            this.P2Ready = true;
        }

        if(this.P1Ready && this.P2Ready)
        {
            this.RemovePlayers();
        }

        
    }

    public void RemovePlayers()
    {
        if (this.player1 != null && this.player2 != null && !this.done)
        {
            this.player1.attackStuns.Remove(this.gameObject);
            this.player2.attackStuns.Remove(this.gameObject);

            /*this.P1Ready = true;
            this.P2Ready = true;*/

            this.done = true;


            this.player1.OnDeath -= this.RemovePlayers;
            this.player2.OnDeath -= this.RemovePlayers;

            this.player1.OnEntranceDone -= this.EntranceDone;
            this.player2.OnEntranceDone -= this.EntranceDone;

            /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
                GameManager.Instance.gameCamera.cameraIsLocked = false;*/

            //Debug.Log("RemovePlayers");
        }
    }
}
