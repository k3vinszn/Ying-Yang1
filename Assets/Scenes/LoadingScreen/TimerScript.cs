using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public TMP_Text loadingText;

    private float timer = 0f;
    private float totalTime = 4.8f;

    void Update()
    {

        timer += Time.deltaTime;
        float percentage = Mathf.Clamp01(timer / totalTime);
        int progressPercentage = Mathf.RoundToInt(percentage * 100);
        loadingText.text = progressPercentage + "%";


    }
}