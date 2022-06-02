using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    private int playerScore;

    private MainLevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<MainLevelManager>();

        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = PlayerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (PlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int number)
    {
        playerScore += number;
        scoreText.text = playerScore.ToString();
    }

    private void TakeLife()
    {
        PlayerLives--;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Randomize loading Quizmaster or LazerDefender
        int miniGameRange = Enum.GetNames(typeof(MiniGamesEnum)).Length;
        int minigameIndex = UnityEngine.Random.Range(0, miniGameRange);

        switch (minigameIndex)
        {
            case (int)MiniGamesEnum.QUIZMASTER:
                levelManager.LoadMiniQuizmasterGame();//SceneManager.LoadScene("QuizMasterGame");
                break;
            case (int)MiniGamesEnum.LAZER_DEFENDER:
                levelManager.LoadMiniLazerDefenderGame();//SceneManager.LoadScene("LazerDefenderGame");
                break;
        }

        livesText.text = PlayerLives.ToString();
    }

    private void ResetGameSession()
    {
        playerScore = 0;
        FindObjectOfType<ScenePresist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


    enum MiniGamesEnum { QUIZMASTER = 0, LAZER_DEFENDER = 1};
}
