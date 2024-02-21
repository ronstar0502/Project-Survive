using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerTextMesh;
    private float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Format the time as you want it to be displayed
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        timerTextMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
