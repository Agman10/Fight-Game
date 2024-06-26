using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaySoundEffects : MonoBehaviour
{
    public CharacterSoundEffect[] soundEffects;

    [ContextMenu("PlaySoundEffects")]
    public void PlaySoundEffects()
    {
        foreach (CharacterSoundEffect sfx in this.soundEffects)
        {
            if(sfx.sound != null)
            {
                sfx.sound.time = sfx.startTime;
                sfx.sound.pitch = Random.Range(sfx.pitch.x, sfx.pitch.y);
                sfx.sound.Play();
            }
        }
    }
}
