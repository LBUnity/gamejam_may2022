using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2F;

    private void Awake()
    {
        //Place the singleton data structure here
    }

    public void LoadMainMenu()
    {
        new NotImplementedException("Load Main Menu not implemented.");
    }

    public void LoadControlsScreen()
    {
        new NotImplementedException("Load Controls screen not implemented.");
    }

    public void LoadGameOverScreen()
    {
        StartCoroutine(WaitAndLoad("LazerDefenderGameOver", sceneLoadDelay));
    }

    public void LoadMainTileVaniaRules()
    {
        new NotImplementedException("Load Main Tilevania Rules");
    }

    public void LoadMiniLazerDefenderRules()
    {
        new NotImplementedException("Load Mini Lazer Defender Rules not implemented.");
    
    }

    public void LoadMiniQuizmasterRules()
    {
        new NotImplementedException(("Load Mini Quizmaster Rules not implemented."));
    }

    public void LoadMainTilevaniaLevel1()
    {
        SceneManager.LoadScene("TilevaniaLevel1");
    }

    public void LoadMainTilevaniaLevel2()
    {
        SceneManager.LoadScene("TilevaniaLevel2");
    }

    public void LoadMainTilevaniaLevel3()
    {
        SceneManager.LoadScene("TilevaniaLevel3");
    }

    public void LoadMiniLazerDefenderGame()
    {
        SceneManager.LoadScene("LazerDefenderGame");
    }

    public void LoadMiniQuizmasterGame()
    {
        SceneManager.LoadScene("QuizmasterGame");
    }


    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
#endif
    }

}
