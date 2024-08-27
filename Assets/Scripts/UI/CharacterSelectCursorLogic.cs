using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectCursorLogic : MonoBehaviour
{
    public CharacterSelectLogic characterSelectLogic;
    public GameObject cursorPanelTransform;
    public Text characterNameText;
    public CharacterSelectInput input;

    public CharacterSelectCursorLogic rivalCursor;

    public System.Action OnReady;

    public bool isPlayer1;
    public int currentCharacterId;
    public int currentSkinId;

    public bool selectingSkin;
    public bool ready;
    public bool lockedIn;

    public CharacterSkinTest[] characterModels;

    public GameObject randomModel;

    public bool canMove;

    public GameObject silhouette;
    public GameObject joinText;

    public MusicTypeSelecter musicTypeSelecter;

    [Space]
    public GameObject readyPanel;
    public GameObject skinSelectPanel;
    public Text skinIndexText;

    public GameObject glowPlatform;

    private void OnEnable()
    {
        if (this.input != null)
        {
            this.input.MoveInput += this.Move;
            this.input.SelectInput += this.Select;
            this.input.BackInput += this.Back;
            this.input.QuitInput += this.Quit;

            this.input.NextMusicTypeInput += this.NextMusicType;
            this.input.PreviousMusicTypeInput += this.PreviousMusicType;
        }
        this.SelectCharacter(this.currentCharacterId);

        if (this.cursorPanelTransform != null)
            this.cursorPanelTransform.SetActive(true);

        this.canMove = false;
        this.StartCoroutine(this.EnableInputting());

        if (this.silhouette != null)
            this.silhouette.SetActive(false);

        if (this.joinText != null)
            this.joinText.SetActive(false);
    }

    private void OnDisable()
    {
        if (this.input != null)
        {
            this.input.MoveInput -= this.Move;
            this.input.SelectInput -= this.Select;
            this.input.BackInput -= this.Back;
            this.input.QuitInput -= this.Quit;

            this.input.NextMusicTypeInput -= this.NextMusicType;
            this.input.PreviousMusicTypeInput -= this.PreviousMusicType;
        }
    }
    [ContextMenu("SelectChar")]
    public void TempSelectChar()
    {
        this.SelectCharacter(-1);
    }

    public void SelectCharacter(int id)
    {
        if(this.characterSelectLogic != null)
        {
            foreach (CharacterSkinTest charactermodel in this.characterModels)
            {
                charactermodel.gameObject.SetActive(false);
                //charactermodel.SetSkin()
            }

            if (this.randomModel != null)
                this.randomModel.gameObject.SetActive(false);

            this.currentSkinId = 0;

            if (id <= this.characterSelectLogic.characters.Length - 1 && id >= 0)
            {
                /*foreach (GameObject charactermodel in this.characterModels)
                {
                    charactermodel.gameObject.SetActive(false);
                }*/
                this.characterModels[id].SetSkin(this.characterSelectLogic.characters[id].skins[0]);

                this.characterModels[id].gameObject.SetActive(true);

                this.cursorPanelTransform.transform.position = this.characterSelectLogic.characters[id].canvasPanel.transform.position;

                if (this.characterNameText != null)
                {
                    this.characterNameText.text = this.characterSelectLogic.characters[id].name;
                    this.characterNameText.fontSize = this.characterSelectLogic.characters[id].fontSize;
                }
                    
            }
            else if (id < 0)
            {
                if (this.randomModel != null)
                    this.randomModel.gameObject.SetActive(true);

                this.cursorPanelTransform.transform.position = this.characterSelectLogic.randomCanvasPanel.transform.position;

                if (this.characterNameText != null)
                {
                    this.characterNameText.text = "Random";
                    this.characterNameText.fontSize = 40;

                    /*this.characterNameText.text = "Random   (Including secrets)";
                    this.characterNameText.text = "Random      (Secrets only)";
                    this.characterNameText.fontSize = 25;*/
                }
                    
            }
            else
            {
                Debug.Log("Character Dont Exist");
            }
            
        }
    }
    public void Move(Vector3 direction)
    {
        //Debug.Log(direction);

        if (!this.ready && this.canMove)
        {
            if (!this.selectingSkin)
            {
                this.PickCharacter(direction);
            }
            else
            {
                this.PickSkin(direction);
            }
        }
    }

    public void PickCharacter(Vector3 direction)
    {
        /*this.SelectCharacter(this.characterSelectLogic.characters[this.currentCharacterId].right);
        this.currentCharacterId = this.characterSelectLogic.characters[this.currentCharacterId].right;*/

        if (direction.x >= 1f)
        {
            if (this.currentCharacterId >= 0)
            {
                this.SelectCharacter(this.characterSelectLogic.characters[this.currentCharacterId].right);
                this.currentCharacterId = this.characterSelectLogic.characters[this.currentCharacterId].right;
            }
            else
            {
                this.SelectCharacter(this.characterSelectLogic.randomRight);
                this.currentCharacterId = this.characterSelectLogic.randomRight;
            }

            //this.PickCharacter(this.characterSelectLogic.characters[this.currentCharacterId].right);
        }
        else if (direction.x <= -1f)
        {
            //this.PickCharacter(this.characterSelectLogic.characters[this.currentCharacterId].left);

            if (this.currentCharacterId >= 0)
            {
                this.SelectCharacter(this.characterSelectLogic.characters[this.currentCharacterId].left);
                this.currentCharacterId = this.characterSelectLogic.characters[this.currentCharacterId].left;
            }
            else
            {
                this.SelectCharacter(this.characterSelectLogic.randomLeft);
                this.currentCharacterId = this.characterSelectLogic.randomLeft;
            }

        }
        else if (direction.y >= 1f)
        {
            //this.PickCharacter(this.characterSelectLogic.characters[this.currentCharacterId].up);
            if (this.currentCharacterId >= 0)
            {
                this.SelectCharacter(this.characterSelectLogic.characters[this.currentCharacterId].up);
                this.currentCharacterId = this.characterSelectLogic.characters[this.currentCharacterId].up;
            }
            else
            {
                this.SelectCharacter(this.characterSelectLogic.randomUp);
                this.currentCharacterId = this.characterSelectLogic.randomUp;
            }

        }
        else if (direction.y <= -1f)
        {
            //this.PickCharacter(this.characterSelectLogic.characters[this.currentCharacterId].down);
            if (this.currentCharacterId >= 0)
            {
                this.SelectCharacter(this.characterSelectLogic.characters[this.currentCharacterId].down);
                this.currentCharacterId = this.characterSelectLogic.characters[this.currentCharacterId].down;
            }
            else
            {
                this.SelectCharacter(this.characterSelectLogic.randomDown);
                this.currentCharacterId = this.characterSelectLogic.randomDown;
            }

        }
        
    }
    public void PickSkin(Vector3 direction)
    {
        if (direction.x >= 1f)
        {
            if (this.currentSkinId < this.characterSelectLogic.characters[this.currentCharacterId].skins.Length - 1)
            {
                this.currentSkinId++;
            }
            else
            {
                this.currentSkinId = 0;
            }

            if (this.characterModels[this.currentCharacterId] != null)
                this.characterModels[this.currentCharacterId].SetSkin(this.characterSelectLogic.characters[this.currentCharacterId].skins[this.currentSkinId]);

            if (this.skinIndexText != null)
                this.skinIndexText.text = (this.currentSkinId + 1).ToString();
        }
        else if (direction.x <= -1f)
        {
            if (this.currentSkinId > 0)
            {
                this.currentSkinId--;
            }
            else
            {
                this.currentSkinId = this.characterSelectLogic.characters[this.currentCharacterId].skins.Length - 1;
            }

            if (this.characterModels[this.currentCharacterId] != null)
                this.characterModels[this.currentCharacterId].SetSkin(this.characterSelectLogic.characters[this.currentCharacterId].skins[this.currentSkinId]);

            if (this.skinIndexText != null)
                this.skinIndexText.text = (this.currentSkinId + 1).ToString();
        }
    }

    public void SetSkin(int id)
    {
        this.currentSkinId = id;
        if (this.characterModels[this.currentCharacterId] != null)
            this.characterModels[this.currentCharacterId].SetSkin(this.characterSelectLogic.characters[this.currentCharacterId].skins[id]);

        this.characterSelectLogic.SetSkin(this.currentCharacterId, this.currentSkinId, this.isPlayer1);

        if (this.skinIndexText != null)
            this.skinIndexText.text = (this.currentSkinId + 1).ToString();
    }

    public void Select(bool selecting)
    {
        if (selecting && !this.ready && this.characterSelectLogic != null && this.canMove)
        {
            if(this.currentCharacterId >= 0)
            {
                if (!this.selectingSkin)
                {
                    this.characterSelectLogic.SetCharacter(this.currentCharacterId, this.isPlayer1);
                    this.selectingSkin = true;
                    if (this.skinSelectPanel != null)
                        this.skinSelectPanel.gameObject.SetActive(true);
                    if (this.skinIndexText != null)
                        this.skinIndexText.text = (this.currentSkinId + 1).ToString();
                }
                else
                {
                    this.ready = true;
                    this.characterSelectLogic.SetSkin(this.currentCharacterId, this.currentSkinId, this.isPlayer1);
                    this.selectingSkin = false;
                    if (this.skinSelectPanel != null)
                        this.skinSelectPanel.SetActive(false);

                    if (this.readyPanel != null)
                        this.readyPanel.SetActive(true);

                    if(this.rivalCursor != null && this.rivalCursor.ready)
                    {
                        if(this.rivalCursor.currentCharacterId == this.currentCharacterId && this.rivalCursor.currentSkinId == this.currentSkinId)
                        {
                            if (this.currentSkinId == 0)
                                this.SetSkin(1);
                            else
                                this.SetSkin(0);
                        }
                    }

                    this.OnReady?.Invoke();
                    if (this.glowPlatform != null)
                        this.glowPlatform.SetActive(true);
                }
            }
            else
            {
                this.ready = true;
                //this.characterSelectLogic.SetRandomCharacter(this.isPlayer1);
                this.characterSelectLogic.SetFixedRandomCharacter(this.isPlayer1);
                if (this.readyPanel != null)
                    this.readyPanel.SetActive(true);

                this.OnReady?.Invoke();

                if (this.glowPlatform != null)
                    this.glowPlatform.SetActive(true);
            }
            
        }
    }

    public void Back(bool backing)
    {
        if (backing && !this.lockedIn && this.canMove)
        {
            if(this.currentCharacterId >= 0)
            {
                if (this.selectingSkin && !this.ready)
                {
                    this.selectingSkin = false;
                    if (this.skinSelectPanel != null)
                        this.skinSelectPanel.gameObject.SetActive(false);
                }
                else if (!this.selectingSkin && this.ready)
                {
                    this.selectingSkin = true;
                    this.ready = false;

                    if (this.skinSelectPanel != null)
                        this.skinSelectPanel.SetActive(true);

                    if (this.readyPanel != null)
                        this.readyPanel.SetActive(false);

                    if (this.glowPlatform != null)
                        this.glowPlatform.SetActive(false);
                }
            }
            else
            {
                if (this.ready)
                {
                    this.ready = false;
                    if (this.readyPanel != null)
                        this.readyPanel.SetActive(false);

                    if (this.glowPlatform != null)
                        this.glowPlatform.SetActive(false);
                }
            }
        }
        
    }
    public void Quit(bool quitting)
    {
        if (quitting && this.characterSelectLogic != null && this.canMove)
            this.characterSelectLogic.QuitToTitle();
    }

    public void NextMusicType(bool next)
    {
        if(this.characterSelectLogic != null && !this.characterSelectLogic.starting && this.characterSelectLogic.gameModeId == 0)
        {
            if (next && this.musicTypeSelecter != null && this.canMove)
                this.musicTypeSelecter.NextMusicType();
        }

        
    }

    public void PreviousMusicType(bool previous)
    {
        if(this.characterSelectLogic != null && !this.characterSelectLogic.starting && this.characterSelectLogic.gameModeId == 0)
        {
            if (previous && this.musicTypeSelecter != null && this.canMove)
                this.musicTypeSelecter.PreviousMusicType();
        }

        
    }

    private IEnumerator EnableInputting()
    {
        yield return new WaitForSeconds(0.1f);
        this.canMove = true;
    }
}
