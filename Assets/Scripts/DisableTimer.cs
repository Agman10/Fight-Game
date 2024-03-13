using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimer : MonoBehaviour
{
    public float time = 1f;
    public bool destroy;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.StartCoroutine(this.DisableTimerCoroutine());
    }
    void OnDisable()
    {
        this.StopAllCoroutines();
    }

    IEnumerator DisableTimerCoroutine()
    {
        yield return new WaitForSeconds(this.time);
        if (!this.destroy)
            this.gameObject.SetActive(false);
        else
            Destroy(this.gameObject);
    }
}
