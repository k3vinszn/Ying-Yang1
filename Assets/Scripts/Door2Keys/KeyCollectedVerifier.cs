using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectedVerifier : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject collectedObject1;
    public GameObject collectedObject2;

    void Update()
    {
        // Check if both objects have been collected (disabled)
        if (!collectedObject1.activeSelf && !collectedObject2.activeSelf)
        {
            // Enable the target object
            objectToEnable.SetActive(true);
        }
    }
}
