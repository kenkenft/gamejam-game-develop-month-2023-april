using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using TMPro;

public class PlayerOverlay : MonoBehaviour
{
    ScoreTextProperties scoreTextProperties;
    Timer timer;
    Canvas playerOverlayCanvas;

    void Start()
    {
        SetUp();
        // StartTimer(0);
    }
    
    public void SetUp()
    {
        playerOverlayCanvas = GetComponentInChildren<Canvas>();
        
        scoreTextProperties = GetComponentInChildren<ScoreTextProperties>();
        timer = GetComponentInChildren<Timer>();
    }

    public void GameStartSetUp()
    {
        StartTimer(60);
        ResetOverlay();
    }

    public void ResetOverlay()
    {
        scoreTextProperties.ResetScore();
    }


    public int GetFinalScore()
    {
        return scoreTextProperties.GetCurrentScore();
    }


    public void TogglePlayerOverlayCanvas(bool state)
    {
        playerOverlayCanvas.enabled = state;
    }

    public void StartTimer(int startTime)
    {
        StartCoroutine(timer.Countdown(startTime));
    }

    public void UpdateScoreText(int points)
    {
        scoreTextProperties.UpdateScore(points);
    }

}
