using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundEffects : MonoBehaviour
{
    public CharacterSoundEffect hitSound;
    

    public void PlayHitSound()
    {
        if(this.hitSound.sound != null)
        {
            this.hitSound.sound.time = this.hitSound.startTime;
            this.hitSound.sound.pitch = Random.Range(this.hitSound.pitch.x, this.hitSound.pitch.y);
            this.hitSound.sound.Play();
        }
    }
}

[System.Serializable]
public class CharacterSoundEffect
{
    //public int opponentId;
    public AudioSource sound;
    public Vector2 pitch = new Vector2(1f, 1f);
    public float startTime = 0f;
}
