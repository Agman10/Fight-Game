using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public GameObject normalKo;
    public GameObject hyperKo;
    public GameObject timeUp;
    public GameObject suicide;
    public GameObject hyperSuicide;
    public GameObject fallOff;

    public GameObject draw;
    public GameObject perfect;

    public void GiveScore(int scoreTypeId = 0)
    {
        if (scoreTypeId == 0)
            this.normalKo.SetActive(true);
        else if (scoreTypeId == 1)
            this.hyperKo.SetActive(true);
        else if (scoreTypeId == 2)
            this.timeUp.SetActive(true);
        else if (scoreTypeId == 3)
            this.suicide.SetActive(true);
        else if (scoreTypeId == 4)
            this.hyperSuicide.SetActive(true);
        else if (scoreTypeId == 5)
            this.fallOff.SetActive(true);

    }

    public void GivePerformance(int performanceTypeId = 0)
    {
        if (performanceTypeId == 1)
            this.draw.SetActive(true);
        else if (performanceTypeId == 2)
            this.perfect.SetActive(true);
    }
}
