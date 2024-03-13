using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempVictoryScreenLogic : MonoBehaviour
{
    public int playerId;
    public Text victoryQuoteText;
    public float textTypeDelay = 0.1f;
    private string currentText = "";

    public VictoryPoseAndQuote[] P1VictoryPoseAndQuotes;
    public VictoryPoseAndQuote[] P2VictoryPoseAndQuotes;

    public VictoryPoseAndQuote error;

    private void OnEnable()
    {

        if(this.playerId == 0 && this.P1VictoryPoseAndQuotes.Length >= 1)
        {
            int vitoryIndex = Random.Range(0, this.P1VictoryPoseAndQuotes.Length);
            //Debug.Log(this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote);
            if (this.victoryQuoteText != null)
            {
                //this.victoryQuoteText.text = this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote;
                this.victoryQuoteText.fontSize = this.P1VictoryPoseAndQuotes[vitoryIndex].textSize;
                this.StartCoroutine(this.ShowText(this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote));
            }
                

            if(this.P1VictoryPoseAndQuotes[vitoryIndex].victoryPoses.Length >= 1)
            {
                int vitoryPoseIndex = Random.Range(0, this.P1VictoryPoseAndQuotes[vitoryIndex].victoryPoses.Length);
                if (this.P1VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex] != null)
                {
                    this.StartCoroutine(this.RotatePose(this.P1VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex]));
                    this.P1VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex].SetActive(true);
                }
                    
            }
        }
        else if (this.playerId == 1 && this.P2VictoryPoseAndQuotes.Length >= 1)
        {
            int vitoryIndex = Random.Range(0, this.P2VictoryPoseAndQuotes.Length);
            //Debug.Log(this.P1VictoryPoseAndQuotes[vitoryIndex].victoryQuote);
            if (this.victoryQuoteText != null)
            {
                //this.victoryQuoteText.text = this.P2VictoryPoseAndQuotes[vitoryIndex].victoryQuote;
                this.victoryQuoteText.fontSize = this.P2VictoryPoseAndQuotes[vitoryIndex].textSize;

                this.StartCoroutine(this.ShowText(this.P2VictoryPoseAndQuotes[vitoryIndex].victoryQuote));
            }
                

            if (this.P2VictoryPoseAndQuotes[vitoryIndex].victoryPoses.Length >= 1)
            {
                int vitoryPoseIndex = Random.Range(0, this.P2VictoryPoseAndQuotes[vitoryIndex].victoryPoses.Length);
                if (this.P2VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex] != null)
                {
                    this.StartCoroutine(this.RotatePose(this.P2VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex]));
                    this.P2VictoryPoseAndQuotes[vitoryIndex].victoryPoses[vitoryPoseIndex].SetActive(true);
                }
                    
            }
        }
        else
        {
            if (this.victoryQuoteText != null)
            {
                this.victoryQuoteText.fontSize = this.error.textSize;
                this.StartCoroutine(this.ShowText(this.error.victoryQuote));
            }

            if (this.error.victoryPoses.Length >= 1 && this.error.victoryPoses[0] != null)
            {
                this.StartCoroutine(this.RotatePose(this.error.victoryPoses[0]));
                this.error.victoryPoses[0].SetActive(true);
            }

        }
    }

    IEnumerator ShowText(string fullText)
    {
        yield return new WaitForSeconds(0.2f);
        float extraWaitTime = 0f;
        for(int i = 0; i < fullText.Length + 1; i++)
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
}

[System.Serializable]
public class VictoryPoseAndQuote
{
    [TextArea(1, 20)]
    public string victoryQuote;
    public int textSize = 14;
    public GameObject[] victoryPoses;
}
