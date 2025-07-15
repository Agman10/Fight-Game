using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBarReadyText : MonoBehaviour
{
    public GameObject[] firstTexts;
    public GameObject[] secondTexts;

    private float duration = 1.5f;

    private void OnEnable()
    {
        this.DisplayFirstTexts();

        this.StartCoroutine(this.EnableAndDisable());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnableAndDisable()
    {
        yield return new WaitForSeconds(this.duration);

        this.DisplaySecondTexts();

        yield return new WaitForSeconds(this.duration);

        this.DisplayFirstTexts();

        yield return StartCoroutine(this.EnableAndDisable());
    }

    public void DisplayFirstTexts()
    {
        foreach (GameObject firstText in this.firstTexts)
        {
            if (firstText != null)
                firstText.SetActive(true);
        }

        foreach (GameObject secondText in this.secondTexts)
        {
            if (secondText != null)
                secondText.SetActive(false);
        }
    }

    public void DisplaySecondTexts()
    {
        foreach (GameObject firstText in this.firstTexts)
        {
            if (firstText != null)
                firstText.SetActive(false);
        }

        foreach (GameObject secondText in this.secondTexts)
        {
            if (secondText != null)
                secondText.SetActive(true);
        }
    }


}
