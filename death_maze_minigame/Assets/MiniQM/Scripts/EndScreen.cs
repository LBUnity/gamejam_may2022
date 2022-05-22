using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    QMScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<QMScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratualtions!\nYou you got a score of " + scoreKeeper.CalculateScore() + "%";
    }
}
