using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOUiLogic : MonoBehaviour
{
    public GameObject k;
    public GameObject o;
    public GameObject perfect;
    public GameObject perfectP2;
    public GameObject draw;
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


    public void RemoveAllText()
    {
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
}
