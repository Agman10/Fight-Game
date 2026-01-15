using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalUiLogic : MonoBehaviour
{
    public GameObject goalP1;
    public GameObject goalP2;

    public TextMeshProUGUI winTextP1;
    public TextMeshProUGUI winTextP2;

    public GameObject perfectP1;
    public GameObject perfectP2;

    public GameObject goText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoalText(bool isP1 = true)
    {
        this.StartCoroutine(this.GoalCoroutine(isP1));
    }

    //[ContextMenu("WinText")]
    public void WinText(bool isP1 = true, string winnerName = "", bool perfect = false)
    {
        /*bool isP1 = true;
        string winnerName = "J-Cap";*/

        this.StartCoroutine(this.WinCoroutine(isP1, winnerName, perfect));
    }

    public void PerfectText(bool isP1 = true)
    {
        this.StartCoroutine(this.PerfectCoroutine(isP1));
    }

    public void GoText()
    {
        this.StartCoroutine(this.GoCoroutine());
    }

    public void RemoveAllText()
    {
        if (this.goalP1 != null)
            this.goalP1.SetActive(false);

        if (this.goalP2 != null)
            this.goalP2.SetActive(false);

        if (this.winTextP1 != null)
            this.winTextP1.gameObject.SetActive(false);

        if (this.winTextP2 != null)
            this.winTextP2.gameObject.SetActive(false);


        if (this.perfectP1 != null)
            this.perfectP1.SetActive(false);

        if (this.perfectP2 != null)
            this.perfectP2.SetActive(false);

        if (this.goText != null)
            this.goText.SetActive(false);
    }

    private IEnumerator GoalCoroutine(bool isP1 = true)
    {
        float textDuration = 1f;
        if (isP1 && this.goalP1 != null)
        {
            this.goalP1.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.goalP1.SetActive(false);
        }
        else if (!isP1 && this.goalP2 != null)
        {
            this.goalP2.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.goalP2.SetActive(false);
        }
    }

    private IEnumerator WinCoroutine(bool isP1 = true, string winnerName = "", bool perfect = false)
    {
        float textDuration = 1.5f;
        if (isP1 && this.winTextP1 != null)
        {
            this.winTextP1.text = winnerName + "<size=100%><br>WINS!";

            this.winTextP1.gameObject.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.winTextP1.gameObject.SetActive(false);
        }
        else if (!isP1 && this.winTextP2 != null)
        {
            this.winTextP2.text = winnerName + "<size=100%><br>WINS!";

            this.winTextP2.gameObject.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.winTextP2.gameObject.SetActive(false);
        }

        if (perfect)
            this.StartCoroutine(this.PerfectCoroutine(isP1));
    }


    private IEnumerator PerfectCoroutine(bool isP1 = true)
    {
        float textDuration = 1.5f;
        if (isP1 && this.perfectP1 != null)
        {
            this.perfectP1.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.perfectP1.SetActive(false);
        }
        else if (!isP1 && this.perfectP2 != null)
        {
            this.perfectP2.SetActive(true);

            yield return new WaitForSeconds(textDuration);

            this.perfectP2.SetActive(false);
        }
    }

    private IEnumerator GoCoroutine()
    {
        if (this.goText != null)
            this.goText.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (this.goText != null)
            this.goText.SetActive(false);
    }
}
