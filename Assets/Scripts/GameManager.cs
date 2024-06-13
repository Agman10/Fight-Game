using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameCamera gameCamera;

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
    public bool isStartingNewRound;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public GameObject player1WinText;
    public GameObject player2WinText;

    public Action OnRoundReset;

    public RandomSkybox tempSkyboxAndStageLogic;

    public int randomNumber;

    public int gameMode;

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

                if (this.p1Input != null)
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
                }

                /*if (UserInputManager.Instance != null)
                {
                    if (UserInputManager.Instance.player1Input != null)
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
                    }
                }*/

                if (CharacterManager.Instance != null)
                {
                    if (CharacterManager.Instance.player1Skin != null && p1.skin != null)
                        p1.skin.SetSkin(CharacterManager.Instance.player1Skin);

                    if (CharacterManager.Instance.player2Skin != null && p2.skin != null)
                        p2.skin.SetSkin(CharacterManager.Instance.player2Skin);
                }


                this.player1 = p1;
                this.player2 = p2;


                p1.tempOpponent = p2;
                p2.tempOpponent = p1;

                if (this.healthBars != null)
                {
                    this.healthBars.player1 = p1;
                    this.healthBars.player2 = p2;
                }

                if (this.gameCamera != null)
                {
                    this.gameCamera.player1 = p1.transform;
                    this.gameCamera.player2 = p2.transform;
                }
            }
        }
        //int number = Random.Range(1, 1001);
        this.randomNumber = UnityEngine.Random.Range(1, 1001);
    }
    private void OnEnable()
    {
        if(this.player1 != null && this.player2 != null)
        {
            /*this.player1.OnDeath += this.OnPlayer1Death;
            this.player2.OnDeath += this.OnPlayer2Death;*/

            this.player1.OnDeath += this.OnPlayerDeath;
            this.player2.OnDeath += this.OnPlayerDeath;


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
            }

            /*if (this.gameMode == 1)
            {
                this.player1.maxHealth = 100000f;
                this.player2.maxHealth = 100000f;

                this.player1.health = 100000f;
                this.player2.health = 100000f;
            }*/
        }
    }
    private void OnDisable()
    {
        if (this.player1 != null && this.player2 != null)
        {
            /*this.player1.OnDeath -= this.OnPlayer1Death;
            this.player2.OnDeath -= this.OnPlayer2Death;*/

            this.player1.OnDeath -= this.OnPlayerDeath;
            this.player2.OnDeath -= this.OnPlayerDeath;
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

    public void OnPlayerDeath()
    {
        if (!this.gameIsOver && !this.isStartingNewRound && this.gameMode == 0)
        {
            this.isStartingNewRound = true;
            this.StartCoroutine(this.PlayerDeathCoroutine());
        }
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        if (this.player1.dead)
            this.GiveScore(2);

        if (this.player2.dead)
            this.GiveScore(1);

        if (this.player1Score >= this.maxScore && this.player2Score < this.maxScore)
            this.EndTheGame(1);
        else if (this.player1Score < this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(2);
        else if (this.player1Score >= this.maxScore && this.player2Score >= this.maxScore)
            this.EndTheGame(0);
        else
            this.StartNewRound();

        /*if (this.player1.dead)
            this.player2Score++;

        if(this.player2.dead)
            this.player1Score++;*/

        //yield return new WaitForSeconds(4.5f);
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
    public void GiveScore(int playerNumber = 0)
    {
        if(playerNumber == 1)
        {
            this.player1Score++;

            if (this.player1ScoreText != null)
                this.player1ScoreText.text = this.player1Score.ToString();
        }
        else if(playerNumber == 2)
        {
            this.player2Score++;

            if (this.player2ScoreText != null)
                this.player2ScoreText.text = this.player2Score.ToString();
        }
        else
        {
            this.player1Score++;
            this.player2Score++;

            if (this.player1ScoreText != null)
                this.player1ScoreText.text = this.player1Score.ToString();

            if (this.player2ScoreText != null)
                this.player2ScoreText.text = this.player2Score.ToString();
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
        this.isStartingNewRound = false;
        if (this.player1 != null)
            this.player1.ResetPlayer();

        if (this.player2 != null)
            this.player2.ResetPlayer();
    }
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
        if(this.tempSkyboxAndStageLogic != null)
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
                this.gameCamera.mainCamera.backgroundColor = Color.black;

                if (this.ragingBeastSkull != null)
                    this.ragingBeastSkull.SetActive(true);
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
}
