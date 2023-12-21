using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void LoadPlayMenuScene()
    {
        SceneManager.LoadScene("PlayMenu");
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
