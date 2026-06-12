using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VersusScreenLogic : MonoBehaviour
{
    public CharacterSelectLogic characterSelectLogic;
    public Transform cameraTransform;
    public Transform directionalLightTransform;
    public Material versusSkybox;
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;
    public GameObject[] p1Models;
    public GameObject[] p2Models;
    public GameObject gameText;
    public GameObject ballText;

    public AudioSource music;
    public int gameModeId;

    private void OnEnable()
    {
        

        if (CharacterManager.Instance != null)
        {
            //CharacterManager.Instance.
            if (this.p1Models[CharacterManager.Instance.player1Id] != null && CharacterManager.Instance.player1Id <= this.p1Models.Length - 1)
            {
                this.p1Models[CharacterManager.Instance.player1Id].SetActive(true);
            }

            if (this.p2Models[CharacterManager.Instance.player2Id] != null && CharacterManager.Instance.player2Id <= this.p2Models.Length - 1)
            {
                this.p2Models[CharacterManager.Instance.player2Id].SetActive(true);
            }

            if (this.characterSelectLogic != null)
            {
                if (this.player1Name != null)
                {
                    this.player1Name.text = this.characterSelectLogic.characters[CharacterManager.Instance.player1Id].name;
                }
                if (this.player2Name != null)
                {
                    this.player2Name.text = this.characterSelectLogic.characters[CharacterManager.Instance.player2Id].name;
                }
            }
        }

        

        if(this.versusSkybox != null)
        {
            RenderSettings.skybox = this.versusSkybox;
        }

        if(this.directionalLightTransform != null)
        {
            this.directionalLightTransform.localEulerAngles = new Vector3(50f, 0f, 0f);
        }

        RenderSettings.ambientLight = new Color32(89, 89, 89, 255);

        if (GameModeManager.Instance != null && GameModeManager.Instance.gameModeId == 1)
        {
            this.gameModeId = GameModeManager.Instance.gameModeId;
            if(GameModeManager.Instance.gameModeId == 1)
            {
                if (this.gameText != null && this.ballText != null)
                {
                    this.gameText.SetActive(false);
                    this.ballText.SetActive(true);
                }
            }
            
        }

        this.StartCoroutine(this.StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);

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
        yield return new WaitForSeconds(1f);

        this.LoadScene();
    }

    public void LoadScene()
    {

        if (this.gameModeId == 1)
        {
            SceneManager.LoadSceneAsync("TestFootBall");
        }
        else
        {
            SceneManager.LoadSceneAsync("TestStage");
        }
    }
}
