using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject OpenAboutGamePanel;
    public GameObject OpenAboutUsPanel;
    public GameObject ClosePanel1;
    public GameObject ClosePanel2;

    public void PlayBTN()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void OpenAboutGame()
    {
        OpenAboutGamePanel.SetActive(true);
        if(OpenAboutUsPanel == true)
        {
            OpenAboutUsPanel.SetActive(false);
        }
    }
    public void OpenAboutUs()
    {
        OpenAboutUsPanel.SetActive(true);
        if (OpenAboutGamePanel == true)
        {
            OpenAboutGamePanel.SetActive(false);
        }
    }
    public void ClosePanelAboutus()
    {
        if(OpenAboutUsPanel == true)
        {
            OpenAboutUsPanel.SetActive(false);
        }
    }
    public void ClosePanelAboutGame()
    {
        if(OpenAboutGamePanel == true)
        {
            OpenAboutGamePanel.SetActive(false);
        }
    }
}
