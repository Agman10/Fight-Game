using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour
{
    public PlayerInput playerInput;
    public bool gameIsPaused;
    public GameObject pauseIndicator;
    private void OnEnable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.StartInput += this.Pause;
        }
    }
    private void OnDisable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.StartInput -= this.Pause;
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            if (!this.gameIsPaused)
            {
                Time.timeScale = 0f;
                this.gameIsPaused = true;
                if (this.pauseIndicator != null)
                {
                    this.pauseIndicator.SetActive(true);
                }
            }
            else
            {
                Time.timeScale = 1f;
                this.gameIsPaused = false;

                if (this.pauseIndicator != null)
                {
                    this.pauseIndicator.gameObject.SetActive(false);
                }
            }
        }
    }
}
