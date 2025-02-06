using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAmplifier : MonoBehaviour
{
    public AudioSource audio;
    private void Awake()
    {
        if (this.audio != null)
            this.audio.PlayOneShot(this.audio.clip, 3f);
    }
}
