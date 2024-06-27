using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundEffects : MonoBehaviour
{
    public CharacterSoundEffect hitSound;
    public CharacterSoundEffect superStartSound;
    public CharacterSoundEffect deathSound;
    

    public void PlayHitSound()
    {
        this.hitSound.PlaySound();
        /*if(this.hitSound.sound != null)
        {
            this.hitSound.sound.time = this.hitSound.startTime;
            this.hitSound.sound.pitch = Random.Range(this.hitSound.pitch.x, this.hitSound.pitch.y);
            this.hitSound.sound.Play();
        }*/
    }

    public void PlayDeathSound()
    {
        this.deathSound.PlaySound();
    }

    public void StopHitSound()
    {
        this.hitSound.StopSound();
    }

    public void PlaySuperSfx()
    {
        this.superStartSound.PlaySound();
    }
}

[System.Serializable]
public class CharacterSoundEffect
{
    //public int opponentId;
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
            /*this.sound.time = this.startTime;
            this.sound.pitch = Random.Range(this.pitch.x, this.pitch.y);*/
            this.sound.Stop();
        }
    }
}