using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public AudioSource sound;
    public Vector2 pitch = new Vector2(1f, 1f);
    public float startTime = 0f;

    public void PlaySound()
    {
        if (this.sound != null)
        {
            this.sound.time = this.startTime;
            this.sound.pitch = Random.Range(this.pitch.x, this.pitch.y);
            this.sound.Play();
        }
    }

    public void StopSound()
    {
        if (this.sound != null)
        {
            this.sound.Stop();
        }
    }
}
