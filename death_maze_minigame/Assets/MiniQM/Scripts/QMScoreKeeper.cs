using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMScoreKeeper : MonoBehaviour
{
    int CorrectAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return CorrectAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        CorrectAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(CorrectAnswers /(float) questionsSeen * 100);
    }
}
