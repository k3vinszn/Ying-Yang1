using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreenTest");
    }
    public void LoadLoadingScreenLevel2()
    {
        SceneManager.LoadScene("LoadingScreenTestLevel2");
    }

    public void LoadBossFight()
    {
        SceneManager.LoadScene("BossFight");
    }
}
