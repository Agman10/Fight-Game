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

    [Space]
    public GameObject randomCanvasPanel;
    public int randomRight;
    public int randomLeft;
    public int randomUp;
    public int randomDown;
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

            this.StartCoroutine(this.StartGameCoroutine());

            //this.StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("TestStage");
    }
    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (this.characterModelsP1 != null)
            this.characterModelsP1.Move();

        if (this.characterModelsP2 != null)
            this.characterModelsP2.Move();

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

    public void QuitToTitle()
    {
        //Debug.Log("Quit");
        SceneManager.LoadSceneAsync("Menu");
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

    [Space]
    public int right;
    public int left;
    public int up;
    public int down;
}
