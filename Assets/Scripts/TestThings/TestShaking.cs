using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaking : MonoBehaviour
{
    public Transform model;

    public bool shakkin = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        this.StartCoroutine(this.ShakingCoroutine());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator ShakingCoroutine()
    {
        float newY = 0f;
        float testTime = 0f;
        float startPosX = this.model.localPosition.x;
        while (this.shakkin)
        {
            //currentImpactStunTime += Time.deltaTime;

            testTime += Time.deltaTime;

            //stunnedTime += Time.deltaTime;

            newY = Mathf.Sin(testTime * 100f);
            this.model.localPosition = new Vector3(startPosX + (newY * 0.02f), this.model.localPosition.y, this.model.localPosition.z);
            yield return null;
        }
        this.model.localPosition = new Vector3(startPosX, this.model.localPosition.y, this.model.localPosition.z);
        //yield return null;

        yield return new WaitForSeconds(3f);
        this.StartCoroutine(this.ShakingCoroutine());
    }
}
