using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreenTest");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadBossFight()
    {
        SceneManager.LoadScene("BossFight");
    }
}
