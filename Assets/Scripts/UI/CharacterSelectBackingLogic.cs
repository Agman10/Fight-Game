using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectBackingLogic : MonoBehaviour
{
    public GameObject backingObject;
    public Slider backingSlider;
    public CharacterSelectCursorLogic selectCursor;

    public float backingProgress;
    public bool hasBacked = false;

    private bool canBack;
    public float canBackTimer;

    private float dissapearValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.dissapearValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.selectCursor != null && this.selectCursor.input != null && this.backingObject != null && this.backingSlider != null && !this.hasBacked && CharacterSelectLogic.Instance != null && !CharacterSelectLogic.Instance.quitting)
        {
            if(!this.selectCursor.selectingSkin && !this.selectCursor.ready && !this.selectCursor.lockedIn)
            {
                if (!this.canBack)
                {
                    this.canBackTimer += 2f * Time.deltaTime;
                }
                else
                {
                    if (this.selectCursor.input.backing)
                    {
                        if (this.backingProgress < 1f)
                            this.backingProgress += 1f * Time.deltaTime;
                        else
                            this.hasBacked = true;
                    }
                    else
                    {
                        if (this.backingProgress > 0f)
                            this.backingProgress -= 2f * Time.deltaTime;
                        else
                            this.backingProgress = 0f;
                    }
                }


                
                    
            }
            else
            {
                if (this.backingProgress > 0f)
                    this.backingProgress -= 2f * Time.deltaTime;
                else
                    this.backingProgress = 0f;

                this.canBackTimer = 0f;
            }
        }

        if(this.backingObject != null && this.backingSlider != null)
        {
            if (this.backingProgress <= 0f)
            {
                if (this.dissapearValue < 1f)
                    this.dissapearValue += 5f * Time.deltaTime;
                else
                    this.dissapearValue = 1f;
            }
            else
            {
                this.dissapearValue = 0f;
            }

            if (this.dissapearValue >= 1f)
            {
                this.backingObject.SetActive(false);
            }
            else
            {
                this.backingObject.SetActive(true);
            }

            //this.backingObject.SetActive(this.backingProgress > 0f);

            this.backingSlider.value = this.backingProgress;
        }

        if (this.canBackTimer >= 1f)
        {
            this.canBack = true;
            this.canBackTimer = 1f;
        }
        else
        {
            this.canBack = false;
            if (this.canBackTimer < 0f)
                this.canBackTimer = 0f;
        }

        if(this.backingProgress >= 1f)
        {
            this.QuitToTitle();
        }
    }


    public void QuitToTitle()
    {
        if(CharacterSelectLogic.Instance != null && !CharacterSelectLogic.Instance.quitting)
        {
            CharacterSelectLogic.Instance.QuitToTitle();
        }
    }
}
