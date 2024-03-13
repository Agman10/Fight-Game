using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSong : MonoBehaviour
{
    public AudioSource song1;
    public AudioSource song2;
    private void Awake()
    {
        float number = Random.Range(1, 101);
        //Debug.Log(number);
        if(number <= 10)
        {
            if (this.song2 != null)
                this.song2.Play();
        }
        else
        {
            if (this.song1 != null)
                this.song1.Play();
        }
    }
}
