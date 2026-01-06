using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetButtonColors : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public TextMeshProUGUI buttonTextTmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        this.SetNormalColor();
    }
    /*public void OnSelect()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.highlightedColor;
        }
    }

    public void OnDeselect()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.normalColor;
        }
        
    }*/

    public void SetNormalColor()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.normalColor;
        }
    }

    public void SetHighlightedColor()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.highlightedColor;
        }
    }

    public void SetPressedColor()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.pressedColor;
        }
    }

    public void SetSelectedColor()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.selectedColor;
        }
    }

    public void SetDisabledColor()
    {
        if (this.button != null)
        {
            if (this.buttonText != null)
                this.buttonText.color = this.button.colors.disabledColor;
        }
    }
}
