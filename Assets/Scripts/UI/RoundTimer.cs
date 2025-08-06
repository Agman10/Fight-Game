using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    public TMP_Text digitTen;
    public TMP_Text digitOne;

    public GameObject infinityTimer;
    public GameObject normalTimer;

    public bool infiniteTime;

    public int startTime = 99;

    public float time;

    private bool blinking;

    public GameManager gameManager;

    /*public Color maxTimeColor;
    public Color lowTimeColor;*/


    private void OnEnable()
    {
        if (this.infiniteTime)
        {
            this.EnableTimer(false);
        }
        else
        {
            this.EnableTimer(true);

            /*this.time = this.startTime;

            this.UpdateTimeDisplay();

            this.StartCoroutine(this.UpdateTimerCoroutine());*/
        }
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    public void StartTimer()
    {
        if (!this.infiniteTime)
        {
            /*this.time = this.startTime;
            this.UpdateTimeDisplay();*/
            this.StartCoroutine(this.UpdateTimerCoroutine());
        }
            
    }

    public void StopTimer()
    {
        this.StopAllCoroutines();

        this.blinking = false;
        if (this.time > 0f)
            this.SetNumberColor(new Color(1f, 1f, 1f, 1f));
    }

    public void ResetTimer()
    {
        if (!this.infiniteTime)
        {
            this.blinking = false;
            this.SetNumberColor(new Color(1f, 1f, 1f, 1f));

            this.time = this.startTime;
            this.UpdateTimeDisplay();
        }
            
    }

    [ContextMenu("Blink")]
    public void Blink()
    {
        if (!this.blinking)
        {
            this.blinking = true;
            this.StartCoroutine(this.BlinkCoroutine());
        }
            
    }

    private IEnumerator BlinkCoroutine()
    {
        this.SetNumberColor(new Color(0.9f, 0.7f, 0.7f, 1f));

        yield return new WaitForSeconds(0.1f);

        this.SetNumberColor(new Color(1f, 1f, 1f, 1f));

        yield return new WaitForSeconds(0.1f);

        if (this.blinking)
            this.StartCoroutine(this.BlinkCoroutine());
    }

    public void SetNumberColor(Color color)
    {
        if(this.digitOne != null && this.digitTen != null)
        {
            this.digitOne.color = color;
            this.digitTen.color = color;
        }
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        //yield return new WaitForSeconds(1);


        //float duration = 1f;
        /*float currentTime = 1f;
        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }*/

        while (this.time > 0f /*this.time >= 1f*/)
        {
            this.time -= Time.deltaTime;
            //this.time -= Time.fixedDeltaTime;
            this.UpdateTimeDisplay();

            yield return null;
        }
        /*this.digitTen.color = this.lowTimeColor;
        this.digitOne.color = this.lowTimeColor;*/

        this.time = 0f;
        this.UpdateTimeDisplay();
        this.SetNumberColor(new Color(0.9f, 0.7f, 0.7f, 1f));

        if (this.gameManager != null)
            this.gameManager.EvaluateTimeOverWinner();
    }

    public void UpdateTimeDisplay()
    {
        //float roundedNumber = Mathf.FloorToInt(this.time);
        float roundedNumber = Mathf.FloorToInt(Mathf.Abs(this.time));
        //Debug.Log(this.time);
        //Debug.Log(roundedNumber);

        //string currentTime = this.time.ToString("00");
        string currentTime = roundedNumber.ToString("00");

        if (this.time < 10f && this.time > 0f)
            this.Blink();
        /*else if (this.time <= 0f)
            this.SetNumberColor(new Color(0.9f, 0.7f, 0.7f, 1f));*/

        if (this.digitTen != null && this.digitOne != null)
        {
            this.digitTen.text = currentTime[0].ToString();
            this.digitOne.text = currentTime[1].ToString();


            /*if (this.time > 99)
            {
                this.digitTen.text = 9.ToString();
                this.digitOne.text = 9.ToString();
            }
            else if (this.time < 0)
            {
                this.digitTen.text = 0.ToString();
                this.digitOne.text = 0.ToString();
            }
            else
            {
                this.digitTen.text = currentTime[0].ToString();
                this.digitOne.text = currentTime[1].ToString();
            }*/

            /*this.digitTen.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, roundedNumber / this.startTime);
            this.digitOne.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, roundedNumber / this.startTime);*/
        }
    }

    public void EnableTimer(bool enable = true)
    {
        if (enable)
        {
            if (this.infinityTimer != null)
                this.infinityTimer.SetActive(false);

            if (this.normalTimer != null)
                this.normalTimer.SetActive(true);

            this.time = this.startTime;

            this.UpdateTimeDisplay();
        }
        else
        {
            if (this.infinityTimer != null)
                this.infinityTimer.SetActive(true);

            if (this.normalTimer != null)
                this.normalTimer.SetActive(false);
        }
    }
}
