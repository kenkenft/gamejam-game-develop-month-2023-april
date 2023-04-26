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
        UIManager.GetFinalScore += GetCurrentScore;
    }

    void OnDisable()
    {
        PlayerStorage.DepositCoin -= UpdateScore;
        UIManager.GetFinalScore -= GetCurrentScore;
    }
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "Money: " + currentScore;
    }

    public void UpdateScore(int points)
    {
        currentScore += points;
        scoreText.text = "Money: " + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Money: " + currentScore;
    }

    public int GetCurrentScore()
    {
        Debug.Log("currentScore: " + currentScore);
        return currentScore;
    }
}
