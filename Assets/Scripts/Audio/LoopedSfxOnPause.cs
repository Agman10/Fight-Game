using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedSfxOnPause : MonoBehaviour
{
    private float pitch;
    private AudioSource audioSource;

    private void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        if (this.audioSource != null)
            this.pitch = this.audioSource.pitch;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && this.audioSource != null)
        {
            if (GameManager.Instance.gameIsPaused)
                this.audioSource.pitch = 0f;
            else
                this.audioSource.pitch = this.pitch;
        }
    }
}
