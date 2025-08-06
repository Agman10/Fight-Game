using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScoreDisplayLogic : MonoBehaviour
{
    public ScorePoint[] scorePoints;
    public int score;

    public void AddScore(int scoreTypeId = 0, int performanceTypeId = 0)
    {
        if (this.scorePoints.Length >= this.score + 1)
        {
            this.scorePoints[this.score].GiveScore(scoreTypeId);
            this.scorePoints[this.score].GivePerformance(performanceTypeId);
        }

        this.score++;
    }

    /*public void AddScore(int scoreTypeId = 0)
    {
        if (this.scorePoints.Length >= this.score + 1)
        {
            this.scorePoints[this.score].GiveScore(scoreTypeId);
        }

        //this.score++;
    }*/

    public void AddPerformance(int performanceTypeId = 0)
    {
        if (this.scorePoints.Length >= this.score + 1)
        {
            this.scorePoints[this.score].GivePerformance(performanceTypeId);
        }

        //this.score++;
    }
}
