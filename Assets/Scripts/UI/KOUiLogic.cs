using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KOUiLogic : MonoBehaviour
{
    public GameObject k;
    public GameObject o;
    public GameObject perfect;
    public GameObject perfectP2;
    public GameObject draw;
    public GameObject hyperKO;

    public GameObject suicide;
    //public GameObject fallOff;
    public GameObject hyperSuicide;
    public GameObject ring;
    public GameObject outText;

    public GameObject timeOver;

    public TextMeshProUGUI roundX;
    public GameObject fight;

    public


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KOText()
    {
        
        this.StartCoroutine(this.KOCoroutine());
    }

    public void DrawText()
    {
        this.StopAllCoroutines();
        this.RemoveAllText();
        this.StartCoroutine(this.DrawCoroutine());
    }

    public void PerfectText(bool isP1 = true)
    {
        this.StopAllCoroutines();
        this.RemoveAllText();
        this.StartCoroutine(this.PerfectCoroutine(isP1));
    }

    public void HyperKOText()
    {

        this.StartCoroutine(this.HyperKOCoroutine());
    }

    public void SuicideText()
    {
        this.StartCoroutine(this.SuicideCoroutine());
    }

    public void RingOutText()
    {
        this.StartCoroutine(this.RingOutCoroutine());
    }

    public void HyperSuicideText()
    {
        this.StartCoroutine(this.HyperSuicideCoroutine());
    }

    public void TimeOverText()
    {
        this.StartCoroutine(this.TimeOverCoroutine());
    }

    public void CurrentRoundText(int round)
    {
        if(this.roundX != null)
        {
            this.roundX.text = "ROUND " + round.ToString();
        }

        this.StartCoroutine(this.CurrentRoundCoroutine());
    }

    public void FightText()
    {
        //this.StopAllCoroutines();
        this.RemoveAllText();

        if (this.roundX.gameObject != null)
            this.roundX.gameObject.SetActive(false);

        this.StartCoroutine(this.FightCoroutine());
    }

    public void RemoveAllText()
    {
        this.StopAllCoroutines();

        if (this.k != null)
        {
            //this.k.transform.localScale = new Vector3(1f, 1f, 1f);
            this.k.SetActive(false);
        }

        if (this.o != null)
        {
            //this.o.transform.localScale = new Vector3(1f, 1f, 1f);
            this.o.SetActive(false);
        }
            

        if (this.perfect != null)
            this.perfect.SetActive(false);

        if (this.perfectP2 != null)
            this.perfectP2.SetActive(false);

        if (this.draw != null)
            this.draw.SetActive(false);

        if (this.hyperKO != null)
            this.hyperKO.SetActive(false);

        if (this.suicide != null)
            this.suicide.SetActive(false);

        if (this.ring != null)
            this.ring.SetActive(false);

        if (this.outText != null)
            this.outText.SetActive(false);

        if (this.hyperSuicide != null)
            this.hyperSuicide.SetActive(false);

        if (this.timeOver != null)
            this.timeOver.SetActive(false);

        if (this.roundX != null)
            this.roundX.gameObject.SetActive(false);

        if (this.fight != null)
            this.fight.SetActive(false);
    }

    private IEnumerator KOCoroutine()
    {
        //yield return new WaitForSeconds(0.25f);

        if (this.k != null)
        {
            this.k.SetActive(true);
        }

        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSecondsRealtime(0.25f);

        if (this.o != null)
        {
            this.o.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        if (this.k != null)
        {
            this.k.SetActive(false);
        }

        if (this.o != null)
        {
            this.o.SetActive(false);
        }




        /*float startScale = 0.1f;

        if (this.k != null)
        {
            this.k.transform.localScale = new Vector3(startScale, startScale, 1f);
            this.k.SetActive(true);
        }

        float currentTime = 0;
        float duration = 0.05f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.k != null)
                this.k.transform.localScale = new Vector3(Mathf.Lerp(startScale, 1f, currentTime / duration), Mathf.Lerp(startScale, 1f, currentTime / duration), 1f);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        //yield return new WaitForSeconds(0.25f);

        if (this.o != null)
        {
            this.o.transform.localScale = new Vector3(startScale, startScale, 1f);
            this.o.SetActive(true);
        }

        currentTime = 0;
        duration = 0.05f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.o != null)
                this.o.transform.localScale = new Vector3(Mathf.Lerp(startScale, 1f, currentTime / duration), Mathf.Lerp(startScale, 1f, currentTime / duration), 1f);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);


        //yield return new WaitForSeconds(0.25f);



        yield return new WaitForSeconds(1f);

        if (this.k != null)
        {
            this.k.transform.localScale = new Vector3(1f, 1f, 1f);
            this.k.SetActive(false);
        }

        if (this.o != null)
        {
            this.o.transform.localScale = new Vector3(1f, 1f, 1f);
            this.o.SetActive(false);
        }*/







        /*if (this.perfect != null)
        {
            this.perfect.transform.localScale = new Vector3(startScale, startScale, 1f);
            this.perfect.SetActive(true);
        }

        currentTime = 0;
        duration = 0.1f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.perfect != null)
                this.perfect.transform.localScale = new Vector3(Mathf.Lerp(startScale, 1f, currentTime / duration), Mathf.Lerp(startScale, 1f, currentTime / duration), 1f);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (this.perfect != null)
        {
            this.perfect.transform.localScale = new Vector3(1f, 1f, 1f);
            this.perfect.SetActive(false);
        }*/


        /*if (this.perfect != null)
            this.perfect.SetActive(true);*/
    }



    private IEnumerator DrawCoroutine()
    {
        if (this.draw != null)
            this.draw.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        if (this.draw != null)
            this.draw.SetActive(false);
    }

    private IEnumerator PerfectCoroutine(bool isP1 = true)
    {
        float textDuration = 1.5f;
        if(isP1 && this.perfect != null)
        {
            this.perfect.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.perfect.SetActive(false);
        }
        else if (!isP1 && this.perfectP2 != null)
        {
            this.perfectP2.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.perfectP2.SetActive(false);
        }


        /*if (this.perfect != null)
            this.perfect.SetActive(true);
        yield return new WaitForSeconds(1f);

        if (this.perfect != null)
            this.perfect.SetActive(false);*/
    }

    private IEnumerator HyperKOCoroutine()
    {
        if (this.hyperKO != null)
            this.hyperKO.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        if (this.hyperKO != null)
            this.hyperKO.SetActive(false);
    }

    private IEnumerator SuicideCoroutine()
    {
        if (this.suicide != null)
            this.suicide.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        if (this.suicide != null)
            this.suicide.SetActive(false);
    }

    private IEnumerator RingOutCoroutine()
    {
        //yield return new WaitForSeconds(0.25f);

        if (this.ring != null)
            this.ring.SetActive(true);

        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSecondsRealtime(0.25f);

        if (this.outText != null)
            this.outText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (this.ring != null)
            this.ring.SetActive(false);

        if (this.outText != null)
            this.outText.SetActive(false);
    }

    private IEnumerator HyperSuicideCoroutine()
    {
        if (this.hyperSuicide != null)
            this.hyperSuicide.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        if (this.hyperSuicide != null)
            this.hyperSuicide.SetActive(false);
    }

    private IEnumerator TimeOverCoroutine()
    {
        if (this.timeOver != null)
            this.timeOver.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (this.timeOver != null)
            this.timeOver.SetActive(false);
    }

    private IEnumerator CurrentRoundCoroutine()
    {
        if (this.roundX != null)
            this.roundX.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (this.roundX != null)
            this.roundX.gameObject.SetActive(false);
    }

    private IEnumerator FightCoroutine()
    {
        if (this.fight != null)
            this.fight.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (this.fight != null)
            this.fight.SetActive(false);
    }
}
