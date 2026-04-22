using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    public GameObject HelpPanel;
    public GameObject NextPanel;
    public GameObject GameOverPanel;


    public void OpenHelpPanel()
    {
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
    }

    public void GameStartButtonAction()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void NextSceneButtonAction1()
    {
        SceneManager.LoadScene("Story_1");
    }

    public void NextSceneButtonAction2()
    {
        SceneManager.LoadScene("Story_2");
    }

    public void NextSceneButtonAction3()
    {
        SceneManager.LoadScene("Story_3");
    }

    public void NextSceneButtonAction4()
    {
        SceneManager.LoadScene("Story_4");
    }

    public void ExitButtonAction()
    {
        Application.Quit();
    }
}
