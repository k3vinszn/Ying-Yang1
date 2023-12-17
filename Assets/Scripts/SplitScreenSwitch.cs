using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenSwitch : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;

    private bool areObjectsEnabled = true;

    public void ToggleGameObjects()
    {
        if (areObjectsEnabled)
        {
            DisableObjects();
        }
        else
        {
            EnableObjects();
        }

        areObjectsEnabled = !areObjectsEnabled;
    }

    private void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    private void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
}
