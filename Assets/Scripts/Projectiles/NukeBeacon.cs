using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeBeacon : MonoBehaviour
{
    public AnimationCurve throwCurve;
    public SuperNuke nuke;
    public TestPlayer belongsTo;
    public GameObject beacon;
    public GameObject model;
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.beacon != null)
            this.beacon.SetActive(false);

    }

    public void ThrowBeacon(float enemyPosition, float forwardZ)
    {
        this.StartCoroutine(this.MoveCoroutine(enemyPosition, forwardZ));
    }

    private IEnumerator MoveCoroutine(float enemyPosition, float forwardZ)
    {
        //yield return new WaitForSeconds(0.1f);

        float currentTime = 0;
        float duration = 0.3f;
        //float targetPositionX = 7f;
        float startX = this.transform.position.x;
        float startRotation = 90f * forwardZ;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(Mathf.Lerp(startX, enemyPosition, currentTime / duration), this.throwCurve.Evaluate(currentTime / duration), 0f);

            if (this.model != null)
                this.model.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(startRotation, 0, currentTime / duration));
            yield return null;
        }

        if (this.beacon != null)
            this.beacon.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        if (this.nuke != null)
        {

            SuperNuke nukePrefab = this.nuke;
            nukePrefab = Instantiate(nukePrefab, new Vector3(enemyPosition, 11f, 0), Quaternion.Euler(0, 0, 0));
            nukePrefab.SetOwner(this.belongsTo);
            //bigSpherePrefab.belongsTo = this.user;
        }

        this.StartCoroutine(this.DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;
        }
    }
}
