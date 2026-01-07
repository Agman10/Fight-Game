using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectLogic : MonoBehaviour
{
    public CharacterSelectCursorLogic p1Cursor;
    public CharacterSelectCursorLogic p2Cursor;

    public CharactersAndSkins[] characters;

    public CharStartToEndPos characterModelsP1;
    public CharStartToEndPos characterModelsP2;
    public AudioSource music;
    public Image fader;

    public bool starting;
    public bool quitting;
    public MusicTypeSelecter musicTypeSelecter;

    public StageSelectLogic stageSelectLogic;
    public GameObject characterSelectUI;
    public GameObject stageSelectUI;
    public GameObject allCharacterModels;

    public StageSelectLogic stageSelectLogicFightBall;
    public GameObject stageSelectUIFightBall;

    public static CharacterSelectLogic Instance;

    [Space]
    public GameObject randomCanvasPanel;
    public int randomRight;
    public int randomLeft;
    public int randomUp;
    public int randomDown;

    [Space]
    public int gameModeId = 0;
    public Material mainGameSkybox;
    public Material fightBallSkybox;
    [Space]
    public bool vsAI = false;
    public int vsAiId = 0;
    public PlayerInput emtyAi;

    [Space]
    public int[] randomCharacterPool;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(this.p1Cursor != null)
        {
            this.p1Cursor.OnReady += this.TryStarting;
        }

        if (this.p2Cursor != null)
        {
            this.p2Cursor.OnReady += this.TryStarting;
        }

        if (this.gameModeId != 0 && this.musicTypeSelecter != null)
            this.musicTypeSelecter.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (this.p1Cursor != null)
        {
            this.p1Cursor.OnReady -= this.TryStarting;
        }

        if (this.p2Cursor != null)
        {
            this.p2Cursor.OnReady -= this.TryStarting;
        }
    }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;

        if (GameModeManager.Instance != null)
        {
            this.gameModeId = GameModeManager.Instance.gameModeId;
            this.vsAI = GameModeManager.Instance.vsAi;
            this.vsAiId = GameModeManager.Instance.vsAiId;

            if (GameModeManager.Instance.gameModeId == 1)
                RenderSettings.skybox = this.fightBallSkybox;

            /*if (this.stageSelectLogic != null)
                this.stageSelectLogic.gameModeId = GameModeManager.Instance.gameModeId;*/
        }
    }

    public void SetCharacter(int id, bool isPlayer1 = true)
    {
        if(CharacterManager.Instance != null)
        {
            if (isPlayer1)
            {
                CharacterManager.Instance.player1 = this.characters[id].character;
                CharacterManager.Instance.player1Skin = this.characters[id].skins[0];
                CharacterManager.Instance.player1Id = this.characters[id].character.characterId;
            }
            else
            {
                CharacterManager.Instance.player2 = this.characters[id].character;
                CharacterManager.Instance.player2Skin = this.characters[id].skins[0];
                CharacterManager.Instance.player2Id = this.characters[id].character.characterId;
            }
            
        }
    }

    public void SetSkin(int characterId = 0, int skinId = 0, bool isPlayer1 = true)
    {
        if (CharacterManager.Instance != null)
        {
            if (isPlayer1)
            {
                //CharacterManager.Instance.player1 = this.characters[characterId].character;
                CharacterManager.Instance.player1Skin = this.characters[characterId].skins[skinId];
            }
            else
            {
                //CharacterManager.Instance.player2 = this.characters[characterId].character;
                CharacterManager.Instance.player2Skin = this.characters[characterId].skins[skinId];
            }

        }
    }

    public void SetRandomCharacter(bool isPlayer1)
    {
        if (isPlayer1)
        {
            int randomPlayerId = Random.Range(0, this.characters.Length);
            int randomPlayerSkinId = Random.Range(0, this.characters[randomPlayerId].skins.Length);

            CharacterManager.Instance.player1 = this.characters[randomPlayerId].character;
            CharacterManager.Instance.player1Skin = this.characters[randomPlayerId].skins[randomPlayerSkinId];
            CharacterManager.Instance.player1Id = randomPlayerId;
        }
        else
        {
            int randomPlayerId = Random.Range(0, this.characters.Length);
            int randomPlayerSkinId = Random.Range(0, this.characters[randomPlayerId].skins.Length);

            CharacterManager.Instance.player2 = this.characters[randomPlayerId].character;
            CharacterManager.Instance.player2Skin = this.characters[randomPlayerId].skins[randomPlayerSkinId];
            CharacterManager.Instance.player2Id = randomPlayerId;
        }
    }

    public void SetFixedRandomCharacter(bool isPlayer1)
    {
        int fixedRandomPlayerId = this.randomCharacterPool[Random.Range(0, this.randomCharacterPool.Length)];

        int randomPlayerId = Random.Range(0, this.characters.Length);
        int randomPlayerSkinId = Random.Range(0, this.characters[fixedRandomPlayerId].skins.Length);

        //Debug.Log(fixedRandomPlayerId);
        if (isPlayer1)
        {
            CharacterManager.Instance.player1 = this.characters[fixedRandomPlayerId].character;
            CharacterManager.Instance.player1Skin = this.characters[fixedRandomPlayerId].skins[randomPlayerSkinId];
            CharacterManager.Instance.player1Id = fixedRandomPlayerId;
        }
        else
        {
            CharacterManager.Instance.player2 = this.characters[fixedRandomPlayerId].character;
            CharacterManager.Instance.player2Skin = this.characters[fixedRandomPlayerId].skins[randomPlayerSkinId];
            CharacterManager.Instance.player2Id = fixedRandomPlayerId;
        }
    }

    public void TryStarting()
    {
        if (this.p1Cursor.ready && this.p2Cursor.ready)
        {
            if(CharacterManager.Instance != null)
            {
                if (CharacterManager.Instance.player1 == CharacterManager.Instance.player2 && CharacterManager.Instance.player1Skin == CharacterManager.Instance.player2Skin)
                {
                    int p1CharId = CharacterManager.Instance.player1.characterId;
                    int p2CharId = CharacterManager.Instance.player2.characterId;

                    if (this.p1Cursor.currentCharacterId < 0 && this.p2Cursor.currentCharacterId >= 0)
                    {
                        if (CharacterManager.Instance.player1Skin == this.characters[p1CharId].skins[0])
                            this.SetSkin(p1CharId, 1, true);
                        else
                            this.SetSkin(p1CharId, 0, true);
                    }
                    else if (this.p1Cursor.currentCharacterId >= 0 && this.p2Cursor.currentCharacterId < 0)
                    {
                        if (CharacterManager.Instance.player2Skin == this.characters[p2CharId].skins[0])
                            this.SetSkin(p2CharId, 1, false);
                        else
                            this.SetSkin(p2CharId, 0, false);
                    }
                    else if (this.p1Cursor.currentCharacterId < 0 && this.p2Cursor.currentCharacterId < 0)
                    {
                        if (CharacterManager.Instance.player2Skin == this.characters[p2CharId].skins[0])
                            this.SetSkin(p2CharId, 1, false);
                        else
                            this.SetSkin(p2CharId, 0, false);

                        Debug.Log("same skin");
                    }
                }

                
            }
            this.p1Cursor.lockedIn = true;
            this.p2Cursor.lockedIn = true;
            /*if (this.gameModeId == 1)
            {
                this.StartCoroutine(this.StartGameCoroutine());
            }
            else
            {
                if (this.stageSelectLogic != null)
                    this.StartCoroutine(this.OpenLevelSelectCoroutine());
                else
                {
                    this.StartCoroutine(this.StartGameCoroutine());
                }
            }*/

            if (this.gameModeId == 1)
            {
                if (this.stageSelectLogicFightBall != null)
                    this.StartCoroutine(this.OpenLevelSelectCoroutine());
                else
                    this.StartCoroutine(this.StartGameCoroutine());
            }
            else
            {
                if (this.stageSelectLogic != null)
                    this.StartCoroutine(this.OpenLevelSelectCoroutine());
                else
                    this.StartCoroutine(this.StartGameCoroutine());
            }

            


            /*if (this.gameModeId == 0 && CharacterManager.Instance != null && this.musicTypeSelecter != null)
                CharacterManager.Instance.musicTypeId = this.musicTypeSelecter.currentId;*/
            //this.StartGame();
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadSceneAsync("TestStage");

        /*if (GameModeManager.Instance != null && GameModeManager.Instance.gameModeId == 1)
        {
            SceneManager.LoadSceneAsync("TestFootBall");
        }
        else
        {
            SceneManager.LoadSceneAsync("TestStage");
        }*/

        if (this.gameModeId == 1)
        {
            SceneManager.LoadSceneAsync("TestFootBall");
        }
        else
        {
            SceneManager.LoadSceneAsync("TestStage");
        }
    }
    private IEnumerator StartGameCoroutine()
    {
        this.starting = true;

        if (this.vsAI && CharacterManager.Instance != null && UserInputManager.Instance != null)
        {
            int p2CharId = CharacterManager.Instance.player2.characterId;
            int p1CharId = CharacterManager.Instance.player1.characterId;

            /*PlayerInput aiInput = Instantiate(this.characters[p2CharId].aiInput, UserInputManager.Instance.transform);
            UserInputManager.Instance.p2Input = aiInput;*/
            if (this.characters[p2CharId].aiInput != null)
                UserInputManager.Instance.p2Input = this.characters[p2CharId].aiInput;
            else
                UserInputManager.Instance.p2Input = this.emtyAi;

            if(this.vsAiId == 1)
            {
                if (this.characters[p1CharId].aiInput != null)
                    UserInputManager.Instance.p1Input = this.characters[p1CharId].aiInput;
                else
                    UserInputManager.Instance.p1Input = this.emtyAi;
            }

            /*CharacterManager.Instance.vsAi = true;
            CharacterManager.Instance.vsAiId = this.vsAiId;*/
        }

        if(CharacterManager.Instance != null)
        {
            CharacterManager.Instance.vsAi = this.vsAI;
            CharacterManager.Instance.vsAiId = this.vsAiId;
        }

        yield return new WaitForSeconds(0.2f);

        if (this.characterModelsP1 != null)
        {
            //this.characterModelsP1.Move();
            this.characterModelsP1.MoveReverseCustomTime(0.25f);
        }

        if (this.characterModelsP2 != null)
        {
            //this.characterModelsP2.Move();
            this.characterModelsP2.MoveReverseCustomTime(0.25f);
        }

        float currentTime = 0;
        float duration = 1f;
        float targetVolume = 0.05f;
        //float targetRotation = 0f;
        float startVolume = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.music != null)
                this.music.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);

            /*if (this.fader != null)
                this.fader.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f,currentTime / duration));*/
            /*if (currentTime >= duration * 0.5f)
            {
                if (this.fader != null)
                    this.fader.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, (currentTime * 0.5f) / (duration * 1f)));
            }*/
            
            yield return null;
        }

        currentTime = 0;
        duration = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (this.fader != null)
                this.fader.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, currentTime / duration));
            yield return null;
        }

        //yield return new WaitForSeconds(1);
        this.StartGame();
    }

    private IEnumerator OpenLevelSelectCoroutine()
    {
        if (this.vsAI && CharacterManager.Instance != null && UserInputManager.Instance != null)
        {
            int p2CharId = CharacterManager.Instance.player2.characterId;
            int p1CharId = CharacterManager.Instance.player1.characterId;

            /*PlayerInput aiInput = Instantiate(this.characters[p2CharId].aiInput, UserInputManager.Instance.transform);
            UserInputManager.Instance.p2Input = aiInput;*/
            if (this.characters[p2CharId].aiInput != null)
                UserInputManager.Instance.p2Input = this.characters[p2CharId].aiInput;
            else
                UserInputManager.Instance.p2Input = this.emtyAi;

            if (this.vsAiId == 1)
            {
                if (this.characters[p1CharId].aiInput != null)
                    UserInputManager.Instance.p1Input = this.characters[p1CharId].aiInput;
                else
                    UserInputManager.Instance.p1Input = this.emtyAi;
            }

            /*CharacterManager.Instance.vsAi = true;
            CharacterManager.Instance.vsAiId = this.vsAiId;*/
        }

        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.vsAi = this.vsAI;
            CharacterManager.Instance.vsAiId = this.vsAiId;
        }

        yield return new WaitForSeconds(0.2f);

        if (this.characterModelsP1 != null)
        {
            //this.characterModelsP1.Move();
            this.characterModelsP1.MoveReverseCustomTime(0.25f);
        }

        if (this.characterModelsP2 != null)
        {
            //this.characterModelsP2.Move();
            this.characterModelsP2.MoveReverseCustomTime(0.25f);
        }

        yield return new WaitForSeconds(0.4f);

        if (this.p1Cursor != null)
            this.p1Cursor.canMove = false;

        if (this.p2Cursor != null)
            this.p2Cursor.canMove = false;

        if (this.characterSelectUI != null)
            this.characterSelectUI.SetActive(false);

        /*if (this.stageSelectUI != null)
            this.stageSelectUI.SetActive(true);

        if (this.stageSelectLogic != null)
            this.stageSelectLogic.gameObject.SetActive(true);*/

        if (this.gameModeId == 1)
        {
            if (this.stageSelectUIFightBall != null)
                this.stageSelectUIFightBall.SetActive(true);

            if (this.stageSelectLogicFightBall != null)
                this.stageSelectLogicFightBall.gameObject.SetActive(true);
        }
        else
        {
            if (this.stageSelectUI != null)
                this.stageSelectUI.SetActive(true);

            if (this.stageSelectLogic != null)
                this.stageSelectLogic.gameObject.SetActive(true);
        }


        if (this.allCharacterModels != null)
            this.allCharacterModels.SetActive(false);

        if (this.p1Cursor != null && this.p1Cursor.readyPanel != null)
            this.p1Cursor.readyPanel.SetActive(false);

        if (this.p2Cursor != null && this.p2Cursor.readyPanel != null)
            this.p2Cursor.readyPanel.SetActive(false);
    }

    public void ReturnToCharacterSelect()
    {
        if (this.characterSelectUI != null)
            this.characterSelectUI.SetActive(true);

        if (this.stageSelectUI != null)
            this.stageSelectUI.SetActive(false);

        if (this.stageSelectLogic != null)
            this.stageSelectLogic.gameObject.SetActive(false);


        if (this.stageSelectUIFightBall != null)
            this.stageSelectUIFightBall.SetActive(false);

        if (this.stageSelectLogicFightBall != null)
            this.stageSelectLogicFightBall.gameObject.SetActive(false);


        if (this.allCharacterModels != null)
            this.allCharacterModels.SetActive(true);

        if (this.p1Cursor != null)
        {
            this.p1Cursor.ready = false;
            this.p1Cursor.lockedIn = false;
        }

        if (this.p2Cursor != null)
        {
            this.p2Cursor.ready = false;
            this.p2Cursor.lockedIn = false;
        }

        this.StartCoroutine(this.ReturnToCharacterSelectCoroutine());

    }

    public IEnumerator ReturnToCharacterSelectCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        if (this.p1Cursor != null)
        {
            this.p1Cursor.canMove = true;
        }

        if (this.p2Cursor != null)
        {
            this.p2Cursor.canMove = true;
        }
    }

    public void QuitToTitle()
    {
        //Debug.Log("Quit");
        //SceneManager.LoadSceneAsync("Menu");

        if (!this.quitting)
        {
            this.quitting = true;
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}

[System.Serializable]
public class CharactersAndSkins
{
    public string name;
    public int fontSize = 40;
    public TestPlayer character;
    public GameObject canvasPanel;
    public SO_Skin[] skins;
    public PlayerInput aiInput;

    [Space]
    public int right;
    public int left;
    public int up;
    public int down;
}
