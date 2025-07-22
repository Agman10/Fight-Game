using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestMatchTimer : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text digit1;
    public TMP_Text digit2;

    public GameObject infinityTimer;
    //public TextMeshProUGUI timer;

    public int startTime = 99;

    public float time;
    public int timeInt;

    public float currentSecond;

    public Color maxTimeColor;
    public Color lowTimeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {

        this.time = this.startTime;
        this.timeInt = this.startTime;
        this.currentSecond = 1f;
        this.StartCoroutine(this.UpdateTimerCoroutine());
    }

    // Update is called once per frame
    /*void Update()
    {
        if (this.time > 0f)
            this.time -= Time.deltaTime;
        else
            this.time = 0f;

        //this.UpdateTimer();
        this.UpdateTimeDisplay();
    }*/

    public void UpdateTimer()
    {
        if (this.timer != null)
            this.timer.text = this.time.ToString("00");

        string currentTime = this.time.ToString("00");

        //Debug.Log(currentTime);

        if (this.digit1 != null && this.digit2 != null)
        {
            this.digit1.text = currentTime[0].ToString();
            this.digit2.text = currentTime[1].ToString();
        }


        /*if (this.timer != null)
            this.timer.text = this.time.ToString("F0");*/

        //this.timer.text = this.time.ToString("D2");
    }

    

    private IEnumerator UpdateTimerCoroutine()
    {
        while (this.time > 0f)
        {
            this.time -= Time.deltaTime;

            /*string currentTime = this.time.ToString("00");

            if (this.digit1 != null && this.digit2 != null)
            {
                this.digit1.text = currentTime[0].ToString();
                this.digit2.text = currentTime[1].ToString();
            }*/
            this.UpdateTimeDisplay();

            yield return null;
        }

        this.time = 0f;
        this.UpdateTimeDisplay();
    }

    public void UpdateTimeDisplay()
    {
        //float roundedNumber = Mathf.FloorToInt(this.time);
        float roundedNumber = Mathf.FloorToInt(Mathf.Abs(this.time));
        Debug.Log(this.time);
        Debug.Log(roundedNumber);

        //string currentTime = this.time.ToString("00");
        string currentTime = roundedNumber.ToString("00");

        if (this.timer != null)
            this.timer.text = this.time.ToString("00");

        //string currentTime = Mathf.FloorToInt(this.time).ToString("00");

        /*float roundedNumber = Mathf.FloorToInt(this.time);
        Debug.Log(this.time);
        Debug.Log(roundedNumber);*/

        if (this.digit1 != null && this.digit2 != null)
        {
            this.digit1.text = currentTime[0].ToString();
            this.digit2.text = currentTime[1].ToString();

            /*this.digit1.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, roundedNumber / this.startTime);
            this.digit2.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, roundedNumber / this.startTime);*/
        }
    }

    private IEnumerator UpdateTimerCoroutine2()
    {
        while (this.timeInt > 0)
        {
            while (this.currentSecond > 0f)
            {
                this.currentSecond -= Time.deltaTime;
                yield return null;
            }
            this.currentSecond = 1;
            this.timeInt--;
            this.UpdateTimeDisplay2();

            yield return null;
        }

        //this.time = 0f;
        this.UpdateTimeDisplay2();
    }

    public void UpdateTimeDisplay2()
    {
        /*float roundedNumber = Mathf.FloorToInt(this.time);
        Debug.Log(this.time);
        Debug.Log(roundedNumber);*/

        //string currentTime = this.time.ToString("00");
        string currentTime = timeInt.ToString("00");

        if (this.timer != null)
            this.timer.text = this.timeInt.ToString("00");

        //string currentTime = Mathf.FloorToInt(this.time).ToString("00");

        /*float roundedNumber = Mathf.FloorToInt(this.time);
        Debug.Log(this.time);
        Debug.Log(roundedNumber);*/
        //float colorLerp = this.timeInt / this.startTime;

        if (this.digit1 != null && this.digit2 != null)
        {
            this.digit1.text = currentTime[0].ToString();
            this.digit2.text = currentTime[1].ToString();

            /*this.digit1.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, this.timeInt / this.startTime);
            this.digit2.color = Color.Lerp(this.lowTimeColor, this.maxTimeColor, this.timeInt / this.startTime);*/

            //Debug.Log(colorLerp);
        }
    }
}
