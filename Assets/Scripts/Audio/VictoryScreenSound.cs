using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenSound : MonoBehaviour
{
    public AudioSource song;
    public AudioSource victoryBgm;
    public AudioSource drawBgm;

    private void OnEnable()
    {
        
    }

    public void PlayVictoryBgm()
    {
        if (this.victoryBgm != null)
            this.victoryBgm.Play();

        this.StartCoroutine(this.PlayMusicCoroutine());
    }

    public void PlayDrawBgm()
    {
        if (this.drawBgm != null)
            this.drawBgm.Play();

        this.StartCoroutine(this.PlayMusicCoroutine());
    }

    IEnumerator PlayMusicCoroutine()
    {
        yield return new WaitForSeconds(5f);
        if (this.song != null)
            this.song.Play();

        float currentTime = 0;
        float duration = 3f;
        //float targetVolume = 0.1f;
        float targetVolume = this.song.volume;
        float start = 0.001f;
        //float start = song.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.song.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
    }
}
