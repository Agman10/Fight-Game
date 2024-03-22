using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDance : MonoBehaviour
{
    public TempPlayerAnimations animations;

    public int danceId;
    private void OnEnable()
    {
        if (this.danceId == 0)
        {
            this.StartCoroutine(this.TortureDance());
        }
        else
        {
            Debug.Log("dance id does not exist");
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator TortureDance()
    {
        if (this.animations != null)
        {
            this.animations.TortureDance(0);

            yield return new WaitForSeconds(0.3f);

            this.animations.TortureDance(1);

            yield return new WaitForSeconds(0.3f);

            this.animations.TortureDance(2);

            yield return new WaitForSeconds(0.35f);

            this.animations.TortureDance(3);

            yield return new WaitForSeconds(0.3f);

            this.StartCoroutine(this.TortureDance());
        }


    }
}
