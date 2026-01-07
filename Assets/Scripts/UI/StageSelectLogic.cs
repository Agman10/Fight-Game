using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelectLogic : MonoBehaviour
{
    public CharacterSelectInput inputP1;
    public CharacterSelectInput inputP2;
    public GameObject cursorPanelTransform;
    private int currentStageId;
    public Image stagePreviewImage;
    public TextMeshProUGUI stageNameText;
    public int gameModeId;
    public CharacterSelectLogic characterSelectLogic;

    public bool canMove;
    public bool starting;

    public AudioSource music;
    public Image fader;
    public GameObject readyText;

    public MusicTypeSelecter musicTypeSelecter;


    public StageButton[] stages;

    [Space]
    public GameObject randomCanvasPanel;
    public GameObject randomPreview;
    public int randomRight;
    public int randomLeft;
    public int randomUp;
    public int randomDown;




    // Start is called before the first frame update
    void Start()
    {
        this.currentStageId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.cursorPanelTransform != null)
        {
            if (this.currentStageId >= 0)
            {
                if (this.stages[this.currentStageId].canvasPanel != null)
                    this.cursorPanelTransform.transform.position = this.stages[this.currentStageId].canvasPanel.transform.position;
            }
            else
            {
                if (this.randomCanvasPanel != null)
                    this.cursorPanelTransform.transform.position = this.randomCanvasPanel.transform.position;
            }
                
        }
    }

    private void OnEnable()
    {
        if(this.inputP1 != null)
        {
            this.inputP1.MoveInput += this.Move;
            this.inputP1.SelectInput += this.Select;
            this.inputP1.BackInput += this.Back;

            this.inputP1.NextMusicTypeInput += this.NextMusicType;
            this.inputP1.PreviousMusicTypeInput += this.PreviousMusicType;
        }

        if (this.inputP2 != null)
        {
            this.inputP2.MoveInput += this.Move;
            this.inputP2.SelectInput += this.Select;
            this.inputP2.BackInput += this.Back;

            this.inputP2.NextMusicTypeInput += this.NextMusicType;
            this.inputP2.PreviousMusicTypeInput += this.PreviousMusicType;
        }

        this.canMove = false;
        if (this.cursorPanelTransform != null)
            this.cursorPanelTransform.SetActive(false);

        this.StartCoroutine(this.EnableInputting());

        //this.currentStageId = -1;
    }
    private void OnDisable()
    {
        if (this.inputP1 != null)
        {
            this.inputP1.MoveInput -= this.Move;
            this.inputP1.SelectInput -= this.Select;
            this.inputP1.BackInput -= this.Back;

            this.inputP1.NextMusicTypeInput -= this.NextMusicType;
            this.inputP1.PreviousMusicTypeInput -= this.PreviousMusicType;
        }

        if (this.inputP2 != null)
        {
            this.inputP2.MoveInput -= this.Move;
            this.inputP2.SelectInput -= this.Select;
            this.inputP2.BackInput -= this.Back;

            this.inputP2.NextMusicTypeInput -= this.NextMusicType;
            this.inputP2.PreviousMusicTypeInput -= this.PreviousMusicType;
        }
    }

    public void Move(Vector3 direction)
    {
        if (this.canMove)
        {
            if (direction.x >= 1f)
            {
                if (this.currentStageId >= 0)
                {
                    this.SelectStage(this.stages[this.currentStageId].right);
                    //this.currentStageId = this.stages[this.currentStageId].right;
                }
                else
                {
                    this.SelectStage(this.randomRight);
                    //this.currentStageId = this.randomRight;
                }


            }
            else if (direction.x <= -1f)
            {
                if (this.currentStageId >= 0)
                {
                    this.SelectStage(this.stages[this.currentStageId].left);
                    //this.currentStageId = this.stages[this.currentStageId].left;
                }
                else
                {
                    this.SelectStage(this.randomLeft);
                    //this.currentStageId = this.randomLeft;
                }

            }
            else if (direction.y >= 1f)
            {
                if (this.currentStageId >= 0)
                {
                    this.SelectStage(this.stages[this.currentStageId].up);
                    //this.currentStageId = this.stages[this.currentStageId].up;
                }
                else
                {
                    this.SelectStage(this.randomUp);
                    //this.currentStageId = this.randomUp;
                }


            }
            else if (direction.y <= -1f)
            {
                if (this.currentStageId >= 0)
                {
                    this.SelectStage(this.stages[this.currentStageId].down);
                    //this.currentStageId = this.stages[this.currentStageId].down;
                }
                else
                {
                    this.SelectStage(this.randomDown);
                    //this.currentStageId = this.randomDown;
                }



            }
        }

        
    }

    public void Select(bool selecting)
    {
        if (this.canMove)
        {
            this.StartGame();
        }
    }

    public void Back(bool backing)
    {
        if (this.canMove)
        {
            if (this.characterSelectLogic != null)
                this.characterSelectLogic.ReturnToCharacterSelect();
        }
    }

    public void SelectStage(int id)
    {
        this.currentStageId = id;

        if (id <= this.stages.Length - 1 && id >= 0)
        {
            if (this.cursorPanelTransform != null && this.stages[id].canvasPanel != null)
                this.cursorPanelTransform.transform.position = this.stages[id].canvasPanel.transform.position;

            if (this.stagePreviewImage != null && this.stages[id].previewImage != null)
                this.stagePreviewImage.sprite = this.stages[id].previewImage;

            if (this.stageNameText != null)
                this.stageNameText.text = this.stages[id].name;

            if (this.randomPreview != null)
                this.randomPreview.SetActive(false);
        }
        else if (id < 0)
        {
            if (this.cursorPanelTransform != null && this.randomCanvasPanel)
                this.cursorPanelTransform.transform.position = this.randomCanvasPanel.transform.position;

            if (this.stagePreviewImage != null)
                this.stagePreviewImage.sprite = null;

            if (this.stageNameText != null)
                this.stageNameText.text = "Random";

            if (this.randomPreview != null)
                this.randomPreview.SetActive(true);
        }

        
    }

    public void StartGame()
    {
        this.canMove = false;

        int stageId = 0;

        if(this.currentStageId < 0)
        {
            //stageId = this.stages[Random.Range(0, this.stages.Length)].stageId;
            stageId = this.GetRandomStage();
            Debug.Log(stageId);
        }
        else
        {
            stageId = this.stages[this.currentStageId].stageId;
        }

        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.stageChosen = true;
            CharacterManager.Instance.stageId = stageId;
        }

        if (this.gameModeId == 0 && CharacterManager.Instance != null && this.musicTypeSelecter != null)
            CharacterManager.Instance.musicTypeId = this.musicTypeSelecter.currentId;

        this.StartCoroutine(this.StartGameCoroutine());
    }

    public int GetRandomStage()
    {
        List<int> stagepool = new List<int>();
        foreach (StageButton stage in this.stages)
        {
            for (int i = 0; i < stage.randomChance; i++)
            {
                stagepool.Add(stage.stageId);
            }
        }
        /*foreach (int id in stagepool)
            Debug.Log(id);*/
        return stagepool[Random.Range(0, stagepool.Count)];
    }

    private IEnumerator StartGameCoroutine()
    {
        this.starting = true;
        if (this.readyText != null)
            this.readyText.SetActive(true);

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

        this.LoadScene();
    }

    public void LoadScene()
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

    public void NextMusicType(bool next)
    {
        if (next && this.musicTypeSelecter != null && this.canMove)
            this.musicTypeSelecter.NextMusicType();
    }

    public void PreviousMusicType(bool previous)
    {
        if (previous && this.musicTypeSelecter != null && this.canMove)
            this.musicTypeSelecter.PreviousMusicType();
    }

    private IEnumerator EnableInputting()
    {
        yield return new WaitForSeconds(0.1f);
        if (this.cursorPanelTransform != null)
            this.cursorPanelTransform.SetActive(true);
        this.canMove = true;
    }
}

[System.Serializable]
public class StageButton
{
    public int stageId;
    public string name;
    public int randomChance = 100;
    
    public GameObject canvasPanel;
    public Sprite previewImage;

    [Space]
    public int right;
    public int left;
    public int up;
    public int down;
}
