using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTextProperties : MonoBehaviour
{
    int currentScore = 0;
    Text scoreText;

    void OnEnable()
    {
        PlayerStorage.DepositCoin += UpdateScore;
    }

    void OnDisable()
    {
        PlayerStorage.DepositCoin -= UpdateScore;
    }
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "Score: " + currentScore;
    }

    public void UpdateScore(int points)
    {
        currentScore += points;
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
