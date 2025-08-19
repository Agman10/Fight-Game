using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameCamera gameCamera;
    public RoundTimer roundTimer;

    public TestPlayer player1;
    public TestPlayer player2;

    public TestPlayer player1Prefab;
    public TestPlayer player2Prefab;

    public PlayerInput p1Input;
    public PlayerInput p2Input;

    public HealthBars healthBars;

    public bool gameIsOver;
    public int maxScore = 2;
    public int player1Score;
    public int player2Score;
    private int currentRoundNumber = 1;
    public bool isStartingNewRound;

    public int p1DeathType;
    public int p2DeathType;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    public MatchScoreDisplayLogic p1ScoreDisplay;
    public MatchScoreDisplayLogic p2ScoreDisplay;

    public CharacterIconManager p1Icon;
    public CharacterIconManager p2Icon;

    public GameObject player1WinText;
    public GameObject player2WinText;

    public Action OnRoundReset;

    public RandomSkybox tempSkyboxAndStageLogic;

    public int randomNumber;

    public int gameMode;

    public bool gameIsPaused = false;
    public PauseLogic pauseLogic;

    public KOUiLogic koUiLogic;

    public CharacterThemePlayer characterThemePlayer;

    public EntranceAnimationHandler entranceAnimationHandler;

    public GameObject blackAndWhiteFilter;

    [Space]
    public GameObject ragingBeastSkull;
    public LayerMask normalCameraLayers;
    public LayerMask ragingBeastMidAttackCameraLayers;
    public LayerMask ragingBeastDeathCameraLayers;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;

        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.draw = false;
            if (CharacterManager.Instance.player1 != null)
            {
                if (this.player1 != null)
                    this.player1.gameObject.SetActive(false);

                this.player1 = null;
                this.player1Prefab = CharacterManager.Instance.player1;
            }

            if (CharacterManager.Instance.player2 != null)
            {
                if (this.player2 != null)
                    this.player2.gameObject.SetActive(false);

                this.player2 = null;
                this.player2Prefab = CharacterManager.Instance.player2;
            }
        }

        if(this.player1 == null && this.player2 == null)
        {
            if (this.player1Prefab != null && this.player2Prefab != null)
            {
                TestPlayer p1 = this.player1Prefab;
                TestPlayer p2 = this.player2Prefab;

                p1 = Instantiate(p1, new Vector3(-7f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                p2 = Instantiate(p2, new Vector3(7f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));

                /*if (this.p1Input != null)
                {
                    PlayerInput player1Input = this.p1Input;
                    player1Input = Instantiate(player1Input, p1.transform);
                    p1.input = player1Input;
                    p1.SetInput(player1Input);
                }

                if (this.p2Input != null)
                {
                    PlayerInput player2Input = this.p2Input;
                    player2Input = Instantiate(player2Input, p2.transform);
                    p2.input = player2Input;
                    p2.SetInput(player2Input);
                }*/

                if (UserInputManager.Instance != null)
                {
                    /*if (UserInputManager.Instance.player1Input != null)
                    {
                        PlayerInput player1Input = UserInputManager.Instance.player1Input.GetComponent<PlayerInput>();
                        p1.input = player1Input;
                        p1.SetInput(player1Input);
                    }

                    if (UserInputManager.Instance.player2Input != null)
                    {
                        PlayerInput player2Input = UserInputManager.Instance.player2Input.GetComponent<PlayerInput>();
                        p2.input = player2Input;
                        p2.SetInput(player2Input);
                    }*/



                    if (UserInputManager.Instance.p1Input != null)
                    {
                        PlayerInput player1Input = UserInputManager.Instance.p1Input;

                        CharacterAI characterAI = UserInputManager.Instance.p1Input.GetComponent<CharacterAI>();
                        if (characterAI != null)
                        {
                            CharacterAI charAi = Instantiate(characterAI, p1.transform);
                            player1Input = charAi.GetComponent<PlayerInput>();

                            charAi.player = p1;
                            charAi.StartAI();
                        }

                        p1.input = player1Input;
                        p1.SetInput(player1Input);
                    }

                    if (UserInputManager.Instance.p2Input != null)
                    {
                        PlayerInput player2Input = UserInputManager.Instance.p2Input;

                        CharacterAI characterAI = UserInputManager.Instance.p2Input.GetComponent<CharacterAI>();
                        if (characterAI != null)
                        {
                            CharacterAI charAi = Instantiate(characterAI, p2.transform);
                            player2Input = charAi.GetComponent<PlayerInput>();

                            charAi.player = p2;
                            charAi.StartAI();
                        }

                        p2.input = player2Input;
                        p2.SetInput(player2Input);
                    }
                }

                if (CharacterManager.Instance != null)
                {
                    if (CharacterManager.Instance.player1Skin != null && p1.skin != null)
                        p1.skin.SetSkin(CharacterManager.Instance.player1Skin);

                    if (CharacterManager.Instance.player2Skin != null && p2.skin != null)
                        p2.skin.SetSkin(CharacterManager.Instance.player2Skin);

                    if (this.p1Icon != null)
                        this.p1Icon.SetIcon();

                    if (this.p2Icon != null)
                        this.p2Icon.SetIcon();
                }


                this.player1 = p1;
                this.player2 = p2;


                p1.tempOpponent = p2;
                p2.tempOpponent = p1;

                /*p1.lookAtPlayer();
                p2.lookAtPlayer();*/

                if (this.healthBars != null)
                {
                    this.healthBars.player1 = p1;
                    this.healthBars.player2 = p2;
                }

                if (this.gameCamera != null)
                {
                    this.gameCamera.player1 = p1.transform;
                    this.gameCamera.player2 = p2.transform;

                    /*if (p1.collision != null && p1.collision is CapsuleCollider capsuleCollider)
                        this.gameCamera.p1ExtraWidth = capsuleCollider.radius - 0.5f;

                    if (p2.collision != null && p2.collision is CapsuleCollider capsuleColliderP2)
                        this.gameCamera.p2ExtraWidth = capsuleColliderP2.radius - 0.5f;*/
                }
            }
        }
        //int number = Random.Range(1, 1001);
        this.randomNumber = UnityEngine.Random.Range(1, 1001);

        this.maxScore = PlayerPrefs.GetInt("MaxWins", 2);
    }
    private void OnEnable()
    {
        if(this.player1 != null && this.player2 != null)
        {
            /*this.player1.OnDeath += this.OnPlayer1Death;
            this.player2.OnDeath += this.OnPlayer2Death;*/

            /*this.player1.OnDeath += this.OnPlayerDeath;
            this.player2.OnDeath += this.OnPlayerDeath;*/

            this.player1.OnKO += this.OnPlayerDeath;
            this.player2.OnKO += this.OnPlayerDeath;


            this.player1.tempOpponent = this.player2;
            this.player2.tempOpponent = this.player1;

            this.player1.playerNumber = 1;
            this.player2.playerNumber = 2;

            if (this.healthBars != null)
            {
                this.healthBars.player1 = this.player1;
                this.healthBars.player2 = this.player2;
            }

            if (this.gameCamera != null)
            {
                this.gameCamera.player1 = this.player1.transform;
                this.gameCamera.player2 = this.player2.transform;


                if (this.player1.collision != null && this.player1.collision is CapsuleCollider capsuleCollider)
                    this.gameCamera.p1ExtraWidth = capsuleCollider.radius - 0.5f;

                if (this.player2.collision != null && this.player2.collision is CapsuleCollider capsuleColliderP2)
                    this.gameCamera.p2ExtraWidth = capsuleColliderP2.radius - 0.5f;
            }

            if (CharacterManager.Instance != null && this.characterThemePlayer != null && this.gameMode == 0)
            {
                if(CharacterManager.Instance.musicTypeId == 1)
                {
                    this.characterThemePlayer.PlayRandomCharacterTheme();
                }
                else if (CharacterManager.Instance.musicTypeId == 2)
                {
                    this.characterThemePlayer.PlayP1CharacterTheme();
                }
                else if (CharacterManager.Instance.musicTypeId == 3)
                {
                    this.characterThemePlayer.PlayP2CharacterTheme();
                }


                if (this.entranceAnimationHandler != null)
                {
                    this.entranceAnimationHandler.AddPlayers(this.player1, this.player2);
                }
            }

            /*if (this.gameMode == 1)
            {
                this.player1.maxHealth = 100000f;
                this.player2.maxHealth = 100000f;

                this.player1.health = 100000f;
                this.player2.health = 100000f;
            }*/

            if(this.p1ScoreDisplay != null && this.p2ScoreDisplay != null)
            {
                this.p1ScoreDisplay.SetMaxScore(this.maxScore);
                this.p2ScoreDisplay.SetMaxScore(this.maxScore);
            }
        }
    }
    private void OnDisable()
    {
        if (this.player1 != null && this.player2 != null)
        {
            /*this.player1.OnDeath -= this.OnPlayer1Death;
            this.player2.OnDeath -= this.OnPlayer2Death;*/

            /*this.player1.OnDeath -= this.OnPlayerDeath;
            this.player2.OnDeath -= this.OnPlayerDeath;*/

            this.player1.OnKO -= this.OnPlayerDeath;
            this.player2.OnKO -= this.OnPlayerDeath;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[ContextMenu("Pause")]
    public void TogglePauseGame(bool pause)
    {
        if (pause)
        {
            if (!this.gameIsPaused)
            {
                Time.timeScale = 0f;
                this.gameIsPaused = true;
                if (this.pauseLogic != null)
                {
                    this.pauseLogic.gameObject.SetActive(true);
                }
            }
            else
            {
                Time.timeScale = 1f;
                this.gameIsPaused = false;

                if (this.pauseLogic != null)
                {
                    this.pauseLogic.gameObject.SetActive(false);
                }
            }
        }
        
    }
    public void PauseGame()
    {
        /*Time.timeScale = 0f;
        this.gameIsPaused = true;*/

        if (this.pauseLogic != null)
        {
            Time.timeScale = 0f;
            this.gameIsPaused = true;
            this.pauseLogic.gameObject.SetActive(true);
        }
    }
    public void UnPauseGame()
    {
        /*Time.timeScale = 1f;
        this.gameIsPaused = false;*/

        if (this.pauseLogic != null)
        {
            Time.timeScale = 1f;
            this.gameIsPaused = false;
            this.pauseLogic.gameObject.SetActive(false);
        }
    }

    public void OnPlayerDeath(int koType, int playerNumber/*bool super*/)
    {
        if (this.roundTimer != null)
            this.roundTimer.StopTimer();

        if (playerNumber == 1)
            this.p1DeathType = koType;

        if (playerNumber == 2)
            this.p2DeathType = koType;

        if (!this.gameIsOver && !this.isStartingNewRound && this.gameMode == 0)
        {
            this.isStartingNewRound = true;

            if (this.koUiLogic != null)
                this.koUiLogic.RemoveAllText();

            if (koType == 1 /*super*/)
            {
                if (this.koUiLogic != null)
                    this.koUiLogic.HyperKOText();
            }
            else if (koType == 3/* || koType == 4*/)
            {
                if (this.koUiLogic != null)
                    this.koUiLogic.SuicideText();
            }
            else if (koType == 4)
            {
                if (this.koUiLogic != null)
                    this.koUiLogic.HyperSuicideText();
            }
            else if (koType == 5)
            {
                if (this.koUiLogic != null)
                    this.koUiLogic.RingOutText();
            }
            else
            {
                if (this.koUiLogic != null)
                    this.koUiLogic.KOText();
            }


            /*if (this.koUiLogic != null)
                this.koUiLogic.KOText();*/

            /*if (this.koUiLogic != null)
                this.koUiLogic.HyperKOText();*/

            //Time.timeScale = 0.5f;

            this.StartCoroutine(this.PlayerDeathCoroutine());
        }
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        //maybe make it so if both are dead in the first frame it says double K.O but if it isnt, then the second frame display it as it usualy is

        /*Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;*/

        /*if (TimeScaler.Instance != null)
        {
            //TimeScaler.Instance.SetTimeScale(0.1f, 0.5f, true);
            TimeScaler.Instance.KoTimeScale(0.1f, 0.5f);

            //TimeScaler.Instance.SetTimeScale(0.05f, 0.5f, true);

            //TimeScaler.Instance.SetTimeScale(0.1f, 0.7f, true);
        }*/

        yield return new WaitForSeconds(0.5f);

        //Time.timeScale = 1f;

        /*if (this.player1.dead)
            this.GiveScore(2);

        if (this.player2.dead)
            this.GiveScore(1);*/

        int perfect = 0;

        if (!this.player1.dead && this.player2.dead)
        {
            if (!this.player1.hasBeenHit)
                perfect = 2;

            this.GiveScore(1, this.p2DeathType, perfect);
        }
        else if (this.player1.dead && !this.player2.dead)
        {
            if (!this.player2.hasBeenHit)
                perfect = 2;

            this.GiveScore(2, this.p1DeathType , perfect);
        }
        else if (this.player1.dead && this.player2.dead)
        {
            this.GiveScore(1, this.p2DeathType, 1);
            this.GiveScore(2, this.p1DeathType, 1);
        }
            

        if (this.player1Score >= this.maxScore && this.player2Score < this.maxScore)
            this.EndTheGame(1);
        else if (this.player1Score < this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(2);
        else if (this.player1Score >= this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(0);
        else
            this.StartNewRound();


        if (this.koUiLogic != null)
        {
            if (this.player1.dead & this.player2.dead)
            {
                this.koUiLogic.DrawText();
            }
            else if (!this.player1.hasBeenHit)
            {
                this.koUiLogic.PerfectText(true);
            }
            else if (!this.player2.hasBeenHit)
            {
                this.koUiLogic.PerfectText(false);
            }
        }

        /*if (this.player1.dead)
            this.player2Score++;

        if(this.player2.dead)
            this.player1Score++;*/

        //yield return new WaitForSeconds(4.5f);
    }

    //[ContextMenu("TimeOver")]
    public void EvaluateTimeOverWinner()
    {
        if(!this.gameIsOver && !this.isStartingNewRound && this.gameMode == 0)
        {
            float p1HealthPercentage = (this.player1.health / this.player1.maxHealth) * 100;
            float p2HealthPercentage = (this.player2.health / this.player2.maxHealth) * 100;
            //Debug.Log(p1HealthPercentage);
            //Debug.Log(p2HealthPercentage);

            
            float healthDifference = Mathf.Abs(p1HealthPercentage - p2HealthPercentage);

            if (healthDifference < 1)
                this.TimeOver(0);
            else if (p1HealthPercentage > p2HealthPercentage)
                this.TimeOver(1);
            else if (p1HealthPercentage < p2HealthPercentage)
                this.TimeOver(2);

            /*float healthDifference = Mathf.Abs(this.player1.health - this.player2.health);

            if (healthDifference <= 1)
                this.TimeOver(0);
            else if (this.player1.health > this.player2.health)
                this.TimeOver(1);
            else if (this.player1.health < this.player2.health)
                this.TimeOver(2);*/

            //Debug.Log(healthDifference);
        }
    }

    
    public void TimeOver(int playerId = 0)
    {
        //int playerId = 0;
        //0 = draw

        if (!this.gameIsOver && !this.isStartingNewRound && this.gameMode == 0)
        {
            this.isStartingNewRound = true;

            if (this.koUiLogic != null)
                this.koUiLogic.TimeOverText();

            //Fix so you can get perfect

            if (playerId == 0)
            {
                this.GiveScore(1, 2, 1);
                this.GiveScore(2, 2, 1);

                this.player1.Die(this.player1.transform.position, false, false, true, false, 2);
                this.player2.Die(this.player2.transform.position, false, false, true, false, 2);
            }
            else if (playerId == 1)
            {
                this.GiveScore(1, 2, 0);

                //this.player1.Die(this.player1.transform.position, false, false, true, false, 2);
                this.player2.Die(this.player2.transform.position, false, false, true, false, 2);
            }
            else if (playerId == 2)
            {
                this.GiveScore(2, 2, 0);

                this.player1.Die(this.player1.transform.position, false, false, true, false, 2);
                //this.player2.Die(this.player2.transform.position, false, false, true, false, 2);
            }


            this.StartCoroutine(this.TimeOverCoroutine(playerId));
        }
    }

    private IEnumerator TimeOverCoroutine(int playerId = 0)
    {
        yield return new WaitForSeconds(0.5f);

        if (playerId == 0)
        {
            if (this.koUiLogic != null)
                this.koUiLogic.DrawText();
        }
        /*else if (playerId == 1 && !this.player1.hasBeenHit)
        {

        }
        else if (playerId == 2 && !this.player2.hasBeenHit)
        {

        }*/


        if (this.player1Score >= this.maxScore && this.player2Score < this.maxScore)
            this.EndTheGame(1);
        else if (this.player1Score < this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(2);
        else if (this.player1Score >= this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(0);
        else
            this.StartNewRound();
    }

    public void OnPlayer1Death()
    {
        if(!this.gameIsOver && !this.isStartingNewRound)
        {
            this.isStartingNewRound = true;
            this.player2Score++;

            if(this.player2ScoreText != null)
                this.player2ScoreText.text = this.player2Score.ToString();

            if (this.player2Score >= this.maxScore)
                this.EndGame(false);
            else
                this.StartNewRound();
        }
    }
    public void OnPlayer2Death()
    {
        if (!this.gameIsOver && !this.isStartingNewRound)
        {
            this.isStartingNewRound = true;
            this.player1Score++;

            if (this.player1ScoreText != null)
                this.player1ScoreText.text = this.player1Score.ToString();

            if (this.player1Score >= this.maxScore)
                this.EndGame(true);
            else
                this.StartNewRound();

        }
    }
    public void GiveScore(int playerNumber = 0, int scoreTypeId = 0, int performanceTypeId = 0)
    {
        if(playerNumber == 1)
        {
            this.player1Score++;

            if (this.p1ScoreDisplay != null)
                this.p1ScoreDisplay.AddScore(scoreTypeId, performanceTypeId);

            if (this.player1ScoreText != null)
                this.player1ScoreText.text = this.player1Score.ToString();

            if (this.p1ScoreText != null)
                this.p1ScoreText.text = this.player1Score.ToString();
        }
        else if(playerNumber == 2)
        {
            this.player2Score++;

            if (this.p2ScoreDisplay != null)
                this.p2ScoreDisplay.AddScore(scoreTypeId, performanceTypeId);

            if (this.player2ScoreText != null)
                this.player2ScoreText.text = this.player2Score.ToString();

            if (this.p2ScoreText != null)
                this.p2ScoreText.text = this.player2Score.ToString();
        }
        else
        {
            this.player1Score++;
            this.player2Score++;

            if (this.player1ScoreText != null)
                this.player1ScoreText.text = this.player1Score.ToString();

            if (this.player2ScoreText != null)
                this.player2ScoreText.text = this.player2Score.ToString();


            if (this.p1ScoreText != null)
                this.p1ScoreText.text = this.player1Score.ToString();

            if (this.p2ScoreText != null)
                this.p2ScoreText.text = this.player2Score.ToString();
        }
    }

    public void StartNewRound()
    {
        this.StartCoroutine(this.StartNewRoundCoroutine());
    }

    IEnumerator StartNewRoundCoroutine()
    {
        yield return new WaitForSeconds(5f);
        this.ResetRound();

    }
    public void ResetRound()
    {
        this.OnRoundReset?.Invoke();

        this.currentRoundNumber++;

        this.p1DeathType = 0;
        this.p2DeathType = 0;

        if (this.koUiLogic != null)
            this.koUiLogic.RemoveAllText();

        this.isStartingNewRound = false;
        if (this.player1 != null)
        {
            this.player1.ResetPlayer();
            //this.player1.LookAtTarget();
            this.player1.LookAtCenter();
        }
            

        if (this.player2 != null)
        {
            this.player2.ResetPlayer();
            //this.player2.LookAtTarget();
            this.player2.LookAtCenter();
        }
            

        if (this.roundTimer != null)
        {
            this.roundTimer.ResetTimer();
            //this.roundTimer.StartTimer();
        }

        if (this.entranceAnimationHandler != null)
        {
            this.entranceAnimationHandler.AddPlayers(this.player1, this.player2);
            this.entranceAnimationHandler.StartNewRound(this.currentRoundNumber);
        }

    }

    /*private IEnumerator StartNewRoundCoroutine()
    {
        yield return new WaitForSeconds(1);
    }*/

    public void EndGame(bool isPlayer1)
    {
        this.gameIsOver = true;
        //this.StartCoroutine(this.EndGameTextCoroutine(isPlayer1));

        if (CharacterManager.Instance != null)
        {
            if (isPlayer1)
            {
                CharacterManager.Instance.winnerCharacterId = this.player1.characterId;
                CharacterManager.Instance.loserCharacterId = this.player2.characterId;
                CharacterManager.Instance.winnerWasPlayer1 = true;
            }
            else
            {
                CharacterManager.Instance.winnerCharacterId = this.player2.characterId;
                CharacterManager.Instance.loserCharacterId = this.player1.characterId;
                CharacterManager.Instance.winnerWasPlayer1 = false;
            }
            CharacterManager.Instance.player1Id = this.player1.characterId;
            CharacterManager.Instance.player2Id = this.player2.characterId;
        }
        this.StartCoroutine(this.EndGameCoroutine());
    }

    public void EndTheGame(int playerNumber = 0)
    {
        this.gameIsOver = true;
        //this.StartCoroutine(this.EndGameTextCoroutine(isPlayer1));

        if (CharacterManager.Instance != null)
        {
            if (playerNumber == 1)
            {
                CharacterManager.Instance.winnerCharacterId = this.player1.characterId;
                CharacterManager.Instance.loserCharacterId = this.player2.characterId;
                CharacterManager.Instance.winnerWasPlayer1 = true;
                CharacterManager.Instance.draw = false;
            }
            else if (playerNumber == 2)
            {
                CharacterManager.Instance.winnerCharacterId = this.player2.characterId;
                CharacterManager.Instance.loserCharacterId = this.player1.characterId;
                CharacterManager.Instance.winnerWasPlayer1 = false;
                CharacterManager.Instance.draw = false;
            }
            else
            {
                CharacterManager.Instance.winnerCharacterId = -1;
                CharacterManager.Instance.loserCharacterId = -1;
                CharacterManager.Instance.winnerWasPlayer1 = false;
                CharacterManager.Instance.draw = true;
            }
            CharacterManager.Instance.player1Id = this.player1.characterId;
            CharacterManager.Instance.player2Id = this.player2.characterId;
        }
        this.StartCoroutine(this.EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        /*if(this.tempSkyboxAndStageLogic != null && this.tempSkyboxAndStageLogic.songs.Length - 1 >= this.tempSkyboxAndStageLogic.currentMusic && this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic] != null)
        {
            AudioSource music = this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic];
            float currentTime = 0;
            float duration = 2f;
            float targetVolume = 0f;
            //float targetRotation = 0f;
            float startVolume = music.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (music != null)
                    music.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }*/

        AudioSource music = null;
        if(this.gameMode == 0 && this.characterThemePlayer != null && CharacterManager.Instance != null && CharacterManager.Instance.musicTypeId > 0)
        {
            music = this.characterThemePlayer.themes[this.characterThemePlayer.musicId];
        }
        else if (this.tempSkyboxAndStageLogic != null && this.tempSkyboxAndStageLogic.songs.Length - 1 >= this.tempSkyboxAndStageLogic.currentMusic && this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic] != null)
        {
            music = this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic];
        }
        
        float currentTime = 0;
        float duration = 2f;
        float targetVolume = 0f;
        //float targetRotation = 0f;
        float startVolume = music.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (music != null)
                music.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

            yield return null;
        }


        SceneManager.LoadSceneAsync(3);
    }

    IEnumerator EndGameTextCoroutine(bool isPlayer1)
    {
        yield return new WaitForSeconds(5f);

        if (isPlayer1)
        {
            /*if (this.player1WinText != null)
                this.player1WinText.SetActive(true);*/

            SceneManager.LoadSceneAsync(3);
        }
        else
        {
            /*if (this.player2WinText != null)
                this.player2WinText.SetActive(true);*/

            SceneManager.LoadSceneAsync(4);
        }
    }

    public void RagingBeastEffect(int ragingBeastStageId = 0)
    {
        if(this.gameCamera != null && this.gameCamera.mainCamera != null)
        {
            if (ragingBeastStageId == 1)
            {
                this.gameCamera.mainCamera.cullingMask = this.ragingBeastMidAttackCameraLayers;
                this.gameCamera.mainCamera.clearFlags = CameraClearFlags.SolidColor;
                this.gameCamera.mainCamera.backgroundColor = Color.black;


            }
            else if (ragingBeastStageId == 2)
            {
                this.gameCamera.mainCamera.cullingMask = this.ragingBeastDeathCameraLayers;
                this.gameCamera.mainCamera.clearFlags = CameraClearFlags.SolidColor;
                this.gameCamera.mainCamera.backgroundColor = Color.black;

                if (this.ragingBeastSkull != null)
                    this.ragingBeastSkull.SetActive(true);
            }
            else if (ragingBeastStageId == 3)
            {
                this.gameCamera.mainCamera.cullingMask = this.ragingBeastDeathCameraLayers;
                this.gameCamera.mainCamera.clearFlags = CameraClearFlags.SolidColor;
                this.gameCamera.mainCamera.backgroundColor = Color.black;

                if (this.ragingBeastSkull != null)
                    this.ragingBeastSkull.SetActive(false);
            }
            else
            {
                this.gameCamera.mainCamera.cullingMask = this.normalCameraLayers;
                this.gameCamera.mainCamera.clearFlags = CameraClearFlags.Skybox;

                if (this.ragingBeastSkull != null)
                    this.ragingBeastSkull.SetActive(false);
            }
        }
        
    }

    public void PauseMusic()
    {
        if (CharacterManager.Instance != null && CharacterManager.Instance.musicTypeId != 0 && this.gameMode == 0 && this.characterThemePlayer != null)
        {
            this.characterThemePlayer.themes[this.characterThemePlayer.musicId].Pause();
        }
        else if (this.tempSkyboxAndStageLogic != null)
        {
            this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic].Pause();
        }
    }

    public void UnPauseMusic()
    {
        if (CharacterManager.Instance != null && CharacterManager.Instance.musicTypeId != 0 && this.gameMode == 0 && this.characterThemePlayer != null)
        {
            this.characterThemePlayer.themes[this.characterThemePlayer.musicId].Play();
        }
        else if (this.tempSkyboxAndStageLogic != null)
        {
            this.tempSkyboxAndStageLogic.songs[this.tempSkyboxAndStageLogic.currentMusic].Play();
        }
    }

    public void TurnBlackAndWhite()
    {
        if (PostProcessController.Instance != null)
            PostProcessController.Instance.TurnBlackAndWhite();

        if (this.blackAndWhiteFilter != null)
            this.blackAndWhiteFilter.SetActive(true);
    }

    public void ReturnColors()
    {
        if (PostProcessController.Instance != null)
            PostProcessController.Instance.ReturnColors();

        if (this.blackAndWhiteFilter != null)
            this.blackAndWhiteFilter.SetActive(false);
    }
}
