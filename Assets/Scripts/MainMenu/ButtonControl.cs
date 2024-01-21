using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadPlayMenuScene()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    public void LoadPracticeMenu()
    {
        SceneManager.LoadScene("PracticeMenu");
    }
    public void LoadPracticeScene()
    {
        SceneManager.LoadScene("Practice");
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
       

        Application.Quit();

    }
}
