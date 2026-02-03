using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVictoryScreen : MonoBehaviour
{
    public int opponentId;
    public Text victoryQuoteText;
    public float textTypeDelay = 0.05f;
    private string currentText = "";
    public string fullVictoryText = "";
    public VictoryPoseAndQuote[] defaultVictoryQuotes;
    public VictoryPoseAndQuoteList[] specificQuotes;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(this.opponentId <= this.specificQuotes.Length - 1 && this.specificQuotes.Length >= 1 && this.specificQuotes[this.opponentId].quotes.Length >= 1)
        {
            int victoryIndex = Random.Range(0, this.specificQuotes[this.opponentId].quotes.Length);
            //Debug.Log(victoryIndex);

            if (this.victoryQuoteText != null)
            {
                //this.victoryQuoteText.text = this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote;
                this.victoryQuoteText.fontSize = this.specificQuotes[this.opponentId].quotes[victoryIndex].textSize;
                this.StartCoroutine(this.ShowText(this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryQuote));
            }

            if (this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryPoses.Length >= 1)
            {
                int victoryPoseIndex = Random.Range(0, this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryPoses.Length);
                if (this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryPoses[victoryPoseIndex] != null)
                {
                    //this.StartCoroutine(this.RotatePose(this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryPoses[victoryPoseIndex]));
                    this.specificQuotes[this.opponentId].quotes[victoryIndex].victoryPoses[victoryPoseIndex].SetActive(true);
                }
            }
        }
        else if (this.defaultVictoryQuotes.Length >= 1)
        {
            int victoryIndex = Random.Range(0, this.defaultVictoryQuotes.Length);
            //Debug.Log(victoryIndex);

            if (this.victoryQuoteText != null)
            {
                //this.victoryQuoteText.text = this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote;
                this.victoryQuoteText.fontSize = this.defaultVictoryQuotes[victoryIndex].textSize;
                this.StartCoroutine(this.ShowText(this.defaultVictoryQuotes[victoryIndex].victoryQuote));
            }

            if (this.defaultVictoryQuotes[victoryIndex].victoryPoses.Length >= 1)
            {
                int victoryPoseIndex = Random.Range(0, this.defaultVictoryQuotes[victoryIndex].victoryPoses.Length);
                if (this.defaultVictoryQuotes[victoryIndex].victoryPoses[victoryPoseIndex] != null)
                {
                    //this.StartCoroutine(this.RotatePose(this.defaultVictoryQuotes[victoryIndex].victoryPoses[victoryPoseIndex]));
                    this.defaultVictoryQuotes[victoryIndex].victoryPoses[victoryPoseIndex].SetActive(true);
                }
            }

        }
        else
        {
            this.StartCoroutine(this.ShowText("Something went wrong..."));
        }
    }


    IEnumerator ShowText(string fullText)
    {
        this.SetFullText(fullText);
        yield return new WaitForSeconds(0.2f);
        float extraWaitTime = 0f;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            /*this.currentText = fullText.Substring(0, i);
            this.victoryQuoteText.text = this.currentText;
            yield return new WaitForSeconds(this.textTypeDelay);*/

            if (i < fullText.Length)
            {
                //Debug.Log(fullText.Substring(i, 1));
                //this.currentText = fullText.Substring(0, i);
                extraWaitTime = 0f;
                if (fullText.Substring(i, 1) != "§" && fullText.Substring(i, 1) != "½")
                {
                    this.currentText = this.currentText + fullText.Substring(i, 1);
                }
                else
                {
                    if (fullText.Substring(i, 1) == "§")
                        extraWaitTime = 0.4f;
                    else
                        extraWaitTime = 0.05f;
                }

                this.victoryQuoteText.text = this.currentText;
                yield return new WaitForSeconds(this.textTypeDelay + extraWaitTime);
            }


        }
    }

    IEnumerator RotatePose(GameObject pose)
    {
        float currentTime = 0;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetRotation = 0f;
        float start = -90f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(start, targetRotation, currentTime / duration), 0);
            yield return null;
        }
    }

    public void SetFullText(string fullText)
    {
        string symbol1 = "§";
        string symbol2 = "½";

        fullText = fullText.Replace(symbol1, "");
        fullText = fullText.Replace(symbol2, "");

        this.fullVictoryText = fullText;
    }

    public void SkipTextScrolling()
    {
        this.StopAllCoroutines();

        if (this.currentText != null)
            this.currentText = this.fullVictoryText;

        if (this.victoryQuoteText != null)
            this.victoryQuoteText.text = this.fullVictoryText;
    }
}

[System.Serializable]
public class VictoryPoseAndQuoteList
{
    //public int opponentId;
    public string opponentName;
    public VictoryPoseAndQuote[] quotes;
}
