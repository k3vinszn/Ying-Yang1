using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public string sceneToLoad; 
    public Animator animator; 
    public Animator animator2;

    private bool isKeyPressed = false;

    void Update()
    {
        // Check if any key is pressed and the animation is not playing
        if (Input.anyKeyDown && !isKeyPressed)
        {
            isKeyPressed = true;
            StartCoroutine(LoadSceneAfterDelay(3f));
            PlayAnimations();
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }

    void PlayAnimations()
    {
        // Play your animations here
        animator.SetTrigger("KeyPressed");
        animator2.SetTrigger("KeyPressed1");
    }
}
