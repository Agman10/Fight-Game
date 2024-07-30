using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public static TimeScaler Instance;
    public float currentTimeScale = 1f;
    public bool currentlySlowedDown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = this.currentTimeScale;

        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.gameIsPaused)
                Time.timeScale = this.currentTimeScale;
        }
        
    }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }

    public void SetTimeScale(float timeScale = 0f, float time = 0.5f, bool overiteCurrent = false)
    {
        
        if (!this.currentlySlowedDown)
        {
            this.currentlySlowedDown = true;
            this.StopAllCoroutines();
            this.StartCoroutine(this.SetTimeScaleCoroutine(timeScale, time));
        }
        else if(this.currentlySlowedDown && overiteCurrent)
        {
            this.currentlySlowedDown = true;
            this.StopAllCoroutines();
            this.StartCoroutine(this.SetTimeScaleCoroutine(timeScale, time));
        }
    }

    private IEnumerator SetTimeScaleCoroutine(float timeScale = 0f, float time = 0.5f)
    {
        float currentTime = 0;

        this.currentTimeScale = timeScale;
        yield return new WaitForSecondsRealtime(time);

        /*while (currentTime < time)
        {
            //currentTime += Time.unscaledDeltaTime;
            currentTime += Time.deltaTime;

            this.currentTimeScale = Mathf.Lerp(timeScale, 1f, currentTime / time);
            yield return null;
        }*/


        this.currentTimeScale = 1f;
        this.currentlySlowedDown = false;
    }
}
