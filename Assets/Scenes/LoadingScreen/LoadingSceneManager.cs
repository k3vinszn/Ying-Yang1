using UnityEngine;
using System.Collections; 
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public float loadingTime = 5f;
    public float moveDistance = 5f;
    private float timer = 0f;
    private bool loadingComplete = false;
    public GameObject objectToMove;
    public string nextSceneName = "YourNextSceneName";

    void Update()
    {
        timer += Time.deltaTime;
        float moveSpeed = moveDistance / loadingTime;

        if (timer <= loadingTime && !loadingComplete)
        {
            MoveObject(objectToMove, new Vector3(moveSpeed, 0f, 0f));
        }
        else if (timer >= loadingTime && !loadingComplete)
        {
            loadingComplete = true;
            DisableParallaxScripts();
            LoadNextScene();
        }
    }

    void DisableParallaxScripts()
    {
        ParallaxLoadingScreen[] parallaxScripts = GameObject.FindObjectsOfType<ParallaxLoadingScreen>();

        foreach (ParallaxLoadingScreen script in parallaxScripts)
        {
            script.enabled = false;
        }
    }

    void MoveObject(GameObject obj, Vector3 speed)
    {
        obj.transform.Translate(speed * Time.deltaTime);
    }

    void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}

