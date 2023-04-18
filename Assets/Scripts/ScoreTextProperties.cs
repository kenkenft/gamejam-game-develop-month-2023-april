using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTextProperties : MonoBehaviour
{
    int currentScore = 0;
    Text scoreText;
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "Score: " + currentScore;
    }

    public void UpdateScore(int truckScore)
    {
        currentScore += truckScore;
        scoreText.text = "Score: " + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
