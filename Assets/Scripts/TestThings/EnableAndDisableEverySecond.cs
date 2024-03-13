using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisableEverySecond : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public float duration = 1f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.object1 != null)
            this.object1.SetActive(true);

        if (this.object2 != null)
            this.object2.SetActive(false);

        this.StartCoroutine(this.EnableAndDisable());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator EnableAndDisable()
    {
        yield return new WaitForSeconds(this.duration);

        if (this.object1 != null)
            this.object1.SetActive(false);

        if (this.object2 != null)
            this.object2.SetActive(true);

        yield return new WaitForSeconds(this.duration);

        if (this.object1 != null)
            this.object1.SetActive(true);

        if (this.object2 != null)
            this.object2.SetActive(false);

        yield return StartCoroutine(this.EnableAndDisable());
    }
}
