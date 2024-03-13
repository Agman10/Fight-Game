using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightBallLogic : MonoBehaviour
{
    public Ball ball;
    public TestPlayer player1;
    public TestPlayer player2;

    public bool gameIsOver;
    public int maxScore = 10;
    public int player1Score;
    public int player2Score;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public ParticleSystem p1GoalEffect;
    public ParticleSystem p2GoalEffect;

    public AudioSource music;
    public AudioSource victoryBgm;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.ball != null)
            this.ball.OnGoal += this.OnGoal;

        if (this.player1ScoreText != null)
            this.player1ScoreText.text = player1Score.ToString();

        if (this.player2ScoreText != null)
            this.player2ScoreText.text = player2Score.ToString();

        if (this.music != null)
            this.music.Play();
    }
    private void OnDisable()
    {
        if (this.ball != null)
            this.ball.OnGoal -= this.OnGoal;
    }

    public void OnGoal(bool isPlayer1)
    {
        //Debug.Log(isPlayer1);
        if (!this.gameIsOver)
        {
            if (isPlayer1)
            {
                this.player1Score++;
                if (this.player1ScoreText != null)
                    this.player1ScoreText.text = player1Score.ToString();

                if (this.player1Score >= this.maxScore)
                    this.EndGame(true);
                else
                    this.StartCoroutine(this.RespawnBallCoroutine());

                if (this.p2GoalEffect != null)
                    this.p2GoalEffect.Play();
            }
            else
            {
                this.player2Score++;
                if (this.player2ScoreText != null)
                    this.player2ScoreText.text = player2Score.ToString();

                if (this.player2Score >= this.maxScore)
                    this.EndGame(false);
                else
                    this.StartCoroutine(this.RespawnBallCoroutine());

                if (this.p1GoalEffect != null)
                    this.p1GoalEffect.Play();
            }
            if(this.player1Score >= this.maxScore - 1 || this.player2Score >= this.maxScore - 1)
            {
                if (this.music != null)
                    this.music.pitch = 1.1f;
            }
            if (this.player1Score >= this.maxScore - 1 && this.player2Score >= this.maxScore - 1)
            {
                if (this.music != null)
                    this.music.pitch = 1.3f;
            }

            if (this.player1 != null)
                this.player1.tempLookAtBall = false;
            if (this.player2 != null)
                this.player2.tempLookAtBall = false;
        }
        

        /*if (this.player1Score >= this.maxScore)
            this.EndGame(true);
        else if(this.player2Score >= this.maxScore)
            this.EndGame(false);*/
    }

    public void EndGame(bool isPlayer1)
    {
        if (!this.gameIsOver)
        {
            this.gameIsOver = true;
            if (isPlayer1)
            {
                if (this.player2 != null)
                    this.player2.Suicide();

                if (this.player1 != null)
                    this.player1.tempLookAtBall = false;
            }
            else
            {
                if (this.player1 != null)
                    this.player1.Suicide();

                if (this.player2 != null)
                    this.player2.tempLookAtBall = false;
            }

            if (this.music != null)
                this.music.Stop();

            if (this.victoryBgm != null)
                this.victoryBgm.Play();
        }
        
    }
    IEnumerator RespawnBallCoroutine()
    {
        yield return new WaitForSeconds(2f);
        /*if (this.player1 != null && this.player2 != null)
        {
            this.player1.Reset();
            this.player2.Reset();
        }*/
        this.RespawnBall();
    }
    public void RespawnBall()
    {
        if (this.player1 != null)
            this.player1.tempLookAtBall = true;
        if (this.player2 != null)
            this.player2.tempLookAtBall = true;
        if (this.ball != null)
            this.ball.gameObject.SetActive(true);
    }
}
