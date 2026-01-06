using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    public ParticleSystem p1GoalEffect;
    public ParticleSystem p2GoalEffect;

    public AudioSource music;
    public AudioSource victoryBgm;

    public RandomSkybox tempRandomSkybox;

    public AudioSource goalSfx;

    public GoalUiLogic goalUiLogic;

    public EntranceAnimationHandler entranceAnimationHandler;
    // Start is called before the first frame update

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player1 != null)
            {
                //TestPlayer p1 = CharacterManager.Instance.player1;

                this.player1 = GameManager.Instance.player1;
                //this.player1 = CharacterManager.Instance.player1;
            }

            if (GameManager.Instance.player2 != null)
            {
                this.player2 = GameManager.Instance.player2;
            }
        }

        /*if (this.player1 != null && this.player2 != null)
        {

            if (this.ball != null)
            {
                this.player1.tempBall = this.ball.gameObject;
                this.player2.tempBall = this.ball.gameObject;
            }

        }*/
    }
    private void OnEnable()
    {
        if (this.ball != null)
            this.ball.OnGoal += this.OnGoal;

        /*if (this.player1ScoreText != null)
            this.player1ScoreText.text = player1Score.ToString();

        if (this.player2ScoreText != null)
            this.player2ScoreText.text = player2Score.ToString();*/

        if (this.p1ScoreText != null)
            this.p1ScoreText.text = this.player1Score.ToString();

        if (this.p2ScoreText != null)
            this.p2ScoreText.text = this.player2Score.ToString();

        /*if (this.music != null)
            this.music.Play();*/


        if (this.player1 != null && this.player2 != null)
        {
            this.player1.maxHealth = 100000f;
            this.player2.maxHealth = 100000f;

            this.player1.health = 100000f;
            this.player2.health = 100000f;

            if (this.ball != null)
            {
                this.player1.tempBall = this.ball.gameObject;
                this.player2.tempBall = this.ball.gameObject;

                this.player1.tempLookAtBall = true;
                this.player2.tempLookAtBall = true;
            }

            if (this.entranceAnimationHandler != null)
            {
                this.entranceAnimationHandler.AddPlayers(this.player1, this.player2);
            }
        }

        //this.StartCoroutine(this.SetBall());
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
                /*if (this.player1ScoreText != null)
                    this.player1ScoreText.text = player1Score.ToString();*/

                if (this.p1ScoreText != null)
                    this.p1ScoreText.text = this.player1Score.ToString();

                if (this.player1Score >= this.maxScore)
                {
                    this.EndGame(true);
                }
                else
                {
                    this.StartCoroutine(this.RespawnBallCoroutine());
                    if (this.goalUiLogic != null)
                        this.goalUiLogic.GoalText(true);
                }

                if (this.p2GoalEffect != null)
                    this.p2GoalEffect.Play();
            }
            else
            {
                this.player2Score++;
                /*if (this.player2ScoreText != null)
                    this.player2ScoreText.text = player2Score.ToString();*/

                if (this.p2ScoreText != null)
                    this.p2ScoreText.text = this.player2Score.ToString();

                if (this.player2Score >= this.maxScore)
                {
                    this.EndGame(false);
                }
                else
                {
                    this.StartCoroutine(this.RespawnBallCoroutine());

                    if (this.goalUiLogic != null)
                        this.goalUiLogic.GoalText(false);
                }

                if (this.p1GoalEffect != null)
                    this.p1GoalEffect.Play();
            }
            if(this.player1Score >= this.maxScore - 1 || this.player2Score >= this.maxScore - 1)
            {
                /*if (this.music != null)
                    this.music.pitch = 1.1f;*/

                if (this.tempRandomSkybox != null)
                {
                    foreach (AudioSource song in this.tempRandomSkybox.songs)
                    {
                        if (song != null)
                            song.pitch = 1.1f;
                    }
                }
            }
            if (this.player1Score >= this.maxScore - 1 && this.player2Score >= this.maxScore - 1)
            {
                /*if (this.music != null)
                    this.music.pitch = 1.3f;*/

                if (this.tempRandomSkybox != null)
                {
                    foreach (AudioSource song in this.tempRandomSkybox.songs)
                    {
                        if (song != null)
                            song.pitch = 1.3f;
                    }
                }
            }

            if (this.player1 != null)
                this.player1.tempLookAtBall = false;
            if (this.player2 != null)
                this.player2.tempLookAtBall = false;

            if (this.goalSfx != null)
                this.goalSfx.Play();
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
                {
                    this.player2.Suicide();
                    this.player2.health = 0f;
                }
                    

                if (this.player1 != null)
                {
                    this.player1.tempLookAtBall = false;

                    if(this.goalUiLogic != null)
                    {
                        bool isPerfect = false;
                        if (this.player2Score <= 0)
                            isPerfect = true;

                        float textSize = this.player1.nameSize;

                        this.goalUiLogic.WinText(true, "<size=" + textSize + "%>" + this.player1.characterName, isPerfect);
                    }
                }
            }
            else
            {
                if (this.player1 != null)
                {
                    this.player1.Suicide();
                    this.player1.health = 0f;
                }
                    

                if (this.player2 != null)
                {
                    this.player2.tempLookAtBall = false;

                    if (this.goalUiLogic != null)
                    {
                        bool isPerfect = false;
                        if(this.player1Score <= 0)
                            isPerfect = true;

                        float textSize = this.player2.nameSize;

                        this.goalUiLogic.WinText(false, "<size=" + textSize + "%>" + this.player2.characterName, isPerfect);
                    }
                }
            }

            /*if (this.music != null)
                this.music.Stop();*/
            if (this.tempRandomSkybox != null)
            {
                foreach (AudioSource song in this.tempRandomSkybox.songs)
                {
                    if (song != null)
                        song.Stop();
                }
            }
                

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

    IEnumerator SetBall()
    {
        yield return new WaitForSeconds(0.05f);
        if (this.player1 != null && this.player2 != null)
        {

            if (this.ball != null)
            {
                /*this.player1.tempBall = null;
                this.player2.tempBall = null;*/

                this.player1.tempBall = this.ball.gameObject;
                this.player2.tempBall = this.ball.gameObject;

                this.player1.tempLookAtBall = true;
                this.player2.tempLookAtBall = true;
            }
            
            Debug.Log("test");

        }
    }
}
