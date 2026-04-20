using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour
{
    public GameObject HelpPanel;

    public void GameStartButtonAction()
    {
        //본인 첫 씬 이름 쓰기
        SceneManager.LoadScene("Level_1");
    }

    public void OpenHelpPanel()
    {
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
    }
}
