using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    QuizScript quiz;
    EndScreen endScreen;
    MainLevelManager levelManager;

    private void Awake()
    {
        quiz = FindObjectOfType<QuizScript>();
        endScreen = FindObjectOfType<EndScreen>();
        levelManager = FindObjectOfType<MainLevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {


        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.IsQuizComplete())
        {
            //return to tilevania level 1 for now.
            levelManager.LoadMainTilevaniaLevel1();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
